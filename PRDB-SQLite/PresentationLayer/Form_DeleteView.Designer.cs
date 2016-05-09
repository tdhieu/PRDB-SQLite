namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_DeleteView
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
            this.ComboBox_DeleteView = new System.Windows.Forms.ComboBox();
            this.ButtonCancel_DeleteView = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_DeleteView = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox_DeleteView
            // 
            this.ComboBox_DeleteView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_DeleteView.FormattingEnabled = true;
            this.ComboBox_DeleteView.Location = new System.Drawing.Point(15, 45);
            this.ComboBox_DeleteView.Name = "ComboBox_DeleteView";
            this.ComboBox_DeleteView.Size = new System.Drawing.Size(334, 24);
            this.ComboBox_DeleteView.TabIndex = 40;
            // 
            // ButtonCancel_DeleteView
            // 
            this.ButtonCancel_DeleteView.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_DeleteView.Appearance.Options.UseFont = true;
            this.ButtonCancel_DeleteView.Location = new System.Drawing.Point(262, 89);
            this.ButtonCancel_DeleteView.Name = "ButtonCancel_DeleteView";
            this.ButtonCancel_DeleteView.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_DeleteView.TabIndex = 39;
            this.ButtonCancel_DeleteView.Text = "Cancel";
            this.ButtonCancel_DeleteView.Click += new System.EventHandler(this.ButtonCancel_DeleteView_Click);
            // 
            // ButtonOK_DeleteView
            // 
            this.ButtonOK_DeleteView.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_DeleteView.Appearance.Options.UseFont = true;
            this.ButtonOK_DeleteView.Location = new System.Drawing.Point(154, 89);
            this.ButtonOK_DeleteView.Name = "ButtonOK_DeleteView";
            this.ButtonOK_DeleteView.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_DeleteView.TabIndex = 38;
            this.ButtonOK_DeleteView.Text = "OK";
            this.ButtonOK_DeleteView.Click += new System.EventHandler(this.ButtonOK_DeleteView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "Choose a view to delete:";
            // 
            // Form_DeleteView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(365, 134);
            this.Controls.Add(this.ComboBox_DeleteView);
            this.Controls.Add(this.ButtonCancel_DeleteView);
            this.Controls.Add(this.ButtonOK_DeleteView);
            this.Controls.Add(this.label1);
            this.Name = "Form_DeleteView";
            this.Text = "Form_DeleteView";
            this.Load += new System.EventHandler(this.Form_DeleteView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_DeleteView;
        private DevExpress.XtraEditors.SimpleButton ButtonCancel_DeleteView;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_DeleteView;
        private System.Windows.Forms.Label label1;
    }
}