using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using OfficeOpenXml;

namespace Shahed_Plate_APLR
{
    public partial class Form1 : Form
    {
        private TCPClient _tcpClient;
        private string token = "dBzsEzYuBy6wgiGlI4UUXJPLp1OoS0Cc2YgyCFOCh2U7pvH16ucL1334OjCmeWFJ";
        private Dictionary<string, System.Windows.Forms.TextBox> textBoxDictionary;

        private List<ScheduledRecording> scheduledRecordings = new List<ScheduledRecording>();

        // Timer to monitor scheduled recording times
        private System.Windows.Forms.Timer timer;
        private MySQLDatabase db;

        

        public Form1()
        {
            InitializeComponent();
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            /*comboBox_qury.Items.Add("e8eecbcf-9234-40e2-90ef-48c69de9174f");
            comboBox_qury.Items.Add("4cac2b8b-5fdd-4aed-a5eb-cd9270d11770");

            combo_gates.Items.Add("e8eecbcf-9234-40e2-90ef-48c69de9174f");
            combo_gates.Items.Add("4cac2b8b-5fdd-4aed-a5eb-cd9270d11770");

            combo_live_gates.Items.Add("e8eecbcf-9234-40e2-90ef-48c69de9174f");
            combo_live_gates.Items.Add("4cac2b8b-5fdd-4aed-a5eb-cd9270d11770");*/

            textBoxDictionary = new Dictionary<string, System.Windows.Forms.TextBox>
            {
                { "e8eecbcf-9234-40e2-90ef-48c69de9174f", text_vehicle_count_gate1 },
                { "4cac2b8b-5fdd-4aed-a5eb-cd9270d11770", text_vehicle_count_gate2 }
            };

            
            // Set the ListView to Details view
            listView_scheduled.View = View.Details;

            // Add columns for the table
            listView_scheduled.Columns.Add("Scheduled Time", 150);
            listView_scheduled.Columns.Add("Duration", 70);
            listView_scheduled.Columns.Add("Camera ID", 150);
            listView_scheduled.Columns.Add("filePath", 200);

            dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;

            dtpEnd.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            dtpEnd.Format = DateTimePickerFormat.Custom;

            dtpStart.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            dtpStart.Format = DateTimePickerFormat.Custom;

            db = new MySQLDatabase("localhost", "root", "1234", "parking");
            db.Connect();

            db.PopulateComboBox(comboBox_qury);
            db.PopulateComboBox(combo_gates);
            db.PopulateComboBox(combo_live_gates);

            SetupDataGridView();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Check every second
            timer.Tick += Timer_Tick;
            timer.Start();
        }
       
        private async void Timer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            // Example logic: Check if the current time matches any scheduled recording times
            foreach (var recording in scheduledRecordings)
            {
                if (recording.ScheduledTime <= currentTime && !recording.IsExecuted)
                {
                    // Execute the recording command
                    await SendRecordingCommand(recording.CameraId, recording.Duration, recording.SelectedFilePath);
                    recording.IsExecuted = true; // Mark the recording as executed
                }
            }
        }

        private void add_recording_scheduling(string selectedFilePath)
        {
            // Get the scheduled time and duration from user inputs
            DateTime scheduledTime = dateTimePicker1.Value; // Assume dateTimePicker is used to select the time
            int duration = int.Parse(text_duration.Text);
            string cameraId = combo_gates.Text.ToString();

            // Add to the list
            var scheduledRecording = new ScheduledRecording(
                scheduledTime,         // scheduledTime (get from a DateTimePicker or another DateTime source)
                int.Parse(text_duration.Text), // duration (parsed from the TextBox input)
                combo_gates.Text,              // cameraId (from a ComboBox or other input)
                selectedFilePath         // description (optional)
            );
            scheduledRecordings.Add(scheduledRecording);

            // Add the scheduled time and duration to the ListView for display
            ListViewItem item = new ListViewItem(scheduledTime.ToString("g")); // First column (Scheduled Time)
            item.SubItems.Add(duration.ToString());   
            item.SubItems.Add(cameraId);               
            item.SubItems.Add(selectedFilePath);           

            // Add the item to the ListView
            listView_scheduled.Items.Add(item);

            // Log the addition
            AppendTextToTextBox($"Scheduled recording for {cameraId} at {scheduledTime} for {duration} seconds.");
        }

        private void AppendTextToTextBox(string message)
        {
            if (txtLogBox.InvokeRequired)
            {
                txtLogBox.Invoke(new Action(() => txtLogBox.AppendText(message + Environment.NewLine)));
            }
            else
            {
                txtLogBox.AppendText(message + Environment.NewLine);
            }
        }


