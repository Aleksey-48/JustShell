using ImageMagick;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using UI.Common;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Messenger;

namespace UI
{
    public partial class MainForm : Form, IMainFormView
    {

        protected ApplicationContext ContextApp;        

        public MainForm(ApplicationContext context)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            ContextApp = context;

            InitializeComponent();

            ReadSetting();
            ReadHeader();                               //35F если размер = 1
            HeaderFont = new Font(HeaderSetup.FontHeader, 48F, FontStyle.Regular, GraphicsUnit.Pixel, 204);
            ShellSize(SizeShell);
            BuildHeader();
            SettingEvents();
            BuildPalette();
            ShellRepaint(NumberPalette);
            SetParBloks();
            
            NumberTrackBar.Width = ColorLabel.Width;

            //FirstButton.Click += (sender, arg) => Invoke(ClickFirstButton);
            SetupPicture.Click += (sender, arg) => Invoke(ClickSetupPicture);
        }

        private void Invoke(Action action)
        {
            action?.Invoke();
        }

        //public event Action ClickFirstButton;

        public event Action ClickSetupPicture;

        // Установка события прокрутки от колесика мышки на панели заголовков, кнопок (стрелок)
        private void SettingEvents()
        {
            HeaderPanel.MouseWheel += Top_MouseWheel;
            LeftArrowButton.MouseWheel += Top_MouseWheel;
            RightArrowButton.MouseWheel += Top_MouseWheel;
        }


        // Прокрутка в заголовке по колесику мыши (в win7 не работает)
        private void Top_MouseWheel(object sender, MouseEventArgs e)
        {
            if (TopWheeIsBool())
            {
                if (e.Delta < 0)
                {
                    if (LenHeader + (e.Delta / 4) > (TopPanel.Width + Math.Abs(HeaderPanel.Location.X)))
                    {
                        Point a = HeaderPanel.Location;
                        HeaderPanel.Location = new Point(a.X + (e.Delta / 4), 0);
                    }
                    else HeaderPanel.Location = new Point(TopPanel.Width - LenHeader, 0);
                }
                else
                {
                    if (HeaderPanel.Location.X + (e.Delta / 4) < 0)
                    {
                        Point a = HeaderPanel.Location;
                        HeaderPanel.Location = new Point(a.X + (e.Delta / 4), 0);
                    }
                    else HeaderPanel.Location = new Point(0, 0);
                }
            }
        }

        private void SetParBloks()
        {
            ParamsBlocks.ColorHeader = TopicColor;
            ParamsBlocks.ColorPanel = FillPanel.BackColor;
            ParamsBlocks.ColorBottom = Color.FromArgb(FillPanel.BackColor.R - 10,
                FillPanel.BackColor.G - 10, FillPanel.BackColor.B - 10);
            ParamsBlocks.ColorText = FillPanel.ForeColor;
            ParamsBlocks.SizeText = TextFont.Size;
            ParamsBlocks.NameText = HeaderSetup.FontText;
            ParamsBlocks.NameHeader = HeaderSetup.FontHeader;
            ParamsBlocks.ColorIcones = ColorIcon;
            ParamsBlocks.NumberShell = NumberTrackBar.Value;
        }        

