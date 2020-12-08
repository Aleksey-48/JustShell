using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Messenger
{
    /// <summary>
    /// Список кнопок информационного окна
    /// </summary>
    public enum Buttons
    {
        None,
        Ok,
        YesNo,        
    }

    /// <summary>
    /// Список иконок информационного окна
    /// </summary>
    public enum Icones
    {
        None,
        Create,
        Calendar,
        Delete,       
        Bug,
        Exit,
        Attention,
        Profile,
        Project,
        Question,
        Info,
    }

    
    /// <summary>
    /// Информационное окно
    /// </summary>
    public static class Blocks
    {

        readonly static int InitialWidth = 480;

        /// <summary>
        /// Ответ из окна
        /// </summary>
        public static int Reply = 0;

        /// <summary>
        /// Handle окна, используется для закрытия окна загрузки
        /// </summary>
        public static IntPtr HandleWindow;


        #region==Перегрузки HardTexts==
        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка и списка ItemHard
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard)
        {
            HardTexts(header, itemsHard, Icones.None, null, Buttons.Ok, InitialWidth, true);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard и кнопкой
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Buttons buttons)
        {
            HardTexts(header, itemsHard, Icones.None, null, buttons, InitialWidth, true);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, кнопкой и новый шириной окна
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Buttons buttons, int widthBlock)
        {
            HardTexts(header, itemsHard, Icones.None, null, buttons, widthBlock, true);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard и иконки
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Icones icones)
        {
            HardTexts(header, itemsHard, icones, null, Buttons.Ok, InitialWidth, true);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard и картинки
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Image image)
        {
            HardTexts(header, itemsHard, Icones.None, image, Buttons.Ok, InitialWidth, true);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, иконки, новой шириной окна и перекраски иконки
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Icones icones, int widthBlock, bool recolor)
        {
            HardTexts(header, itemsHard, icones, null, Buttons.Ok, widthBlock, recolor);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, иконки и новой шириной окна
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Icones icones, int widthBlock)
        {
            HardTexts(header, itemsHard, icones, null, Buttons.Ok, widthBlock, true);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, картинки, новой шириной окна и перекраски картинки
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Image image, int widthBlock, bool recolor)
        {
            HardTexts(header, itemsHard, Icones.None, image, Buttons.Ok, widthBlock, recolor);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, картинки и новой шириной окна
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Image image, int widthBlock)
        {
            HardTexts(header, itemsHard, Icones.None, image, Buttons.Ok, widthBlock, true);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, иконки, кнопки и перекраски иконки
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Icones icones, Buttons buttons, bool recolor)
        {
            HardTexts(header, itemsHard, icones, null, buttons, InitialWidth, recolor);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, иконки и кнопки
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Icones icones, Buttons buttons)
        {
            HardTexts(header, itemsHard, icones, null, buttons, InitialWidth, true);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, картинки, кнопки и перекраски картинки
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Image image, Buttons buttons, bool recolor)
        {
            HardTexts(header, itemsHard, Icones.None, image, buttons, InitialWidth, recolor);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, картинки и кнопки
        /// </summary>       
        public static int HardTexts(string header, List<ItemHard> itemsHard, Image image, Buttons buttons)
        {
            HardTexts(header, itemsHard, Icones.None, image, buttons, InitialWidth, true);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, иконки, кнопки, новой ширины окна и перекраски иконки
        /// </summary>  
        public static int HardTexts(string header, List<ItemHard> itemsHard, Icones icones, Buttons buttons, int widthBlock, bool recolor)
        {
            HardTexts(header, itemsHard, icones, null, buttons, widthBlock, recolor);
            return Reply;
        }

        /// <summary>
        /// Выводит Hard сообщение состоящее из заголовка, списка ItemHard, картинки, кнопки, новой ширины окна и перекраски картинки
        /// </summary>  
        public static int HardTexts(string header, List<ItemHard> itemsHard, Image image, Buttons buttons, int widthBlock, bool recolor)
        {
            HardTexts(header, itemsHard, Icones.None, image, buttons, widthBlock, recolor);
            return Reply;
        }

        static int HardTexts(string header, List<ItemHard> itemsHard, Icones icones, Image image, Buttons buttons, int widthBlock, bool recolor)
        {
            SetBox.Button = buttons;
            SetBox.Icon = icones;
            SetBox.ContentHardTexts = itemsHard;
            SetBox.Cross = true;
            SetBox.Download = false;
            SetBox.RecolorIcon = recolor;
            SetBox.Picture = image;

            Box box = new Box
                (
                    header,
                    "",
                    widthBlock
                )
            {
                ShowInTaskbar = false
            };
            box.ShowDialog();

            return Reply;
        }
        #endregion

        #region==Перегрузки Text==
        /// <summary>
        /// Выводит простое окно сообщения
        /// </summary>
        public static int Text()
        {
            return Text("", "", Icones.None, Buttons.Ok, InitialWidth, false);
        }

        /// <summary>
        /// Выводит простое окно сообщения c историей
        /// </summary>
        public static int Text(string Story)
        {
            return Text("", Story, Icones.None, Buttons.Ok, InitialWidth, false);
        }

        /// <summary>
        /// Выводит простое окно сообщения с заголовком и историей
        /// </summary>
        public static int Text(string Header, string Story)
        {
            return Text(Header, Story, Icones.None, Buttons.Ok, InitialWidth, true);
        }

        /// <summary>
        /// Выводит простое окно сообщения с заголовком, историей и новой шириной окна
        /// </summary>
        public static int Text(string Header, string Story, int WidthBlock)
        {
            return Text(Header, Story, Icones.None, Buttons.Ok, WidthBlock, true);
        }

        /// <summary>
        /// Выводит простое окно сообщения с заголовком, историей и иконкой
        /// </summary>
        public static int Text(string Header, string Story, Icones Icon)
        {
            return Text(Header, Story, Icon, Buttons.Ok, InitialWidth, true);
        }

        /// <summary>
        /// Выводит простое окно сообщения с заголовком, историей, иконкой, перекраской иконки,
        /// новой шириной окна
        /// </summary>
        public static int Text(string Header, string Story, Icones Icon, int WidthBlock, bool Recolor)
        {
            return Text(Header, Story, Icon, Buttons.Ok, WidthBlock, Recolor);
        }

        /// <summary>
        /// Выводит простое окно сообщения с заголовком, историей и кнопкой
        /// </summary>
        public static int Text(string Header, string Story, Buttons Button)
        {
            return Text(Header, Story, Icones.None, Button, InitialWidth, true);
        }

        /// <summary>
        /// Выводит простое окно сообщения с заголовком, историей, иконкой и новой шириной окна
        /// </summary>
        public static int Text(string Header, string Story, Buttons Button, int WidthBlock)
        {
            return Text(Header, Story, Icones.None, Button, WidthBlock, true);
        }

        /// <summary>
        /// Выводит простое окно сообщения с заголовком, историей, иконкой, кнопкой и новой шириной окна
        /// </summary>
        public static int Text(string Header, string Story, Icones Icon, Buttons Button, int WidthBlock)
        {
            return Text(Header, Story, Icon, Button, WidthBlock, true);
        }

        /// <summary>
        /// Выводит простое окно сообщения с заголовком, историей, иконкой, кнопкой и новой шириной окна
        /// </summary>
        public static int Text(string Header, string Story, Icones Icon, Buttons Button)
        {
            return Text(Header, Story, Icon, Button, InitialWidth, true);
        }

        /// <summary>
        /// Выводит простое окно сообщения с заголовком, историей, иконкой, кнопкой и перекраской иконки
        /// </summary>
        public static int Text(string Header, string Story, Icones Icon, Buttons Button, bool Recolor)
        {
            return Text(Header, Story, Icon, Button, InitialWidth, Recolor);
        }

        /// <summary>
        /// Выводит простое окно сообщения с заголовком, историей, иконкой, кнопкой,
        /// перекраской иконки и новой шириной окна
        /// </summary>
        public static int Text(string Header, string Story, Icones Icon, Buttons Button, int WidthBlock, bool Recolor)
        {
            SetBox.Button = Button;
            SetBox.Icon = Icon;
            SetBox.Picture = null;
            SetBox.ContentHardTexts = null;
            SetBox.Cross = true;
            SetBox.Download = false;
            SetBox.RecolorIcon = Recolor;
            Box box = new Box
                (
                    Header,
                    Story,
                    WidthBlock
                )
            {
                ShowInTaskbar = false
            };
            box.ShowDialog();
            return Reply;
        }
        #endregion

        public static void Progres(string header, string story, int milisec, Icones icones, int widthBlock, bool recolor)
        {
            SetBox.Icon = icones;
            SetBox.Picture = null;
            SetBox.Cross = true;
            SetBox.ContentHardTexts = null;
            SetBox.Download = true;
            SetBox.RecolorIcon = recolor;
            SetBox.MiliSec = milisec;
            Box box = new Box
                (
                    header,
                    story,
                    widthBlock
                )
            {
                ShowInTaskbar = false
            };
            box.ShowDialog();
        }
    }
}
