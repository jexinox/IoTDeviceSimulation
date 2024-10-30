using SukiUI.Controls;

namespace IoTDeviceSimulation.MainWindow;

public partial class MainWindowView : SukiWindow
{
    public MainWindowView(MainWindowViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}