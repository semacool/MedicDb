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

namespace Medic.Views.AnalizeShitViews
{
    /// <summary>
    /// Interaction logic for AnalizeShitsView.xaml
    /// </summary>
    public partial class AnalizeShitsView : Window
    {
        AnalizeShitsController controller = new AnalizeShitsController();
        public AnalizeShitsView()
        {
            InitializeComponent();
        }

        private void AnalizeShitsView_Loaded(object sender, RoutedEventArgs e)
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
            new AnalizeShitPageView().ShowDialog();
            UpdateList();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelected(out AnalizeShit AnalizeShit))
                new AnalizeShitPageView(AnalizeShit).ShowDialog();
            UpdateList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelected(out AnalizeShit AnalizeShit) && HelpMessageBox.MessageBoxQuest("Вы уверены?") == MessageBoxResult.Yes)
                controller.DeleteAnalizeShit(AnalizeShit);
            UpdateList();
        }

        private bool CheckSelected(out AnalizeShit AnalizeShit)
        {
            var selected = ClList.SelectedItem;
            if (selected == null)
            {
                HelpMessageBox.MessageBoxError("Объект не выбран");
                AnalizeShit = null;
                return false;
            }
            else
            {
                AnalizeShit = selected as AnalizeShit;
                return true;
            }
        }

        private async void UpdateList()
        {
            try
            {
                List<AnalizeShit> list = new List<AnalizeShit>();
                await Task.Run(() =>
                {
                    list = controller.GetAnalizeShits();
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
