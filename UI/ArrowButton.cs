using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    class ArrowButton : Panel
    {
        public enum Ar
        {
            Left,
            Right
        }

        public ArrowButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            _arrow = Ar.Left;
        }

        private Ar _arrow;
        public Ar Arrow
        {
            get { return _arrow; }
            set
            {
                if (_arrow == value) return;
                _arrow = value;

                OnSizeChanged(EventArgs.Empty);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            using (var path = new GraphicsPath())
            {
                PointF[] fsx = null;
                var rect = ClientRectangle;
                rect.Inflate(-3, 0);
                float Del = ClientRectangle.Height / 2.8f;
                if (_arrow == Ar.Right)
                {
                    PointF[] fs = { new PointF((float)rect.Width, (float)rect.Height / 2f),
                        new PointF((float)rect.X, (float)rect.Height - Del),
                        new PointF((float)rect.X, (float)rect.Y + Del) };
                    fsx = fs;
                }
                if (_arrow == Ar.Left)
                {
                    PointF[] fs = { new PointF((float)rect.X, (float)rect.Height  / 2f),
                        new PointF((float)rect.Width, (float)rect.Y + Del),
                        new PointF((float)rect.Width, (float)rect.Height - Del) };
                    fsx = fs;
                }
                path.AddPolygon(fsx);
                Region = new Region(path);
            }
        }
    }
}
