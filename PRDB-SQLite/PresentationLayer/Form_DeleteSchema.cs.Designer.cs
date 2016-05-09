namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_DeleteSchema
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
            this.ComboBox_DeleteSchema = new System.Windows.Forms.ComboBox();
            this.ButtonCancel_DeleteSchema = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_DeleteSchema = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox_DeleteSchema
            // 
            this.ComboBox_DeleteSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_DeleteSchema.FormattingEnabled = true;
            this.ComboBox_DeleteSchema.Location = new System.Drawing.Point(15, 48);
            this.ComboBox_DeleteSchema.Name = "ComboBox_DeleteSchema";
            this.ComboBox_DeleteSchema.Size = new System.Drawing.Size(334, 24);
            this.ComboBox_DeleteSchema.TabIndex = 36;
            // 
            // ButtonCancel_DeleteSchema
            // 
            this.ButtonCancel_DeleteSchema.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_DeleteSchema.Appearance.Options.UseFont = true;
            this.ButtonCancel_DeleteSchema.Location = new System.Drawing.Point(262, 92);
            this.ButtonCancel_DeleteSchema.Name = "ButtonCancel_DeleteSchema";
            this.ButtonCancel_DeleteSchema.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_DeleteSchema.TabIndex = 35;
            this.ButtonCancel_DeleteSchema.Text = "Cancel";
            this.ButtonCancel_DeleteSchema.Click += new System.EventHandler(this.ButtonCancel_DeleteSchema_Click);
            // 
            // ButtonOK_DeleteSchema
            // 
            this.ButtonOK_DeleteSchema.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_DeleteSchema.Appearance.Options.UseFont = true;
            this.ButtonOK_DeleteSchema.Location = new System.Drawing.Point(154, 92);
            this.ButtonOK_DeleteSchema.Name = "ButtonOK_DeleteSchema";
            this.ButtonOK_DeleteSchema.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_DeleteSchema.TabIndex = 34;
            this.ButtonOK_DeleteSchema.Text = "OK";
            this.ButtonOK_DeleteSchema.Click += new System.EventHandler(this.ButtonOK_DeleteSchema_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 16);
            this.label1.TabIndex = 33;
            this.label1.Text = "Choose a schema to delete:";
            // 
            // Form_DeleteSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(365, 148);
            this.Controls.Add(this.ComboBox_DeleteSchema);
            this.Controls.Add(this.ButtonCancel_DeleteSchema);
            this.Controls.Add(this.ButtonOK_DeleteSchema);
            this.Controls.Add(this.label1);
            this.Name = "Form_DeleteSchema";
            this.Text = "Delete Schema...";
            this.Load += new System.EventHandler(this.Form_DeleteSchema_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_DeleteSchema;
        private DevExpress.XtraEditors.SimpleButton ButtonCancel_DeleteSchema;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_DeleteSchema;
        private System.Windows.Forms.Label label1;
    }
}