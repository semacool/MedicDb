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
    /// Interaction logic for ClientPageView.xaml
    /// </summary>
    public partial class ClientPageView : Window
    {
        ClientsController controller;
        Client client;
        public ClientPageView()
        {
            controller = new ClientsController();
            InitializeComponent();
            Loaded += ClientPageView_LoadedAdd;
        }

        public ClientPageView(Client client)
        {
            this.client = client;
            controller = new ClientsController();
            InitializeComponent();
            Loaded += ClientPageView_LoadedUpdate;
        }

        private void ClientPageView_LoadedAdd(object sender, RoutedEventArgs e)
        {
            BtnSave.Click += BtnAdd_Click;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new Client() { User = new User() };
                client.Name = pageName.Input;
                client.Surname = pageSurname.Input;
                client.Middlename = pageMiddlename.Input;
                client.Birthday = pageDate.Input.GetValueOrDefault();
                client.User.Login = pageLogin.Input;
                client.User.IdType = HelpDb.IdTypeForClient;
                client.User.Password = pagePassword.Input;
                await Task.Run(() =>controller.AddClient(client));
                GoodClose();

            }
            catch (Exception ex) 
            {
                HelpMessageBox.MessageBoxError(ex.Message, Title);
            }

        }



        private void ClientPageView_LoadedUpdate(object sender, RoutedEventArgs e)
        {
            pageName.Input = client.Name;
            pageSurname.Input =  client.Surname;
            pageMiddlename.Input =  client.Middlename;
            pageDate.Input =  client.Birthday;
            pageLogin.Input = client.User.Login;
            pagePassword.Input =  client.User.Password;
            BtnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client.Name = pageName.Input;
                client.Surname = pageSurname.Input;
                client.Middlename = pageMiddlename.Input;
                client.Birthday = pageDate.Input.GetValueOrDefault();
                client.User.Login = pageLogin.Input;
                client.User.Password = pagePassword.Input;
                await Task.Run(() =>
                {
                        controller.UpdateClient(client);
                });
                GoodClose();

            }
            catch (Exception ex)
            {
                HelpMessageBox.MessageBoxError(ex.Message, Title);
            }
        }

        private void GoodClose() 
        {
            MessageBox.Show("Успешно", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
