using System.Windows;

namespace Comercial_Estefannny.View
{
    /// <summary>
    /// Interaction logic for GradientTestWindow.xaml
    /// </summary>
    public partial class GradientTestWindow : Window
    {
        public GradientTestWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
