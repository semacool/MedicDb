using Medic.Controllers;
using Medic.DataSource;
using Medic.Services;
using Medic.Views.BarcodeViews;
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

namespace Medic.Views.AnalizeMochaViews
{
    /// <summary>
    /// Interaction logic for AnalizeMochaPageView.xaml
    /// </summary>
    public partial class AnalizeMochaPageView : Window
    {
        AnalizeMochasController controller;
        AnalizeMocha AnalizeMocha;
        public AnalizeMochaPageView()
        {
            controller = new AnalizeMochasController();
            InitializeComponent();
            Loaded += AnalizeMochaPageView_LoadedAdd;
        }

        public AnalizeMochaPageView(AnalizeMocha AnalizeMocha)
        {
            this.AnalizeMocha = AnalizeMocha;
            controller = new AnalizeMochasController();
            InitializeComponent();
            Loaded += AnalizeMochaPageView_LoadedUpdate;
        }

        private async void AnalizeMochaPageView_LoadedAdd(object sender, RoutedEventArgs e)
        {
            var clients = new List<Client>();
            var laborants = new List<Laborant>();
            await Task.Run(() =>
            {
                clients = controller.getClients();
                laborants = controller.getLaborants();
            });
            pageClient.ConfigComboBox(clients, null);
            pageLaborant.ConfigComboBox(laborants, null);

            BtnSave.Click += BtnAdd_Click;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var AnalizeMocha = new AnalizeMocha()
                {
                    Analizi = new Analizi()
                    {
                        Client = pageClient.Input as Client,
                        Laborant = pageLaborant.Input as Laborant,
                        IdAnalizType = HelpDb.IdTypeForMocha
                    }
                };

                AnalizeMocha.Mocha = float.Parse(pageMocha.Input);
                AnalizeMocha.ZhelezoMocha = float.Parse(pageZhelezo.Input);
                AnalizeMocha.Analizi.IdClient = (pageClient.Input as Client).Id;
                AnalizeMocha.Analizi.Client = null;
                AnalizeMocha.Analizi.IdLaborant = (pageLaborant.Input as Laborant).Id;
                AnalizeMocha.Analizi.Laborant = null;
                AnalizeMocha.Analizi.IdAnalizType = HelpDb.IdTypeForMocha;
                await Task.Run(() => controller.AddAnalizeMocha(AnalizeMocha));
                GoodClose();

            }
            catch (Exception ex)
            {
                HelpMessageBox.MessageBoxError(ex.Message, Title);
            }

        }



        private async void AnalizeMochaPageView_LoadedUpdate(object sender, RoutedEventArgs e)
        {
            var clients = new List<Client>();
            var laborants = new List<Laborant>();
            await Task.Run(() =>
            {
                clients = controller.getClients();
                laborants = controller.getLaborants();
            });

            pageMocha.Input = AnalizeMocha.Mocha.ToString();
            pageZhelezo.Input = AnalizeMocha.ZhelezoMocha.ToString();
            pageClient.ConfigComboBox(clients, AnalizeMocha.Analizi.Client);
            pageLaborant.ConfigComboBox(laborants, AnalizeMocha.Analizi.Laborant);
            BtnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnalizeMocha.Mocha = float.Parse(pageMocha.Input);
                AnalizeMocha.ZhelezoMocha = float.Parse(pageZhelezo.Input);
                AnalizeMocha.Analizi.IdClient = (pageClient.Input as Client).Id;
                AnalizeMocha.Analizi.Client = null;
                AnalizeMocha.Analizi.IdLaborant = (pageLaborant.Input as Laborant).Id;
                AnalizeMocha.Analizi.Laborant = null;
                AnalizeMocha.Analizi.IdAnalizType = HelpDb.IdTypeForMocha;
                await Task.Run(() =>
                {
                    controller.UpdateAnalizeMocha(AnalizeMocha);
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

        private void BtnBarcode_Click(object sender, RoutedEventArgs e)
        {
            new BarcodeView($"{AnalizeMocha.Id}/{AnalizeMocha.Analizi.IdAnalizType}/{AnalizeMocha.Analizi.IdClient}/{AnalizeMocha.Analizi.IdLaborant}").ShowDialog();
        }
    }
}
