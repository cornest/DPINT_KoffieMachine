using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
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

        public override double Price { get => base.Price; set => base.Price = value; }

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
    }
}
