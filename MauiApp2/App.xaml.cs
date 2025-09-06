using MauiApp2.Helpers;

namespace MauiApp2
{
    public partial class App : Application
    {
        static SQLiteDatabaseHeper _db;
        public static SQLiteDatabaseHeper Db
        {
            get {
                if (_db == null)
                {
                    string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"banco_sqlite_comporas.db3");

                    _db = new SQLiteDatabaseHeper(path);
                }
                return _db;
            }
        }


        public App()
        {
            InitializeComponent();

            // MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
