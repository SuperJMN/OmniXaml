namespace SampleWpfApp
{
    using OmniXaml.Services.Mvvm;
    using OmniXaml.Wpf;

    [ViewToken("Main", typeof(MainWindow))]
    public class MainWindow : WpfWindow
    {
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }
    }  
}
