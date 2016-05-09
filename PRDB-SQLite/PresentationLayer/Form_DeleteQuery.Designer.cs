namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_DeleteQuery
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
            this.ButtonCancel_DeleteQuery = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_DeleteQuery = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ComboBox_DeleteQuery = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ButtonCancel_DeleteQuery
            // 
            this.ButtonCancel_DeleteQuery.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_DeleteQuery.Appearance.Options.UseFont = true;
            this.ButtonCancel_DeleteQuery.Location = new System.Drawing.Point(262, 89);
            this.ButtonCancel_DeleteQuery.Name = "ButtonCancel_DeleteQuery";
            this.ButtonCancel_DeleteQuery.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_DeleteQuery.TabIndex = 27;
            this.ButtonCancel_DeleteQuery.Text = "Cancel";
            this.ButtonCancel_DeleteQuery.Click += new System.EventHandler(this.ButtonCancel_DeleteQuery_Click);
            // 
            // ButtonOK_DeleteQuery
            // 
            this.ButtonOK_DeleteQuery.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_DeleteQuery.Appearance.Options.UseFont = true;
            this.ButtonOK_DeleteQuery.Location = new System.Drawing.Point(154, 89);
            this.ButtonOK_DeleteQuery.Name = "ButtonOK_DeleteQuery";
            this.ButtonOK_DeleteQuery.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_DeleteQuery.TabIndex = 26;
            this.ButtonOK_DeleteQuery.Text = "OK";
            this.ButtonOK_DeleteQuery.Click += new System.EventHandler(this.ButtonOK_DeleteQuery_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = "Choose a query to delete:";
            // 
            // ComboBox_DeleteQuery
            // 
            this.ComboBox_DeleteQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_DeleteQuery.FormattingEnabled = true;
            this.ComboBox_DeleteQuery.Location = new System.Drawing.Point(15, 45);
            this.ComboBox_DeleteQuery.Name = "ComboBox_DeleteQuery";
            this.ComboBox_DeleteQuery.Size = new System.Drawing.Size(334, 24);
            this.ComboBox_DeleteQuery.TabIndex = 28;
            // 
            // Form_DeleteQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(362, 149);
            this.Controls.Add(this.ComboBox_DeleteQuery);
            this.Controls.Add(this.ButtonCancel_DeleteQuery);
            this.Controls.Add(this.ButtonOK_DeleteQuery);
            this.Controls.Add(this.label1);
            this.Name = "Form_DeleteQuery";
            this.Text = "Delete Query...";
            this.Load += new System.EventHandler(this.Form_DeleteQuery_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton ButtonCancel_DeleteQuery;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_DeleteQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboBox_DeleteQuery;
    }
}