namespace Shahed_Plate_APLR
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_tcpip = new System.Windows.Forms.TextBox();
            this.text_tcpport = new System.Windows.Forms.TextBox();
            this.btn_connect_tcp = new System.Windows.Forms.Button();
            this.btn_recording = new System.Windows.Forms.Button();
            this.text_duration = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLogBox = new System.Windows.Forms.RichTextBox();
            this.btn_txtbox = new System.Windows.Forms.Button();
            this.btn_disconnect = new System.Windows.Forms.Button();
            this.save_video_file = new System.Windows.Forms.SaveFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.text_vehicle_count_gate1 = new System.Windows.Forms.TextBox();
            this.text_vehicle_count_gate2 = new System.Windows.Forms.TextBox();
            this.combo_gates = new System.Windows.Forms.ComboBox();
            this.Conection = new System.Windows.Forms.TabControl();
            this.Connection = new System.Windows.Forms.TabPage();
            this.Recording = new System.Windows.Forms.TabPage();
            this.listView_scheduled = new System.Windows.Forms.ListView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.combo_live_gates = new System.Windows.Forms.ComboBox();
            this.text_live_duration = new System.Windows.Forms.TextBox();
            this.live_View_picture = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_show_live = new System.Windows.Forms.Button();
            this.tapquery1 = new System.Windows.Forms.TabPage();
            this.btn_image_folder = new System.Windows.Forms.Button();
            this.text_image_floder = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Plate = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.txtPlate = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.comboBox_qury = new System.Windows.Forms.ComboBox();
            this.Conection.SuspendLayout();
            this.Connection.SuspendLayout();
            this.Recording.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.live_View_picture)).BeginInit();
            this.tapquery1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "TCP IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "TCP IP";
            // 
            // text_tcpip
            // 
            this.text_tcpip.Location = new System.Drawing.Point(177, 54);
            this.text_tcpip.Name = "text_tcpip";
            this.text_tcpip.Size = new System.Drawing.Size(100, 30);
            this.text_tcpip.TabIndex = 2;
            this.text_tcpip.Text = "127.0.0.1";
            // 
            // text_tcpport
            // 
            this.text_tcpport.Location = new System.Drawing.Point(177, 137);
            this.text_tcpport.Name = "text_tcpport";
            this.text_tcpport.Size = new System.Drawing.Size(100, 30);
            this.text_tcpport.TabIndex = 3;
            this.text_tcpport.Text = "59112";
            // 
            // btn_connect_tcp
            // 
            this.btn_connect_tcp.Location = new System.Drawing.Point(408, 134);
            this.btn_connect_tcp.Name = "btn_connect_tcp";
            this.btn_connect_tcp.Size = new System.Drawing.Size(183, 33);
            this.btn_connect_tcp.TabIndex = 4;
            this.btn_connect_tcp.Text = "Connect TCP";
            this.btn_connect_tcp.UseVisualStyleBackColor = true;
            this.btn_connect_tcp.Click += new System.EventHandler(this.btn_connect_tcp_Click);
            // 
            // btn_recording
            // 
            this.btn_recording.Location = new System.Drawing.Point(881, 113);
            this.btn_recording.Name = "btn_recording";
            this.btn_recording.Size = new System.Drawing.Size(175, 33);
            this.btn_recording.TabIndex = 5;
            this.btn_recording.Text = "Recording";
            this.btn_recording.UseVisualStyleBackColor = true;
            this.btn_recording.Click += new System.EventHandler(this.btn_recording_Click);
            // 
            // text_duration
            // 
            this.text_duration.Location = new System.Drawing.Point(171, 72);
            this.text_duration.Name = "text_duration";
            this.text_duration.Size = new System.Drawing.Size(100, 30);
            this.text_duration.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Gate Label";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Duration";
            // 
            // txtLogBox
            // 
            this.txtLogBox.Location = new System.Drawing.Point(639, 25);
            this.txtLogBox.Name = "txtLogBox";
            this.txtLogBox.Size = new System.Drawing.Size(502, 542);
            this.txtLogBox.TabIndex = 10;
            this.txtLogBox.Text = "";
            // 
            // btn_txtbox
            // 
            this.btn_txtbox.Location = new System.Drawing.Point(928, 586);
            this.btn_txtbox.Name = "btn_txtbox";
            this.btn_txtbox.Size = new System.Drawing.Size(175, 34);
            this.btn_txtbox.TabIndex = 11;
            this.btn_txtbox.Text = "Clear Text Box";
            this.btn_txtbox.UseVisualStyleBackColor = true;
            this.btn_txtbox.Click += new System.EventHandler(this.btn_txtbox_Click);
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Location = new System.Drawing.Point(656, 586);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(164, 33);
            this.btn_disconnect.TabIndex = 12;
            this.btn_disconnect.Text = "Disconnect";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.btn_disconnect_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 309);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "Vehicle Count";
            // 
            // text_vehicle_count_gate1
            // 
            this.text_vehicle_count_gate1.Location = new System.Drawing.Point(214, 306);
            this.text_vehicle_count_gate1.Name = "text_vehicle_count_gate1";
            this.text_vehicle_count_gate1.Size = new System.Drawing.Size(100, 30);
            this.text_vehicle_count_gate1.TabIndex = 14;
            // 
            // text_vehicle_count_gate2
            // 
            this.text_vehicle_count_gate2.Location = new System.Drawing.Point(388, 304);
            this.text_vehicle_count_gate2.Name = "text_vehicle_count_gate2";
            this.text_vehicle_count_gate2.Size = new System.Drawing.Size(100, 30);
            this.text_vehicle_count_gate2.TabIndex = 15;
            // 
            // combo_gates
            // 
            this.combo_gates.FormattingEnabled = true;
            this.combo_gates.Location = new System.Drawing.Point(171, 17);
            this.combo_gates.Name = "combo_gates";
            this.combo_gates.Size = new System.Drawing.Size(413, 33);
            this.combo_gates.TabIndex = 16;
            // 
            // Conection
            // 
            this.Conection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Conection.Controls.Add(this.Connection);
            this.Conection.Controls.Add(this.Recording);
            this.Conection.Controls.Add(this.tabPage1);
            this.Conection.Controls.Add(this.tapquery1);
            this.Conection.Location = new System.Drawing.Point(12, 12);
            this.Conection.Name = "Conection";
            this.Conection.SelectedIndex = 0;
            this.Conection.Size = new System.Drawing.Size(1186, 679);
            this.Conection.TabIndex = 18;
            // 
            // Connection
            // 
            this.Connection.Controls.Add(this.label1);
            this.Connection.Controls.Add(this.text_tcpip);
            this.Connection.Controls.Add(this.btn_txtbox);
            this.Connection.Controls.Add(this.btn_disconnect);
            this.Connection.Controls.Add(this.label5);
            this.Connection.Controls.Add(this.text_vehicle_count_gate1);
            this.Connection.Controls.Add(this.txtLogBox);
            this.Connection.Controls.Add(this.text_vehicle_count_gate2);
            this.Connection.Controls.Add(this.btn_connect_tcp);
            this.Connection.Controls.Add(this.label2);
            this.Connection.Controls.Add(this.text_tcpport);
            this.Connection.Location = new System.Drawing.Point(4, 34);
            this.Connection.Name = "Connection";
            this.Connection.Padding = new System.Windows.Forms.Padding(3);
            this.Connection.Size = new System.Drawing.Size(1178, 641);
            this.Connection.TabIndex = 0;
            this.Connection.Text = "Connecting";
            this.Connection.UseVisualStyleBackColor = true;
            // 
            // Recording
            // 
            this.Recording.Controls.Add(this.listView_scheduled);
            this.Recording.Controls.Add(this.dateTimePicker1);
            this.Recording.Controls.Add(this.label3);
            this.Recording.Controls.Add(this.btn_recording);
            this.Recording.Controls.Add(this.label4);
            this.Recording.Controls.Add(this.combo_gates);
            this.Recording.Controls.Add(this.text_duration);
            this.Recording.Location = new System.Drawing.Point(4, 34);
            this.Recording.Name = "Recording";
            this.Recording.Padding = new System.Windows.Forms.Padding(3);
            this.Recording.Size = new System.Drawing.Size(1178, 641);
            this.Recording.TabIndex = 1;
            this.Recording.Text = "Recording";
            this.Recording.UseVisualStyleBackColor = true;
            // 
            // listView_scheduled
            // 
            this.listView_scheduled.HideSelection = false;
            this.listView_scheduled.Location = new System.Drawing.Point(28, 206);
            this.listView_scheduled.Name = "listView_scheduled";
            this.listView_scheduled.Size = new System.Drawing.Size(606, 412);
            this.listView_scheduled.TabIndex = 18;
            this.listView_scheduled.UseCompatibleStateImageBehavior = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(171, 133);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(397, 30);
            this.dateTimePicker1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.combo_live_gates);
            this.tabPage1.Controls.Add(this.text_live_duration);
            this.tabPage1.Controls.Add(this.live_View_picture);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.btn_show_live);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1178, 641);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Live View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // combo_live_gates
            // 
            this.combo_live_gates.FormattingEnabled = true;
            this.combo_live_gates.Location = new System.Drawing.Point(118, 69);
            this.combo_live_gates.Name = "combo_live_gates";
            this.combo_live_gates.Size = new System.Drawing.Size(313, 33);
            this.combo_live_gates.TabIndex = 21;
            // 
            // text_live_duration
            // 
            this.text_live_duration.Location = new System.Drawing.Point(118, 128);
            this.text_live_duration.Name = "text_live_duration";
            this.text_live_duration.Size = new System.Drawing.Size(100, 30);
            this.text_live_duration.TabIndex = 20;
            // 
            // live_View_picture
            // 
            this.live_View_picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.live_View_picture.Location = new System.Drawing.Point(555, 67);
            this.live_View_picture.Name = "live_View_picture";
            this.live_View_picture.Size = new System.Drawing.Size(586, 435);
            this.live_View_picture.TabIndex = 19;
            this.live_View_picture.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 25);
            this.label7.TabIndex = 18;
            this.label7.Text = "Duration";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 25);
            this.label6.TabIndex = 17;
            this.label6.Text = "Gates";
            // 
            // btn_show_live
            // 
            this.btn_show_live.Location = new System.Drawing.Point(118, 208);
            this.btn_show_live.Name = "btn_show_live";
            this.btn_show_live.Size = new System.Drawing.Size(136, 42);
            this.btn_show_live.TabIndex = 0;
            this.btn_show_live.Text = "Show Live";
            this.btn_show_live.UseVisualStyleBackColor = true;
            this.btn_show_live.Click += new System.EventHandler(this.btn_show_live_Click);
            // 
            // tapquery1
            // 
            this.tapquery1.Controls.Add(this.comboBox_qury);
            this.tapquery1.Controls.Add(this.btn_image_folder);
            this.tapquery1.Controls.Add(this.text_image_floder);
            this.tapquery1.Controls.Add(this.label11);
            this.tapquery1.Controls.Add(this.label10);
            this.tapquery1.Controls.Add(this.label9);
            this.tapquery1.Controls.Add(this.Plate);
            this.tapquery1.Controls.Add(this.dgvResults);
            this.tapquery1.Controls.Add(this.btnExport);
            this.tapquery1.Controls.Add(this.btnQuery);
            this.tapquery1.Controls.Add(this.dtpEnd);
            this.tapquery1.Controls.Add(this.dtpStart);
            this.tapquery1.Controls.Add(this.txtPlate);
            this.tapquery1.Location = new System.Drawing.Point(4, 34);
            this.tapquery1.Name = "tapquery1";
            this.tapquery1.Padding = new System.Windows.Forms.Padding(3);
            this.tapquery1.Size = new System.Drawing.Size(1178, 641);
            this.tapquery1.TabIndex = 3;
            this.tapquery1.Text = "Query";
            this.tapquery1.UseVisualStyleBackColor = true;
            // 
            // btn_image_folder
            // 
            this.btn_image_folder.Location = new System.Drawing.Point(490, 145);
            this.btn_image_folder.Name = "btn_image_folder";
            this.btn_image_folder.Size = new System.Drawing.Size(188, 32);
            this.btn_image_folder.TabIndex = 13;
            this.btn_image_folder.Text = "Select image folder";
            this.btn_image_folder.UseVisualStyleBackColor = true;
            this.btn_image_folder.Click += new System.EventHandler(this.button1_Click);
            // 
            // text_image_floder
            // 
            this.text_image_floder.Location = new System.Drawing.Point(30, 147);
            this.text_image_floder.Name = "text_image_floder";
            this.text_image_floder.Size = new System.Drawing.Size(283, 30);
            this.text_image_floder.TabIndex = 12;
            this.text_image_floder.Text = "C:\\home\\linaro\\images\\plate";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 212);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 25);
            this.label11.TabIndex = 11;
            this.label11.Text = "Gate";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(710, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 25);
            this.label10.TabIndex = 10;
            this.label10.Text = "End Time";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(342, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 25);
            this.label9.TabIndex = 9;
            this.label9.Text = "Start Time";
            // 
            // Plate
            // 
            this.Plate.AutoSize = true;
            this.Plate.Location = new System.Drawing.Point(25, 16);
            this.Plate.Name = "Plate";
            this.Plate.Size = new System.Drawing.Size(56, 25);
            this.Plate.TabIndex = 8;
            this.Plate.Text = "Plate";
            // 
            // dgvResults
            // 
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(30, 321);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.RowTemplate.Height = 24;
            this.dgvResults.Size = new System.Drawing.Size(1094, 289);
            this.dgvResults.TabIndex = 6;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(715, 259);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(89, 32);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(490, 261);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(115, 30);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(715, 54);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(342, 30);
            this.dtpEnd.TabIndex = 2;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(336, 56);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(342, 30);
            this.dtpStart.TabIndex = 1;
            // 
            // txtPlate
            // 
            this.txtPlate.Location = new System.Drawing.Point(30, 56);
            this.txtPlate.Name = "txtPlate";
            this.txtPlate.Size = new System.Drawing.Size(273, 30);
            this.txtPlate.TabIndex = 0;
            this.txtPlate.Tag = "";
            // 
            // comboBox_qury
            // 
            this.comboBox_qury.FormattingEnabled = true;
            this.comboBox_qury.Location = new System.Drawing.Point(32, 261);
            this.comboBox_qury.Name = "comboBox_qury";
            this.comboBox_qury.Size = new System.Drawing.Size(313, 33);
            this.comboBox_qury.TabIndex = 22;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 703);
            this.Controls.Add(this.Conection);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "TCP IP";
            this.Conection.ResumeLayout(false);
            this.Connection.ResumeLayout(false);
            this.Connection.PerformLayout();
            this.Recording.ResumeLayout(false);
            this.Recording.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.live_View_picture)).EndInit();
            this.tapquery1.ResumeLayout(false);
            this.tapquery1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_tcpip;
        private System.Windows.Forms.TextBox text_tcpport;
        private System.Windows.Forms.Button btn_connect_tcp;
        private System.Windows.Forms.Button btn_recording;
        private System.Windows.Forms.TextBox text_duration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txtLogBox;
        private System.Windows.Forms.Button btn_txtbox;
        private System.Windows.Forms.Button btn_disconnect;
        private System.Windows.Forms.SaveFileDialog save_video_file;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox text_vehicle_count_gate1;
        private System.Windows.Forms.TextBox text_vehicle_count_gate2;
        private System.Windows.Forms.ComboBox combo_gates;
        private System.Windows.Forms.TabControl Conection;
        private System.Windows.Forms.TabPage Connection;
        private System.Windows.Forms.TabPage Recording;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ListView listView_scheduled;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btn_show_live;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox live_View_picture;
        private System.Windows.Forms.ComboBox combo_live_gates;
        private System.Windows.Forms.TextBox text_live_duration;
        private System.Windows.Forms.TabPage tapquery1;
        private System.Windows.Forms.TextBox txtPlate;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label Plate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_image_folder;
        private System.Windows.Forms.TextBox text_image_floder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox comboBox_qury;
    }
}

