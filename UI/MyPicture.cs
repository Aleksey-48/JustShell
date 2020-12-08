using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public class MyPic : PictureBox
    {
        public bool IsEnter = false;
        public bool IsPressed = false;
        public Image GoImage;
        //public Rectangle IsPanel;
        public string IsText;
        public Color TextColor = Color.FromArgb(230, 230, 230);
        public float Proportion;

        public MyPic()
        {
            AutoSize = false;
            BackColor = Color.Transparent;
            Dock = DockStyle.Left;
            SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
