namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_Table
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.ButtonCancel_NewTable = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_NewTable = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập tên bảng:";
            // 
            // txtTableName
            // 
            this.txtTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableName.Location = new System.Drawing.Point(117, 27);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(232, 22);
            this.txtTableName.TabIndex = 1;
            // 
            // ButtonCancel_NewTable
            // 
            this.ButtonCancel_NewTable.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_NewTable.Appearance.Options.UseFont = true;
            this.ButtonCancel_NewTable.Location = new System.Drawing.Point(205, 87);
            this.ButtonCancel_NewTable.Name = "ButtonCancel_NewTable";
            this.ButtonCancel_NewTable.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_NewTable.TabIndex = 7;
            this.ButtonCancel_NewTable.Text = "Cancel";
            this.ButtonCancel_NewTable.Click += new System.EventHandler(this.ButtonCancel_NewTable_Click);
            // 
            // ButtonOK_NewTable
            // 
            this.ButtonOK_NewTable.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_NewTable.Appearance.Options.UseFont = true;
            this.ButtonOK_NewTable.Location = new System.Drawing.Point(80, 87);
            this.ButtonOK_NewTable.Name = "ButtonOK_NewTable";
            this.ButtonOK_NewTable.Size = new System.Drawing.Size(75, 23);
            this.ButtonOK_NewTable.TabIndex = 6;
            this.ButtonOK_NewTable.Text = "OK";
            this.ButtonOK_NewTable.Click += new System.EventHandler(this.ButtonOK_NewTable_Click);
            // 
            // Form_Table
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 137);
            this.Controls.Add(this.ButtonCancel_NewTable);
            this.Controls.Add(this.ButtonOK_NewTable);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.label1);
            this.Name = "Form_Table";
            this.Text = "Tạo Bảng Mới...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTableName;
        private DevExpress.XtraEditors.SimpleButton ButtonCancel_NewTable;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_NewTable;
    }
}