using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zd4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditCalc : ContentPage
    {
        public CreditCalc()
        {
            InitializeComponent();
        }
        private void SliderValueChange(object sender, ValueChangedEventArgs e)
        {
            SliderLabel.Text = $"{Slider.Value}%";

            if (LoanEntry.Text != "" && MonthEntry.Text != "")
            {
                Calculation(LoanEntry.Text, MonthEntry.Text, PaymentTypePicker.SelectedIndex, Slider.Value);
            }
            else
            {
                DisplayAlert("Ошибка", "Заполните все поля", "");

                MonthlyPaymentLabel.Text = "Ежемесячный платеж: ";
                TotalLabel.Text = "Общая сумма: ";
                OverpaymentLabel.Text = "Переплата: ";
            }
        }

        private void Calculation(string loanAmount, string loanTermInMonths, int PickerPayment, double interestRate)
        {
            try
            {
                if (Convert.ToDouble(loanTermInMonths) > 0 && Convert.ToDouble(loanAmount) > 0)
                {
                    double monthlyInterestRate = interestRate / 1200;
                    double annuityFactor = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, int.Parse(loanTermInMonths)) / (Math.Pow(1 + monthlyInterestRate, int.Parse(loanTermInMonths)) - 1); 
                    double annuityPayment = Math.Round(double.Parse(loanAmount) * annuityFactor, 2); 

                    MonthlyPaymentLabel.Text = $"Ежемесячный платеж: {annuityPayment}";
                    TotalLabel.Text = $"Общая сумма: {Math.Round(annuityPayment, 2) * int.Parse(loanTermInMonths)}";
                    OverpaymentLabel.Text = $"Переплата: {Math.Round(Math.Round(annuityPayment, 2) * int.Parse(loanTermInMonths) - Math.Round(double.Parse(loanAmount), 2), 2)}";
                }
                else
                {
                    this.DisplayAlert("Ошибка", "Введите положительное число", "");
                    MonthlyPaymentLabel.Text = "Ежемесячный платеж: NULL";
                    TotalLabel.Text = "Общая сумма: NULL";
                    OverpaymentLabel.Text = "Переплата: NULL";
                }
            }
            catch
            {
                this.DisplayAlert("Ошибка", "Можно вводить только числа", "");
            }
        }
    }
}