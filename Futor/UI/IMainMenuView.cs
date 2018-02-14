using System;

namespace Futor
{
    public interface IMainMenuView : IView
    {
        event Action OnShowOptionsClicked;
        event Action OnShowStackClicked;
        event Action<bool> OnBypassAllChanged;
    }
}
