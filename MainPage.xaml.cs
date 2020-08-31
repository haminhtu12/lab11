using lLab11_U49.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace lLab11_U49
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Sound> Sounds;
        private List<MenuItem> MenuIems;
        private List<String> Suggestions;
        public MainPage()
        {
            this.InitializeComponent();
            Sounds = new ObservableCollection<Sound>();
            SoundManager.GetAllSounds(Sounds);
            MenuIems = new List<MenuItem>();
            MenuIems.Add(new MenuItem { IcoinFile = "Assets/Icons/animals.png", Category = SoundCategory.Animals });
            MenuIems.Add(new MenuItem { IcoinFile = "Assets/Icons/cartoon.png", Category = SoundCategory.Cartoons });
            MenuIems.Add(new MenuItem { IcoinFile = "Assets/Icons/taunt.png", Category = SoundCategory.Taunts });
            MenuIems.Add(new MenuItem { IcoinFile = "Assets/Icons/warning.png", Category = SoundCategory.Warnings });
            BackButton.Visibility = Visibility.Collapsed;
            Debug.WriteLine(22222);
            Debug.WriteLine(Sounds);

        }

        private void HumbergerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.GetAllSounds(Sounds);
            CategotyTextBlock.Text = "All Sounds";
            MenuItemListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;
            goBack();


        }

        private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            SoundManager.GetAllSounds(Sounds);
            Suggestions = Sounds.Where(p => p.name.StartsWith(sender.Text)).Select(p => p.name).ToList();
            SearchAutoSuggestBox.ItemsSource = Suggestions;
            if (string.IsNullOrEmpty(sender.Text)) goBack();

        }

        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SoundManager.GetSoundByname(Sounds,sender.Text);
            CategotyTextBlock.Text = sender.Text;
            MenuItemListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Visible;


        }

        private void MenuItemListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;
            //fileter on category
            CategotyTextBlock.Text = menuItem.Category.ToString();
            SoundManager.GetSoundByCategory(Sounds, menuItem.Category);
           
        }

        private void SoundGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sound = (Sound)e.ClickedItem;
            MyMediaElement.Source = new Uri(this.BaseUri, sound.AudioFile);
            BackButton.Visibility = Visibility.Visible;
        }

        public async void SoundGridView_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Any())
                {
                    var storageFile = items[0] as StorageFile;
                    var contentType = storageFile.ContentType;

                    StorageFolder folder = ApplicationData.Current.LocalCacheFolder;
                    if(contentType == "audio/wav" || contentType == "audio/mpeg")
                    {
                        StorageFile newFile = await storageFile.CopyAsync(folder,
                         storageFile.Name, NameCollisionOption.GenerateUniqueName);
                        MyMediaElement.SetSource(await storageFile.OpenAsync(FileAccessMode.Read),
                            contentType);
                        MyMediaElement.Play();
                    }
                }

            }
        }

        private void SoundGridView_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
            e.DragUIOverride.Caption = "Drop to  create a custom sound and title";
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
                
        }
        private void goBack()
        {
            SoundManager.GetAllSounds(Sounds);
            CategotyTextBlock.Text = "All Sounds";
            MenuItemListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;
        }

    }
}