        #region== Движение заголовков по нажатию кнопок прокрутки (стрелки) Mouse down, move
        private void LeftArrowButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (HeaderPanel.Location.X + LenHeader - 50 > TopPanel.Width)
            {
                Point a = HeaderPanel.Location;
                HeaderPanel.Location = new Point(a.X - 50, 0);
            }
            else HeaderPanel.Location = new Point(TopPanel.Width - LenHeader, 0);
        }

        private void LeftArrowButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (HeaderPanel.Location.X + LenHeader - 5 > TopPanel.Width)
                {
                    Point a = HeaderPanel.Location;
                    HeaderPanel.Location = new Point(a.X - 5, 0);
                }
                else HeaderPanel.Location = new Point(TopPanel.Width - LenHeader, 0);
            }
        }

        private void RightArrowButton_MouseDown(object sender, MouseEventArgs e)
        {//            
            if (HeaderPanel.Location.X + 50 < 0)
            {
                Point a = HeaderPanel.Location;
                HeaderPanel.Location = new Point(a.X + 50, 0);
            }
            else HeaderPanel.Location = new Point(0, 0);
        }

        private void RightArrowButton_MouseMove(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Left)
            {
                if (HeaderPanel.Location.X + 5 < 0)
                {
                    Point a = HeaderPanel.Location;
                    HeaderPanel.Location = new Point(a.X + 5, 0);
                }
                else HeaderPanel.Location = new Point(0, 0);
            }
        }
        #endregion

        #region// Реакция кнопок прокрутки (стрелки) на Mouse leave, enter, timer
        private void LeftArrowButton_MouseEnter(object sender, EventArgs e)
        {
            if (ScrollLeftActiv) LeftArrowButton.BackColor = Color.FromArgb(255, ArrowColor);
        }

        private void LeftArrowButton_MouseLeave(object sender, EventArgs e)
        {
            if (ScrollLeftActiv) LeftArrowButton.BackColor = Color.FromArgb(180, ArrowColor);
        }

        private void LeftArrowTimer_Tick(object sender, EventArgs e)
        {
            if (ScrollLeftActiv) 
            {
                if (HeaderPanel.Location.X <= TopPanel.Width - LenHeader)
                {
                    LeftArrowButton.BackColor = Color.FromArgb(65, ArrowColor);
                    ScrollLeftActiv = false;
                }
            }
            else
            {
                if (HeaderPanel.Location.X > TopPanel.Width - LenHeader)
                {
                    if (LeftArrowButton.ClientRectangle.Contains(LeftArrowButton.PointToClient(MousePosition)))
                    {
                        LeftArrowButton.BackColor = Color.FromArgb(255, ArrowColor);
                    }
                    else
                    {
                        LeftArrowButton.BackColor = Color.FromArgb(180, ArrowColor);
                    }
                    ScrollLeftActiv = true;
                }
            }
        }

        private void RightArrowButton_MouseEnter(object sender, EventArgs e)
        {
            if (ScrollRightActiv) RightArrowButton.BackColor = Color.FromArgb(255, ArrowColor);            
        }

        private void RightArrowButton_MouseLeave(object sender, EventArgs e)
        {
            if (ScrollRightActiv) RightArrowButton.BackColor = Color.FromArgb(180, ArrowColor);
        }

        private void RightArrowTimer_Tick(object sender, EventArgs e)
        {
            if (ScrollRightActiv)
            {
                if (HeaderPanel.Location.X >= 0)
                {
                    RightArrowButton.BackColor = Color.FromArgb(65, ArrowColor);
                    ScrollRightActiv = false;
                }
            }
            else
            {
                if (HeaderPanel.Location.X < 0)
                {
                    if (RightArrowButton.ClientRectangle.Contains(RightArrowButton.PointToClient(MousePosition)))
                    {
                        RightArrowButton.BackColor = Color.FromArgb(255, ArrowColor);
                    }
                    else
                    {
                        RightArrowButton.BackColor = Color.FromArgb(180, ArrowColor);
                    }
                    ScrollRightActiv = true;
                }
            }
        }
        #endregion

        public new void Show()
        {
            ContextApp.MainForm = this;
            Application.Run(ContextApp);

        }

        //public int Wing => FirstButton.Width;

        //public int NewWing
        //{
        //    set { FirstButton.Text = value.ToString(); }
        //}

        public bool VisibleFill { set { FillPanel.Visible = value; } }

        public string Super(string a, string b)
        {
            return a + b;
        }

        #region== Реакция в области Размер на Mouse, Key
        private void NumberTrackBar_KeyUp(object sender, KeyEventArgs e)
        {
            TrackSizeDoIt();
        }

        private void NumberTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            TrackSizeDoIt();
        }

        private void NumberTrackBar_ValueChanged(object sender, EventArgs e)
        {
            TrackSizeIsChanger = true;
        }

        private void TrackSizeDoIt()
        {
            if (TrackSizeIsChanger)
            {
                SizeShell = NumberTrackBar.Value;
                ShellSize(SizeShell);
                TrackSizeIsChanger = false;
            }
        }
        #endregion

        #region== Реакция кнопок на Mouse leave, enter
        private void CreatePicture_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = BlurEffect(CreateImage);
        }

        private void CreatePicture_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = CreateImage;
        }

        private void SavePicture_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = BlurEffect(SaveImage);
        }

        private void SavePicture_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = SaveImage;
        }

        private void SetupPicture_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = BlurEffect(SetupImage);
        }

        private void SetupPicture_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Image = SetupImage;
        }
        #endregion

        // Эффект размытия для кнопок
        private Image BlurEffect(Image FirstImage)
        {
            MemoryStream Blenda = new MemoryStream();
            FirstImage.Save(Blenda, FirstImage.RawFormat);
            Blenda.Position = 0;
            MagickImage image = new MagickImage(Blenda);
            //image.Colorize(MagickColors.Gray, new Percentage(100));
            image.Blur(6, 8);

            MemoryStream Original = new MemoryStream();
            FirstImage.Save(Original, FirstImage.RawFormat);
            Original.Position = 0;
            MagickImage ImageNew = new MagickImage(Original);
            //ImageNew.Colorize(MColor, new Percentage(100));
            ImageNew.Composite(image, /*Gravity.Forget,*/ CompositeOperator.Over);
            ImageNew.Write(Original);
            Image im = Image.FromStream(Original);

            ImageNew.Dispose();
            Original.Dispose();
            Blenda.Dispose();
            image.Dispose();

            return im;
        }


        // Изменение позиции заголовков при изменении размера окна
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            CorrectPositionHeaderPanel();
            ColorLabel.Refresh();
        }


        // Эффект затемнения основного окна, перед выводом сообщения
        private Control BlackoutForm()
        {
            int Xx = (ClientRectangle.Width - Width) / 2;
            int Yy = ClientRectangle.Height - Height - Xx;
            Rectangle Rec = new Rectangle(0, 0, ClientRectangle.Width - Xx * 2, ClientRectangle.Height - Yy - Xx);
            Bitmap b = new Bitmap(Rec.Width, Rec.Height);
            DrawToBitmap(b, new Rectangle(Point.Empty, Rec.Size));
            using (Graphics G = Graphics.FromImage(b))
            {
                double percent = 0.5;
                Color darken = Color.FromArgb((int)(255 * percent), Color.Black);
                using (Brush brsh = new SolidBrush(darken))
                {
                    G.FillRectangle(brsh, Rec);
                }
            }

            PictureBox p = new PictureBox
            {
                Location = new Point(Xx, Yy),
                Size = new Size(Rec.Width, Rec.Height),
                Image = b
            };
            Controls.Add(p);
            p.BringToFront();
            return p;
        }


        // Построение контента №1 для HardTexts сообщения
        private List<ItemHard> ContentHard1()
        {
            List<ItemHard> Cht = new List<ItemHard>();
            ItemHard Wer = new ItemHard()
            {
                Text = "Список сообщений",
                SizeText = ParamsBlocks.SizeText + 2,
                BoldText = true,
            };
            Cht.Add(Wer);
            ItemHard Wer2 = new ItemHard()
            {
                Icon = Icones.Create,
                Text = "Создать событие.",
            };
            Cht.Add(Wer2);
            ItemHard Wer3 = new ItemHard()
            {
                Icon = Icones.Calendar,
                Text = "Сообщение календарь.",
            };
            Cht.Add(Wer3);
            ItemHard Wer4 = new ItemHard()
            {
                Icon = Icones.Bug,
                Text = "Сообщение об ошибке.",
            };
            Cht.Add(Wer4);
            ItemHard Wer22 = new ItemHard()
            {
                Icon = Icones.Question,
                Text = "Вопрос в сообщении.",
            };
            Cht.Add(Wer22);
            ItemHard Wer32 = new ItemHard()
            {
                Icon = Icones.Project,
                Text = "Сообщение проект.",
            };
            Cht.Add(Wer32);
            ItemHard Wer42 = new ItemHard()
            {
                Icon = Icones.Info,
                Text = "Сообщение информирование.",
            };
            Cht.Add(Wer42);
            ItemHard Wer222 = new ItemHard()
            {
                Icon = Icones.Profile,
                Text = "Профиль в сообщении.",
            };
            Cht.Add(Wer222);
            ItemHard Wer322 = new ItemHard()
            {
                Icon = Icones.Attention,
                Text = "Сообщение внимание.",
            };
            Cht.Add(Wer322);
            ItemHard Wer423 = new ItemHard()
            {
                Icon = Icones.Delete,
                Text = "Сообщение удаление.",
            };
            Cht.Add(Wer423);
            ItemHard Wer312 = new ItemHard()
            {
                Icon = Icones.Exit,
                Text = "Сообщение выход.",
            };
            Cht.Add(Wer312);
            ItemHard Wer5 = new ItemHard()
            {
                Text = "Сообщение сформировано с помощью метода Block.HardTexts.",
                ColorText = Color.Green,
                SizeText = 10,
            };
            Cht.Add(Wer5);

            return Cht;
        }


        // Построение контента №2 для HardTexts сообщения
        private List<ItemHard> ContentHard2()
        {
            List<ItemHard> Cht = new List<ItemHard>();
            ItemHard Wer = new ItemHard()
            {
                Text = "Внимание!",
                SizeText = ParamsBlocks.SizeText + 2,
                ColorText = Color.Black,
                BackColorItem = Color.Red,
                BoldText = true,
            };
            Cht.Add(Wer);
            ItemHard Wer2 = new ItemHard()
            {
                Text = "Избирательная направленность восприятия на тот или иной объект, повышенный " +
                "интерес к объекту с целью получения каких-либо данных. Изменение внимания выражается в " +
                "изменении переживания степени ясности и отчётливости содержания, являющегося предметом " +
                "деятельности человека. Внимание находит себе выражение в отношении субъекта (например, " +
                "человека) к объекту.",
            };
            Cht.Add(Wer2);
            ItemHard Wer3 = new ItemHard()
            {
                Icon = Icones.Calendar,
                Text = "В зону внимания попадают объекты.",
                Bord = ItemHard.Border.Single,
            };
            Cht.Add(Wer3);
            ItemHard Wer5 = new ItemHard()
            {
                Text = "Сообщение сформировано с помощью метода Block.HardTexts.",
                ColorText = Color.Blue,
                SizeText = 10,
            };
            Cht.Add(Wer5);

            return Cht;
        }


        // Эффект затемнения основного окна, перед выводом сообщения
        private void CreatePicture_Click(object sender, EventArgs e)
        {
            Control c = BlackoutForm();
            Blocks.HardTexts("Информация пользователю", ContentHard1());
            Blocks.HardTexts("Особенности", ContentHard2(), Icones.Attention, 600);
            Blocks.Progres("Загрузка", "Процесс загрузки...", 5000, Icones.Exit, 300, true);
            c.Dispose();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings settings = new Settings
            {
                FormX = Location.X.ToString(),
                FormY = Location.Y.ToString(),
                FormState = WindowState.ToString(),
                FormWigth = Width.ToString(),
                FormHeight = Height.ToString(),
                NumberPalette = NumberPalette.ToString(),
                SizeShell = SizeShell.ToString(),
                ColorTopic = TopicColor.ToString()
            };
            settings.SaveSettings();
        }


        //----------------------------

        int LenHeader;
        readonly Color ArrowColor = Color.FromArgb(250, 250, 250);
        bool ScrollLeftActiv = true;
        bool ScrollRightActiv = true;
        readonly Font HeaderFont;
        bool TrackSizeIsChanger;
        Image CreateImage;
        Image SaveImage;
        Image SetupImage;
        readonly Color TextHeaderColor = Color.FromArgb(230, 230, 230);
        int NumberPalette;
        int SizeShell;        
    }
}
