using LightInject;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Common;
using UI.Presenters;
using BL.Kernel;


namespace UI
{
    static class Program
    {
        public static readonly ApplicationContext Context = new ApplicationContext();

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //ulong appid = ulong.Parse(ConfigurationSettings.AppSettings["AppIdForTest"]);//(ConfigurationManager.AppSettings["AppIdForTest"]);
            JustShellWorke JustWorke = new JustShellWorke();

            ServiceContainer container = new ServiceContainer();
            container.RegisterInstance<JustShellWorke>(JustWorke);
            container.RegisterInstance<ApplicationContext>(Context);
            container.Register<IMainFormView, MainForm>();
            container.Register<MainFormPresenter>();

            ApplicationController controller = new ApplicationController(container);
            controller.Run<MainFormPresenter>();
        }
    }
}
