using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace HCHSoft.WPF.Behaviors
{
    public class AutoScrollBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += new SelectionChangedEventHandler(AssociatedObject_SelectionChanged);
        }

        void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox)
            {
                var listbox = (sender as ListBox);
                if (listbox.SelectedItem != null)
                {
                    listbox.UpdateLayout();
                    listbox.ScrollIntoView(listbox.SelectedItem);
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.SelectionChanged -= new SelectionChangedEventHandler(AssociatedObject_SelectionChanged);
        }
    }
}
