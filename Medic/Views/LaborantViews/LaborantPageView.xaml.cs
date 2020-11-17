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

namespace Medic.Views.LaborantViews
{
    /// <summary>
    /// Interaction logic for LaborantPageView.xaml
    /// </summary>
    public partial class LaborantPageView : Window
    {
        LaborantsController controller;
        Laborant Laborant;
        public LaborantPageView()
        {
            controller = new LaborantsController();
            InitializeComponent();
            Loaded += LaborantPageView_LoadedAdd;
        }

        public LaborantPageView(Laborant Laborant)
        {
            this.Laborant = Laborant;
            controller = new LaborantsController();
            InitializeComponent();
            Loaded += LaborantPageView_LoadedUpdate;
        }

        private void LaborantPageView_LoadedAdd(object sender, RoutedEventArgs e)
        {
            pageLab.ConfigComboBox(controller.GetLaboratories(), null);
            BtnSave.Click += BtnAdd_Click;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Laborant = new Laborant() { User = new User() };
                Laborant.Fio = pageFio.Input;
                Laborant.Salary = decimal.Parse(pageSalary.Input);
                Laborant.IdLaboratory = (pageLab.Input as Laboratory).Id;
                Laborant.Birthday = pageDate.Input.GetValueOrDefault(); 
                Laborant.User.IdType = HelpDb.IdTypeForLaborant;
                Laborant.User.Login = pageLogin.Input;
                Laborant.User.Password = pagePassword.Input;
                await Task.Run(() => controller.AddLaborant(Laborant));
                GoodClose();

            }
            catch (Exception ex) 
            {
                HelpMessageBox.MessageBoxError(ex.Message, Title);
            }

        }

        private void LaborantPageView_LoadedUpdate(object sender, RoutedEventArgs e)
        {
            pageFio.Input = Laborant.Fio;
            pageSalary.Input =  Laborant.Salary.ToString();
            pageDate.Input =  Laborant.Birthday;
            pageLab.ConfigComboBox(controller.GetLaboratories(), Laborant.Laboratory);
            pageLogin.Input = Laborant.User.Login;
            pagePassword.Input =  Laborant.User.Password;
            BtnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Laborant.Fio = pageFio.Input;
                Laborant.Salary = decimal.Parse(pageSalary.Input);
                Laborant.Laboratory = null;
                Laborant.IdLaboratory = (pageLab.Input as Laboratory).Id;
                Laborant.Birthday = pageDate.Input.GetValueOrDefault();
                Laborant.User.IdType = HelpDb.IdTypeForLaborant;
                Laborant.User.Login = pageLogin.Input;
                Laborant.User.Password = pagePassword.Input;
                await Task.Run(() => controller.UpdateLaborant(Laborant));
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
