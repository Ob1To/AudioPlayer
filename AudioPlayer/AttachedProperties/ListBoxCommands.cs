using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace AudioPlayer.AttachedProperties
{
    public class ListBoxCommands
    {


        public static ICommand GetDoubleTapped(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DoubleTappedProperty);
        }

        public static void SetDoubleTapped(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DoubleTappedProperty, value);
        }

        // Using a DependencyProperty as the backing store for DoubleClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DoubleTappedProperty =
            DependencyProperty.RegisterAttached("DoubleTapped", 
                typeof(ICommand), 
                typeof(object), 
                new PropertyMetadata(null,OnDoubleTappedCommandChanged));

        private static void OnDoubleTappedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as UIElement;
            if (control == null)
            {
                return;
            }

            control.DoubleTapped += (obj, args) =>
            
            {
                 ICommand command = e.NewValue as ICommand;
                 if (command == null)
                 {
                     return;
                 }
                 command.Execute(null);
             };
        }
    }
}
