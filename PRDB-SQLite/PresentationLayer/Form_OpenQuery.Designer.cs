namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_OpenQuery
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
            this.ComboBox_OpenQuery = new System.Windows.Forms.ComboBox();
            this.ButtonCancel_OpenQuery = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_OpenQuery = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox_OpenQuery
            // 
            this.ComboBox_OpenQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_OpenQuery.FormattingEnabled = true;
            this.ComboBox_OpenQuery.Location = new System.Drawing.Point(15, 47);
            this.ComboBox_OpenQuery.Name = "ComboBox_OpenQuery";
            this.ComboBox_OpenQuery.Size = new System.Drawing.Size(334, 24);
            this.ComboBox_OpenQuery.TabIndex = 44;
            // 
            // ButtonCancel_OpenQuery
            // 
            this.ButtonCancel_OpenQuery.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_OpenQuery.Appearance.Options.UseFont = true;
            this.ButtonCancel_OpenQuery.Location = new System.Drawing.Point(262, 91);
            this.ButtonCancel_OpenQuery.Name = "ButtonCancel_OpenQuery";
            this.ButtonCancel_OpenQuery.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_OpenQuery.TabIndex = 43;
            this.ButtonCancel_OpenQuery.Text = "Cancel";
            this.ButtonCancel_OpenQuery.Click += new System.EventHandler(this.ButtonCancel_OpenQuery_Click);
            // 
            // ButtonOK_OpenQuery
            // 
            this.ButtonOK_OpenQuery.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_OpenQuery.Appearance.Options.UseFont = true;
            this.ButtonOK_OpenQuery.Location = new System.Drawing.Point(154, 91);
            this.ButtonOK_OpenQuery.Name = "ButtonOK_OpenQuery";
            this.ButtonOK_OpenQuery.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_OpenQuery.TabIndex = 42;
            this.ButtonOK_OpenQuery.Text = "OK";
            this.ButtonOK_OpenQuery.Click += new System.EventHandler(this.ButtonOK_OpenQuery_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 16);
            this.label1.TabIndex = 41;
            this.label1.Text = "Choose a query to open:";
            // 
            // Form_OpenQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(367, 144);
            this.Controls.Add(this.ComboBox_OpenQuery);
            this.Controls.Add(this.ButtonCancel_OpenQuery);
            this.Controls.Add(this.ButtonOK_OpenQuery);
            this.Controls.Add(this.label1);
            this.Name = "Form_OpenQuery";
            this.Text = "Open Query...";
            this.Load += new System.EventHandler(this.Form_OpenQuery_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_OpenQuery;
        private DevExpress.XtraEditors.SimpleButton ButtonCancel_OpenQuery;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_OpenQuery;
        private System.Windows.Forms.Label label1;
    }
}