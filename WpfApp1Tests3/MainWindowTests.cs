using WpfApp1Tests3;

namespace WpfTApp1Tests3
{
public class MainWindowTests  : WpfTestsBase
{
[WpfFact]
public void MainWindowTestsShow()
{
var mainWindow = containerScope.Resolve<MainWindow>();
mainWindow.Show();
}
}
}
