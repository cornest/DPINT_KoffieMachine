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
    public class Card : IPayment
    {
        Dictionary<string, double> cashOnCards { get; set; }

        public Card()
        {
            cashOnCards = new Dictionary<string, double>();
            cashOnCards["Arjen"] = 5.0;
            cashOnCards["Bert"] = 3.5;
            cashOnCards["Chris"] = 7.0;
            cashOnCards["Daan"] = 6.0;
        }

        public double remainingPriceToPay(IDrink selectedDrink, string selectedPaymentCardUsername, double remainingPriceToPay, ObservableCollection<string> logText, double insertedMoney)
        {
            insertedMoney = cashOnCards[selectedPaymentCardUsername];
            if (remainingPriceToPay <= insertedMoney)
            {
                cashOnCards[selectedPaymentCardUsername] = insertedMoney - remainingPriceToPay;
                remainingPriceToPay = 0;
            }
            else // Pay what you can, fill up with coins later.
            {
                cashOnCards[selectedPaymentCardUsername] = 0;

                remainingPriceToPay -= insertedMoney;
            }

            logText.Add($"Inserted {insertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {remainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");

            return remainingPriceToPay;
        }


        public Dictionary<string, double> GetCashOnCards()
        {
            return this.cashOnCards;
        }
    }
}
