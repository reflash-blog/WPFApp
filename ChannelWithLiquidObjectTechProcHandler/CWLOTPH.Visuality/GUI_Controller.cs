namespace CWLOTPH.Visuality
{
    public class GuiController
    {

        public static void start_UserGUI()            // Метод запускающий пользовательский интерфейс
        {
            var userWindow = new UserWindow();
            userWindow.Show();
            System.Windows.Threading.Dispatcher.Run();
        }

        public static void start_AdminGUI()         // Метод запускающий администраторский интерфейс
        {
            var adminWindow = new AdminWindow();
            adminWindow.Show();
            System.Windows.Threading.Dispatcher.Run();
        }

        public static void start_LoadGUI()          // Метод запускающий загрузочный интерфейс
        {
            var loadWindow = Load_Window.Initialize();
            loadWindow.Show();
            System.Windows.Threading.Dispatcher.Run();
        }
    }
}
