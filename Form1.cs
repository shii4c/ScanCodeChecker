using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScanCodeChecker
{
    public partial class Form1 : Form
    {
        private KeyboardHook keyboardHook_ = new KeyboardHook();
        private Label[] labels = new Label[256];
        private Label[] topHeaderLabels = new Label[16];
        private Label[] leftHeaderLabels = new Label[16];
        
        private Label[] vkLabels = new Label[256];
        private Label[] vkTopHeaderLabels = new Label[16];
        private Label[] vkLeftHeaderLabels = new Label[16];
        
        private Color normalStateColor = Color.White;
        private Color normalStateColor2  = Color.LightCyan;
        private Color pressedStateColor  = Color.Red;
        private Color pressedStateColor2 = Color.Orange;
        private Color headerColor        = Color.LightGray;
        Font font = new Font("MS UI Gothic", 8);

        public Form1()
        {
            InitializeComponent();


            for (int i = 0; i < 256; i++)
            {
                labels[i] = createLabel(GetKeyNameTextEx(i), i < 128 ? normalStateColor : normalStateColor2);
                labels[i].Tag = new KeyInfo(WinAPI.MapVirtualKey(i, 1), i);
                labels[i].MouseDown += new MouseEventHandler(Form1_MouseDown);
                labels[i].MouseUp += new MouseEventHandler(Form1_MouseUp);
                tabPageScanCode.Controls.Add(labels[i]);

                vkLabels[i] = createLabel(VirtualKeys.GetVirtualKeyName(i), normalStateColor);
                vkLabels[i].Tag = new KeyInfo(i, WinAPI.MapVirtualKey(i, 0));
                vkLabels[i].MouseDown += new MouseEventHandler(Form1_MouseDown);
                vkLabels[i].MouseUp += new MouseEventHandler(Form1_MouseUp);
                tabPageVirtualKeyCode.Controls.Add(vkLabels[i]);
            }
            for (int i = 0; i < 16; i++)
            {
                string hex = "0123456789ABCDEF".Substring(i, 1);
                topHeaderLabels[i] = createLabel(hex, headerColor);
                tabPageScanCode.Controls.Add(topHeaderLabels[i]);
                vkTopHeaderLabels[i] = createLabel(hex, headerColor);
                tabPageVirtualKeyCode.Controls.Add(vkTopHeaderLabels[i]);

                hex = "012345678".Substring(i % 8, 1);
                leftHeaderLabels[i] = createLabel(hex, headerColor);
                tabPageScanCode.Controls.Add(leftHeaderLabels[i]);
                vkLeftHeaderLabels[i] = createLabel(hex, headerColor);
                tabPageVirtualKeyCode.Controls.Add(vkLeftHeaderLabels[i]);
            }
            LayoutLabels();

            keyboardHook_.LowLevelKeyboardEvent += new KeyboardHook.LowLevelKeyboardProc(keyboardHook__LowLevelKeyboardEvent);
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            if (label == null) { return; }
            KeyInfo keyInfo = label.Tag as KeyInfo;
            if (keyInfo == null) { return; }
            WinAPI.keybd_event((byte)keyInfo.vkCode, (byte)keyInfo.scanCode, WinAPI.KEYEVENTF_KEYUP, 0);
        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            if (label == null) { return; }
            KeyInfo keyInfo = label.Tag as KeyInfo;
            if (keyInfo == null) { return; }
            WinAPI.keybd_event((byte)keyInfo.vkCode, (byte)keyInfo.scanCode, 0, 0);
        }

        private Label createLabel(string text, Color color)
        {
            Label label = new Label();
            label.Text = text;
            label.BackColor = color;
            label.BorderStyle = BorderStyle.Fixed3D;
            label.Font = font;
            label.TextAlign = ContentAlignment.MiddleCenter;
            return label;
        }

        private string GetKeyNameTextEx(int code)
        {
            int scanCode = code & 127;
            int extflag = ((code & 128) != 0) ? (1 << 24) : 0;

            StringBuilder sbText = new StringBuilder("                                                                 ");
            int result = WinAPI.GetKeyNameText((scanCode << 16) | extflag, sbText, sbText.Length);
            if (result == 1 && code >= 128) { return ""; }
            string text = sbText.ToString().TrimEnd();
            //System.Diagnostics.Debug.WriteLine("code=" + code + " retVal=" + result + " text=" + text);
            return text;
        }

        int keyboardHook__LowLevelKeyboardEvent(int nCode, int wParam, ref KeyboardHook.KBDLLHOOKSTRUCT kbdHookInfo)
        {
            System.Diagnostics.Debug.WriteLine("VkCode=" + kbdHookInfo.vkCode + " ScanCode=" + kbdHookInfo.scanCode + " flags=" + kbdHookInfo.flags + " extra=" + kbdHookInfo.dwExtraInfo + " msg=" + wParam + " nCode=" + nCode);
            int time = kbdHookInfo.time;
            int scanCode = kbdHookInfo.scanCode;
            int vkCode = kbdHookInfo.vkCode;
            int flags = kbdHookInfo.flags;
            if (scanCode < 0 || scanCode >= 128) { return 0; }
            BeginInvoke((MethodInvoker)delegate()
            {
                // ■ ScanCode
                int index = scanCode;
                if ((flags & 1) != 0) { index += 128; }

                Color color = index < 128 ? normalStateColor : normalStateColor2;
                if ((flags & 128) == 0)
                {
                    color = pressedStateColor;
                    if ((flags & 16) != 0) { color = pressedStateColor2; }
                }
                labels[index].BackColor = color;

                // ■ VirtualKeyCode
                color = normalStateColor;
                if ((flags & 128) == 0) { color = pressedStateColor; }
                if (vkCode >= 0 && vkCode < 256) { vkLabels[vkCode].BackColor = color; }

                outputLog(vkCode, scanCode, flags, labels[index].Text, time);
            });
            
            //System.Diagnostics.Debug.WriteLine("** " + kbdHookInfo.scanCode);
            return 0;
        }

        private void outputLog(int vkCode, int scanCode, int flags, string keyName, int time)
        {
            ListViewItem item = new ListViewItem();
            item.Text = time.ToString();
            item.SubItems.Add((flags & 128) == 0 ? "Press" : "Release");
            item.SubItems.Add(vkCode.ToString() + " (" + VirtualKeys.GetVirtualKeyName(vkCode) + ")");
            item.SubItems.Add(scanCode.ToString() + " (" + keyName + ")");
            item.SubItems.Add(flags.ToString());

            lstKeyLog.Items.Insert(0, item);
            if (lstKeyLog.Items.Count > 64)
            {
                for (int i = lstKeyLog.Items.Count - 1; i > 32; i--) { lstKeyLog.Items.RemoveAt(i); }
            }
        }

        private void LayoutLabels()
        {
            TabPage tabPage = tabControl1.SelectedTab;
            int w = tabPage.Width / 17;
            int h = tabPage.Height / 17;
            int index = 0;
            for (int j = 0; j < 16; j++)
            {
                int y = j * h;
                for (int i = 0; i < 16; i++)
                {
                    int x = i * w;
                    labels[index].SetBounds(x + w, y + h, w, h);
                    vkLabels[index].SetBounds(x + w, y + h, w, h);
                    index++;
                }

                topHeaderLabels[j].SetBounds(j * w + w, 0, w, h);
                leftHeaderLabels[j].SetBounds(0, y + h, w, h);
                vkTopHeaderLabels[j].SetBounds(j * w + w, 0, w, h);
                vkLeftHeaderLabels[j].SetBounds(0, y + h, w, h);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyboardHook_.Dispose();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            LayoutLabels();
        }

        #region クリックでフォームがアクティブにならないようにする
        private const int WS_EX_NOACTIVATE = 0x8000000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                if (!base.DesignMode)
                {
                    p.ExStyle = p.ExStyle | (WS_EX_NOACTIVATE);
                }
                return p;
            }
        }
        #endregion

        #region なぜかフォームの移動が見えなくなるので独自にドラッグ処理を実装
        private bool _MoveByDrag_isWindowMoving = false;
        private Point _MoveByDrag_prevMouseLocation;

        // WndProc のオーバーライド
        protected override void WndProc(ref Message m)
        {
            const int HTCAPTION = 2;
            const int WM_NCLBUTTONDOWN = 0xA1;
            const int WM_CREATE = 1;

            if (m.Msg == WM_NCLBUTTONDOWN)
            {
                if (m.WParam.ToInt32() == HTCAPTION)
                {
                    _MoveByDrag_isWindowMoving = true;
                    int lParam = m.LParam.ToInt32();
                    _MoveByDrag_prevMouseLocation = new Point(lParam & 0xFFFF, (lParam >> 16) & 0xFFFF);
                    Capture = true;
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            else if (m.Msg == WM_CREATE)
            {
                MoveByDrag_FormCreate();
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void MoveByDrag_FormCreate()
        {
            // イベントハンドラのバインド
            MouseMove += MoveByDrag_MouseMove;
            MouseUp += MoveByDrag_MouseUp;
            // 常に手前に表示
            TopMost = true;
        }

        private void MoveByDrag_MouseMove(object sender, MouseEventArgs e)
        {
            if (_MoveByDrag_isWindowMoving)
            {
                Point mp = PointToScreen(e.Location);
                int dx = mp.X - _MoveByDrag_prevMouseLocation.X;
                int dy = mp.Y - _MoveByDrag_prevMouseLocation.Y;
                Left += dx;
                Top += dy;

                _MoveByDrag_prevMouseLocation = mp;
            }
        }

        private void MoveByDrag_MouseUp(object sender, MouseEventArgs e)
        {
            if (_MoveByDrag_isWindowMoving)
            {
                _MoveByDrag_isWindowMoving = false;
                Capture = false;
            }
        }
        #endregion
    }

    class KeyInfo
    {
        public int vkCode;
        public int scanCode;
        public KeyInfo(int vkCode, int scanCode)
        {
            this.vkCode = vkCode;
            this.scanCode = scanCode;
        }
    }
}
