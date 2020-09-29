namespace RosinBankTestProject
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.connectBtn = new System.Windows.Forms.Button();
            this.connstrTxt = new System.Windows.Forms.TextBox();
            this.resultTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(496, 11);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(72, 21);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "Start";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // connstrTxt
            // 
            this.connstrTxt.Location = new System.Drawing.Point(12, 12);
            this.connstrTxt.Name = "connstrTxt";
            this.connstrTxt.Size = new System.Drawing.Size(478, 20);
            this.connstrTxt.TabIndex = 1;
            this.connstrTxt.Text = "Data Source=(local);Initial Catalog=master;Persist Security Info=True;User ID=sa;" +
    "Password=123";
            this.connstrTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConnstrTxt_KeyPress);
            // 
            // resultTxt
            // 
            this.resultTxt.Location = new System.Drawing.Point(12, 39);
            this.resultTxt.Multiline = true;
            this.resultTxt.Name = "resultTxt";
            this.resultTxt.Size = new System.Drawing.Size(556, 399);
            this.resultTxt.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 450);
            this.Controls.Add(this.resultTxt);
            this.Controls.Add(this.connstrTxt);
            this.Controls.Add(this.connectBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "RosinBank Test Poject";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.TextBox connstrTxt;
        private System.Windows.Forms.TextBox resultTxt;
    }
}

