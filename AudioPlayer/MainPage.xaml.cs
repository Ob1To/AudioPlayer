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

        private void CheckSDCardClick(object sender, RoutedEventArgs e)
        {
            GetFilesFromSDCard();
        }

        private async void GetFilesFromSDCard()
        {
            try
            {
                StorageFolder externalDevices = KnownFolders.RemovableDevices;
                StorageFolder sdCard = (await externalDevices.GetFoldersAsync()).FirstOrDefault();
                if (sdCard != null)
                {
                    var folderInfo = new StringBuilder();
                    folderInfo.AppendLine("SD card content:");

                    var files = await sdCard.GetItemsAsync();
                    foreach (var file in files)
                    {
                        folderInfo.AppendLine(file.Name);
                    }
                    this.TextBlockFiles.Text = folderInfo.ToString();
                }
                else
                {
                    this.TextBlockFiles.Text = "No SD card is present";
                }

            }
            catch (Exception ex)
            {
                this.TextBlockFiles.Text = ex.Message;
            }
        }
    }
}
