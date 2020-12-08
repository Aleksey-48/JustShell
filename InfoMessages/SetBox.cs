using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    /// <summary>
    /// Индивидуальные настройки для разных типов информационного окно
    /// для передачи из оболочки Blocks в окно вывода информации Box
    /// </summary>
    public static class SetBox
    {
        /// <summary>
        /// Иконка в информационном окне
        /// </summary>
        public static Icones Icon;

        /// <summary>
        /// Кнопка в информационном окне
        /// </summary>
        public static Buttons Button;

        /// <summary>
        /// Лист элементов ItemHardText вместо текста
        /// </summary>
        public static List<ItemHard> ContentHardTexts;

        /// <summary>
        /// Картинка в информационном окне для HardTexts
        /// </summary>
        public static Image Picture;

        private static int milisec = 0;
        /// <summary>
        /// Время в миллисекундах, для захлопывания информационного окна,
        /// если = 0, то захлопывается окно по Handel инф. окна.
        /// Используется при Blocks.Download
        /// </summary>
        public static int MiliSec
        {
            get => milisec;
            set
            {
                if (value > 7000) value = 7000;
                if (value < 0) value = 0;
                milisec = value;
            }
        }

        /// <summary>
        /// Параметр определяющий показывать кнопку закрытия окна или нет
        /// </summary>
        public static bool Cross;

        /// <summary>
        /// Параметр определяющий показывать окно как окно загрузки
        /// </summary>
        public static bool Download;

        /// <summary>
        /// Параметр определяющий перекрашивать Иконку в цвет палитры главной формы
        /// </summary>
        public static bool RecolorIcon;
    }
}
