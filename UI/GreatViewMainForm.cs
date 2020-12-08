using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ImageMagick;
using Messenger;
using SetHeader;

namespace UI
{
    partial class MainForm
    {
        private void ReadSetting()
        {
            Settings settings = Globals.Settings;
            
            if (settings.FormState == "Normal") 
            {
                WindowState = FormWindowState.Normal;
                Location = new Point(Convert.ToInt32(settings.FormX), Convert.ToInt32(settings.FormY));         
                Width = Convert.ToInt32(settings.FormWigth);
                Height = Convert.ToInt32(settings.FormHeight);
            }
            
            if (settings.FormState == "Minimized")
            {
                WindowState = FormWindowState.Normal;
                Location = new Point(100, 100);
                Width = 500;
                Height = 300;
            }

            if (settings.FormState == "Maximized") WindowState = FormWindowState.Maximized;

            NumberPalette = Convert.ToInt32(settings.NumberPalette);
            SizeShell = Convert.ToInt32(settings.SizeShell);
            NumberTrackBar.Value = SizeShell;
            TopicColor = ParsColor(settings.ColorTopic);
        }

        private Color ParsColor(string s)
        {
            MatchCollection p = Regex.Matches(s, @"(?<=[ARGB]\=)(\d+)");
            return Color.FromArgb(int.Parse(p[0].Value),
                              int.Parse(p[1].Value), int.Parse(p[2].Value), int.Parse(p[3].Value));
        }

        #region == Загрузка заголовков и кнопок, имя штифтов для заголовков и текстов ==
        private void ReadHeader()
        {
            bool IsNoHeader = false;
            string paths = Application.StartupPath.ToString() + "\\Header.shl";
            if (File.Exists(paths))
            {
                FileInfo fileInf = new FileInfo(paths);
                if (fileInf.Length == 0) return;
                try
                {
                    var formatter = new BinaryFormatter();
                    using (var fileStream = new FileStream(paths, FileMode.Open))
                    {
                        HeaderSetup = (HeaderShell)formatter.Deserialize(fileStream);
                    }
                }
                catch
                {
                    IsNoHeader = true;
                }
            }
            else { IsNoHeader = true; }

            if (IsNoHeader) HeaderSetup = OnDefault();
            SetIcones();
        }

        private HeaderShell OnDefault()
        {
            return new HeaderShell()
            {                
                ButtonCreat = Properties.Resources.CreateButton,
                ButtonSave = Properties.Resources.SaveButton,
                ButtonSetup = Properties.Resources.SetupButton,
                IconDelette = Properties.Resources.bin,
                IconStop = Properties.Resources.exit,
                IconBug = Properties.Resources.bug,
                IconNewDay = Properties.Resources.calendar,
                IconСreat = Properties.Resources.cr,
                IconInfo = Properties.Resources.info,
                IconAttention = Properties.Resources.attention,
                IconProfile = Properties.Resources.profile,
                IconProject = Properties.Resources.project,
                IconQuestion = Properties.Resources.question,
                ListHeader = new List<string> { "НЕДАВНО", "СЕГОДНЯ", "ЗАВТРА", "СКОРО", "ПОИСК" },
                FontHeader = "Century Gothic",
                FontText = "Century Gothic",
            };
        }

        private void SetIcones()
        {
            BitmapIcones.Bug = HeaderSetup.IconBug;
            BitmapIcones.Calendar = HeaderSetup.IconNewDay;
            BitmapIcones.Create = HeaderSetup.IconСreat;
            BitmapIcones.Delete = HeaderSetup.IconDelette;
            BitmapIcones.Exit = HeaderSetup.IconStop;
            BitmapIcones.Attention = HeaderSetup.IconAttention;
            BitmapIcones.Question = HeaderSetup.IconQuestion;
            BitmapIcones.Info = HeaderSetup.IconInfo;
            BitmapIcones.Profile = HeaderSetup.IconProfile;
            BitmapIcones.Project = HeaderSetup.IconProject;
        }
        #endregion

