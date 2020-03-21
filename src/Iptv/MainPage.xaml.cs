using Iptv.Pages.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Iptv
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<M3uParser.M3uMedia> Medias { get; } = new ObservableCollection<M3uParser.M3uMedia>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var url = ApplicationData.Current.RoamingSettings.Values["ChannelListUrl"] as string;
            var parser = new M3uParser.M3uParser();
            var channelList = await parser.ParseFrom(new Uri(url));

            foreach (var media in channelList.Medias)
                Medias.Add(media);
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e) => this.Frame.Navigate(typeof(Player), e.ClickedItem);
    }
}
