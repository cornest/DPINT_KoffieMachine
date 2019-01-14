using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Interface
{
    public interface IPayment
    {
        double remainingPriceToPay(IDrink selectedDrink, string selectedPaymentCardUsername, double remainingPriceToPay, ObservableCollection<string> logText, double insertedMoney);
        Dictionary<string, double> GetCashOnCards();

    }
}
