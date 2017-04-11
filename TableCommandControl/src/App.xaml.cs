using System.Windows;

using TableCommandControl.View;

namespace TableCommandControl {
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application {
        /// <summary>Löst das <see cref="E:System.Windows.Application.Startup" />-Ereignis aus.</summary>
        /// <param name="e">Ein <see cref="T:System.Windows.StartupEventArgs" />, das die Ereignisdaten enthält.</param>
        protected override void OnStartup(StartupEventArgs e) {
            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainViewModel();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
