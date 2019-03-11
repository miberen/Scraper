namespace BritScraper
{
    partial class BritScraper
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
            this.tssl_bar = new System.Windows.Forms.StatusStrip();
            this.tssl_bar_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.btn_goToJob = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.olv_jobs = new BrightIdeasSoftware.ObjectListView();
            this.employer = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.category = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.jobTitle = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.dueDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.txb_filter = new System.Windows.Forms.TextBox();
            this.lbl_filter = new System.Windows.Forms.Label();
            this.tssl_bar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_jobs)).BeginInit();
            this.SuspendLayout();
            // 
            // tssl_bar
            // 
            this.tssl_bar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_bar_status});
            this.tssl_bar.Location = new System.Drawing.Point(0, 540);
            this.tssl_bar.Name = "tssl_bar";
            this.tssl_bar.Size = new System.Drawing.Size(1436, 22);
            this.tssl_bar.TabIndex = 0;
            // 
            // tssl_bar_status
            // 
            this.tssl_bar_status.Name = "tssl_bar_status";
            this.tssl_bar_status.Size = new System.Drawing.Size(52, 17);
            this.tssl_bar_status.Text = "Loader...";
            // 
            // btn_goToJob
            // 
            this.btn_goToJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_goToJob.Location = new System.Drawing.Point(1354, 514);
            this.btn_goToJob.Name = "btn_goToJob";
            this.btn_goToJob.Size = new System.Drawing.Size(75, 23);
            this.btn_goToJob.TabIndex = 5;
            this.btn_goToJob.Text = "Gå til jobbet";
            this.btn_goToJob.UseVisualStyleBackColor = true;
            this.btn_goToJob.Click += new System.EventHandler(this.btn_goToJob_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_refresh.Location = new System.Drawing.Point(1354, 485);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_refresh.TabIndex = 6;
            this.btn_refresh.Text = "Load jobs";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // olv_jobs
            // 
            this.olv_jobs.AllColumns.Add(this.employer);
            this.olv_jobs.AllColumns.Add(this.category);
            this.olv_jobs.AllColumns.Add(this.jobTitle);
            this.olv_jobs.AllColumns.Add(this.dueDate);
            this.olv_jobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olv_jobs.CellEditUseWholeCell = false;
            this.olv_jobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.employer,
            this.category,
            this.jobTitle,
            this.dueDate});
            this.olv_jobs.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_jobs.FullRowSelect = true;
            this.olv_jobs.GridLines = true;
            this.olv_jobs.Location = new System.Drawing.Point(16, 38);
            this.olv_jobs.MultiSelect = false;
            this.olv_jobs.Name = "olv_jobs";
            this.olv_jobs.SelectAllOnControlA = false;
            this.olv_jobs.Size = new System.Drawing.Size(1332, 499);
            this.olv_jobs.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.olv_jobs.SpaceBetweenGroups = 5;
            this.olv_jobs.TabIndex = 7;
            this.olv_jobs.UseCompatibleStateImageBehavior = false;
            this.olv_jobs.UseFilterIndicator = true;
            this.olv_jobs.UseFiltering = true;
            this.olv_jobs.View = System.Windows.Forms.View.Details;
            this.olv_jobs.ItemActivate += new System.EventHandler(this.olv_jobs_ItemActivate);
            // 
            // employer
            // 
            this.employer.AspectName = "Employer";
            this.employer.IsEditable = false;
            this.employer.Text = "Arbejdssted";
            this.employer.Width = 105;
            // 
            // category
            // 
            this.category.AspectName = "Category";
            this.category.IsEditable = false;
            this.category.Text = "Arbejdsområde";
            // 
            // jobTitle
            // 
            this.jobTitle.AspectName = "JobTitle";
            this.jobTitle.IsEditable = false;
            this.jobTitle.Text = "Jobtitel";
            this.jobTitle.UseInitialLetterForGroup = true;
            // 
            // dueDate
            // 
            this.dueDate.AspectName = "DueDateString";
            this.dueDate.AspectToStringFormat = "{0:d}";
            this.dueDate.IsEditable = false;
            this.dueDate.Text = "Ansøgningsfrist";
            // 
            // txb_filter
            // 
            this.txb_filter.Location = new System.Drawing.Point(54, 9);
            this.txb_filter.Name = "txb_filter";
            this.txb_filter.Size = new System.Drawing.Size(233, 20);
            this.txb_filter.TabIndex = 8;
            this.txb_filter.TextChanged += new System.EventHandler(this.txb_filter_TextChanged);
            // 
            // lbl_filter
            // 
            this.lbl_filter.AutoSize = true;
            this.lbl_filter.Location = new System.Drawing.Point(16, 12);
            this.lbl_filter.Name = "lbl_filter";
            this.lbl_filter.Size = new System.Drawing.Size(32, 13);
            this.lbl_filter.TabIndex = 9;
            this.lbl_filter.Text = "Filter:";
            // 
            // BritScraper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 562);
            this.Controls.Add(this.lbl_filter);
            this.Controls.Add(this.txb_filter);
            this.Controls.Add(this.olv_jobs);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.btn_goToJob);
            this.Controls.Add(this.tssl_bar);
            this.Name = "BritScraper";
            this.Text = "BritScraper";
            this.tssl_bar.ResumeLayout(false);
            this.tssl_bar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_jobs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip tssl_bar;
        private System.Windows.Forms.ToolStripStatusLabel tssl_bar_status;
        private System.Windows.Forms.Button btn_goToJob;
        private System.Windows.Forms.Button btn_refresh;
        private BrightIdeasSoftware.ObjectListView olv_jobs;
        private BrightIdeasSoftware.OLVColumn employer;
        private BrightIdeasSoftware.OLVColumn category;
        private BrightIdeasSoftware.OLVColumn jobTitle;
        private BrightIdeasSoftware.OLVColumn dueDate;
        private System.Windows.Forms.TextBox txb_filter;
        private System.Windows.Forms.Label lbl_filter;
    }
}

