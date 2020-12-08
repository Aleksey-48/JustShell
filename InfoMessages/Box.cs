using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Messenger
{
    public partial class Box : Form
    {
        #region -- Тень от формы --
        private bool m_aeroEnabled = false;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return enabled == 1;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);

                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };

                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }

            base.WndProc(ref m);

        }
        #endregion

        #region==Свойства==
        private string headerText;
        public string HeaderText
        {
            get => headerText;
            set
            {
                if (value.Length > 65)
                {
                    value = value.Substring(0, 65);
                }
                value = value.Replace("\n", " ");
                headerText = value;
            }
        }

        private string storyText;
        public string StoryText
        {
            get => storyText;
            set
            {
                if (value.Length > 300)
                {
                    value = value.Substring(0, 300) + "...";
                }
                storyText = value;
            }
        }

        public int WindowHeight { get; set; } = HeighBox;

        private int windowWidth;
        public int WindowWidth
        {
            get => windowWidth;
            set
            {
                if (value > 900) value = 900;
                if (value < 300) value = 300;
                windowWidth = value;
            }
        }

        #endregion

        #region==Поля==
        const int HeighBox = 185;
        protected Button buttonNo = new Button();
        protected Button buttonOk = new Button();
        protected int HeightWord = 0;
        protected int PanelWidth;
        protected int LabelX;
        protected int PanelRight;
        readonly Color HeaderColor = ParamsBlocks.ColorHeader;
        readonly Color HeaderColorText = Color.FromArgb(240,240,240);
        readonly Color WindowColor = ParamsBlocks.ColorPanel;
        readonly Color BottomColor = ParamsBlocks.ColorBottom;
        /*public*/ readonly Color ColorStory = ParamsBlocks.ColorText;
        protected int BottomHeight = 46;
        /*public*/ int HeaderHeight = 32;
        readonly double KofX = 1.4;

        readonly StringFormat SF = new StringFormat();
        readonly Font FontHeader = new Font(ParamsBlocks.NameHeader, ParamsBlocks.SizeText + 8f, FontStyle.Regular, GraphicsUnit.Pixel, 204);//Font("Arial", 11F, FontStyle.Regular);
        readonly Font FontStory = new Font(ParamsBlocks.NameText, ParamsBlocks.SizeText, FontStyle.Regular, GraphicsUnit.Pixel, 204);
        readonly Font FontButton = new Font(ParamsBlocks.NameHeader, ParamsBlocks.SizeText, FontStyle.Regular, GraphicsUnit.Pixel, 204);
        readonly Pen WhitePen = new Pen(Color.White) { Width = 1.55F };
        readonly Pen GrayPen = new Pen(Color.WhiteSmoke) { Width = 1.4F };
        readonly Size IconSize = new Size(72, 72);
        readonly int ButtonWidht = 100;
        int ButtonHeight = 28;
        int InterBottom;
        readonly int InterBetween = 7;
        bool MousePressed = false; // Кнопка мыши нажата
        Point clickPosition; // Начальная позиция курсора в момент клика
        Point moveStartPosition; // Начальная позиция формы в момент клика
        Rectangle rectBtnClose = new Rectangle();
        bool btnCloseHovered = false;
        #endregion

        public Box(string header, string story, int width)
        {
            InitializeComponent();
            HeaderText = header;
            StoryText = story;
            WindowWidth = width;
            Width = WindowWidth;
            HandleCreated += Box_HandleCreated;
        }

        private void Box_HandleCreated(object sender, EventArgs e)
        {
            BottomHeight += ParamsBlocks.NumberShell * 4;
            ButtonHeight += ParamsBlocks.NumberShell * 3;
            HeaderHeight += ParamsBlocks.NumberShell * 4;
            SF.Alignment = StringAlignment.Near;
            SF.LineAlignment = StringAlignment.Center;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = WindowColor;
            //Width = 310;
            Height = WindowHeight;
            AutoScaleMode = AutoScaleMode.None;
            InterBottom = (BottomHeight - ButtonHeight) / 2;

            SetDoubleBuffered(this);
            Paint += Box_Paint;
            MouseDown += Box_MouseDown;
            MouseUp += Box_MouseUp;
            MouseMove += Box_MouseMove;
            MouseLeave += Box_MouseLeave;
            Load += Box_Load;
        }

        private void Box_Load(object sender, EventArgs e)
        {
            Blocks.HandleWindow = Handle;

            Size len = TextRenderer.MeasureText(HeaderText, FontHeader);
            string str = HeaderText;
            int i = 1;
            while (len.Width > (int)(Width - 20 - HeaderHeight * KofX))
            {
                str = HeaderText.Substring(0, HeaderText.Length - i);
                i++;
                len = TextRenderer.MeasureText(str, FontHeader);
            }
            if (HeaderText.Length != str.Length) HeaderText = str + "...";

            if (SetBox.ContentHardTexts == null) BuildStory();
            else BuildPanelStorys();                
            
            if (SetBox.Button != Buttons.None) BuildButtons();
            
            if (WindowHeight > HeighBox)
            {
                Top -= (WindowHeight - HeighBox) / 2;
            }

            if (SetBox.Download)
            {
                BuildProgressBar();

                if (SetBox.MiliSec != 0)
                {
                    Timer timer1 = new Timer
                    {
                        Interval = SetBox.MiliSec,
                        Enabled = true
                    };
                    timer1.Tick += Timer1_Tick;
                }
                else
                {
                    Timer timer1 = new Timer
                    {
                        Interval = 20000,
                        Enabled = true
                    };
                    timer1.Tick += Timer1_Tick;
                }
            }
        }

        // Закрытие окна сообщения по таймеру через 20 сек., если не закроется раньше
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

        // Построение ProgressBar
        private void BuildProgressBar()
        {
            ProgressBar progress = new ProgressBar
            {
                Height = 20,
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30
            };
            progress.Width = Width - 36;
            progress.Location = new Point((Width - progress.Width) / 2,
                WindowHeight - BottomHeight + (BottomHeight - progress.Height) / 2);
            Controls.Add(progress);
        }

        #region ==Движение окна Сообщения и реакция на нажатие "Х"
        private void Box_MouseLeave(object sender, EventArgs e)
        {
            btnCloseHovered = false;
            Invalidate();
        }

        private void Box_MouseMove(object sender, MouseEventArgs e)
        {
            if (MousePressed && e.Button == MouseButtons.Left)
            {
                // Moving
                Size frmOffset = new Size(Point.Subtract(Cursor.Position, new Size(clickPosition)));
                Location = Point.Add(moveStartPosition, frmOffset);
            }
            else
            {
                if (!SetBox.Cross) return;
                // Close Button
                if (rectBtnClose.Contains(e.Location))
                {
                    if (btnCloseHovered == false)
                    {
                        btnCloseHovered = true;
                        Invalidate();
                    }
                }
                else
                {
                    if (btnCloseHovered == true)
                    {
                        btnCloseHovered = false;
                        Invalidate();
                    }
                }
            }
        }

        private void Box_MouseUp(object sender, MouseEventArgs e)
        {
            MousePressed = false;
            if (e.Button == MouseButtons.Left)
            {
                if (!SetBox.Cross) return;
                // Close Button Click
                if (rectBtnClose.Contains(e.Location))
                {
                    Blocks.Reply = 0;
                    Close();
                }
            }
        }

        private void Box_MouseDown(object sender, MouseEventArgs e)
        {
            void CloseDown()
            {
                MousePressed = true;
                clickPosition = Cursor.Position;
                moveStartPosition = Location;
            }

            if (!SetBox.Cross)
            {
                if (e.Location.Y <= HeaderHeight) CloseDown();
            }
            else
            {
                if (e.Location.Y <= HeaderHeight
                && !rectBtnClose.Contains(e.Location)) CloseDown();
            }
        }
        #endregion

        #region==Построение кнопок==
        /// <summary>
        /// Построение кнопок
        /// </summary>
        private void BuildButtons()
        {
            if (SetBox.Download) return;

            int positionX;

            buttonOk.Size = new Size(ButtonWidht, ButtonHeight);
            buttonOk.Font = FontButton;
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.ForeColor = Color.Black;
            buttonOk.TextAlign = ContentAlignment.MiddleCenter;

            Controls.Add(buttonOk);
            buttonOk.Text = "OK";
            positionX = Width / 2 - buttonOk.Width / 2;
            buttonOk.Location = new Point(positionX, Height - buttonOk.Height - InterBottom);
            buttonOk.Click += ButtonOk_Click;

            if (SetBox.Button == Buttons.YesNo)
            {
                buttonNo.Size = new Size(ButtonWidht, ButtonHeight);
                buttonNo.Font = FontButton;
                buttonNo.UseVisualStyleBackColor = true;
                buttonNo.ForeColor = Color.Black;
                buttonNo.TextAlign = ContentAlignment.MiddleCenter;
                //);
                Controls.Add(buttonNo);
                buttonNo.Text = "Нет";
                buttonNo.Click += ButtonNo_Click;
                buttonOk.Text = "Да";
                positionX = (Width - buttonNo.Width - buttonOk.Width - InterBetween) / 2;
                buttonOk.Location = new Point(positionX, WindowHeight - buttonOk.Height - InterBottom);
                positionX = buttonOk.Left + buttonOk.Width + InterBetween;
                buttonNo.Location = new Point(positionX, WindowHeight - buttonNo.Height - InterBottom);
            }
        }

        private void ButtonNo_Click(object sender, System.EventArgs e)
        {
            Blocks.Reply = 2;
            Close();
        }

        private void ButtonOk_Click(object sender, System.EventArgs e)
        {
            Blocks.Reply = 1;
            Close();
        }
        #endregion

        readonly int LeftIndent = 0;
        readonly int IconIndent = 20;
        readonly int NoIconIndent = 60;

        #region==Построение текста==
        private void BuildStory()
        {
            if (SetBox.Icon == Icones.None)
            {
                LabelX = NoIconIndent;
                PanelRight = 15;
                PanelWidth = Width - (LabelX + PanelRight);
            }
            else
            {
                LabelX = LeftIndent + IconIndent + IconSize.Width + IconIndent;
                PanelRight = 5;
                PanelWidth = Width - (LabelX + PanelRight);
            }

            if (SetBox.Download && StoryText=="") StoryText = "Loading process...";
            Label story = new Label
            {
                MaximumSize = new Size(PanelWidth, 0),
                AutoSize = true,
                Text = StoryText,
                BackColor = Color.Transparent,
                Font = FontStory,
                ForeColor = ColorStory,
                TextAlign = ContentAlignment.MiddleLeft
            };
            Controls.Add(story);
            if (StoryText == "") { story.Visible = false; story.Height = 0; }
            else HeightWord = story.Height;

            int ee = HeaderHeight + 30 + BottomHeight + HeightWord;
            if (ee > WindowHeight)
            {
                WindowHeight = ee;
                Height = WindowHeight;
            }

            int UpOtstup = (WindowHeight - (HeaderHeight + BottomHeight + HeightWord)) / 2;

            int StartPos;
            StartPos = HeaderHeight + UpOtstup;

            story.Location = new Point(LabelX, StartPos);
        }
        #endregion

        #region==Построение Hard текстов==  
        int PanelX;
        private void BuildPanelStorys()
        {           
            Size ImageSize = new Size(48, 48);
            
            if (SetBox.Icon == Icones.None && SetBox.Picture == null)
            {
                PanelX = NoIconIndent;
                LabelX = 0;
                PanelRight = 15;
                PanelWidth = Width - (PanelX + PanelRight);
            }
            else
            {
                PanelX = LeftIndent + IconIndent + IconSize.Width + IconIndent;
                LabelX = 0;
                PanelRight = 10;
                PanelWidth = Width - (PanelX + PanelRight);
            }

            bool tur = false;
            if (SetBox.Icon == Icones.None && SetBox.Picture == null)
            {
                foreach (var item in SetBox.ContentHardTexts)
                {
                    if (item.Picture != null || item.Icon != Icones.None)
                    {
                        tur = true;
                        break;
                    }
                }
            }

            if (tur)
            {
                PanelX = 1;                                
                PanelRight = 0;
                PanelWidth = Width - (PanelX + PanelRight)-10;                
                LabelX = LeftIndent + IconIndent + ImageSize.Width + IconIndent ;
            }
            
            Panel pan = new Panel()
            {
                Size = new Size(PanelWidth, 50),
                AutoSize = true,
                Location = new Point(PanelX, HeaderHeight + 15),
            };
            Controls.Add(pan);

            SetBox.ContentHardTexts.Reverse();
            foreach (var item in SetBox.ContentHardTexts)
            {
                Panel pn = new Panel()
                {
                    //
                    Dock = DockStyle.Top,
                    Height = 30

                };
                pan.Controls.Add(pn);
                
                Font lbFont;
                if (!item.BoldText) lbFont = new Font(item.NameText, item.SizeText, FontStyle.Regular, GraphicsUnit.Pixel, 204);
                else lbFont = new Font(item.NameText, item.SizeText, FontStyle.Bold, GraphicsUnit.Pixel, 204);

                if (tur)
                {
                    int HeightIndent = 7;
                    if (item.Icon != Icones.None || item.Picture != null)
                    {
                        PictureBox pb = new PictureBox()
                        {
                            Location = new Point(LeftIndent + IconIndent, HeightIndent),
                            Size = ImageSize,
                            //Image = item.Image,
                            SizeMode = PictureBoxSizeMode.Zoom
                        };
                        
                        if (item.Icon != Icones.None)
                        {
                            if (item.RecolorImage) pb.Image = GetNewColor(BitmapIcones.GetBitmap(item.Icon), ParamsBlocks.ColorIcones);
                            else pb.Image = BitmapIcones.GetBitmap(item.Icon);
                        }
                        else
                        {
                            if (item.RecolorImage) pb.Image = GetNewColor((Bitmap)item.Picture, ParamsBlocks.ColorIcones);
                            else pb.Image = item.Picture;
                        }
                        //pb.BackColor = Color.AliceBlue;
                        pn.Controls.Add(pb);
                        pn.Height = HeightIndent + ImageSize.Height + 3;
                    }
                }

                Panel pn2 = new Panel()
                {
                    Width = pn.Width - LabelX,
                    Dock = DockStyle.Right,
                    BackColor = item.BackColorItem,
                };

                if (item.Bord == ItemHard.Border.Single) pn2.BorderStyle = BorderStyle.FixedSingle;
                if (item.Bord == ItemHard.Border.Single3D) pn2.BorderStyle = BorderStyle.Fixed3D;                
                pn.Controls.Add(pn2);

                Label lb = new Label()
                {
                    ForeColor = item.ColorText,
                    Font = lbFont,
                    Text = item.Text,
                    //Width = pan.Width,
                    MaximumSize = new Size(PanelWidth-LabelX, 0),
                    //BackColor = item.BackColorItem,
                    AutoSize = true,
                    Location = new Point(0, 5),
                    BackColor = Color.Transparent,
                };              
                pn2.Controls.Add(lb);
                if ((lb.Top + lb.Height + lb.Top) > pn.Height)
                {
                    pn.Height = lb.Top + lb.Height + lb.Top;  
                }
                else
                {
                    lb.Location = new Point(lb.Location.X, ((pn.Height - lb.Height) / 2));
                }

            }
            SetBox.ContentHardTexts.Reverse();

            HeightWord = pan.Height;
            int ee = HeaderHeight + 30 + BottomHeight + HeightWord;
            if (ee > WindowHeight)
            {
                WindowHeight = ee;
                Height = WindowHeight;
            }

            int UpOtstup = (WindowHeight - (HeaderHeight + BottomHeight + HeightWord)) / 2;
            int StartPos;
            StartPos = HeaderHeight + UpOtstup;

            pan.Location = new Point(PanelX, StartPos);
        }
        #endregion


        // Перекраска картинки в нужный цвет
        public Bitmap GetNewColor(Bitmap Blacks, Color Col)
        {
            float Er = Col.R / 225f;
            float Er2 = Col.G / 225f;
            float Er3 = Col.B / 225f;

            Bitmap newBitmap = new Bitmap(Blacks.Width, Blacks.Height);
            Graphics g = Graphics.FromImage(newBitmap);
            ColorMatrix colorMatrix = new ColorMatrix(
            new float[][]
            {
                new float[] {0,  0,  0,  0, 0},
                new float[] {0,  0,  0,  0, 0},
                new float[] {0,  0,  0,  0, 0},
                new float[] {0,  0,  0,  1, 0},
                new float[] {Er, Er2,Er3,0, 1}
            });

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(Blacks, new Rectangle(0, 0, Blacks.Width, Blacks.Height),
                0, 0, Blacks.Width, Blacks.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            return newBitmap;
        }        

        public static void SetDoubleBuffered(Control c)
        {
            if (SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo pDoubleBuffered =
                  typeof(Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            pDoubleBuffered.SetValue(c, true, null);
        }

        #region== Отрисовка окна сообщения ==
        private void Box_Paint(object sender, PaintEventArgs e)
        {
            DrawStyle(e.Graphics);
        }

        private void DrawStyle(Graphics graph)
        {
            graph.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle rectHeader = new Rectangle(0, 0, Width - 1, HeaderHeight);
            Rectangle rectBottom = new Rectangle(0, Height - BottomHeight, Width - 1, BottomHeight);
            Rectangle rectBorder = new Rectangle(0, 0, Width - 1, WindowHeight - 1);
            Rectangle rectIcon = new Rectangle(LeftIndent + IconIndent,
                (HeaderHeight + (WindowHeight - HeaderHeight - IconSize.Height - BottomHeight) / 2),
                IconSize.Width, IconSize.Height);
            Rectangle rectHeaderText = new Rectangle(rectHeader.X + 10, rectHeader.Y, rectHeader.Width, rectHeader.Height);
            

            int buttonWidth = (int)(rectHeader.Height * KofX);
            rectBtnClose = new Rectangle(rectHeader.Width - buttonWidth, rectHeader.Y, buttonWidth, rectHeader.Height);
            Rectangle rectCrosshair = new Rectangle(
                rectBtnClose.X + rectBtnClose.Width / 2 - 5,
                rectBtnClose.Height / 2 - 5,
                10, 10);

            // Шапка
            graph.DrawRectangle(new Pen(HeaderColor), rectHeader);
            graph.FillRectangle(new SolidBrush(HeaderColor), rectHeader);

            // Низ
            graph.FillRectangle(new SolidBrush(BottomColor), rectBottom);

            // Текст заголовка
            graph.DrawString(HeaderText, FontHeader, new SolidBrush(HeaderColorText), rectHeaderText, SF);

            // Иконка
            if (SetBox.Icon != Icones.None || SetBox.Picture != null)
            {

                //graph.FillRectangle(new SolidBrush(HeaderColor), rectIcon);
                Bitmap bitmap;
                if (SetBox.ContentHardTexts != null)
                {
                    if (SetBox.Icon != Icones.None) bitmap = BitmapIcones.GetBitmap(SetBox.Icon);
                    else bitmap = ReBmp(SetBox.Picture, rectIcon.Width, rectIcon.Height);
                }
                else
                {
                    bitmap = BitmapIcones.GetBitmap(SetBox.Icon);
                }
                
                int Xbit = 0, Ybit = 0;
                if (bitmap.Width >= bitmap.Height)
                {
                    Ybit = (rectIcon.Height - bitmap.Height) / 2;
                }
                else
                {
                    Xbit = (rectIcon.Width - bitmap.Width) / 2;
                }

                if (!SetBox.RecolorIcon)
                {
                    graph.DrawImage(bitmap, new Rectangle(rectIcon.X + Xbit, rectIcon.Y + Ybit, bitmap.Width, bitmap.Height));
                    //graph.DrawImage(bitmap, new Point(rectIcon.X + Xbit, rectIcon.Y + Ybit));
                }
                else
                {
                    graph.DrawImage(GetNewColor(bitmap, ParamsBlocks.ColorIcones), new Point(rectIcon.X + Xbit, rectIcon.Y + Ybit));
                }
            }

            if (SetBox.Cross)
            {
                // Кнопка Х
                graph.DrawRectangle(new Pen(btnCloseHovered ? Color.Red : HeaderColor), rectBtnClose);
                graph.FillRectangle(new SolidBrush(btnCloseHovered ? Color.Red : HeaderColor), rectBtnClose);
                DrawCrosshair(graph, rectCrosshair, btnCloseHovered ? WhitePen : GrayPen);
            }

            // Обводка
            graph.DrawRectangle(new Pen(Color.Gray), rectBorder);
        }

        private void DrawCrosshair(Graphics graph, Rectangle rect, Pen pen)
        {
            graph.DrawLine(pen, rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
            graph.DrawLine(pen, rect.X + rect.Width, rect.Y, rect.X, rect.Y + rect.Height);
        }
        #endregion

        // создание новой картинки по заданному шаблону
        private Bitmap ReBmp(Image ima, int wp, int hp)
        {
            Image img = ima;
            int wt, ht; // высота и ширина img
            double prop; // пропорция загружаемой картинки

            if (img.Width < wp && img.Height < hp)
            {
                return new Bitmap(img, img.Width, img.Height);
            }

            prop = (double)img.Width / img.Height;
            if (prop > wp / hp)
            {
                wt = wp;
                ht = (int)(wt / prop);
            }
            else
            {
                ht = hp;
                wt = (int)(ht * prop);
            }
            return new Bitmap(img, wt, ht);
        }
    }
}
