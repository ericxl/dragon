using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class ActionExtensions
{
    public static Action<T> To<T>(this Action action)
    {
        if (action != null)
        {
            return t =>
            {
                action();
            };
        }
        return null;
    }
}
