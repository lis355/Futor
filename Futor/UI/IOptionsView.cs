using System;

namespace Futor
{
    public interface IOptionsView : IView
    {
        event Action<bool> OnAutorunChanged;
    }
}
