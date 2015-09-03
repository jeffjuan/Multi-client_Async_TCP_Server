namespace ChatRoomAsync_Server
{
    partial class ServerForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new System.Windows.Forms.Button();
            this.listBoxMsg = new System.Windows.Forms.ListBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonStart.Location = new System.Drawing.Point(12, 378);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(106, 28);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "啟動聊天室";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // listBoxMsg
            // 
            this.listBoxMsg.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBoxMsg.FormattingEnabled = true;
            this.listBoxMsg.ItemHeight = 16;
            this.listBoxMsg.Location = new System.Drawing.Point(12, 12);
            this.listBoxMsg.Name = "listBoxMsg";
            this.listBoxMsg.Size = new System.Drawing.Size(575, 340);
            this.listBoxMsg.TabIndex = 1;
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonClose.Location = new System.Drawing.Point(124, 378);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(97, 28);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "關閉聊天室";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 418);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.listBoxMsg);
            this.Controls.Add(this.buttonStart);
            this.Name = "ServerForm";
            this.Text = "聊天室 Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ListBox listBoxMsg;
        private System.Windows.Forms.Button buttonClose;
    }
}

