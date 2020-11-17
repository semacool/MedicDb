using Medic.Views.AnalizeBloodViews;
using Medic.Views.AnalizeMochaViews;
using Medic.Views.AnalizeShitViews;
using Medic.Views.ClientViews;
using Medic.Views.LaborantViews;
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

namespace Medic.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void ClickClients(object sender, RoutedEventArgs e)
        {
            new ClientsView().ShowDialog();
        }

        private void ClickLaborants(object sender, RoutedEventArgs e)
        {
            new LaborantsView().ShowDialog();
        }

        private void ClickAnalizes(object sender, RoutedEventArgs e)
        {
            new AnalizeBloodsView().ShowDialog();
            new AnalizeMochasView().ShowDialog();
            new AnalizeShitsView().ShowDialog();    
        }
    }
}
