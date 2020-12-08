using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UI
{
    public class Settings
    {
        // Какие-то данные которые надо хранить в файле настроек
        public string Username { get; set; } = "Aleksey Shuvalov";

        public string Password { get; set; } = "123";

        public string FormX { get; set; } = "100";

        public string FormY { get; set; } = "100";

        public string FormState { get; set; } = "Normal";

        public string FormWigth { get; set; } = "500";

        public string FormHeight { get; set; } = "300";

        public string ColorTopic { get; set; } = Color.FromArgb(12, 77, 116).ToString();

        public string NumberPalette { get; set; } = "1";

        public string SizeShell { get; set; } = "1";


        /// <summary>
        /// Метод получения данных настроек приложения из файла
        /// </summary>
        public static Settings GetSettings()
        {
            Settings settings = null;
            string filename = Globals.SettingsFile;

            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    XmlSerializer xser = new XmlSerializer(typeof(Settings));
                    settings = (Settings)xser.Deserialize(fs);
                    fs.Close();
                }
                settings.GetCrypto();
            }
            else
            {
                settings = new Settings();
            }

            return settings;
        }

        /// <summary>
        /// Метод сохранения настроек в файл
        /// </summary>
        public void SaveSettings()
        {
            SetCrypto();
            string filename = Globals.SettingsFile;

            if (File.Exists(filename)) File.Delete(filename);


            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                XmlSerializer xser = new XmlSerializer(typeof(Settings));
                xser.Serialize(fs, this);
                fs.Close();
            }
        }

        // Шифрование данных перед записью в файл
        public void SetCrypto()
        {
            Username = Crypto.Encrypt(Username);
            Password = Crypto.Encrypt(Password);
            FormX = Crypto.Encrypt(FormX);
            FormY = Crypto.Encrypt(FormY);
            FormWigth = Crypto.Encrypt(FormWigth);
            FormHeight = Crypto.Encrypt(FormHeight);
            FormState = Crypto.Encrypt(FormState);
            ColorTopic = Crypto.Encrypt(ColorTopic);
            NumberPalette = Crypto.Encrypt(NumberPalette);
            SizeShell = Crypto.Encrypt(SizeShell);
        }

        // Расшифровывание данных после чтение из файла
        public void GetCrypto()
        {
            try
            {
                Username = Crypto.Decrypt(Username);
                Password = Crypto.Decrypt(Password);
                FormX = Crypto.Decrypt(FormX);
                FormY = Crypto.Decrypt(FormY);
                FormWigth = Crypto.Decrypt(FormWigth);
                FormHeight = Crypto.Decrypt(FormHeight);
                FormState = Crypto.Decrypt(FormState);
                ColorTopic = Crypto.Decrypt(ColorTopic);
                NumberPalette = Crypto.Decrypt(NumberPalette);
                SizeShell = Crypto.Decrypt(SizeShell);
            }

            catch { }
        }       

    }
}
