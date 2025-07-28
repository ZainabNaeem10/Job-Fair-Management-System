namespace TPO
{
    partial class Form14
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
            this.jobfairDataSet6 = new TPO.jobfairDataSet6();
            this.departmentPlacementSummaryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.departmentPlacementSummaryTableAdapter = new TPO.jobfairDataSet6TableAdapters.DepartmentPlacementSummaryTableAdapter();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentPlacementSummaryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.departmentPlacementSummaryBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TPO.Report5.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(163, 88);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // jobfairDataSet6
            // 
            this.jobfairDataSet6.DataSetName = "jobfairDataSet6";
            this.jobfairDataSet6.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // departmentPlacementSummaryBindingSource
            // 
            this.departmentPlacementSummaryBindingSource.DataMember = "DepartmentPlacementSummary";
            this.departmentPlacementSummaryBindingSource.DataSource = this.jobfairDataSet6;
            // 
            // departmentPlacementSummaryTableAdapter
            // 
            this.departmentPlacementSummaryTableAdapter.ClearBeforeFill = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(586, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form14
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form14";
            this.Text = "Form14";
            this.Load += new System.EventHandler(this.Form14_Load);
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentPlacementSummaryBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private jobfairDataSet6 jobfairDataSet6;
        private System.Windows.Forms.BindingSource departmentPlacementSummaryBindingSource;
        private jobfairDataSet6TableAdapters.DepartmentPlacementSummaryTableAdapter departmentPlacementSummaryTableAdapter;
        private System.Windows.Forms.Button button2;
    }
}