        #region== Установка размера оболочки ==
        private void ShellSize(int i)
        {
            switch (i)
            {
                case 0:
                    TopPanel.Height = 84;
                    LeftPanel.Width = 70;
                    DescriptLabel.Height = 20;
                    break;
                case 1:
                    TopPanel.Height = 116;
                    LeftPanel.Width = 96;
                    DescriptLabel.Height = 25;
                    break;
                case 2:
                    TopPanel.Height = FirstHeightTopPanel; //148;
                    LeftPanel.Width = 123;
                    DescriptLabel.Height = FirstHeightDescriptLabel; //30;
                    break;
                case 3:
                    TopPanel.Height = 180;
                    LeftPanel.Width = 150;
                    DescriptLabel.Height = 40;
                    break;
                default:

                    break;
            }
            HeaderPanel.Height = DescriptLabel.Location.Y;
            LenHeader = 0;
            foreach (var item in TopButtons)
            {
                item.Width = Convert.ToInt32(HeaderPanel.Height * item.Proportion);
                LenHeader += item.Width;
            }
            HeaderPanel.Width = LenHeader;
            SizeTexts(i);
            SizeButton(i, LeftPanel.Width);
            CorrectPositionHeaderPanel();
            SetParBloks();
        }

        private void SizeTexts(int i)
        {
            int TopPadding = 0;
            switch (i)
            {
                case 0: DescriptLabel.Font = TextFont = new Font(HeaderSetup.FontText, 13F, FontStyle.Regular, GraphicsUnit.Pixel, 204);
                    TopPadding = 2;
                    break;
                case 1: DescriptLabel.Font = TextFont = new Font(HeaderSetup.FontText, 15F, FontStyle.Regular, GraphicsUnit.Pixel, 204);
                    TopPadding = 2;
                    break;
                case 2: DescriptLabel.Font = TextFont = new Font(HeaderSetup.FontText, 18F, FontStyle.Regular, GraphicsUnit.Pixel, 204);
                    TopPadding = 4;
                    break;
                case 3: DescriptLabel.Font = TextFont = new Font(HeaderSetup.FontText, 22F, FontStyle.Regular, GraphicsUnit.Pixel, 204);
                    TopPadding = 8;
                    break;
                default:
                    break;
            }
            FillPanel.Font = TextFont;
            DescriptLabel.Padding = new Padding(TopPadding, TopPadding, 0, 0);
        }

        private void SizeButton(int Inter, int Widther)
        {
            int h = Inter * 7;
            int w = Convert.ToInt32(Widther / 1.7);
            int x = (Widther - w) / 2;  // 3 = 13
            int y = 10 + Convert.ToInt32(Inter * 6);
            SavePicture.Width = w;
            CreatePicture.Width = w;
            SetupPicture.Width = w;
            CreatePicture.Location = new Point(x, y);
            CreatePicture.Height = CreatePicture.Width;
            SavePicture.Location = new Point(x, CreatePicture.Location.Y + CreatePicture.Height + 20 + h);
            SavePicture.Height = SavePicture.Width;
            SetupPicture.Location = new Point(x, SavePicture.Location.Y + SavePicture.Height + 20 + h);
            SetupPicture.Height = SetupPicture.Width;
        }
        #endregion

        #region== Построение заголовков ==
        private void BuildHeader()
        {
            int StartBet = FirstHeightTopPanel - FirstHeightDescriptLabel - UpDownLabel.Height;
            TopButtons.Clear();
            HeaderPanel.Controls.Clear();
            LenHeader = 0;
            float koff = 0.7f;
            for (int i = HeaderSetup.ListHeader.Count - 1; i > -1; i--)
            {
                MyPic TopPic = new MyPic
                {
                    IsText = HeaderSetup.ListHeader[i],
                };
                
                HeaderPanel.Controls.Add(TopPic);
                TopPic.GoImage = TopPic.Image = ImageFromString(TopPic.IsText, TextHeaderColor);
                TopPic.Proportion = ((float)TopPic.Image.Width / (float)StartBet) * koff;
                TopPic.Width = Convert.ToInt32(HeaderPanel.Height * TopPic.Proportion);
                TopPic.Size = new Size(TopPic.Width, HeaderPanel.Height);
                
                LenHeader += TopPic.Width;
                TopPic.MouseClick += TopPic_MouseClick;
                TopPic.MouseEnter += TopPic_MouseEnter;
                TopPic.MouseLeave += TopPic_MouseLeave;
                TopButtons.Add(TopPic);

                //if (PushText == TopPic.IsText) TopPic.BackColor = TopicColor;
            }
            HeaderPanel.Width = LenHeader;
            CorrectPositionHeaderPanel();
        }

