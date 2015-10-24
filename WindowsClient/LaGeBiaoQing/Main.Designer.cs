namespace LaGeBiaoQing
{
    partial class Main
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tagsLoader = new System.ComponentModel.BackgroundWorker();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.exprsLoader = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PictureLoader = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tagsLoader
            // 
            this.tagsLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.tagsLoader_DoWork);
            this.tagsLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.tagsLoader_RunWorkerCompleted);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(254, 195);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // exprsLoader
            // 
            this.exprsLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.exprsLoader_DoWork);
            this.exprsLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.exprsLoader_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(12, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 201);
            this.panel1.TabIndex = 2;
            // 
            // PictureLoader
            // 
            this.PictureLoader.WorkerReportsProgress = true;
            this.PictureLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PictureLoader_DoWork);
            this.PictureLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.PictureLoader_ProgressChanged);
            this.PictureLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.PictureLoader_RunWorkerCompleted);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 469);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Main";
            this.Text = "Main";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.ComponentModel.BackgroundWorker tagsLoader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.ComponentModel.BackgroundWorker exprsLoader;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker PictureLoader;
    }
}