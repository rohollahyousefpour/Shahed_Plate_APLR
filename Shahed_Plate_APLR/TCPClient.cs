using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Data;
using OfficeOpenXml;



namespace Shahed_Plate_APLR
{
    public class ScheduledRecording
    {
        // Time when the recording is scheduled to start
        public DateTime ScheduledTime { get; set; }

        // Duration of the recording in seconds
        public int Duration { get; set; }

        // The ID of the camera for which the recording is scheduled
        public string CameraId { get; set; }

        // A flag to indicate whether the recording command has been executed
        public bool IsExecuted { get; set; } = false;

        // Optional: A description or label for the recording
        public string SelectedFilePath { get; set; }

        // Constructor to easily initialize a new ScheduledRecording
        public ScheduledRecording(DateTime scheduledTime, int duration, string cameraId, string selectedFilePath)
        {
            ScheduledTime = scheduledTime;
            Duration = duration;
            CameraId = cameraId;
            SelectedFilePath = selectedFilePath;
        }

        // Optional: Override ToString to make it easier to display in logs or UI
        public override string ToString()
        {
            return $"Recording scheduled at {ScheduledTime}, Duration: {Duration} seconds, Camera: {CameraId}";
        }
    }

    public class TCPClient
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private readonly int maxRetries = 5; // Maximum retry attempts
        private readonly int baseDelay = 2000; // Base delay (in milliseconds) between reconnection attempts
        private RichTextBox _logTextBox;
        private string _ip { get; set; }
        private int port { get; set; }
        private bool isListening { get; set; }
        // To prevent multiple listeners
        private string authToken; // Store authentication token
        private bool _shouldReconnect = true;
        private PictureBox _pictureBox;
        public bool ShouldReconnect
        {
            get { return _shouldReconnect; }
            set { _shouldReconnect = value; }
        }// By default, the client should reconnect on disconnection

        private Dictionary<string, System.Windows.Forms.TextBox> textBoxs;


        public TCPClient(PictureBox pictureBox, RichTextBox logTextBox, Dictionary<string, System.Windows.Forms.TextBox> text_boxes_, string token)
        {
            _logTextBox = logTextBox;
            authToken = token; // Set the token during initialization
            textBoxs = text_boxes_;
            _pictureBox = pictureBox;
        }

        // Append text to the RichTextBox
        private void AppendTextToTextBox(string message)
        {
            if (_logTextBox.InvokeRequired)
            {
                _logTextBox.Invoke(new Action(() =>
                {
                    _logTextBox.AppendText(message + Environment.NewLine);
                    // Scroll to the last line
                    _logTextBox.SelectionStart = _logTextBox.Text.Length;
                    _logTextBox.ScrollToCaret();
                }));
            }
            else
            {
                _logTextBox.AppendText(message + Environment.NewLine);
                // Scroll to the last line
                _logTextBox.SelectionStart = _logTextBox.Text.Length;
                _logTextBox.ScrollToCaret();
            }
        }

        // Method to connect to the server and authenticate
        public async Task ConnectAsync(string server, int port_)
        {
            int retries = 0;
            _ip = server;
            port = port_;
            while (true)
            {
                try
                {
                    _client = new TcpClient();
                    await _client.ConnectAsync(_ip, port);
                    _stream = _client.GetStream();
                    AppendTextToTextBox("Connected to the server.");

                    // Perform authentication immediately after connection
                    await AuthenticateAsync(authToken);

                    if (!isListening)
                    {
                        // Start listening for messages after authenticating
                        _ = Task.Run(async () => await ListenForMessagesAsync());
                    }

                    return; // Exit once connected and authenticated
                }
                catch (Exception ex)
                {
                    retries++;
                    int delay = baseDelay; // Fixed delay for retries
                    AppendTextToTextBox($"Connection failed: {ex.Message}. Retrying in {delay / 1000} seconds...");
                    await Task.Delay(delay); // Wait before retrying
                }
            }
        }

        // Authentication logic
        public async Task AuthenticateAsync(string token)
        {
            var authMessage = new
            {
                messageId = Guid.NewGuid().ToString(),
                messageType = "authentication",
                messageBody = new { token = token }
            };

            string jsonMessage = JsonConvert.SerializeObject(authMessage);
            await SendMessageAsync(jsonMessage);
            string response = await ReceiveMessageAsync();
            AppendTextToTextBox("Authentication response: " + response);
        }

