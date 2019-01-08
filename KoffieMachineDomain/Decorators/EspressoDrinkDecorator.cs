using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Decorators
{
    public class EspressoDrinkDecorator : BaseDrinkDecorator
    {
        public EspressoDrinkDecorator(IDrink drink) : base(drink)
        {
            this.Name = "Espresso";
        }

        public override double GetPrice()
        {
            double price = Price;

            if (HasSugar)
            {
                price += SugarPrice;
            }

            if (HasMilk)
            {
                price += MilkPrice;
            }


            return price;
        }



        public override Amount SugarAmount { get => base.SugarAmount; set => base.SugarAmount = value; }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {Strength.Strong}.");
            log.Add($"Setting coffee amount to {Amount.Few}.");
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
            if (HasMilk && HasSugar)
            {
                log.Add($"Selected {Name } with sugar and milk, price: { GetPrice().ToString("C", CultureInfo.CurrentCulture)}");
            }
            else if (HasSugar)
            {
                log.Add($"Selected { Name} with sugar, price: { GetPrice().ToString("C", CultureInfo.CurrentCulture)}");
            }
            else if (HasMilk)
            {
                log.Add($"Selected { Name} with milk, price: { GetPrice().ToString("C", CultureInfo.CurrentCulture)}");
            }
            else
            {
                log.Add($"Selected { Name}, price: { GetPrice().ToString("C", CultureInfo.CurrentCulture)}");
            }
        }
    }
}
