namespace TPO
{
    partial class Form11
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
            this.jobfairDataSet1 = new TPO.jobfairDataSet1();
            this.jobfairDataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.companyOfferApplicationRatiosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.companyOfferApplicationRatiosTableAdapter = new TPO.jobfairDataSet1TableAdapters.CompanyOfferApplicationRatiosTableAdapter();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyOfferApplicationRatiosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.companyOfferApplicationRatiosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TPO.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(158, 63);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // jobfairDataSet1
            // 
            this.jobfairDataSet1.DataSetName = "jobfairDataSet1";
            this.jobfairDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // jobfairDataSet1BindingSource
            // 
            this.jobfairDataSet1BindingSource.DataSource = this.jobfairDataSet1;
            this.jobfairDataSet1BindingSource.Position = 0;
            // 
            // companyOfferApplicationRatiosBindingSource
            // 
            this.companyOfferApplicationRatiosBindingSource.DataMember = "CompanyOfferApplicationRatios";
            this.companyOfferApplicationRatiosBindingSource.DataSource = this.jobfairDataSet1;
            // 
            // companyOfferApplicationRatiosTableAdapter
            // 
            this.companyOfferApplicationRatiosTableAdapter.ClearBeforeFill = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(588, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form11
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form11";
            this.Text = "Form11";
            this.Load += new System.EventHandler(this.Form11_Load);
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobfairDataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyOfferApplicationRatiosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource jobfairDataSet1BindingSource;
        private jobfairDataSet1 jobfairDataSet1;
        private System.Windows.Forms.BindingSource companyOfferApplicationRatiosBindingSource;
        private jobfairDataSet1TableAdapters.CompanyOfferApplicationRatiosTableAdapter companyOfferApplicationRatiosTableAdapter;
        private System.Windows.Forms.Button button2;
    }
}