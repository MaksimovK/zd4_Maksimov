using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zd4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExchangeRates : ContentPage
    {
        public ExchangeRates()
        {
            InitializeComponent();
        }

        private void USDChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (double.Parse(usdEntry.Text) >= 0)
                {
                    eurLabel.Text = (double.Parse(usdEntry.Text) * 1.075).ToString();
                }
                else
                {
                    DisplayAlert("Ошибка", "Введите положительное число", "OK");
                }
            }
            catch
            {
                DisplayAlert("Ошибка", "Можно вводить только числа", "OK");
            }
        }
    }
}