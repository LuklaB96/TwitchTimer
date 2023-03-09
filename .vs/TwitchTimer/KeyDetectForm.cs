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
    public partial class KeyDetectForm : Form
    {
        public KeyHandler[] keyHandler = null;
        public int keyValue;

        public KeyDetectForm(KeyHandler[] keyHandler)
        {
            InitializeComponent();
            this.keyHandler = keyHandler;
        }

        private void KeyDetectForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 27)
            {
                keyValue = e.KeyValue;
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.Cancel;
        }
    }
}