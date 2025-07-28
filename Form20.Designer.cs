namespace TPO
{
    partial class Form20
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
            this.jobfairDataSet13 = new TPO.jobfairDataSet13();
            this.peakParticipationHoursBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.peakParticipationHoursTableAdapter = new TPO.jobfairDataSet13TableAdapters.PeakParticipationHoursTableAdapter();
            this.jobfairDataSet14 = new TPO.jobfairDataSet14();
            this.peakParticipationHoursBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.peakParticipationHoursTableAdapter1 = new TPO.jobfairDataSet14TableAdapters.PeakParticipationHoursTableAdapter();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peakParticipationHoursBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peakParticipationHoursBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.peakParticipationHoursBindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TPO.Report11.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(166, 60);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // jobfairDataSet13
            // 
            this.jobfairDataSet13.DataSetName = "jobfairDataSet13";
            this.jobfairDataSet13.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // peakParticipationHoursBindingSource
            // 
            this.peakParticipationHoursBindingSource.DataMember = "PeakParticipationHours";
            this.peakParticipationHoursBindingSource.DataSource = this.jobfairDataSet13;
            // 
            // peakParticipationHoursTableAdapter
            // 
            this.peakParticipationHoursTableAdapter.ClearBeforeFill = true;
            // 
            // jobfairDataSet14
            // 
            this.jobfairDataSet14.DataSetName = "jobfairDataSet14";
            this.jobfairDataSet14.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // peakParticipationHoursBindingSource1
            // 
            this.peakParticipationHoursBindingSource1.DataMember = "PeakParticipationHours";
            this.peakParticipationHoursBindingSource1.DataSource = this.jobfairDataSet14;
            // 
            // peakParticipationHoursTableAdapter1
            // 
            this.peakParticipationHoursTableAdapter1.ClearBeforeFill = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(582, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form20
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form20";
            this.Text = "Form20";
            this.Load += new System.EventHandler(this.Form20_Load);
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peakParticipationHoursBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peakParticipationHoursBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private jobfairDataSet13 jobfairDataSet13;
        private System.Windows.Forms.BindingSource peakParticipationHoursBindingSource;
        private jobfairDataSet13TableAdapters.PeakParticipationHoursTableAdapter peakParticipationHoursTableAdapter;
        private jobfairDataSet14 jobfairDataSet14;
        private System.Windows.Forms.BindingSource peakParticipationHoursBindingSource1;
        private jobfairDataSet14TableAdapters.PeakParticipationHoursTableAdapter peakParticipationHoursTableAdapter1;
        private System.Windows.Forms.Button button2;
    }
}