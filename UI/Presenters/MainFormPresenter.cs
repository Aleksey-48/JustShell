using BL.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Common;

namespace UI.Presenters
{
    public class MainFormPresenter : IPresenter
    {
        readonly IMainFormView _main;
        readonly JustShellWorke _justWorke;
        readonly ApplicationController _appliController;

        public MainFormPresenter(ApplicationController applicationController, IMainFormView mainForm, JustShellWorke justWorke)
        {
            _justWorke = justWorke;
            _main = mainForm;
            _appliController = applicationController;


            //_main.ClickFirstButton += () => Click_First_Button();
            _main.ClickSetupPicture += () => Click_Setup_Button();
        }

        //private void Click_First_Button()
        //{
        //    _main.NewWing = _justWorke.ClickFirstButton(_main.Wing, 2);
        //}

        private void Click_Setup_Button()
        {
            _justWorke.ClickSetupButton();
            _main.VisibleFill = _justWorke.VisibleFill;
        }

        public void Run()
        {
            _main.Show();
        }
    }
}
