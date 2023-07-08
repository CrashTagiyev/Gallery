using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;
using System.Reflection.Metadata;
using Bogus;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;

namespace Gallery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DispatcherTimer playTimer;
        private int currentIndex;
        public List<ImageClass>? MyImages { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MyImages = new Faker<ImageClass>().RuleFor(ic => ic.ImageUrl, faker => faker.Image.PicsumUrl()).Generate(5);
            ImageList.SelectedIndex = -1;

            playTimer = new DispatcherTimer();
            playTimer.Interval = TimeSpan.FromSeconds(1);
            playTimer.Tick += new EventHandler(PlayTimer_Tick);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        void INotifyPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));


        private int imageSize;

        public int ImageSize
        {
            get { return imageSize; }

            set { imageSize = value; INotifyPropertyChanged(); }
        }


        private void ImageList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ImageClass? ic = ImageList.SelectedItem as ImageClass;
            Uri? imageUri = new Uri(ic.ImageUrl, UriKind.RelativeOrAbsolute);
            ImageSource imageSource = new BitmapImage(imageUri);
            ShowImg.Source = imageSource;
        }







       

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

                Binding binding = new Binding();
                binding.Source = sldWith;
                binding.Path = new PropertyPath("Value");
                binding.Mode = BindingMode.TwoWay;
                BindingOperations.SetBinding(sldHeight, Slider.ValueProperty, binding);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void Prev_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ImageList.SelectedIndex - 1 > -1)
            {
                ImageClass? ic = ImageList.Items[ImageList.SelectedIndex - 1] as ImageClass;
                ImageList.SelectedIndex -= 1;
                Uri? imageUri = new Uri(ic.ImageUrl, UriKind.RelativeOrAbsolute);
                ImageSource imageSource = new BitmapImage(imageUri);
                ShowImg.Source = imageSource;
            }
        }

        private void Next_Button_Click(object sender,RoutedEventArgs e)
        {
            if (ImageList.SelectedIndex + 1 < ImageList.Items.Count)
            {
                ImageClass? ic = ImageList.Items[ImageList.SelectedIndex + 1] as ImageClass;
                ImageList.SelectedIndex += 1;
                Uri? imageUri = new Uri(ic.ImageUrl, UriKind.RelativeOrAbsolute);
                ImageSource imageSource = new BitmapImage(imageUri);
                ShowImg.Source = imageSource;
            }
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            playTimer.Start();
        }
        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (currentIndex < ImageList.Items.Count - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }

            ImageClass? ic = ImageList.Items[currentIndex] as ImageClass;
            ImageList.SelectedIndex = currentIndex;
            Uri? imageUri = new Uri(ic.ImageUrl, UriKind.RelativeOrAbsolute);
            ImageSource imageSource = new BitmapImage(imageUri);
            ShowImg.Source = imageSource;
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            playTimer.Stop();
        }
    }
}
