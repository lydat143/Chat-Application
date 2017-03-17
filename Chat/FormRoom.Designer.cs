namespace Chat
{
    partial class FormRoom
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTeen = new System.Windows.Forms.RadioButton();
            this.btnDate = new System.Windows.Forms.RadioButton();
            this.btnEva = new System.Windows.Forms.RadioButton();
            this.btnAdam = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTeen);
            this.groupBox1.Controls.Add(this.btnDate);
            this.groupBox1.Controls.Add(this.btnEva);
            this.groupBox1.Controls.Add(this.btnAdam);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 182);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List Room";
            // 
            // btnTeen
            // 
            this.btnTeen.AutoSize = true;
            this.btnTeen.Location = new System.Drawing.Point(6, 143);
            this.btnTeen.Name = "btnTeen";
            this.btnTeen.Size = new System.Drawing.Size(50, 17);
            this.btnTeen.TabIndex = 7;
            this.btnTeen.Text = "Teen";
            this.btnTeen.UseVisualStyleBackColor = true;
            this.btnTeen.CheckedChanged += new System.EventHandler(this.btnTeen_CheckedChanged);
            // 
            // btnDate
            // 
            this.btnDate.AutoSize = true;
            this.btnDate.Location = new System.Drawing.Point(6, 106);
            this.btnDate.Name = "btnDate";
            this.btnDate.Size = new System.Drawing.Size(60, 17);
            this.btnDate.TabIndex = 6;
            this.btnDate.Text = "Hẹn hò";
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.CheckedChanged += new System.EventHandler(this.btnDate_CheckedChanged);
            // 
            // btnEva
            // 
            this.btnEva.AutoSize = true;
            this.btnEva.Location = new System.Drawing.Point(6, 67);
            this.btnEva.Name = "btnEva";
            this.btnEva.Size = new System.Drawing.Size(78, 17);
            this.btnEva.TabIndex = 5;
            this.btnEva.Text = "Bí mật Eva";
            this.btnEva.UseVisualStyleBackColor = true;
            this.btnEva.CheckedChanged += new System.EventHandler(this.btnEva_CheckedChanged);
            // 
            // btnAdam
            // 
            this.btnAdam.AutoSize = true;
            this.btnAdam.Location = new System.Drawing.Point(6, 32);
            this.btnAdam.Name = "btnAdam";
            this.btnAdam.Size = new System.Drawing.Size(86, 17);
            this.btnAdam.TabIndex = 4;
            this.btnAdam.Text = "Bí mật Adam";
            this.btnAdam.UseVisualStyleBackColor = true;
            this.btnAdam.CheckedChanged += new System.EventHandler(this.btnAdam_CheckedChanged);
            // 
            // FormRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 208);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormRoom";
            this.Text = "FormRoom";
            this.Load += new System.EventHandler(this.FormRoom_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton btnTeen;
        private System.Windows.Forms.RadioButton btnDate;
        private System.Windows.Forms.RadioButton btnEva;
        private System.Windows.Forms.RadioButton btnAdam;

    }
}