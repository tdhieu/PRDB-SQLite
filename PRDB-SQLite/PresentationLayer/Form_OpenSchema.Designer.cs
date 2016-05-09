namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_OpenSchema
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
            this.ComboBox_OpenSchema = new System.Windows.Forms.ComboBox();
            this.ButtonCancel_OpenSchema = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_OpenSchema = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox_OpenSchema
            // 
            this.ComboBox_OpenSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_OpenSchema.FormattingEnabled = true;
            this.ComboBox_OpenSchema.Location = new System.Drawing.Point(15, 48);
            this.ComboBox_OpenSchema.Name = "ComboBox_OpenSchema";
            this.ComboBox_OpenSchema.Size = new System.Drawing.Size(334, 24);
            this.ComboBox_OpenSchema.TabIndex = 52;
            // 
            // ButtonCancel_OpenSchema
            // 
            this.ButtonCancel_OpenSchema.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_OpenSchema.Appearance.Options.UseFont = true;
            this.ButtonCancel_OpenSchema.Location = new System.Drawing.Point(262, 92);
            this.ButtonCancel_OpenSchema.Name = "ButtonCancel_OpenSchema";
            this.ButtonCancel_OpenSchema.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_OpenSchema.TabIndex = 51;
            this.ButtonCancel_OpenSchema.Text = "Cancel";
            this.ButtonCancel_OpenSchema.Click += new System.EventHandler(this.ButtonCancel_OpenSchema_Click);
            // 
            // ButtonOK_OpenSchema
            // 
            this.ButtonOK_OpenSchema.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_OpenSchema.Appearance.Options.UseFont = true;
            this.ButtonOK_OpenSchema.Location = new System.Drawing.Point(154, 92);
            this.ButtonOK_OpenSchema.Name = "ButtonOK_OpenSchema";
            this.ButtonOK_OpenSchema.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_OpenSchema.TabIndex = 50;
            this.ButtonOK_OpenSchema.Text = "OK";
            this.ButtonOK_OpenSchema.Click += new System.EventHandler(this.ButtonOK_OpenSchema_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 16);
            this.label1.TabIndex = 49;
            this.label1.Text = "Choose a schema to open:";
            // 
            // Form_OpenSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(366, 156);
            this.Controls.Add(this.ComboBox_OpenSchema);
            this.Controls.Add(this.ButtonCancel_OpenSchema);
            this.Controls.Add(this.ButtonOK_OpenSchema);
            this.Controls.Add(this.label1);
            this.Name = "Form_OpenSchema";
            this.Text = "Open Schema...";
            this.Load += new System.EventHandler(this.Form_OpenSchema_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_OpenSchema;
        private DevExpress.XtraEditors.SimpleButton ButtonCancel_OpenSchema;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_OpenSchema;
        private System.Windows.Forms.Label label1;
    }
}