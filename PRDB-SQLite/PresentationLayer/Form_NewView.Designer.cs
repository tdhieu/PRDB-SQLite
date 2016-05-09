namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_NewView
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
            this.ButtonCancel_NewView = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_NewView = new DevExpress.XtraEditors.SimpleButton();
            this.txtViewName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonCancel_NewView
            // 
            this.ButtonCancel_NewView.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_NewView.Appearance.Options.UseFont = true;
            this.ButtonCancel_NewView.Location = new System.Drawing.Point(262, 92);
            this.ButtonCancel_NewView.Name = "ButtonCancel_NewView";
            this.ButtonCancel_NewView.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel_NewView.TabIndex = 23;
            this.ButtonCancel_NewView.Text = "Cancel";
            this.ButtonCancel_NewView.Click += new System.EventHandler(this.ButtonCancel_NewView_Click);
            // 
            // ButtonOK_NewView
            // 
            this.ButtonOK_NewView.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_NewView.Appearance.Options.UseFont = true;
            this.ButtonOK_NewView.Location = new System.Drawing.Point(154, 92);
            this.ButtonOK_NewView.Name = "ButtonOK_NewView";
            this.ButtonOK_NewView.Size = new System.Drawing.Size(87, 23);
            this.ButtonOK_NewView.TabIndex = 22;
            this.ButtonOK_NewView.Text = "OK";
            this.ButtonOK_NewView.Click += new System.EventHandler(this.ButtonOK_NewView_Click);
            // 
            // txtViewName
            // 
            this.txtViewName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewName.Location = new System.Drawing.Point(15, 48);
            this.txtViewName.Name = "txtViewName";
            this.txtViewName.Size = new System.Drawing.Size(334, 22);
            this.txtViewName.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Enter a  name for new view:";
            // 
            // Form_NewView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(363, 154);
            this.Controls.Add(this.ButtonCancel_NewView);
            this.Controls.Add(this.ButtonOK_NewView);
            this.Controls.Add(this.txtViewName);
            this.Controls.Add(this.label1);
            this.Name = "Form_NewView";
            this.Text = "Create New View...";
            this.Load += new System.EventHandler(this.Form_NewView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton ButtonCancel_NewView;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_NewView;
        private System.Windows.Forms.TextBox txtViewName;
        private System.Windows.Forms.Label label1;
    }
}