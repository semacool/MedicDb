using Medic.Controllers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Medic.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MedicAuth : Window
    {
        AuthController controller;
        public MedicAuth()
        {
            controller = new AuthController();
            InitializeComponent();
        }

        private async void Auth_Click(object sender, RoutedEventArgs e)
        {

            string login = TbLogin.Text;
            string password = TbPassword.Password;
            int id = -1;
            IsEnabled = false;
            try
            {
                await Task.Run(() =>
                {
                    id = controller.Auth(login, password);
                });

            }catch(Exception ex) 
            {
                HelpMessageBox.MessageBoxError(ex.Message);
            }
            IsEnabled = true;
            switch (id)
            {
                case 3: new Menu().ShowDialog(); break;
            }

        }
    }
}
