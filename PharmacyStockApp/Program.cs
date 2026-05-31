using PharmacyStockApp.Presenters;
using PharmacyStockApp.Views;

namespace PharmacyStockApp
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mainView = new MainForm();

            MainFormPresenter presenter = new MainFormPresenter(mainView);

            Application.Run(mainView);
        }
    }
}