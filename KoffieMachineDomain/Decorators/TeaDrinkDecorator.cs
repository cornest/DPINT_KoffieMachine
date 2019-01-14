using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoffieMachineDomain.Interface;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain.Decorators
{
    public class TeaDrinkDecorator : BaseDrinkDecorator
    {
        private Tea tea;

        public TeaDrinkDecorator(IDrink drink, string blendName) : base(drink)
        {
            tea = new Tea();
            this.Name = "Tea (" + blendName + ")";
            this.Price = Tea.Price;
        }

        public override double GetPrice()
        {
            double price = Price;

            if (HasSugar)
            {
                price += SugarPrice;
            }

            return price;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Heat up water");
            log.Add($"Adding blend");

            if (HasSugar)
            {
                log.Add($"Setting sugar amount to {SugarAmount}.");
                log.Add("Adding sugar...");
            }
        }

        public override void LogSelect(ICollection<string> log)
        {
            log.Add($"Selected {this.Name}, price: {this.GetPrice().ToString("C", CultureInfo.CurrentCulture)}");
        }

    }
}
