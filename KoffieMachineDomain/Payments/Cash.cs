using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Payments
{
    public class Cash : IPayment
    {
        public Dictionary<string, double> cashOnCards { get; set; }

        public Cash()
        {
            cashOnCards = new Dictionary<string, double>();
            cashOnCards["Arjen"] = 5.0;
            cashOnCards["Bert"] = 3.5;
            cashOnCards["Chris"] = 7.0;
            cashOnCards["Daan"] = 6.0;
        }



        public double remainingPriceToPay(IDrink selectedDrink, string selectedPaymentCardUsername, double remainingPriceToPay, ObservableCollection<string> logText ,double insertedMoney = 0)
        {
            remainingPriceToPay = Math.Max(Math.Round(remainingPriceToPay - insertedMoney, 2), 0);
            logText.Add($"Inserted {insertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {remainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");

            return remainingPriceToPay;
        }


        public Dictionary<string, double> GetCashOnCards()
        {
            return this.cashOnCards;
        }
    }
}
