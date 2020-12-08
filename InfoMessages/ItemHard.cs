using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    /// <summary>
    /// Элемент информационного окна с расширенными свойствами,
    /// передается в метод Block.HardText списком List ItemHard
    /// </summary>
    public class ItemHard
    {
        /// <summary>
        /// Список рамок элемента информационного окна
        /// </summary>
        public enum Border
        {
            None,
            Single,
            Single3D,
        }

        /// <summary>
        /// Рамка элемента информационного окна
        /// </summary>
        public Border Bord { get; set; } = Border.None;

        /// <summary>
        /// Картинка элемента информационного окна отображается, если Icones.None у элемента
        /// ItemHard
        /// </summary>
        public Image Picture { get; set; } = null;

        /// <summary>
        /// Иконка элемента информационного окна
        /// </summary>
        public Icones Icon { get; set; } = Icones.None;

        private string text;
        /// <summary>
        /// Текст элемента информационного окна
        /// </summary>
        public string Text
        {
            get => text;
            set
            {
                if (value.Length > 300)
                {
                    value = value.Substring(0, 300) + "...";
                }
                text = value;
            }
        }

        private float sizeText;
        /// <summary>
        /// Размер текста элемента информационного окна
        /// </summary>
        public float SizeText
        {
            get => sizeText;
            set
            {
                if (value > 40) value = 40;
                if (value < 9) value = 9;
                sizeText = value;
            }
        }


        /// <summary>
        /// Название шрифта текста элемента информационного окна
        /// </summary>
        public string NameText { get; set; } = ParamsBlocks.NameText;

        /// <summary>
        /// Задний цвет элемента информационного окна
        /// </summary>
        public Color BackColorItem { get; set; } = Color.Transparent;

        /// <summary>
        /// Цвет текста элемента информационного окна
        /// </summary>
        public Color ColorText { get; set; } = ParamsBlocks.ColorText;

        /// <summary>
        /// Параметр определяющий жирный текст элемента информационного окна
        /// </summary>
        public bool BoldText { get; set; } = false;

        /// <summary>
        /// Параметр определяющий перекрашивать Иконку или Картинку в цвет палитры главной формы
        /// элемента информационного окна
        /// </summary>
        public bool RecolorImage { get; set; } = true;

        public ItemHard()
        {
            SizeText = ParamsBlocks.SizeText;          
        }
    }
}
