using Medic.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Medic.Views.BarcodeViews
{
    /// <summary>
    /// Interaction logic for BarcodeView.xaml
    /// </summary>
    public partial class BarcodeView : Window
    {
        string Code;
        List<BarcodeChar> list;
        public BarcodeView(string Code)
        {
            this.Code = $"*{Code}*";
            InitializeComponent();
            this.Loaded += BarcodeView_Loaded;
        }

        private async void BarcodeView_Loaded(object sender, RoutedEventArgs e)
        {
            await getChars();
            PrintBarcode();
        }

        private string getPattern(char i)
        {
            string pattern = "Error";
            pattern = list.FirstOrDefault(e => e.Character == i.ToString())?.Pattern;
            return pattern;
        }

        private void ClickBack(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            foreach (Window w in App.Current.Windows)
            {
                if (!w.Equals(this) && w.GetType().Name != typeof(MedicAuth).Name)
                    w.Close();
            }

            Close();
        }

        private void ClickPrint(object sender, RoutedEventArgs e)
        {
            var print = new PrintDialog();
            if(print.ShowDialog() == true) 
            {
                print.PrintVisual(PicBarcode, "Штрих код");
            }
            
        }

        private async Task getChars() 
        {
            await Task.Run(() =>
            {
                using (var db = new MedicModel())
                {
                   list = db.BarcodeChars.ToList();
                }
            }
            );
        }

        private void PrintBarcode() 
        {
            int step = 0;
            Rectangle rect;
            foreach (char i in Code)
            {
                string pattern = getPattern(i);

                foreach (char p in pattern)
                {
                    rect = new Rectangle();
                    rect.Width = 5;
                    rect.Height = 100;
                    if (p == '1') 
                    {
                        rect.Fill = new SolidColorBrush(Colors.Black);
                    }
                    else 
                    {
                        rect.Fill = new SolidColorBrush(Colors.White);
                    }
                    PicBarcode.Children.Add(rect);

                }
                rect = new Rectangle();
                rect.Width = 5;
                rect.Height = 100;
                rect.Fill = new SolidColorBrush(Colors.White);
                PicBarcode.Children.Add(rect);

            }
        }
    }
}
