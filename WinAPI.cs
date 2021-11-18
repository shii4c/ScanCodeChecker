using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;


namespace ScanCodeChecker
{
    public class WinAPI
    {
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetKeyNameText(int lParam, StringBuilder sbString, int nSize);

        public const int INPUT_KEYBOARD = 1;
        public const int KEYEVENTF_EXTENDEDKEY = 0x1;
        public const int KEYEVENTF_KEYUP = 0x2;
        public const int KEYEVENTF_UNICODE = 0x4;

        // マウスイベント(mouse_eventの引数と同様のデータ)
        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public int dwExtraInfo;
        };

        // キーボードイベント(keybd_eventの引数と同様のデータ)
        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public int dwExtraInfo;
        };

        // ハードウェアイベント
        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        };

        // 各種イベント(SendInputの引数データ)
        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        };

        [DllImport("user32.dll")]
        public extern static void SendInput(int nInputs, INPUT[] pInputs, int cbsize);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        /// <summary>
        /// キーストロークを合成します。システムは、合成されたキーストロークから、 または メッセージを生成します。
        /// </summary>
        /// <param name="bVk">仮想キーコードを指定します。このコードは、1～254 の範囲内の値でなければなりません。</param>
        /// <param name="bScan">ハードウェアスキャンコード</param>
        /// <param name="dwFlags">
        /// 関数の動作を指定します。次のフラグを任意に組み合わせて指定します。<br/>
        ///  KEYEVENTF_EXTENDEDKEY: このフラグをセットすると、スキャンコードにプリフィックスバイト 0xE0（224）を追加します。<br/>
        ///  KEYEVENTF_KEYUP: このフラグをセットすると、キーを離す操作になります。セットしない場合、キーを押す操作になります。
        /// </param>
        /// <param name="dwExtraInfo">キーストロークに関連する 32 ビットの追加情報を指定します。</param>
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        
        /// <summary>
        /// 仮想キーコードをスキャンコード、または文字の値（ASCII 値）へ変換します。また、スキャンコードを仮想コードへ変換することもできます。
        /// </summary>
        /// <param name="code">キーの仮想キーコード、またはスキャンコードを指定します。この値の解釈方法は、uMapType パラメータの値に依存します。</param>
        /// <param name="mapType">
        /// 実行したい変換の種類を指定します。このパラメータの値に基づいて、uCode パラメータの値は次のように解釈されます。<br/>
        ///  0: uCode は仮想キーコードであり、スキャンコードへ変換されます。左右のキーを区別しない仮想キーコードのときは、関数は左側のスキャンコードを返します。スキャンコードに変換されないときは、関数は 0 を返します。<br/>
        ///  1: uCode はスキャンコードであり、仮想キーコードへ変換されます。この仮想キーコードは、左右のキーを区別します。変換されないときは、関数は 0 を返します。<br/>
        ///  2: uCode は仮想キーコードであり、戻り値の下位ワードにシフトなしの ASCII 値が格納されます。デッドキー（ 分音符号）は、戻り値の上位ビットをセットすることにより明示されます。変換されないときは、関数は 0 を返します。<br/>
        ///  3: uCode はスキャンコードであり、左右のキーを区別する仮想キーコードへ変換されます。変換されないときは、関数は 0 を返します。
        /// </param>
        /// <returns>uCode パラメータと uMapType パラメータの値に従って、スキャンコード、仮想キーコード、ASCII 値のいずれかが返ります。変換されないときは、0 が返ります。</returns>
        [DllImport("user32.dll")]
        public static extern int MapVirtualKey(int code, int mapType);
    }
}
