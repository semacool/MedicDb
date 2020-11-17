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

namespace Medic.Views.AnalizeMochaViews
{
    /// <summary>
    /// Interaction logic for AnalizeMochasView.xaml
    /// </summary>
    public partial class AnalizeMochasView : Window
    {
        AnalizeMochasController controller = new AnalizeMochasController();
        public AnalizeMochasView()
        {
            InitializeComponent();
        }

        private void AnalizeMochasView_Loaded(object sender, RoutedEventArgs e)
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
            new AnalizeMochaPageView().ShowDialog();
            UpdateList();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelected(out AnalizeMocha AnalizeMocha))
                new AnalizeMochaPageView(AnalizeMocha).ShowDialog();
            UpdateList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelected(out AnalizeMocha AnalizeMocha) && HelpMessageBox.MessageBoxQuest("Вы уверены?") == MessageBoxResult.Yes)
                controller.DeleteAnalizeMocha(AnalizeMocha);
            UpdateList();
        }

        private bool CheckSelected(out AnalizeMocha AnalizeMocha)
        {
            var selected = ClList.SelectedItem;
            if (selected == null)
            {
                HelpMessageBox.MessageBoxError("Объект не выбран");
                AnalizeMocha = null;
                return false;
            }
            else
            {
                AnalizeMocha = selected as AnalizeMocha;
                return true;
            }
        }

        private async void UpdateList()
        {
            try
            {
                List<AnalizeMocha> list = new List<AnalizeMocha>();
                await Task.Run(() =>
                {
                    list = controller.GetAnalizeMochas();
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
