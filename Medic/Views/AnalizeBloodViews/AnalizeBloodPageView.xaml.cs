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

namespace Medic.Views.AnalizeBloodViews
{
    /// <summary>
    /// Interaction logic for AnalizeBloodPageView.xaml
    /// </summary>
    public partial class AnalizeBloodPageView : Window
    {
        AnalizeBloodsController controller;
        AnalizeBlood AnalizeBlood;
        public AnalizeBloodPageView()
        {
            controller = new AnalizeBloodsController();
            InitializeComponent();
            Loaded += AnalizeBloodPageView_LoadedAdd;
        }

        public AnalizeBloodPageView(AnalizeBlood AnalizeBlood)
        {
            this.AnalizeBlood = AnalizeBlood;
            controller = new AnalizeBloodsController();
            InitializeComponent();
            Loaded += AnalizeBloodPageView_LoadedUpdate;
        }

        private async void AnalizeBloodPageView_LoadedAdd(object sender, RoutedEventArgs e)
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
                var AnalizeBlood = new AnalizeBlood()
                {
                    Analizi = new Analizi()
                    {
                        Client = pageClient.Input as Client,
                        Laborant = pageLaborant.Input as Laborant,
                        IdAnalizType = HelpDb.IdTypeForBlood
                    }
                };

                AnalizeBlood.Blood = float.Parse(pageBlood.Input);
                AnalizeBlood.Zhelezo = float.Parse(pageZhelezo.Input);
                AnalizeBlood.Analizi.IdClient = (pageClient.Input as Client).Id;
                AnalizeBlood.Analizi.Client = null;
                AnalizeBlood.Analizi.IdLaborant = (pageLaborant.Input as Laborant).Id;
                AnalizeBlood.Analizi.Laborant = null;
                AnalizeBlood.Analizi.IdAnalizType = HelpDb.IdTypeForBlood;
                await Task.Run(() => controller.AddAnalizeBlood(AnalizeBlood));
                GoodClose();

            }
            catch (Exception ex)
            {
                HelpMessageBox.MessageBoxError(ex.Message, Title);
            }

        }



        private async void AnalizeBloodPageView_LoadedUpdate(object sender, RoutedEventArgs e)
        {
            var clients = new List<Client>();
            var laborants = new List<Laborant>();
            await Task.Run(() =>
            {
                clients = controller.getClients();
                laborants = controller.getLaborants();
            });

            pageBlood.Input = AnalizeBlood.Blood.ToString();
            pageZhelezo.Input = AnalizeBlood.Zhelezo.ToString();
            pageClient.ConfigComboBox(clients, AnalizeBlood.Analizi.Client);
            pageLaborant.ConfigComboBox(laborants, AnalizeBlood.Analizi.Laborant);
            BtnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnalizeBlood.Blood = float.Parse(pageBlood.Input);
                AnalizeBlood.Zhelezo = float.Parse(pageZhelezo.Input);
                AnalizeBlood.Analizi.IdClient = (pageClient.Input as Client).Id;
                AnalizeBlood.Analizi.Client = null;
                AnalizeBlood.Analizi.IdLaborant = (pageLaborant.Input as Laborant).Id;
                AnalizeBlood.Analizi.Laborant = null;
                AnalizeBlood.Analizi.IdAnalizType = HelpDb.IdTypeForBlood;
                await Task.Run(() =>
                {
                    controller.UpdateAnalizeBlood(AnalizeBlood);
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
            new BarcodeView($"{AnalizeBlood.Id}/{AnalizeBlood.Analizi.IdAnalizType}/{AnalizeBlood.Analizi.IdClient}/{AnalizeBlood.Analizi.IdLaborant}").ShowDialog();
        }
    }
}
