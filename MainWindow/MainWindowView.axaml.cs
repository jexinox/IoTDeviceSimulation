using Avalonia.Controls;

namespace IoTDeviceSimulation.MainWindow;

public partial class MainWindowView : Window
{
    public MainWindowView(MainWindowViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}