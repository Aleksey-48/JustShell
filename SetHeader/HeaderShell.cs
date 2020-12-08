using System;
using System.Collections.Generic;
using System.Drawing;

namespace SetHeader
{
    /// <summary>
    /// Образ заголовочного объекта с кнопками, иконками, заголовками,
    /// названиями шрифтов заголовков и текста. Формируется из файла Header.shl
    /// </summary>
    [Serializable]
    public class HeaderShell
    {
        public Bitmap ButtonCreat { get; set; }
        public Bitmap ButtonSave { get; set; }
        public Bitmap ButtonSetup { get; set; }
        public Bitmap IconDelette { get; set; }
        public Bitmap IconStop { get; set; }
        public Bitmap IconBug { get; set; }
        public Bitmap IconNewDay { get; set; }
        public Bitmap IconСreat { get; set; }
        public Bitmap IconInfo { get; set; }
        public Bitmap IconAttention { get; set; }
        public Bitmap IconQuestion { get; set; }
        public Bitmap IconProfile { get; set; }
        public Bitmap IconProject { get; set; }
        public List<string> ListHeader { get; set; } = new List<string>();
        public string FontHeader { get; set; }
        public string FontText { get; set; }
    }
}
