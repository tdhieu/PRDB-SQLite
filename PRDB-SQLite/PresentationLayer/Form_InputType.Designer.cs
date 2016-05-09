namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_InputType
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
            this.ComboBox_DataType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtListValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserDefined = new System.Windows.Forms.TextBox();
            this.ButtonOK_InputType = new System.Windows.Forms.Button();
            this.ButtonCancel_InputType = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Type:";
            // 
            // ComboBox_DataType
            // 
            this.ComboBox_DataType.BackColor = System.Drawing.Color.White;
            this.ComboBox_DataType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_DataType.ForeColor = System.Drawing.Color.Black;
            this.ComboBox_DataType.FormattingEnabled = true;
            this.ComboBox_DataType.Items.AddRange(new object[] {
            "Int16",
            "Int32",
            "Int64",
            "Byte",
            "String",
            "Single",
            "Double",
            "Boolean",
            "Decimal",
            "DateTime",
            "Binary",
            "Currency",
            "UserDefined"});
            this.ComboBox_DataType.Location = new System.Drawing.Point(153, 21);
            this.ComboBox_DataType.Name = "ComboBox_DataType";
            this.ComboBox_DataType.Size = new System.Drawing.Size(184, 24);
            this.ComboBox_DataType.TabIndex = 1;
            this.ComboBox_DataType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_DataType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "List of Value Type:";
            // 
            // txtListValue
            // 
            this.txtListValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtListValue.Location = new System.Drawing.Point(15, 122);
            this.txtListValue.Multiline = true;
            this.txtListValue.Name = "txtListValue";
            this.txtListValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtListValue.Size = new System.Drawing.Size(322, 179);
            this.txtListValue.TabIndex = 3;
            this.txtListValue.TextChanged += new System.EventHandler(this.txtListValue_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Type Name:";
            // 
            // txtUserDefined
            // 
            this.txtUserDefined.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserDefined.Location = new System.Drawing.Point(153, 53);
            this.txtUserDefined.Name = "txtUserDefined";
            this.txtUserDefined.Size = new System.Drawing.Size(184, 22);
            this.txtUserDefined.TabIndex = 2;
            // 
            // ButtonOK_InputType
            // 
            this.ButtonOK_InputType.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ButtonOK_InputType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonOK_InputType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_InputType.Location = new System.Drawing.Point(59, 322);
            this.ButtonOK_InputType.Name = "ButtonOK_InputType";
            this.ButtonOK_InputType.Size = new System.Drawing.Size(97, 35);
            this.ButtonOK_InputType.TabIndex = 4;
            this.ButtonOK_InputType.Text = "OK";
            this.ButtonOK_InputType.UseVisualStyleBackColor = false;
            this.ButtonOK_InputType.Click += new System.EventHandler(this.ButtonOK_InputType_Click);
            // 
            // ButtonCancel_InputType
            // 
            this.ButtonCancel_InputType.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonCancel_InputType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonCancel_InputType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_InputType.Location = new System.Drawing.Point(182, 322);
            this.ButtonCancel_InputType.Name = "ButtonCancel_InputType";
            this.ButtonCancel_InputType.Size = new System.Drawing.Size(100, 35);
            this.ButtonCancel_InputType.TabIndex = 5;
            this.ButtonCancel_InputType.Text = "Cancel";
            this.ButtonCancel_InputType.UseVisualStyleBackColor = false;
            this.ButtonCancel_InputType.Click += new System.EventHandler(this.ButtonCancel_InputType_Click);
            // 
            // Form_InputType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(350, 369);
            this.Controls.Add(this.ButtonCancel_InputType);
            this.Controls.Add(this.ButtonOK_InputType);
            this.Controls.Add(this.txtUserDefined);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtListValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboBox_DataType);
            this.Controls.Add(this.label1);
            this.Name = "Form_InputType";
            this.Text = "Choose Data Type...";
            this.Load += new System.EventHandler(this.Form_InputType_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboBox_DataType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtListValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserDefined;
        private System.Windows.Forms.Button ButtonOK_InputType;
        private System.Windows.Forms.Button ButtonCancel_InputType;
    }
}