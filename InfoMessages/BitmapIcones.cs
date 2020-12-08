using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public static class BitmapIcones
    {
        public static Bitmap Create;
        public static Bitmap Calendar;
        public static Bitmap Delete;
        public static Bitmap Bug;
        public static Bitmap Exit;
        public static Bitmap Attention;
        public static Bitmap Profile;
        public static Bitmap Project;
        public static Bitmap Question;
        public static Bitmap Info;

        public static Bitmap GetBitmap(Icones icones)
        {
            switch (icones)
            {
                case Icones.Create: return Create;

                case Icones.Calendar: return Calendar;

                case Icones.Delete: return Delete;

                case Icones.Bug: return Bug;

                case Icones.Exit: return Exit;

                case Icones.Attention: return Attention;

                case Icones.Info: return Info;

                case Icones.Profile: return Profile;

                case Icones.Project: return Project;

                case Icones.Question: return Question;

                default: return Bug;
            }
        }
    }
}
