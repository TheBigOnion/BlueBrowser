using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace BlueBrowser.helpers
{
    public class Finder
    {
        public static T FindVisualChild<T>(DependencyObject parent, string objectName) where T : DependencyObject
        {
            T foundChild = null;

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    foundChild = (T)child;
                    break;
                }
                else
                {
                    foundChild = FindVisualChild<T>(child, objectName);
                    if (foundChild != null)
                    {
                        dynamic dynObject = foundChild;
                        if (dynObject.Name == objectName)
                        {
                            break;
                        }
                    }
                }
            }

            return foundChild;
        }

        public static T FindGrandChildControl<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            // Loop through all children of the current parent
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                // If the child is of type T, return it
                if (child is T)
                {
                    return (T)child;
                }

                // Recursively search in the child controls
                T result = FindGrandChildControl<T>(child);
                if (result != null) return result;
            }

            return null;
        }
    }
}