        // Send message to server
        public async Task SendMessageAsync(string message)
        {
            if (_stream == null) throw new InvalidOperationException("No connection established.");

            byte[] data = Encoding.UTF8.GetBytes(message + "\n"); // Add newline to match server's read_until
            await _stream.WriteAsync(data, 0, data.Length);

            // Ensure the stream is flushed
            await _stream.FlushAsync();
        }

        // Receive message from server
        // Receive message from server
        private StringBuilder messageBuffer = new StringBuilder(); // Buffer to store incoming data

        // Continuously receive messages until a newline character is found
        public async Task<string> ReceiveMessageAsync()
        {
            if (_stream == null) throw new InvalidOperationException("No connection established.");

            byte[] buffer = new byte[1024]; // Buffer for receiving data
            int bytesRead;

            try
            {
                while (true)
                {
                    // Read from the stream
                    bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        throw new Exception("Disconnected from server.");
                    }

                    // Convert bytes to string and append to the buffer
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    messageBuffer.Append(receivedData);

                    // Check if a full message (ending with '\n') has been received
                    string currentBuffer = messageBuffer.ToString();
                    int newlineIndex = currentBuffer.IndexOf('\n');

                    // If a newline is found, extract the complete message
                    if (newlineIndex != -1)
                    {
                        // Extract the full message (everything up to the newline)
                        string fullMessage = currentBuffer.Substring(0, newlineIndex).TrimEnd('\n');

                        // Remove the processed message from the buffer
                        messageBuffer.Remove(0, newlineIndex + 1);

                        // Return the full message that was received
                        return fullMessage;
                    }

                    // If no newline was found, loop continues, appending new data
                    // and waiting for a complete message
                }
            }
            catch (Exception ex)
            {
                AppendTextToTextBox($"Error receiving message: {ex.Message}");
                throw; // Rethrow to handle disconnection
            }
        }



        // Continuously listen for new messages
        private async Task ListenForMessagesAsync()
        {
            isListening = true; // Mark as listening to prevent multiple listeners
            while (ShouldReconnect)
            {
                try
                {
                    string message = await ReceiveMessageAsync();
                    //AppendTextToTextBox("Received message: " + message);
                    JObject parsedMessage = JObject.Parse(message);

                    // Extract the messageType
                    string messageType = parsedMessage["messageType"]?.ToString();
                    if (messageType == "information")
                    {
                        AppendTextToTextBox("Received message: " + message);
                        // Extract the messageBody
                        JObject messageBody = (JObject)parsedMessage["messageBody"];

                        // Extract specific fields from messageBody
                        string gate = messageBody["gate"]?.ToString();
                        string informationType = messageBody["informationType"]?.ToString();
                        string details = messageBody["details"]?.ToString();

                        if (informationType == "1")
                        {
                            //UpdateTextBoxSafe(textBoxs[gate], details);
                        }
                    }
                    else if (messageType == "plates_data")
                    {
                        JObject messageBody = (JObject)parsedMessage["messageBody"];
                        string gate = messageBody["gate"]?.ToString();
                    }
                    else if (messageType == "live")
                    {
                        JObject messageBody = (JObject)parsedMessage["messageBody"];
                        string gate = messageBody["gate"]?.ToString();
                        Image live = Base64ToImage(messageBody["live_image"]?.ToString());
                        UpdatePictureBoxSafe(live);

                        AppendTextToTextBox("Recieve Live view");
                    }
                    else
                    {
                        Console.WriteLine("Unknown messageType: " + messageType);
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Error parsing JSON: " + ex.Message);
                }
                catch (SocketException ex) // Handle TCP-related issues (like disconnection)
                {
                    AppendTextToTextBox($"Connection lost: {ex.Message}. Attempting to reconnect...");
                    await ReconnectAsync(_ip, port);

                    // After reconnection, reinitialize the stream and restart the listener
                    if (_client.Connected)
                    {
                        _stream = _client.GetStream(); // Reinitialize the stream after reconnection
                        AppendTextToTextBox("Reconnected. Listening for new messages...");

                        // Re-authenticate after reconnecting
                        await AuthenticateAsync(authToken);
                    }
                }
                catch (Exception ex)
                {
                    AppendTextToTextBox($"Connection lost: {ex.Message}. Attempting to reconnect...");
                    await ReconnectAsync(_ip, port);

                    // After reconnection, reinitialize the stream and restart the listener
                    if (_client.Connected)
                    {
                        _stream = _client.GetStream(); // Reinitialize the stream after reconnection
                        AppendTextToTextBox("Reconnected. Listening for new messages...");

                        // Re-authenticate after reconnecting
                        await AuthenticateAsync(authToken);
                    }

                }
            }
        }

        // Reconnect to the server if disconnected
        public async Task ReconnectAsync(string server, int port_)
        {
            isListening = false; // Stop the current listener if reconnecting
            while (ShouldReconnect && !_client.Connected) // Only try reconnecting if ShouldReconnect is true
            {
                AppendTextToTextBox("Attempting to reconnect...");
                try
                {
                    if (!ShouldReconnect) // Re-check before attempting to reconnect
                    {
                        AppendTextToTextBox("Reconnection stopped by user.");
                        return;
                    }

                    await ConnectAsync(server, port_); // This will restart the listener and authenticate
                    return; // Exit once reconnected
                }
                catch (Exception ex)
                {
                    AppendTextToTextBox($"Reconnection failed: {ex.Message}. Retrying...");
                    await Task.Delay(2000); // Wait before retrying
                }
            }
        }

        private void parse_plate_data(JObject messageBody)
        {
            string timestamp = messageBody["timestamp"]?.ToString();
            string gate = messageBody["gate"]?.ToString();
            string fullImagePath = messageBody["fullimagepath"]?.ToString();

            // Handle the cars array
            JArray carsArray = (JArray)messageBody["cars"];
            foreach (JObject car in carsArray)
            {
                // Extract the "box" object
                JObject box = (JObject)car["box"];
                int left = box["left"].ToObject<int>();
                int top = box["top"].ToObject<int>();
                int right = box["right"].ToObject<int>();
                int bottom = box["bottom"].ToObject<int>();


                // Extract the "plate" object
                JObject plate = (JObject)car["plate"];
                string plateNumber = plate["plate"]?.ToString();
                string plateType = plate["plate_type"]?.ToString();
                string plateImage = plate["plate_image"]?.ToString();  // Base64 encoded image


                // Extract the "vehicle_class" and "vehicle_type" objects
                JObject vehicleClass = (JObject)car["vehicle_class"];
                string vehicleClassConf = vehicleClass["conf"]?.ToString();

                JObject vehicleType = (JObject)car["vehicle_type"];
                int vehicleTypeClass = vehicleType["class"].ToObject<int>();
                int vehicleTypeConf = vehicleType["conf"].ToObject<int>();


                // Extract other information
                string ocrAccuracy = car["ocr_accuracy"]?.ToString();
                double visionSpeed = car["vision_speed"].ToObject<double>();
                double radarSpeed = car["radar_speed"].ToObject<double>();


                // You can also extract and process "vehicle_color" and "meta_data" if needed
                // e.g., JObject vehicleColor = (JObject)car["vehicle_color"];
            }
        }

        // Close the connection
        public void Close()
        {
            _stream?.Close();
            _client?.Close();
            isListening = false;
        }

        public async Task DisconnectAsync()
        {
            try
            {
                // Log the disconnection process
                AppendTextToTextBox("Disconnecting from the server...");

                // Stop listening for new messages and disable reconnection
                ShouldReconnect = false;
                isListening = false;

                // Close the stream and client safely
                _stream?.Close();
                _client?.Close();

                AppendTextToTextBox("Disconnected from the server.");
            }
            catch (Exception ex)
            {
                AppendTextToTextBox($"Error while disconnecting: {ex.Message}");
            }
        }

        private void UpdateTextBoxSafe(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action(() =>
                {
                    textBox.Text = text;
                }));
            }
            else
            {
                textBox.Text = text;
            }
        }

        private Image Base64ToImage(string base64String)
        {
            // Convert Base64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);

            // Convert byte[] to Image
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }
        private void UpdatePictureBoxSafe(Image image)
        {
            // Check if we're on a background thread
            if (_pictureBox.InvokeRequired)
            {
                // Marshal to the UI thread to update the PictureBox safely
                _pictureBox.Invoke(new Action(() =>
                {
                    _pictureBox.Image = image;
                    _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Adjust as needed
                }));
            }
            else
            {
                // Already on the UI thread, so update the PictureBox directly
                _pictureBox.Image = image;
                _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

    }


    public class MySQLDatabase
    {
        private string connectionString;
        private MySqlConnection connection;
        private Dictionary<char, string> dicts = new Dictionary<char, string>
            {
                { 'a', "ا" }, { 'b', "ب" }, { 'p', "پ" }, { 't', "ت" }, { 'o', "ث" }, { 'j', "ج" },
                { 'd', "د" }, { 'i', "ع" }, { 'f', "ف" }, { 'q', "ق" }, { 'l', "ل" }, { 'm', "م" },
                { 'n', "ن" }, { 'v', "و" }, { 'h', "ه" }, { 'y', "ی" }, { 'r', "ر" }, { 's', "س" },
                { 'u', "ش" }, { 'c', "ص" }, { 'w', "ط" }, { 'z', "ز" }, { 'k', "ک" }, { 'g', "گ" },
                { 'e', "ژ" }, { 'D', "D" }, { 'S', "S" }
            };

        public MySQLDatabase(string host, string user, string password, string database)
        {
            connectionString = $"Server={host};Database={database};User ID={user};Password={password};";
            connection = new MySqlConnection(connectionString);
        }

        public void Connect()
        {
            try
            {
                connection.Open();
                MessageBox.Show("Connected to MySQL database");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        public DataTable QueryData(string plate, DateTime? startDate, DateTime? endDate, string gate)
        {

            DataTable dt = new DataTable();
            if (connection.State == ConnectionState.Open)
            {
                string query = "SELECT pass_date, plate, plate_image, gate_id FROM pass WHERE 1=1"; // Base query

                if (!string.IsNullOrEmpty(plate))
                    query += " AND plate LIKE @plate";

                if (startDate.HasValue)
                    query += " AND pass_date >= @startDate";

                if (endDate.HasValue)
                    query += " AND pass_date <= @endDate";

                if (!string.IsNullOrEmpty(gate))
                    query += " AND gate_id = @gate";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(plate))
                        cmd.Parameters.AddWithValue("@plate", "%" + plate + "%");  // Partial matching

                    if (startDate.HasValue)
                        cmd.Parameters.AddWithValue("@startDate", startDate);

                    if (endDate.HasValue)
                        cmd.Parameters.AddWithValue("@endDate", endDate);

                    if (!string.IsNullOrEmpty(gate))
                        cmd.Parameters.AddWithValue("@gate", gate);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    string rowPlate = row["plate"].ToString();  // Get the plate value from the row

                    // Step 2: Check if the plate has at least 3 characters and modify the third one if necessary
                    if (!string.IsNullOrEmpty(rowPlate) && rowPlate.Length >= 3)
                    {
                        char thirdChar = rowPlate[2];  // Get the third character (index 2)

                        // Step 3: Check if the third character is in the dictionary and replace it if found
                        if (dicts.ContainsKey(thirdChar))
                        {
                            string persianChar = dicts[thirdChar];  // Get the Persian equivalent
                            row["plate"] = rowPlate.Substring(3)+ persianChar + rowPlate.Substring(0, 2);  // Update the plate value in the DataRow
                        }
                    }
                }
            }
            return dt;
        }

        public void Disconnect()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                MessageBox.Show("Disconnected from MySQL database");
            }
        }

        public DataTable GetGates()
        {
            DataTable dtGates = new DataTable();
            if (connection.State == ConnectionState.Open)
            {
                string query = "SELECT id FROM gate"; // Adjust the query as per your table structure

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtGates); // Fill the DataTable with the result set
                    }
                }
            }
            return dtGates;
        }

        public void PopulateComboBox(ComboBox comboBox)
        {
            DataTable dtGates = GetGates();

            if (dtGates.Rows.Count > 0)
            {
                comboBox.ValueMember = "id";       // Use the gate IDs as values
                comboBox.DataSource = dtGates;     // Bind the data to the ComboBox
            }
        }

    }

    public class ExcelHelper
    {
        public void SaveToExcel(DataTable dt, string fileName)
        {
            // Ensure that EPPlus license is set to non-commercial usage
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create a new Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a worksheet to the package
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Add DataTable headers to the worksheet
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dt.Columns[i].ColumnName;
                }

                // Add DataTable rows to the worksheet
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = dt.Rows[row][col];
                    }
                }

                // Save the package to a file
                FileInfo fi = new FileInfo(fileName);
                package.SaveAs(fi);

                Console.WriteLine("Excel file created successfully!");
            }
        }
    }

}
