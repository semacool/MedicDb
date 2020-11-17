using Medic.Controllers;
using Medic.DataSource;
using Medic.Services;
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

namespace Medic.Views.ClientViews
{
    /// <summary>
    /// Interaction logic for ClientsView.xaml
    /// </summary>
    public partial class ClientsView : Window
    {
        ClientsController controller = new ClientsController();
        public ClientsView()
        {
            InitializeComponent();
        }

        private void ClientsView_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();
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

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            new ClientPageView().ShowDialog();
            UpdateList();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelected(out Client client))
                new ClientPageView(client).ShowDialog();
            UpdateList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelected(out Client client) && HelpMessageBox.MessageBoxQuest("Вы уверены?") == MessageBoxResult.Yes)
                controller.DeleteClient(client);
            UpdateList();
        }

        private bool CheckSelected(out Client client)
        {
            var selected = ClList.SelectedItem;
            if (selected == null)
            {
                HelpMessageBox.MessageBoxError("Объект не выбран");
                client = null;
                return false;
            }
            else
            {
                client = selected as Client;
                return true;
            }
        }

        private async void UpdateList()
        {
            try
            {
                List<Client> list = new List<Client>();
                await Task.Run(() =>
                {
                    list = controller.GetClients();
                });
                ClList.ItemsSource = null;
                ClList.ItemsSource = list;
                ClList.Items.Refresh();
            }
            catch(Exception ex)
            {
                HelpMessageBox.MessageBoxError(ex.Message);
            }
        }

        private void ClickPrint(object sender, RoutedEventArgs e)
        {
            var print = new PrintDialog();
            
            if (print.ShowDialog() == true)
            {
                var doc = CreateDocFromDataGrid.GetDocument(ClList);
                print.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "MyDocument");
            }

        }
    }
}
