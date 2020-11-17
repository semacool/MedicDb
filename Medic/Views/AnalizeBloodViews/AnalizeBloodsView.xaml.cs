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

namespace Medic.Views.AnalizeBloodViews
{
    /// <summary>
    /// Interaction logic for AnalizeBloodsView.xaml
    /// </summary>
    public partial class AnalizeBloodsView : Window
    {
        AnalizeBloodsController controller = new AnalizeBloodsController();
        public AnalizeBloodsView()
        {
            InitializeComponent();
        }

        private void AnalizeBloodsView_Loaded(object sender, RoutedEventArgs e)
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
            new AnalizeBloodPageView().ShowDialog();
            UpdateList();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelected(out AnalizeBlood AnalizeBlood))
                new AnalizeBloodPageView(AnalizeBlood).ShowDialog();
            UpdateList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelected(out AnalizeBlood AnalizeBlood) && HelpMessageBox.MessageBoxQuest("Вы уверены?") == MessageBoxResult.Yes)
                controller.DeleteAnalizeBlood(AnalizeBlood);
            UpdateList();
        }

        private bool CheckSelected(out AnalizeBlood AnalizeBlood)
        {
            var selected = ClList.SelectedItem;
            if (selected == null)
            {
                HelpMessageBox.MessageBoxError("Объект не выбран");
                AnalizeBlood = null;
                return false;
            }
            else
            {
                AnalizeBlood = selected as AnalizeBlood;
                return true;
            }
        }

        private async void UpdateList()
        {
            try
            {
                List<AnalizeBlood> list = new List<AnalizeBlood>();
                await Task.Run(() =>
                {
                    list = controller.GetAnalizeBloods();
                });
                ClList.ItemsSource = list;
                ClList.Items.Refresh();
            }
            catch(Exception ex)
            {
                HelpMessageBox.MessageBoxError(ex.Message);
            }
        }
    }
}
