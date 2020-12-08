using LightInject;

namespace UI.Common
{
    public class ApplicationController
    {
        ServiceContainer serviceContainer;

        public ApplicationController(ServiceContainer service)
        {
            serviceContainer = service;
            serviceContainer.RegisterInstance(this);
        }

        public void Run<TPresenter>() where TPresenter:class, IPresenter
        {
            var presenter = serviceContainer.GetInstance<TPresenter>();
            presenter.Run();
        }
    }
}
