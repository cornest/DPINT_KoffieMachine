using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Decorators
{
    public class CafeAuLaitDrinkDecorator : BaseDrinkDecorator
    {
        public CafeAuLaitDrinkDecorator(IDrink drink) : base(drink)
        {
            this.Name = "Café au lait";
        }

        public override double GetPrice() {          
            double price = base.GetPrice() + 0.5;
            return price;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add("Filling half with coffee...");
            log.Add("Filling other half with milk...");
            log.Add($"Finished making {Name}");
        }

        public override void LogSelect(ICollection<string> log)
        {
            base.LogSelect(log);
            log.Add($"Selected {this.Name}, price: {this.GetPrice().ToString("C", CultureInfo.CurrentCulture)}");
        }
    }
}
