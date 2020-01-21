
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1Tests3.WpfUtils
{
    class WindowWrap<T> : Window where T : Visual
    {
        private readonly T contentInstance;
        public WindowWrap()
        {
            try
            {
                contentInstance = Activator.CreateInstance<T>();
                Content = contentInstance;
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}
