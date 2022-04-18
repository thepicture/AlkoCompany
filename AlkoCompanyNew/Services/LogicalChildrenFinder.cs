using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AlkoCompanyNew.Services
{
    public static class LogicalChildrenFinder
    {
        public static IEnumerable<T> Find<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in Find<T>(ithChild)) yield return childOfChild;
            }
        }
    }
}
