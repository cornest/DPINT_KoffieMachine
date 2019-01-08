using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Decorators
{
    public class WienerMelangeDrinkDecorator : BaseDrinkDecorator
    {
        public WienerMelangeDrinkDecorator(IDrink drink) : base(drink)
        {
            this.Name = "Wiener Melange";
        }

        public override double GetPrice()
        {
            double price = base.GetPrice() * 2;
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

            if (HasMilk)
            {
                log.Add($"Setting milk amount to {MilkAmount}.");
                log.Add("Adding milk...");
            }

            log.Add($"Finished making {Name}");
        }
    }
}
