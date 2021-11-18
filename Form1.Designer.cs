namespace ScanCodeChecker
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageScanCode = new System.Windows.Forms.TabPage();
            this.tabPageVirtualKeyCode = new System.Windows.Forms.TabPage();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.lstKeyLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.tabControl1.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageScanCode);
            this.tabControl1.Controls.Add(this.tabPageVirtualKeyCode);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(801, 463);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageScanCode
            // 
            this.tabPageScanCode.Location = new System.Drawing.Point(4, 22);
            this.tabPageScanCode.Name = "tabPageScanCode";
            this.tabPageScanCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScanCode.Size = new System.Drawing.Size(793, 437);
            this.tabPageScanCode.TabIndex = 0;
            this.tabPageScanCode.Text = "ScanCode";
            this.tabPageScanCode.UseVisualStyleBackColor = true;
            // 
            // tabPageVirtualKeyCode
            // 
            this.tabPageVirtualKeyCode.Location = new System.Drawing.Point(4, 22);
            this.tabPageVirtualKeyCode.Name = "tabPageVirtualKeyCode";
            this.tabPageVirtualKeyCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVirtualKeyCode.Size = new System.Drawing.Size(793, 437);
            this.tabPageVirtualKeyCode.TabIndex = 1;
            this.tabPageVirtualKeyCode.Text = "VirtualKeyCode";
            this.tabPageVirtualKeyCode.UseVisualStyleBackColor = true;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.lstKeyLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(793, 437);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // lstKeyLog
            // 
            this.lstKeyLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lstKeyLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstKeyLog.Location = new System.Drawing.Point(0, 0);
            this.lstKeyLog.Name = "lstKeyLog";
            this.lstKeyLog.Size = new System.Drawing.Size(793, 437);
            this.lstKeyLog.TabIndex = 0;
            this.lstKeyLog.UseCompatibleStateImageBehavior = false;
            this.lstKeyLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 111;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Press/Release";
            this.columnHeader2.Width = 93;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Virtual key code";
            this.columnHeader3.Width = 140;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ScanCode";
            this.columnHeader4.Width = 74;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "flags";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 463);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "KeyCode Checker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.tabControl1.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageScanCode;
        private System.Windows.Forms.TabPage tabPageVirtualKeyCode;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.ListView lstKeyLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}

