using PruebaP3JhoelSuarez.Repositories;

namespace PruebaP3JhoelSuarez
{
    public partial class App : Application
    {
        private static BDDJsuarezRepository _database;

        public static BDDJsuarezRepository Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new BDDJsuarezRepository(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                }
                return _database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}