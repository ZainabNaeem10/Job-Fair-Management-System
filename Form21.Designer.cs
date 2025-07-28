namespace TPO
{
    partial class Form21
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
            this.jobfairDataSet15 = new TPO.jobfairDataSet15();
            this.coordinatorTimeUsageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.coordinatorTimeUsageTableAdapter = new TPO.jobfairDataSet15TableAdapters.CoordinatorTimeUsageTableAdapter();
            this.jobfairDataSet16 = new TPO.jobfairDataSet16();
            this.coordinatorTimeUsageBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.coordinatorTimeUsageTableAdapter1 = new TPO.jobfairDataSet16TableAdapters.CoordinatorTimeUsageTableAdapter();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coordinatorTimeUsageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coordinatorTimeUsageBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.coordinatorTimeUsageBindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TPO.Report12.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(162, 57);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // jobfairDataSet15
            // 
            this.jobfairDataSet15.DataSetName = "jobfairDataSet15";
            this.jobfairDataSet15.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // coordinatorTimeUsageBindingSource
            // 
            this.coordinatorTimeUsageBindingSource.DataMember = "CoordinatorTimeUsage";
            this.coordinatorTimeUsageBindingSource.DataSource = this.jobfairDataSet15;
            // 
            // coordinatorTimeUsageTableAdapter
            // 
            this.coordinatorTimeUsageTableAdapter.ClearBeforeFill = true;
            // 
            // jobfairDataSet16
            // 
            this.jobfairDataSet16.DataSetName = "jobfairDataSet16";
            this.jobfairDataSet16.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // coordinatorTimeUsageBindingSource1
            // 
            this.coordinatorTimeUsageBindingSource1.DataMember = "CoordinatorTimeUsage";
            this.coordinatorTimeUsageBindingSource1.DataSource = this.jobfairDataSet16;
            // 
            // coordinatorTimeUsageTableAdapter1
            // 
            this.coordinatorTimeUsageTableAdapter1.ClearBeforeFill = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(589, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form21
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form21";
            this.Text = "Form21";
            this.Load += new System.EventHandler(this.Form21_Load);
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coordinatorTimeUsageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coordinatorTimeUsageBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private jobfairDataSet15 jobfairDataSet15;
        private System.Windows.Forms.BindingSource coordinatorTimeUsageBindingSource;
        private jobfairDataSet15TableAdapters.CoordinatorTimeUsageTableAdapter coordinatorTimeUsageTableAdapter;
        private jobfairDataSet16 jobfairDataSet16;
        private System.Windows.Forms.BindingSource coordinatorTimeUsageBindingSource1;
        private jobfairDataSet16TableAdapters.CoordinatorTimeUsageTableAdapter coordinatorTimeUsageTableAdapter1;
        private System.Windows.Forms.Button button2;
    }
}