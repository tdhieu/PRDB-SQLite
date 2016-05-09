namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_NewQuery
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
            this.ButtonCancel_NewQuery = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_NewQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtQueryName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonCancel_NewQuery
            // 
            this.ButtonCancel_NewQuery.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_NewQuery.Appearance.Options.UseFont = true;
            this.ButtonCancel_NewQuery.Location = new System.Drawing.Point(269, 99);
            this.ButtonCancel_NewQuery.Name = "ButtonCancel_NewQuery";
            this.ButtonCancel_NewQuery.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_NewQuery.TabIndex = 19;
            this.ButtonCancel_NewQuery.Text = "Cancel";
            this.ButtonCancel_NewQuery.Click += new System.EventHandler(this.ButtonCancel_NewQuery_Click);
            // 
            // ButtonOK_NewQuery
            // 
            this.ButtonOK_NewQuery.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_NewQuery.Appearance.Options.UseFont = true;
            this.ButtonOK_NewQuery.Location = new System.Drawing.Point(161, 99);
            this.ButtonOK_NewQuery.Name = "ButtonOK_NewQuery";
            this.ButtonOK_NewQuery.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_NewQuery.TabIndex = 18;
            this.ButtonOK_NewQuery.Text = "OK";
            this.ButtonOK_NewQuery.Click += new System.EventHandler(this.ButtonOK_NewQuery_Click);
            // 
            // txtQueryName
            // 
            this.txtQueryName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQueryName.Location = new System.Drawing.Point(22, 51);
            this.txtQueryName.Name = "txtQueryName";
            this.txtQueryName.Size = new System.Drawing.Size(334, 22);
            this.txtQueryName.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Enter a  name for new query:";
            // 
            // Form_NewQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(378, 158);
            this.Controls.Add(this.ButtonCancel_NewQuery);
            this.Controls.Add(this.ButtonOK_NewQuery);
            this.Controls.Add(this.txtQueryName);
            this.Controls.Add(this.label1);
            this.Name = "Form_NewQuery";
            this.Text = "Create new query...";
            this.Load += new System.EventHandler(this.Form_NewQuery_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton ButtonCancel_NewQuery;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_NewQuery;
        private System.Windows.Forms.TextBox txtQueryName;
        private System.Windows.Forms.Label label1;
    }
}