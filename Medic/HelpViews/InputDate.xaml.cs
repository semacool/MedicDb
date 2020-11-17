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
    public partial class InputDate : UserControl
    {
        public string Title {
            get => TbTitle.Text;
            set 
            {
                TbTitle.Text = value;
            }
        }

        public DateTime? Input
        {
            get {
                checkNull();
                return TbInput.SelectedDate;
            }
            set 
            {
                TbInput.SelectedDate = value;
            }
        }

        public string Pattern { get; set; }
        public bool NotNull { get; set; } = false;


        public InputDate()
        {
            InitializeComponent();
        }

        private void checkNull()
        {
            if (!NotNull) return;
            if (!TbInput.SelectedDate.HasValue) throw new Exception($"{Title} - Не может быть пустым");
        }
        private void checkPattern()
        {
            if (Pattern == null) return;
            if (!Regex.IsMatch(TbInput.Text, Pattern)) throw new Exception($"{Title} - Имеет не верный формат");
        }
    }
}
