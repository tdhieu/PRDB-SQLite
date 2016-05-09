namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_OpenView
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
            this.ComboBox_OpenView = new System.Windows.Forms.ComboBox();
            this.ButtonCancel_OpenView = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_OpenView = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox_OpenView
            // 
            this.ComboBox_OpenView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_OpenView.FormattingEnabled = true;
            this.ComboBox_OpenView.Location = new System.Drawing.Point(15, 46);
            this.ComboBox_OpenView.Name = "ComboBox_OpenView";
            this.ComboBox_OpenView.Size = new System.Drawing.Size(334, 24);
            this.ComboBox_OpenView.TabIndex = 48;
            // 
            // ButtonCancel_OpenView
            // 
            this.ButtonCancel_OpenView.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_OpenView.Appearance.Options.UseFont = true;
            this.ButtonCancel_OpenView.Location = new System.Drawing.Point(262, 90);
            this.ButtonCancel_OpenView.Name = "ButtonCancel_OpenView";
            this.ButtonCancel_OpenView.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_OpenView.TabIndex = 47;
            this.ButtonCancel_OpenView.Text = "Cancel";
            this.ButtonCancel_OpenView.Click += new System.EventHandler(this.ButtonCancel_OpenView_Click);
            // 
            // ButtonOK_OpenView
            // 
            this.ButtonOK_OpenView.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_OpenView.Appearance.Options.UseFont = true;
            this.ButtonOK_OpenView.Location = new System.Drawing.Point(154, 90);
            this.ButtonOK_OpenView.Name = "ButtonOK_OpenView";
            this.ButtonOK_OpenView.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_OpenView.TabIndex = 46;
            this.ButtonOK_OpenView.Text = "OK";
            this.ButtonOK_OpenView.Click += new System.EventHandler(this.ButtonOK_OpenView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 16);
            this.label1.TabIndex = 45;
            this.label1.Text = "Choose a view to open:";
            // 
            // Form_OpenView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(366, 150);
            this.Controls.Add(this.ComboBox_OpenView);
            this.Controls.Add(this.ButtonCancel_OpenView);
            this.Controls.Add(this.ButtonOK_OpenView);
            this.Controls.Add(this.label1);
            this.Name = "Form_OpenView";
            this.Text = "Open View...";
            this.Load += new System.EventHandler(this.Form_OpenView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_OpenView;
        private DevExpress.XtraEditors.SimpleButton ButtonCancel_OpenView;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_OpenView;
        private System.Windows.Forms.Label label1;
    }
}