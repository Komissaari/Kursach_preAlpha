using System.Windows.Controls;

namespace LibraryInformationSystem
{
    internal class Manager
    {
        private static LibraryInformationSystemEntities _context;
        public static Frame MainFrame { get; set; }
        public static Frame AdminFrame { get; set; }
        public static Frame RegisterFrame { get; set; }
        public static string Login { get; set; }
        public static MainWindow mainWindow { get; set; }
        public static LibraryInformationSystemEntities GetContext()
        {
            if (_context == null)
                _context = new LibraryInformationSystemEntities();
            return _context;
        }


    }
}
