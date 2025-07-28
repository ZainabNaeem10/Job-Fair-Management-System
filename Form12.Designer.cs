namespace TPO
{
    partial class Form12
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.jobfairDataSet3 = new TPO.jobfairDataSet3();
            this.recruiterRatingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.recruiterRatingTableAdapter = new TPO.jobfairDataSet3TableAdapters.RecruiterRatingTableAdapter();
            this.jobfairDataSet4 = new TPO.jobfairDataSet4();
            this.recruiterRatingBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.recruiterRatingTableAdapter1 = new TPO.jobfairDataSet4TableAdapters.RecruiterRatingTableAdapter();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recruiterRatingBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recruiterRatingBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.recruiterRatingBindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TPO.Report3.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(133, 86);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // jobfairDataSet3
            // 
            this.jobfairDataSet3.DataSetName = "jobfairDataSet3";
            this.jobfairDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // recruiterRatingBindingSource
            // 
            this.recruiterRatingBindingSource.DataMember = "RecruiterRating";
            this.recruiterRatingBindingSource.DataSource = this.jobfairDataSet3;
            // 
            // recruiterRatingTableAdapter
            // 
            this.recruiterRatingTableAdapter.ClearBeforeFill = true;
            // 
            // jobfairDataSet4
            // 
            this.jobfairDataSet4.DataSetName = "jobfairDataSet4";
            this.jobfairDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // recruiterRatingBindingSource1
            // 
            this.recruiterRatingBindingSource1.DataMember = "RecruiterRating";
            this.recruiterRatingBindingSource1.DataSource = this.jobfairDataSet4;
            // 
            // recruiterRatingTableAdapter1
            // 
            this.recruiterRatingTableAdapter1.ClearBeforeFill = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(569, 201);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form12
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form12";
            this.Text = "Form12";
            this.Load += new System.EventHandler(this.Form12_Load);
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recruiterRatingBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recruiterRatingBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private jobfairDataSet3 jobfairDataSet3;
        private System.Windows.Forms.BindingSource recruiterRatingBindingSource;
        private jobfairDataSet3TableAdapters.RecruiterRatingTableAdapter recruiterRatingTableAdapter;
        private jobfairDataSet4 jobfairDataSet4;
        private System.Windows.Forms.BindingSource recruiterRatingBindingSource1;
        private jobfairDataSet4TableAdapters.RecruiterRatingTableAdapter recruiterRatingTableAdapter1;
        private System.Windows.Forms.Button button2;
    }
}