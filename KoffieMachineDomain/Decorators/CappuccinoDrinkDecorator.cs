using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Decorators
{
    public class CappuccinoDrinkDecorator : BaseDrinkDecorator
    {
        public CappuccinoDrinkDecorator(IDrink drink) : base(drink)
        {
            this.Name = "Capuccino";
        }

        public override double GetPrice()
        {
            double price = base.GetPrice() + 0.5;

            if (HasSugar)
            {
                price += SugarPrice;
            }

            return price;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {DrinkStrength}.");
            log.Add("Filling with coffee...");

            if (HasSugar)
            {
                log.Add($"Setting sugar amount to {SugarAmount}.");
                log.Add("Adding sugar...");
            }

            log.Add("Creaming milk...");
            log.Add("Adding milk to coffee...");
            log.Add($"Finished making {Name}");
        }

        public override void LogSelect(ICollection<string> log)
        {
            base.LogSelect(log);

            if (HasSugar)
            {
                log.Add($"Selected {this.Name}, with sugar, price: {this.GetPrice().ToString("C", CultureInfo.CurrentCulture)}");
            }
            else
            {
                log.Add($"Selected {this.Name}, price: {this.GetPrice().ToString("C", CultureInfo.CurrentCulture)}");
            }
        }
    }
}