        private Image ImageFromString(string s, Color c)
        {
            MemoryStream Original = new MemoryStream();
            int Wi = TextRenderer.MeasureText(s, HeaderFont).Width;
            MagickReadSettings readSettings = new MagickReadSettings
            {
                FillColor = MagickColor.FromRgb(c.R, c.G, c.B), // цвет текста
                BackgroundColor = MagickColors.Transparent, // фон текста
                FontStyle = FontStyleType.Normal,

                FontFamily = HeaderSetup.FontHeader, //"Century Gothic",
                Width = Wi, // Ширина текста
                TextGravity = Gravity.Center,//.Undefined,
            };
            MagickImage Maglabel = new MagickImage("label:" + s, readSettings);
            Bitmap fgg = new Bitmap(Maglabel.Width + 20, Maglabel.Height);
            fgg.Save(Original, ImageFormat.Png);

            Original.Position = 0;
            MagickImage image = new MagickImage(Original);
            image.Composite(Maglabel, 10, 0, CompositeOperator.Over);
            image.Write(Original);

            Image imageX = Image.FromStream(Original);

            fgg.Dispose();
            image.Dispose();
            Original.Dispose();
            Maglabel.Dispose();

            return imageX;
        }
        #endregion

        #region== Реакция заголовков на Mouse leave, enter, click
        private void TopPic_MouseLeave(object sender, EventArgs e)
        {
            if ((sender as MyPic).IsPressed) return;
            (sender as MyPic).IsEnter = false;
            (sender as MyPic).Image = (sender as MyPic).GoImage;
        }

        private void TopPic_MouseEnter(object sender, EventArgs e)
        {
            if ((sender as MyPic).IsPressed) return;
            (sender as MyPic).IsEnter = true;
            (sender as MyPic).Image = BlurEffHeader((sender as MyPic).GoImage);
        }

        private void TopPic_MouseClick(object sender, MouseEventArgs e)
        {
            if ((sender as MyPic).IsPressed) return;

            foreach (var item in TopButtons)
            {
                if (item.IsPressed || (item.IsText == PushText))
                {
                    item.IsPressed = false;
                    item.BackColor = Color.Transparent;
                    item.IsEnter = false;
                    item.Image = item.GoImage;
                    break;
                }
            }

            (sender as MyPic).IsPressed = true;
            PushText = (sender as MyPic).IsText;
            (sender as MyPic).BackColor = TopicColor;
            (sender as MyPic).Image = BoldEffHeader((sender as MyPic).GoImage);
            DescriptLabel.Text = "Описание для вкладки " + PushText;
        }
        #endregion


