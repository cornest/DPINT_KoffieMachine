using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain.Decorators
{
    public class ChocolateDrinkDecorator : BaseDrinkDecorator
    {
        private HotChocolate hotChocolate;

        public ChocolateDrinkDecorator(IDrink drink, bool isDeluxe) : base(drink)
        {
            hotChocolate = new HotChocolate();

            if (isDeluxe)
            {
                hotChocolate.MakeDeluxe();
            }

            this.Name = hotChocolate.GetNameOfDrink();
            this.Price = hotChocolate.Cost();
        }

        public override double GetPrice()
        {
            double price = base.GetPrice() + 0.5;
            return price;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);

            foreach (string buildStep in hotChocolate.GetBuildSteps())
            {
                log.Add(buildStep);
            }
        }

        public override void LogSelect(ICollection<string> log)
        {
            base.LogSelect(log);
            log.Add($"Selected {this.Name}, price: {this.GetPrice().ToString("C", CultureInfo.CurrentCulture)}");
        }
    }
}
