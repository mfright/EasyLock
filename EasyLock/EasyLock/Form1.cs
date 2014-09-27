using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace AutoLockCanceller
{
    public partial class Form1 : Form
    {
        
        // kernel32 APIを使用する。
        [DllImport("kernel32.dll")]
        extern static ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        //引数のExecutionState列挙体
        [FlagsAttribute]
        public enum ExecutionState : uint
        {
            // Return value of failed.
            Null = 0,

            // Anti standby.
            SystemRequired = 1,

            // Anti Display-off
            DisplayRequired = 2,

            // continuous
            Continuous = 0x80000000,
        }




        public Form1()
        {
            InitializeComponent();

            this.Visible = false;

        }

        // Quitを押したとき
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit application.
            Application.Exit();
        }

        


        // 定期的に、画面ロックされないようアクションする
        private void timerMouseCheck_Tick(object sender, EventArgs e)
        {
            
            // DisplayRequiredをSetThreadExecutionStateへ送信.
            ExecutionState es = new ExecutionState();
            es = ExecutionState.DisplayRequired;
            SetThreadExecutionState(es);
                

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
        }
    }
}
