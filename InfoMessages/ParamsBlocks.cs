using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    /// <summary>
    /// Общие параметры информационного окно для передачи
    /// из главной формы в оболочку Blocks
    /// </summary>
    public static class ParamsBlocks
    {
        /// <summary>
        /// Цвет заголовка
        /// </summary>
        public static Color ColorHeader;

        /// <summary>
        /// Цвет панели
        /// </summary>
        public static Color ColorPanel;

        /// <summary>
        /// Цвет текста
        /// </summary>
        public static Color ColorText;

        /// <summary>
        /// Цвет низа
        /// </summary>
        public static Color ColorBottom;

        /// <summary>
        /// Цвет иконок в сообщениях
        /// </summary>
        public static Color ColorIcones;

        /// <summary>
        /// Размер шрифта окна в Pixel
        /// </summary>
        public static float SizeText;

        /// <summary>
        /// Название шрифта окна
        /// </summary>
        public static string NameText;

        /// <summary>
        /// Название шрифта заголовка
        /// </summary>
        public static string NameHeader;

        /// <summary>
        /// Размер оболочки из 0 - 3
        /// </summary>
        public static int NumberShell;
    }
}
