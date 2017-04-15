using System;
using System.Windows;

namespace TableCommandControl.View {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            Console.SetOut(new ListBoxWriter(ListBox));
            ListBox.Items.Add("Test");
        }
    }
}
