using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchTimer
{
    public partial class MainWindow : Form
    {
        private readonly KeyHandler[] ghk = new KeyHandler[3];
        private readonly TimerManager timer = new TimerManager();
        public MainWindow()
        {
            InitializeComponent();
            GlobalSettings.Load();
            ghk[0] = new KeyHandler(GlobalSettings.pauseKeyCode, this, KeyType.Pause);
            ghk[0].Register();
            ghk[1] = new KeyHandler(GlobalSettings.resumeKeyCode, this, KeyType.Resume);
            ghk[1].Register();
            ghk[2] = new KeyHandler(GlobalSettings.resetKeyCode, this, KeyType.Reset);
            ghk[2].Register();
            UpdateLabels();
            labelStatus.Text = "Status: Paused";
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (KeyDetectForm kdf = new KeyDetectForm(ghk))
            {
                var result = kdf.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ghk[0].Unregiser();
                    ghk[0] = new KeyHandler(kdf.keyValue, this, KeyType.Pause);
                    ghk[0].Register();
                    GlobalSettings.pauseKeyName = "" + (Keys)kdf.keyValue;
                    GlobalSettings.pauseKeyCode = kdf.keyValue;
                    GlobalSettings.Save();
                    UpdateLabels();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (KeyDetectForm kdf = new KeyDetectForm(ghk))
            {
                var result = kdf.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ghk[1].Unregiser();
                    ghk[1] = new KeyHandler(kdf.keyValue, this, KeyType.Resume);
                    ghk[1].Register();
                    GlobalSettings.resumeKeyName = "" + (Keys)kdf.keyValue;
                    GlobalSettings.resumeKeyCode = kdf.keyValue;
                    GlobalSettings.Save();
                    UpdateLabels();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (KeyDetectForm kdf = new KeyDetectForm(ghk))
            {
                var result = kdf.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ghk[2].Unregiser();
                    ghk[2] = new KeyHandler(kdf.keyValue, this, KeyType.Reset);
                    ghk[2].Register();
                    GlobalSettings.resetKeyName = "" + (Keys)kdf.keyValue;
                    GlobalSettings.resetKeyCode = kdf.keyValue;
                    GlobalSettings.Save();
                    UpdateLabels();
                }
            }
        }

        private void HandleHotkey(Message m)
        {
            
            for (int i = 0; i < ghk.Length; i++)
            {
                
                if (m.WParam.ToString() == ghk[i].GetHashCode().ToString())
                {
                    switch (ghk[i].GetKeyType())
                    {
                        case KeyType.Pause:
                            labelStatus.Text = "Status: Paused";
                            timer.Pause();
                            break;
                        case KeyType.Resume:
                            labelStatus.Text = "Status: Resumed";
                            timer.Resume();
                            break;
                        case KeyType.Reset:
                            labelStatus.Text = "Status: Paused";
                            timer.Reset();
                            break;
                        default:
                            break;
                    }
                    return;
                }
            }
        }
        private void UpdateLabels()
        {
            label1.Text = $"Pause: {GlobalSettings.pauseKeyName}";
            label2.Text = $"Resume: {GlobalSettings.resumeKeyName}";
            label3.Text = $"Reset: {GlobalSettings.resetKeyName}";
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == GlobalSettings.WM_HOTKEY_MSG_ID)
            {
                HandleHotkey(m);
            }
            base.WndProc(ref m);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer.Update();
            timerLabel.Text = timer.GetTimer();
        }
    }
}
