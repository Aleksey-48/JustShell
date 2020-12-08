using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public static class Globals
    {
        //Задать файл хранения настроек и записать все строки в хэш коде
        public static readonly string SettingsFile = Application.StartupPath.ToString() + "\\Settings.xml";

        private static Settings _settings;
        public static Settings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = Settings.GetSettings();
                }

                return _settings;
            }
        }        
    }
}
