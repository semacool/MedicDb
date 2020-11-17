using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Medic.Services
{
    class HelpMessageBox
    {
        public static void MessageBoxError(string text = "Ошибка", string title = "Ошибка") 
        {
            MessageBox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void MessageBoxWarning(string text = "Предупреждение", string title = "Предупреждение")
        {
            MessageBox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static MessageBoxResult MessageBoxQuest(string text = "Вопрос", string title = "Вопрос")
        {
            return MessageBox.Show(text, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