        // Эффект размытия для заголовков
        private Image BlurEffHeader(Image FirstImage)
        {
            MemoryStream Blenda = new MemoryStream();
            FirstImage.Save(Blenda, FirstImage.RawFormat);
            Blenda.Position = 0;
            MagickImage image = new MagickImage(Blenda);
            image.Colorize(MagickColors.White, new Percentage(100));
            image.Blur(25, 4);

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

        // Эффект жирный текст для заголовков
        private Image BoldEffHeader(Image FirstImage)
        {
            MemoryStream Blenda = new MemoryStream();
            FirstImage.Save(Blenda, FirstImage.RawFormat);
            Blenda.Position = 0;
            MagickImage image = new MagickImage(Blenda);
            image.Colorize(MagickColors.White, new Percentage(100));
            image.Blur(25, 1);

            MemoryStream Original = new MemoryStream();
            FirstImage.Save(Original, FirstImage.RawFormat);
            Original.Position = 0;
            MagickImage ImageNew = new MagickImage(Original);
            ImageNew.Colorize(MagickColors.White, new Percentage(100));
            ImageNew.Composite(image, Gravity.Center/*.Forget*/, CompositeOperator.Over);
            ImageNew.Write(Original);
            //pictureBox2.Image = 
            Image imageX = Image.FromStream(Original);
            ImageNew.Dispose();
            Original.Dispose();
            Blenda.Dispose();
            image.Dispose();

            return imageX;
        }        

        // Корректная позиция панели заголовков при растягивании окна и изменении размера
        private void CorrectPositionHeaderPanel()
        {
            TopWheelIsVisible();

            if (LenHeader <= TopPanel.Width) HeaderPanel.Location = new Point(0, 0);
            else
            {
                if (HeaderPanel.Location.X < (TopPanel.Width - LenHeader))
                {
                    HeaderPanel.Location = new Point(TopPanel.Width - LenHeader, 0);
                }
            }
        }

        #region== Видимость прокрутки (стрелки) в заголовке
        private void TopWheelIsVisible()
        {
            if (TopWheeIsBool())
            {
                RightArrowButton.Visible = true;
                LeftArrowButton.Visible = true;
            }
            else
            {
                RightArrowButton.Visible = false;
                LeftArrowButton.Visible = false;
            }
        }

        private bool TopWheeIsBool()
        {
            if (TopPanel.Width < LenHeader)
                return true;
            else return false;
        }
        #endregion

        // Построение компонента палитра (встроенный неизменяемый control user)
        private void BuildPalette()
        {
            int Wi = 50; // ширина квадрата цвета палитры
            int Hi = 40;
            int Shi = 3; // кол-во столбцов в палитре
            int Dow = 0;
            string[] Palette = { "#0C4D74", "#3f4f57", "#a6280a", "#85004B",
                "#004f46","#e0e0e0", "#ff0000", "#282828" };
            for (int i = 0; i < Palette.Length; i++)
            {
                Label Labs = new Label
                {
                    AutoSize = false,
                    Size = new Size(Wi, Hi),
                    BackColor = Color.Transparent,
                    BorderStyle = BorderStyle.FixedSingle,
                };
                Labs.MouseEnter += Labs_MouseEnter;
                Labs.MouseLeave += Labs_MouseLeave;
                Labs.MouseClick += Labs_MouseClick;
                Labs.Paint += Labs_Paint;
                Labs.Location = new Point((i - (Dow * Shi)) * (Wi - 1), Dow * (Hi - 1));
                Labs.ForeColor = ColorTranslator.FromHtml(Palette[i]);
                ColorLabel.Controls.Add(Labs);
                if (((i + 1) % Shi) == 0) Dow++;
            }
            if ((Palette.Length % Shi) != 0) Dow++;
            if (Shi > Palette.Length) Shi = Palette.Length;
            ColorLabel.Size = new Size(Wi * Shi - (Shi - 1), Hi * Dow - (Dow - 1));
        }

        #region== Реакция палитры на Mouse leave, enter, click, paint
        private void Labs_Paint(object sender, PaintEventArgs e)
        {

            Rectangle Rc = new Rectangle(5, 5, e.ClipRectangle.Width - 10, e.ClipRectangle.Height - 10);
            LabelColor_Item(e.Graphics, Rc, (sender as Label).ForeColor);
        }

        private void LabelColor_Item(Graphics g, Rectangle R, Color c)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(AlfaColorItem, c)), R);
        }

        private void Labs_MouseClick(object sender, MouseEventArgs e)
        {
            int i = 1;
            foreach (var item in ColorLabel.Controls)
            {
                if (item == (sender as Label))
                {
                    break;
                }
                i++;
            }

            if (i != 2 && i != 6 && i != 7) TopicColor = (sender as Label).ForeColor;
            if (i == 2 || i == 6) TopicColor = Color.FromArgb(50, 50, 50);
            if (i == 7) TopicColor = Color.FromArgb(64, 64, 64);
            NumberPalette = i;
            ShellRepaint(NumberPalette);
        }

        private void Labs_MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).BackColor = Color.Transparent;
            AlfaColorItem = 170;
            (sender as Label).Refresh();
        }

        private void Labs_MouseEnter(object sender, EventArgs e)
        {
            (sender as Label).BackColor = Color.WhiteSmoke;
            AlfaColorItem = 170;
            ColorLabel.Refresh();
            AlfaColorItem = 255;
            (sender as Label).Refresh();
        }
        #endregion

        #region== Перекраска кнопок, панелей, по гамме из палитры ==
        private void ShellRepaint(int i)
        {
            //TextColor = Color.FromArgb(230, 230, 230);
            //else ColorFormaText = ColorTranslator.FromHtml("#1a1a1a");//ColorTranslator.FromHtml("#323232"); //Color.FromArgb(230, 230, 230);

            if (ScrollLeftActiv) ScrollLeftActiv = false;
            else ScrollLeftActiv = true;
            //BackColor = Color.FromArgb(38, ColorFormaTopic);
            DescriptLabel.BackColor = TopicColor;
            //ScrollLeftLabel.BackColor = TopicColor;
            //ScrollRightLabel.BackColor = TopicColor;
            ColorIcon = TopicColor;
            ButtonRepaint(ColorIcon);            

            FillPanel.ForeColor = ColorTranslator.FromHtml("#323232");
            NumberPictureBox.Image = ReColor((Bitmap)NumberPictureBox.Image, FillPanel.ForeColor);
            NewColor(i);

            foreach (var item in HeaderPanel.Controls)
            {
                if ((item as MyPic).IsPressed || ((item as MyPic).IsText == PushText)) (item as MyPic).BackColor = TopicColor;
            }
            AlfaColorItem = 170;
            label1.BackColor = FillPanel.BackColor;
            CreatePicture.BackColor = FillPanel.BackColor;
            SavePicture.BackColor = FillPanel.BackColor;
            SetupPicture.BackColor = FillPanel.BackColor;
            SetParBloks();
        }

        private void ButtonRepaint(Color c)
        {
            CreateImage = CreatePicture.Image = ReColor(HeaderSetup.ButtonCreat, c);
            SaveImage = SavePicture.Image = ReColor(HeaderSetup.ButtonSave, c);
            SetupImage = SetupPicture.Image = ReColor(HeaderSetup.ButtonSetup, c);
        }

        public Bitmap ReColor(Bitmap Blacks, Color Col)
        {
            MemoryStream Original = new MemoryStream();
            Blacks.Save(Original, Blacks.RawFormat);
            Original.Position = 0;
            MagickImage ImageNew = new MagickImage(Original);
            ImageNew.Colorize(MagickColor.FromRgb(Col.R,Col.G,Col.B), new Percentage(100));
            ImageNew.Write(Original);            
            Bitmap bitmap = (Bitmap)Image.FromStream(Original);
            ImageNew.Dispose();
            Original.Dispose();
            return bitmap;
        }

        private void NewColor(int i)
        {
            int r, g, b;
            int r2, g2, b2;

            switch (i)
            {
                case 1:
                    r = 60; g = 112; b = 141;
                    r2 = 219; g2 = 228; b2 = 234;
                    break;
                case 2:
                    r = 63; g = 79; b = 87;
                    r2 = 215; g2 = 185; b2 = 168;
                    break;
                case 3:
                    r = 238; g = 100; b = 63;
                    r2 = 255; g2 = 205; b2 = 177;
                    break;
                case 4:
                    r = 164; g = 65; b = 121;
                    r2 = 237; g2 = 217; b2 = 228;
                    break;
                case 5:
                    r = 65; g = 124; b = 117;
                    r2 = 217; g2 = 229; b2 = 227;
                    break;
                case 6:
                    r = 102; g = 102; b = 102;
                    r2 = 224; g2 = 224; b2 = 224;
                    break;
                case 7:
                    r = 89; g = 89; b = 89;
                    r2 = 255; g2 = 255; b2 = 255;
                    ColorIcon = Color.Red;
                    ButtonRepaint(ColorIcon);
                    break;
                case 8:
                    r = 60; g = 60; b = 60;
                    r2 = 40; g2 = 40; b2 = 40;
                    ColorIcon = Color.FromArgb(240, 240, 240);//Color.White;
                    ButtonRepaint(ColorIcon);                   
                    FillPanel.ForeColor = Color.FromArgb(240, 240, 240);
                    NumberPictureBox.Image = ReColor((Bitmap)NumberPictureBox.Image, FillPanel.ForeColor);
                    break;
                default:
                    r = 0; g = 0; b = 0;
                    r2 = 0; g2 = 0; b2 = 0;
                    break;
            }
            LeftPanel.BackColor = Color.FromArgb(r2, g2, b2);
            TopPanel.BackColor = Color.FromArgb(r, g, b);
            FillPanel.BackColor = Color.FromArgb(r2, g2, b2);
            BackColor = FillPanel.BackColor;
        }
        #endregion


        //-------------------------

        int AlfaColorItem = 170;
        string PushText = ""; // для нажатого заголовка
        public Color TopicColor; // = Color.FromArgb(12, 77, 116);
        Color ColorIcon;
        private readonly List<MyPic> TopButtons = new List<MyPic>();
        readonly int FirstHeightTopPanel = 148;
        readonly int FirstHeightDescriptLabel = 30;

        /// <summary>
        /// Текущие заголовочные настройки (кнопки, иконки, заголовки, названия шрифтов)
        /// </summary>
        HeaderShell HeaderSetup = new HeaderShell();

        Font TextFont;
    }               
}
