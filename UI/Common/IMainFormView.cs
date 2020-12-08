using System;

namespace UI.Common
{
    public interface IMainFormView : IView
    {
        //int Wing { get; }

        //int NewWing { set; }

        //event Action ClickFirstButton;

        bool VisibleFill { set; }
        event Action ClickSetupPicture;
    }
}
