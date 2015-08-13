using AudioPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
        string name;

        private PlaylistVM myPlaylist = new PlaylistVM();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void On_Button_Add_Click(object sender, RoutedEventArgs e)
        {
            //TextBlockFiles.Text = "";

            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.HomeGroup;
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.PickMultipleFilesAndContinue();
        }

        private void DisplayFiles(StorageFile[] files)
        {
            if (files != null)
            {
                name = files[0].Path;
                foreach (var item in files)
                {
                    Song newSong = new Song(item.Name, item.Path);
                    myPlaylist.AddSong(newSong);
                }

                //CHECK

                var sb = new StringBuilder();
                foreach (var item in myPlaylist.ListOfSongs)
                {
                    sb.AppendLine(item.Key);
                }
                //this.TextBlockFiles.Text = sb.ToString();
            }
        }

        internal void PhonePickedFiles(FileOpenPickerContinuationEventArgs arguments)
        {
            var files = arguments.Files;
            DisplayFiles(files.ToArray());
        }

        private void On_Button_Play_Click(object sender, RoutedEventArgs e)
        {
            //myMediaElement.Source = new Uri(name, UriKind.RelativeOrAbsolute);
            //myMediaElement.Play();
        }
    }
}
