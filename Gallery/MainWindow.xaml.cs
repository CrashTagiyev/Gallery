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

namespace Gallery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ImageClass>? MyImages { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MyImages = new Faker<ImageClass>().RuleFor(ic => ic.ImageUrl, faker => faker.Image.PicsumUrl()).Generate(20);
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
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

      

        

        private void sldWith_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (BothAuto.IsChecked == true)
            {
                sldHeight.Value = sldWith.Value;
            }
        }

        private void sldHeight_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (BothAuto.IsChecked == true)
            {
                sldWith.Value = sldHeight.Value;
            }
        }
    }
}
