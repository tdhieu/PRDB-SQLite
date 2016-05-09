namespace PRDB_SQLite.PresentationLayer
{
    partial class Form_InputValue
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CheckBox_UD = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinProb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaxProb = new System.Windows.Forms.TextBox();
            this.GridViewVaLue = new System.Windows.Forms.DataGridView();
            this.ButtonCancel_InputType = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK_InputValue = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonRemove_InputValue = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonAdd_InputValue = new DevExpress.XtraEditors.SimpleButton();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMinProb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaxProb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewVaLue)).BeginInit();
            this.SuspendLayout();
            // 
            // CheckBox_UD
            // 
            this.CheckBox_UD.AutoSize = true;
            this.CheckBox_UD.Location = new System.Drawing.Point(13, 13);
            this.CheckBox_UD.Name = "CheckBox_UD";
            this.CheckBox_UD.Size = new System.Drawing.Size(169, 20);
            this.CheckBox_UD.TabIndex = 1;
            this.CheckBox_UD.Text = "Xác suất phân bố chuẩn";
            this.CheckBox_UD.UseVisualStyleBackColor = true;
            this.CheckBox_UD.CheckedChanged += new System.EventHandler(this.CheckBox_UD_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sum of MinProb:";
            // 
            // txtMinProb
            // 
            this.txtMinProb.BackColor = System.Drawing.Color.White;
            this.txtMinProb.ForeColor = System.Drawing.Color.Black;
            this.txtMinProb.Location = new System.Drawing.Point(121, 43);
            this.txtMinProb.Name = "txtMinProb";
            this.txtMinProb.Size = new System.Drawing.Size(60, 22);
            this.txtMinProb.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sum of MaxProb:";
            // 
            // txtMaxProb
            // 
            this.txtMaxProb.BackColor = System.Drawing.Color.White;
            this.txtMaxProb.ForeColor = System.Drawing.Color.Black;
            this.txtMaxProb.Location = new System.Drawing.Point(312, 43);
            this.txtMaxProb.Name = "txtMaxProb";
            this.txtMaxProb.Size = new System.Drawing.Size(60, 22);
            this.txtMaxProb.TabIndex = 3;
            // 
            // GridViewVaLue
            // 
            this.GridViewVaLue.AllowUserToAddRows = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewVaLue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.GridViewVaLue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewVaLue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnValue,
            this.ColumnMinProb,
            this.ColumnMaxProb});
            this.GridViewVaLue.GridColor = System.Drawing.Color.Black;
            this.GridViewVaLue.Location = new System.Drawing.Point(-1, 82);
            this.GridViewVaLue.Name = "GridViewVaLue";
            this.GridViewVaLue.Size = new System.Drawing.Size(387, 271);
            this.GridViewVaLue.TabIndex = 5;
            this.GridViewVaLue.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewVaLue_CellEndEdit);
            this.GridViewVaLue.SelectionChanged += new System.EventHandler(this.GridViewVaLue_SelectionChanged);
            // 
            // ButtonCancel_InputType
            // 
            this.ButtonCancel_InputType.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel_InputType.Appearance.Options.UseFont = true;
            this.ButtonCancel_InputType.Location = new System.Drawing.Point(294, 369);
            this.ButtonCancel_InputType.Name = "ButtonCancel_InputType";
            this.ButtonCancel_InputType.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel_InputType.TabIndex = 9;
            this.ButtonCancel_InputType.Text = "Cancel";
            this.ButtonCancel_InputType.Click += new System.EventHandler(this.ButtonCancel_InputType_Click);
            // 
            // ButtonOK_InputValue
            // 
            this.ButtonOK_InputValue.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK_InputValue.Appearance.Options.UseFont = true;
            this.ButtonOK_InputValue.Location = new System.Drawing.Point(204, 369);
            this.ButtonOK_InputValue.Name = "ButtonOK_InputValue";
            this.ButtonOK_InputValue.Size = new System.Drawing.Size(75, 23);
            this.ButtonOK_InputValue.TabIndex = 8;
            this.ButtonOK_InputValue.Text = "OK";
            this.ButtonOK_InputValue.Click += new System.EventHandler(this.ButtonOK_InputValue_Click);
            // 
            // ButtonRemove_InputValue
            // 
            this.ButtonRemove_InputValue.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonRemove_InputValue.Appearance.Options.UseFont = true;
            this.ButtonRemove_InputValue.Location = new System.Drawing.Point(112, 369);
            this.ButtonRemove_InputValue.Name = "ButtonRemove_InputValue";
            this.ButtonRemove_InputValue.Size = new System.Drawing.Size(75, 23);
            this.ButtonRemove_InputValue.TabIndex = 7;
            this.ButtonRemove_InputValue.Text = "Remove";
            this.ButtonRemove_InputValue.Click += new System.EventHandler(this.ButtonRemove_InputValue_Click);
            // 
            // ButtonAdd_InputValue
            // 
            this.ButtonAdd_InputValue.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAdd_InputValue.Appearance.Options.UseFont = true;
            this.ButtonAdd_InputValue.Location = new System.Drawing.Point(20, 369);
            this.ButtonAdd_InputValue.Name = "ButtonAdd_InputValue";
            this.ButtonAdd_InputValue.Size = new System.Drawing.Size(75, 23);
            this.ButtonAdd_InputValue.TabIndex = 6;
            this.ButtonAdd_InputValue.Text = "Add";
            this.ButtonAdd_InputValue.Click += new System.EventHandler(this.ButtonAdd_InputValue_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(-1, 82);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtValue.Size = new System.Drawing.Size(387, 271);
            this.txtValue.TabIndex = 4;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // ColumnValue
            // 
            this.ColumnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnValue.DefaultCellStyle = dataGridViewCellStyle10;
            this.ColumnValue.HeaderText = "Value";
            this.ColumnValue.Name = "ColumnValue";
            // 
            // ColumnMinProb
            // 
            this.ColumnMinProb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnMinProb.DefaultCellStyle = dataGridViewCellStyle11;
            this.ColumnMinProb.HeaderText = "MinProb";
            this.ColumnMinProb.Name = "ColumnMinProb";
            // 
            // ColumnMaxProb
            // 
            this.ColumnMaxProb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnMaxProb.DefaultCellStyle = dataGridViewCellStyle12;
            this.ColumnMaxProb.HeaderText = "MaxProb";
            this.ColumnMaxProb.Name = "ColumnMaxProb";
            // 
            // Form_InputValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(386, 410);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.ButtonAdd_InputValue);
            this.Controls.Add(this.ButtonRemove_InputValue);
            this.Controls.Add(this.ButtonOK_InputValue);
            this.Controls.Add(this.ButtonCancel_InputType);
            this.Controls.Add(this.GridViewVaLue);
            this.Controls.Add(this.txtMaxProb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMinProb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CheckBox_UD);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_InputValue";
            this.Text = "Nhập giá trị thuộc tính";
            this.Load += new System.EventHandler(this.Form_InputValue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewVaLue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CheckBox_UD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMinProb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaxProb;
        private System.Windows.Forms.DataGridView GridViewVaLue;
        private DevExpress.XtraEditors.SimpleButton ButtonCancel_InputType;
        private DevExpress.XtraEditors.SimpleButton ButtonOK_InputValue;
        private DevExpress.XtraEditors.SimpleButton ButtonRemove_InputValue;
        private DevExpress.XtraEditors.SimpleButton ButtonAdd_InputValue;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMinProb;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaxProb;
    }
}