        private async void btn_connect_tcp_Click(object sender, EventArgs e)
        {
            _tcpClient = new TCPClient(live_View_picture,txtLogBox, textBoxDictionary, token);

            try
            {
                // Attempt to connect to the server
                AppendTextToTextBox("Connecting to the server...");
                await _tcpClient.ConnectAsync(text_tcpip.Text.ToString(), int.Parse(text_tcpport.Text)); // Replace with your server's IP and port


                // Notify the user that the connection was successful
                AppendTextToTextBox("Connected to the server!");
            }
            catch (Exception ex)
            {
                // Handle any connection exceptions and notify the user
                MessageBox.Show($"Failed to connect: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_txtbox_Click(object sender, EventArgs e)
        {
            if (txtLogBox.InvokeRequired)
            {
                txtLogBox.Invoke(new Action(() => txtLogBox.Clear()));
            }
            else
            {
                txtLogBox.Clear();
            }

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tcpClient?.Close();
        }

        private async void btn_disconnect_Click(object sender, EventArgs e)
        {
            if (_tcpClient != null)
            {
                // Stop the client from reconnecting
                _tcpClient.ShouldReconnect = false;

                // Disconnect from the server
                await _tcpClient.DisconnectAsync();

                // Clean up the client instance
                _tcpClient = null;

                AppendTextToTextBox("Client disconnected and resources cleared.");
            }
        }

        private void btn_recording_Click(object sender, EventArgs e)
        {
            save_video_file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos); // Default to the Videos folder
            save_video_file.Filter = "AVI files (*.avi)|*.avi"; // Allow only .avi files
            save_video_file.FilterIndex = 1;
            save_video_file.RestoreDirectory = true;

            if (save_video_file.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = save_video_file.FileName;
                add_recording_scheduling(selectedFilePath);
                /*if (_tcpClient != null) // Assuming you have an IsConnected property
                {
                    // Create the JSON structure as a dictionary or anonymous object
                    var auth_message = new Dictionary<string, object>
                                {
                                    {"messageId", Guid.NewGuid().ToString()},
                                    {"messageType", "command"},
                                    {
                                        "messageBody", new Dictionary<string, object>
                                        {
                                            {"commandType",  "recording"},
                                            {"cameraId", combo_gates.Text.ToString()},
                                            {"fileName", selectedFilePath},
                                            {"duration", int.Parse(text_duration.Text)}
                                        }
                                    }
                                };

                    // Serialize the object to JSON
                    string jsonString = JsonConvert.SerializeObject(auth_message, Formatting.None);

                    try
                    {
                        // Send the serialized JSON string to the server
                        AppendTextToTextBox("Sending recording command to the server...");
                        await _tcpClient.SendMessageAsync(jsonString);

                        // Notify the user that the message was sent successfully
                        AppendTextToTextBox("Recording command sent to the server!");
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors during message sending
                        AppendTextToTextBox($"Error sending message: {ex.Message}");
                    }
                }
                else
                {
                    AppendTextToTextBox("Not connected to the server. Please connect first.");
                }*/
            }
        }


        private async Task SendRecordingCommand(string cameraId, int duration, string path)
        {
            // Create the JSON structure
            var auth_message = new Dictionary<string, object>
            {
                {"messageId", Guid.NewGuid().ToString()},
                {"messageType", "command"},
                {
                    "messageBody", new Dictionary<string, object>
                    {
                        {"commandType", "recording"},
                        {"cameraId", cameraId},
                        {"fileName", path}, // Generate a unique filename
                        {"duration", duration}
                    }
                }
            };

            // Serialize the object to JSON
            string jsonString = JsonConvert.SerializeObject(auth_message, Formatting.None);

            try
            {
                // Send the serialized JSON string to the server
                AppendTextToTextBox($"Sending recording command for {cameraId}...");
                await _tcpClient.SendMessageAsync(jsonString);
                AppendTextToTextBox("Recording command sent!");
            }
            catch (Exception ex)
            {
                AppendTextToTextBox($"Error sending command: {ex.Message}");
            }
        }

        private async void btn_show_live_Click(object sender, EventArgs e)
        {
            string cameraId = combo_live_gates.Text.ToString();
            var auth_message = new Dictionary<string, object>
            {
                {"messageId", Guid.NewGuid().ToString()},
                {"messageType", "command"},
                {
                    "messageBody", new Dictionary<string, object>
                    {
                        {"commandType", "streaming"},
                        {"cameraId", cameraId},
                        {"duration", int.Parse(text_live_duration.Text.ToString())}
                    }
                }
            };

            // Serialize the object to JSON
            string jsonString = JsonConvert.SerializeObject(auth_message, Formatting.None);

            try
            {
                // Send the serialized JSON string to the server
                AppendTextToTextBox($"Sending streaming command for {cameraId}...");
                await _tcpClient.SendMessageAsync(jsonString);
                AppendTextToTextBox("Streaming command sent!");
            }
            catch (Exception ex)
            {
                AppendTextToTextBox($"Error sending command: {ex.Message}");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string plate = txtPlate.Text;
            DateTime? startDate = dtpStart.Value;
            DateTime? endDate = dtpEnd.Value;
            string gate = comboBox_qury.SelectedText;

            DataTable result = db.QueryData(plate, startDate, endDate, gate);
            AddImagesToDataTable(result);  // Add images to the DataTable

            dgvResults.DataSource = result;
        }

        private void AddImagesToDataTable(DataTable dt)
        {
            // Add a new column for images if it doesn't already exist
            if (!dt.Columns.Contains("Image"))
            {
                dt.Columns.Add("Image", typeof(Bitmap));  // Add a new column for images
            }

            foreach (DataRow row in dt.Rows)
            {
                string imagePath = @text_image_floder.Text.ToString()+"//"+ row["plate_image"].ToString();
                if (File.Exists(imagePath))
                {
                    try
                    {
                        // Load image from file path
                        Bitmap image = new Bitmap(imagePath);
                        row["Image"] = image;

                        // Set DataGridView row height based on image size
                        int imageHeight = image.Height;
                        dgvResults.RowTemplate.Height = Math.Max(imageHeight, dgvResults.RowTemplate.Height);  // Set the row height to the image height if it's larger

                        // Now the "Image" column exists, so you can set its width
                        dgvResults.Columns["Image"].Width = Math.Max(image.Width, dgvResults.Columns["Image"].Width);  // Set the column width to the image width if it's larger

                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions when loading the image
                        Console.WriteLine($"Error loading image: {ex.Message}");
                        row["Image"] = null;  // Set to null if image can't be loaded
                    }
                }
                else
                {
                    row["Image"] = null;  // If image path doesn't exist, set it as null
                }
            }
        }

        private void SetupDataGridView()
        {
            // Setup DataGridView settings
            dgvResults.AutoGenerateColumns = false;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Add columns to DataGridView
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date", DataPropertyName = "pass_date" });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Plate", DataPropertyName = "plate" });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Gate", DataPropertyName = "gate_id" });


            // Add image column for plate_image
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Image";
            imageColumn.DataPropertyName = "Image";  // This matches the "Image" column in the DataTable
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;  // Ensure the image fits within the cell
            imageColumn.Name = "Image";  // Set the Name property so you can reference it later
            dgvResults.Columns.Add(imageColumn);
        }

        public void SaveToExcel(DataTable dt, string fileName)
        {
            // Set the license context for EPPlus
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Report");

                // Add headers
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ws.Cells[1, i + 1].Value = dt.Columns[i].ColumnName;
                }

                // Add DataTable rows to worksheet
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (dt.Columns[col].DataType == typeof(Bitmap))
                        {
                            // Add image to Excel if the column is for images
                            Bitmap bmp = dt.Rows[row][col] as Bitmap;
                            if (bmp != null)
                            {
                                // Convert Bitmap to MemoryStream
                                using (var stream = new MemoryStream())
                                {
                                    bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);  // Save bitmap as PNG to stream
                                    stream.Position = 0;  // Reset stream position to the beginning

                                    // Add image to Excel worksheet from the MemoryStream
                                    var img = ws.Drawings.AddPicture($"Image{row}_{col}", stream);
                                    img.SetPosition(row + 1, 0, col, 0);  // Set image position in the worksheet

                                    // Adjust the column width and row height based on the image size
                                    double imageHeightInPoints = bmp.Height * 0.75; // Convert pixels to Excel points
                                    double imageWidthInPoints = bmp.Width * 0.14; // Convert pixels to Excel column width units

                                    ws.Row(row + 2).Height = imageHeightInPoints; // Set row height based on image height
                                    ws.Column(col + 1).Width = imageWidthInPoints; // Set column width based on image width
                                }
                            }
                        }
                        else
                        {
                            // Add text data
                            ws.Cells[row + 2, col + 1].Value = dt.Rows[row][col].ToString();
                        }
                    }
                }

                // Save Excel package
                FileInfo fi = new FileInfo(fileName);
                package.SaveAs(fi);
                MessageBox.Show("Data exported to Excel");
            }
        }




        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files|*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveToExcel((DataTable)dgvResults.DataSource, sfd.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                text_image_floder.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }



}
