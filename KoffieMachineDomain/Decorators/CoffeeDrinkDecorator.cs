using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Decorators
{
    public class CoffeeDrinkDecorator : BaseDrinkDecorator
    {
        public CoffeeDrinkDecorator(IDrink drink) : base(drink)
        {
            this.Name = "Coffee";
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

        public override void LogSelect(ICollection<string> log)
        {
            base.LogSelect(log);
            if(HasMilk && HasSugar)
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
