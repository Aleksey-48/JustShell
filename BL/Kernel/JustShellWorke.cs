using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Kernel
{
    public class JustShellWorke
    {
        public bool VisibleFill { get; set; } = false;

        public int ClickFirstButton(int x, int y)
        {
            return x + y;
        }

        public void ClickSetupButton()
        {
            //
            VisibleFill = true;
        }
    }
}
