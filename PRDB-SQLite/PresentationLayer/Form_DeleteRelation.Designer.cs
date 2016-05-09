namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_DeleteRelation
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
            this.ComboBox_DeleteRelation = new System.Windows.Forms.ComboBox();
            this.ButtonCancel_DeleteRelation = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_DeleteRelation = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox_DeleteRelation
            // 
            this.ComboBox_DeleteRelation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_DeleteRelation.FormattingEnabled = true;
            this.ComboBox_DeleteRelation.Location = new System.Drawing.Point(15, 48);
            this.ComboBox_DeleteRelation.Name = "ComboBox_DeleteRelation";
            this.ComboBox_DeleteRelation.Size = new System.Drawing.Size(334, 24);
            this.ComboBox_DeleteRelation.TabIndex = 32;
            // 
            // ButtonCancel_DeleteRelation
            // 
            this.ButtonCancel_DeleteRelation.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_DeleteRelation.Appearance.Options.UseFont = true;
            this.ButtonCancel_DeleteRelation.Location = new System.Drawing.Point(262, 92);
            this.ButtonCancel_DeleteRelation.Name = "ButtonCancel_DeleteRelation";
            this.ButtonCancel_DeleteRelation.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_DeleteRelation.TabIndex = 31;
            this.ButtonCancel_DeleteRelation.Text = "Cancel";
            this.ButtonCancel_DeleteRelation.Click += new System.EventHandler(this.ButtonCancel_DeleteRelation_Click);
            // 
            // ButtonOK_DeleteRelation
            // 
            this.ButtonOK_DeleteRelation.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_DeleteRelation.Appearance.Options.UseFont = true;
            this.ButtonOK_DeleteRelation.Location = new System.Drawing.Point(154, 92);
            this.ButtonOK_DeleteRelation.Name = "ButtonOK_DeleteRelation";
            this.ButtonOK_DeleteRelation.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_DeleteRelation.TabIndex = 30;
            this.ButtonOK_DeleteRelation.Text = "OK";
            this.ButtonOK_DeleteRelation.Click += new System.EventHandler(this.ButtonOK_DeleteRelation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 16);
            this.label1.TabIndex = 29;
            this.label1.Text = "Choose a relation to delete:";
            // 
            // Form_DeleteRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(365, 140);
            this.Controls.Add(this.ComboBox_DeleteRelation);
            this.Controls.Add(this.ButtonCancel_DeleteRelation);
            this.Controls.Add(this.ButtonOK_DeleteRelation);
            this.Controls.Add(this.label1);
            this.Name = "Form_DeleteRelation";
            this.Text = "Delete Relation...";
            this.Load += new System.EventHandler(this.Form_DeleteRelation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox_DeleteRelation;
        private DevExpress.XtraEditors.SimpleButton ButtonCancel_DeleteRelation;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_DeleteRelation;
        private System.Windows.Forms.Label label1;
    }
}