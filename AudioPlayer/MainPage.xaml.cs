using AudioPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace AudioPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PlaylistVM myPlaylist = new PlaylistVM();
        public static MainPage currentMainPage;

        public bool KeyboardVisible { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = myPlaylist;
            this.NavigationCacheMode = NavigationCacheMode.Required;
            currentMainPage = this;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //InputPane.GetForCurrentView().Showing += onKeyboardShowing;
            //InputPane.GetForCurrentView().Hiding += onKeyboardHidding;
            // TODO: Prepare page for display here.
            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.,
        }

        
        //private void onKeyboardShowing(InputPane sender, InputPaneVisibilityEventArgs args)
        //{
        //    KeyboardVisible = true;

        //}

        //private void onKeyboardHidding(InputPane sender, InputPaneVisibilityEventArgs args)
        //{

        //    KeyboardVisible = false;
        //}

        //private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        //{

        //}

    }
}
