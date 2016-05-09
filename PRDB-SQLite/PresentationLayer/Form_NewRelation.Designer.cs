namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_NewRelation
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
            this.ButtonCancel_NewRelation = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_NewRelation = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ComboBox_NewRelation = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRelationName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ButtonCancel_NewRelation
            // 
            this.ButtonCancel_NewRelation.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_NewRelation.Appearance.Options.UseFont = true;
            this.ButtonCancel_NewRelation.Location = new System.Drawing.Point(262, 155);
            this.ButtonCancel_NewRelation.Name = "ButtonCancel_NewRelation";
            this.ButtonCancel_NewRelation.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_NewRelation.TabIndex = 19;
            this.ButtonCancel_NewRelation.Text = "Cancel";
            this.ButtonCancel_NewRelation.Click += new System.EventHandler(this.ButtonCancel_NewTable_Click);
            // 
            // ButtonOK_NewRelation
            // 
            this.ButtonOK_NewRelation.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_NewRelation.Appearance.Options.UseFont = true;
            this.ButtonOK_NewRelation.Location = new System.Drawing.Point(154, 155);
            this.ButtonOK_NewRelation.Name = "ButtonOK_NewRelation";
            this.ButtonOK_NewRelation.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_NewRelation.TabIndex = 18;
            this.ButtonOK_NewRelation.Text = "OK";
            this.ButtonOK_NewRelation.Click += new System.EventHandler(this.ButtonOK_NewTable_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Choose the correlative schema:";
            // 
            // ComboBox_NewRelation
            // 
            this.ComboBox_NewRelation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_NewRelation.FormattingEnabled = true;
            this.ComboBox_NewRelation.Location = new System.Drawing.Point(15, 106);
            this.ComboBox_NewRelation.Name = "ComboBox_NewRelation";
            this.ComboBox_NewRelation.Size = new System.Drawing.Size(334, 24);
            this.ComboBox_NewRelation.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 16);
            this.label2.TabIndex = 54;
            this.label2.Text = "Enter relation name:";
            // 
            // txtRelationName
            // 
            this.txtRelationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRelationName.Location = new System.Drawing.Point(15, 47);
            this.txtRelationName.Name = "txtRelationName";
            this.txtRelationName.Size = new System.Drawing.Size(334, 22);
            this.txtRelationName.TabIndex = 55;
            // 
            // Form_NewRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(366, 203);
            this.Controls.Add(this.txtRelationName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboBox_NewRelation);
            this.Controls.Add(this.ButtonCancel_NewRelation);
            this.Controls.Add(this.ButtonOK_NewRelation);
            this.Controls.Add(this.label1);
            this.Name = "Form_NewRelation";
            this.Text = "Create New Relation...";
            this.Load += new System.EventHandler(this.Form_NewRelation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton ButtonCancel_NewRelation;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_NewRelation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboBox_NewRelation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRelationName;
    }
}