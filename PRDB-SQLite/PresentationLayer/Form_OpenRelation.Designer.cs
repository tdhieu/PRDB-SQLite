namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_OpenRelation
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
            this.ComboBox_OpenRelation = new System.Windows.Forms.ComboBox();
            this.ButtonCancel_OpenRelation = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_OpenRelation = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox_OpenRelation
            // 
            this.ComboBox_OpenRelation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_OpenRelation.FormattingEnabled = true;
            this.ComboBox_OpenRelation.Location = new System.Drawing.Point(18, 46);
            this.ComboBox_OpenRelation.Name = "ComboBox_OpenRelation";
            this.ComboBox_OpenRelation.Size = new System.Drawing.Size(334, 24);
            this.ComboBox_OpenRelation.TabIndex = 56;
            // 
            // ButtonCancel_OpenRelation
            // 
            this.ButtonCancel_OpenRelation.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_OpenRelation.Appearance.Options.UseFont = true;
            this.ButtonCancel_OpenRelation.Location = new System.Drawing.Point(265, 90);
            this.ButtonCancel_OpenRelation.Name = "ButtonCancel_OpenRelation";
            this.ButtonCancel_OpenRelation.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_OpenRelation.TabIndex = 55;
            this.ButtonCancel_OpenRelation.Text = "Cancel";
            this.ButtonCancel_OpenRelation.Click += new System.EventHandler(this.ButtonCancel_OpenRelation_Click);
            // 
            // ButtonOK_OpenRelation
            // 
            this.ButtonOK_OpenRelation.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_OpenRelation.Appearance.Options.UseFont = true;
            this.ButtonOK_OpenRelation.Location = new System.Drawing.Point(157, 90);
            this.ButtonOK_OpenRelation.Name = "ButtonOK_OpenRelation";
            this.ButtonOK_OpenRelation.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_OpenRelation.TabIndex = 54;
            this.ButtonOK_OpenRelation.Text = "OK";
            this.ButtonOK_OpenRelation.Click += new System.EventHandler(this.ButtonOK_OpenRelation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "Choose a relation to open:";
            // 
            // Form_OpenRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(371, 149);
            this.Controls.Add(this.ComboBox_OpenRelation);
            this.Controls.Add(this.ButtonCancel_OpenRelation);
            this.Controls.Add(this.ButtonOK_OpenRelation);
            this.Controls.Add(this.label1);
            this.Name = "Form_OpenRelation";
            this.Text = "Open Relation...";
            this.Load += new System.EventHandler(this.Form_OpenRelation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_OpenRelation;
        private DevExpress.XtraEditors.SimpleButton ButtonCancel_OpenRelation;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_OpenRelation;
        private System.Windows.Forms.Label label1;
    }
}