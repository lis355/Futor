using System;

namespace Futor
{
    public interface IView
    {
        void ShowView();
        void CloseView();

        event Action OnViewClosed; 
    }
}
