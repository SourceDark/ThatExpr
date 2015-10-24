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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.exprsLoader = new System.ComponentModel.BackgroundWorker();
            this.PictureLoader = new System.ComponentModel.BackgroundWorker();
            this.largerViewerr = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.CollectionPage = new System.Windows.Forms.TabPage();
            this.DiscoverPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.largerViewerr)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.DiscoverPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 34);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(270, 167);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // exprsLoader
            // 
            this.exprsLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.exprsLoader_DoWork);
            this.exprsLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.exprsLoader_RunWorkerCompleted);
            // 
            // PictureLoader
            // 
            this.PictureLoader.WorkerReportsProgress = true;
            this.PictureLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PictureLoader_DoWork);
            this.PictureLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.PictureLoader_ProgressChanged);
            this.PictureLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.PictureLoader_RunWorkerCompleted);
            // 
            // largerViewerr
            // 
            this.largerViewerr.Location = new System.Drawing.Point(6, 20);
            this.largerViewerr.Name = "largerViewerr";
            this.largerViewerr.Size = new System.Drawing.Size(257, 154);
            this.largerViewerr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.largerViewerr.TabIndex = 3;
            this.largerViewerr.TabStop = false;
            this.largerViewerr.Click += new System.EventHandler(this.largerViewerr_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.CollectionPage);
            this.tabControl1.Controls.Add(this.DiscoverPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(290, 420);
            this.tabControl1.TabIndex = 4;
            // 
            // CollectionPage
            // 
            this.CollectionPage.Location = new System.Drawing.Point(4, 22);
            this.CollectionPage.Name = "CollectionPage";
            this.CollectionPage.Padding = new System.Windows.Forms.Padding(3);
            this.CollectionPage.Size = new System.Drawing.Size(282, 394);
            this.CollectionPage.TabIndex = 0;
            this.CollectionPage.Text = "收藏";
            this.CollectionPage.UseVisualStyleBackColor = true;
            // 
            // DiscoverPage
            // 
            this.DiscoverPage.Controls.Add(this.groupBox1);
            this.DiscoverPage.Controls.Add(this.flowLayoutPanel1);
            this.DiscoverPage.Location = new System.Drawing.Point(4, 22);
            this.DiscoverPage.Name = "DiscoverPage";
            this.DiscoverPage.Padding = new System.Windows.Forms.Padding(3);
            this.DiscoverPage.Size = new System.Drawing.Size(282, 394);
            this.DiscoverPage.TabIndex = 1;
            this.DiscoverPage.Text = "发现";
            this.DiscoverPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.largerViewerr);
            this.groupBox1.Location = new System.Drawing.Point(7, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 180);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 444);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "辣个表情 Alpha版";
            ((System.ComponentModel.ISupportInitialize)(this.largerViewerr)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.DiscoverPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.ComponentModel.BackgroundWorker exprsLoader;
        private System.ComponentModel.BackgroundWorker PictureLoader;
        private System.Windows.Forms.PictureBox largerViewerr;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage CollectionPage;
        private System.Windows.Forms.TabPage DiscoverPage;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}