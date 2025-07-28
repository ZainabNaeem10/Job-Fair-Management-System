namespace TPO
{
    partial class Form16
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
            this.jobfairDataSet9 = new TPO.jobfairDataSet9();
            this.departmentWiseRegistrationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.departmentWiseRegistrationTableAdapter = new TPO.jobfairDataSet9TableAdapters.DepartmentWiseRegistrationTableAdapter();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentWiseRegistrationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.departmentWiseRegistrationBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TPO.Report7.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(176, 83);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // jobfairDataSet9
            // 
            this.jobfairDataSet9.DataSetName = "jobfairDataSet9";
            this.jobfairDataSet9.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // departmentWiseRegistrationBindingSource
            // 
            this.departmentWiseRegistrationBindingSource.DataMember = "DepartmentWiseRegistration";
            this.departmentWiseRegistrationBindingSource.DataSource = this.jobfairDataSet9;
            // 
            // departmentWiseRegistrationTableAdapter
            // 
            this.departmentWiseRegistrationTableAdapter.ClearBeforeFill = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(596, 199);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form16
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form16";
            this.Text = "Form16";
            this.Load += new System.EventHandler(this.Form16_Load);
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentWiseRegistrationBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private jobfairDataSet9 jobfairDataSet9;
        private System.Windows.Forms.BindingSource departmentWiseRegistrationBindingSource;
        private jobfairDataSet9TableAdapters.DepartmentWiseRegistrationTableAdapter departmentWiseRegistrationTableAdapter;
        private System.Windows.Forms.Button button2;
    }
}