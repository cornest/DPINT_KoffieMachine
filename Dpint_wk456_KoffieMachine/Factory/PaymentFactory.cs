
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoffieMachineDomain.Interface;
using KoffieMachineDomain.Payments;

namespace Dpint_wk456_KoffieMachine.Factory
{
    public class PaymentFactory
    {
        public Dictionary<string, IPayment> PaymentOptions { get; set; }

        public PaymentFactory()
        {
            this.PaymentOptions = new Dictionary<string, IPayment>();
            this.PaymentOptions.Add("CASH", new Cash());
            this.PaymentOptions.Add("CARD", new Card());
        }

        public IPayment Pay(string name)
        {
            return this.PaymentOptions[name];   
        } 
    }
}
