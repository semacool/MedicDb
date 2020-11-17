using Medic.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Medic.HelpViews
{
    /// <summary>
    /// Interaction logic for InputText.xaml
    /// </summary>
    public partial class InputCombobox : UserControl
    {
        public string Title {
            get => TbTitle.Text;
            set 
            {
                TbTitle.Text = value;
            }
        }

        public object Input
        {
            get 
            {
                checkNull();
                checkPattern();
                return TbInput.SelectedValue;
            }
        }

        public string Pattern { get; set; }
        public bool NotNull { get; set; } = false;

        public void ConfigComboBox<T>(IEnumerable<T> list, T item) where T : IEntity
        {
            TbInput.ItemsSource = list;
            if(item != null) 
            {
                var CurrentItem = list.ToList().Find(e => e.Id == item.Id);
                TbInput.SelectedItem = CurrentItem;
            }
        }

        public InputCombobox()
        {
            InitializeComponent();
        }

        private void checkNull()
        {
            if (!NotNull) return;
            if (string.IsNullOrWhiteSpace(TbInput.Text) || string.IsNullOrEmpty(TbInput.Text)) throw new Exception($"{Title} - Не может быть пустым");
        }
        private void checkPattern()
        {
            if (Pattern == null) return;
            if (!Regex.IsMatch(TbInput.Text, Pattern)) throw new Exception($"{Title} - Имеет не верный формат");
        }
    }
}
