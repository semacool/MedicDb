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

namespace Medic.Views.AnalizeShitViews
{
    /// <summary>
    /// Interaction logic for AnalizeShitPageView.xaml
    /// </summary>
    public partial class AnalizeShitPageView : Window
    {
        AnalizeShitsController controller;
        AnalizeShit AnalizeShit;
        public AnalizeShitPageView()
        {
            controller = new AnalizeShitsController();
            InitializeComponent();
            Loaded += AnalizeShitPageView_LoadedAdd;
        }

        public AnalizeShitPageView(AnalizeShit AnalizeShit)
        {
            this.AnalizeShit = AnalizeShit;
            controller = new AnalizeShitsController();
            InitializeComponent();
            Loaded += AnalizeShitPageView_LoadedUpdate;
        }

        private async void AnalizeShitPageView_LoadedAdd(object sender, RoutedEventArgs e)
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
                var AnalizeShit = new AnalizeShit()
                {
                    Analizi = new Analizi()
                    {
                        Client = pageClient.Input as Client,
                        Laborant = pageLaborant.Input as Laborant,
                        IdAnalizType = HelpDb.IdTypeForShit
                    }
                };

                AnalizeShit.Shit = float.Parse(pageShit.Input);
                AnalizeShit.ZhelezoShit = float.Parse(pageZhelezo.Input);
                AnalizeShit.Analizi.IdClient = (pageClient.Input as Client).Id;
                AnalizeShit.Analizi.Client = null;
                AnalizeShit.Analizi.IdLaborant = (pageLaborant.Input as Laborant).Id;
                AnalizeShit.Analizi.Laborant = null;
                AnalizeShit.Analizi.IdAnalizType = HelpDb.IdTypeForShit;
                await Task.Run(() => controller.AddAnalizeShit(AnalizeShit));
                GoodClose();

            }
            catch (Exception ex)
            {
                HelpMessageBox.MessageBoxError(ex.Message, Title);
            }

        }



        private async void AnalizeShitPageView_LoadedUpdate(object sender, RoutedEventArgs e)
        {
            var clients = new List<Client>();
            var laborants = new List<Laborant>();
            await Task.Run(() =>
            {
                clients = controller.getClients();
                laborants = controller.getLaborants();
            });

            pageShit.Input = AnalizeShit.Shit.ToString();
            pageZhelezo.Input = AnalizeShit.ZhelezoShit.ToString();
            pageClient.ConfigComboBox(clients, AnalizeShit.Analizi.Client);
            pageLaborant.ConfigComboBox(laborants, AnalizeShit.Analizi.Laborant);
            BtnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnalizeShit.Shit = float.Parse(pageShit.Input);
                AnalizeShit.ZhelezoShit = float.Parse(pageZhelezo.Input);
                AnalizeShit.Analizi.IdClient = (pageClient.Input as Client).Id;
                AnalizeShit.Analizi.Client = null;
                AnalizeShit.Analizi.IdLaborant = (pageLaborant.Input as Laborant).Id;
                AnalizeShit.Analizi.Laborant = null;
                AnalizeShit.Analizi.IdAnalizType = HelpDb.IdTypeForShit;
                await Task.Run(() =>
                {
                    controller.UpdateAnalizeShit(AnalizeShit);
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
            new BarcodeView($"{AnalizeShit.Id}/{AnalizeShit.Analizi.IdAnalizType}/{AnalizeShit.Analizi.IdClient}/{AnalizeShit.Analizi.IdLaborant}").ShowDialog();
        }

    }
}
