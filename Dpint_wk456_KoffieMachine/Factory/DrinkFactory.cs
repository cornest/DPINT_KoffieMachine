using KoffieMachineDomain;
using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dpint_wk456_KoffieMachine.Factory
{
    public class DrinkFactory
    {
        public Dictionary<string, string> DrinkOptions { get; private set; }

        public DrinkFactory()
        {
            DrinkOptions = new Dictionary<string, string>();
            DrinkOptions["Coffee"] = "Coffee description";
            DrinkOptions["Café au Lait"] = "Café au lait description";
            DrinkOptions["Capuccino"] = "Cappuccino description";
            DrinkOptions["Espresso"] = "Esspresso description";
            DrinkOptions["Wiener Melange"] = "Wiener Melange description";
            DrinkOptions["Chocolate"] = "Hot chocolate ";
            DrinkOptions["Chocolate Deluxe"] = "Hot chocolate deluxe";
            DrinkOptions["Tea"] = "Hot water";

        }

        public IDrink CreateDrink(string option, bool hasSugar, bool hasMilk, Amount sugarAmount, Amount milkAmount, Strength drinkStrength, string blendName = "")
        {
            IDrink newDrink = new Drink(hasSugar, hasMilk, sugarAmount, milkAmount, drinkStrength);

            switch (option)
            {
                    case "Coffee":
                        newDrink = new CoffeeDrinkDecorator(newDrink);
                        break;
                    case "Café au Lait":
                        newDrink = new CafeAuLaitDrinkDecorator(newDrink);
                        break;
                    case "Capuccino":
                        newDrink = new CappuccinoDrinkDecorator(newDrink);
                        break;
                    case "Espresso":
                        newDrink = new EspressoDrinkDecorator(newDrink);
                        break;
                    case "Wiener Melange":
                        newDrink = new WienerMelangeDrinkDecorator(newDrink);
                        break;
                    case "Chocolate":
                        newDrink = new ChocolateDrinkDecorator(newDrink, false);
                        break;
                    case "Chocolate Deluxe":
                        newDrink = new ChocolateDrinkDecorator(newDrink, true);
                        break;
                    case "Tea":
                        newDrink = new TeaDrinkDecorator(newDrink, blendName);
                        break;
                    default:
                        break;
                }
            return newDrink;
        }
    }
}
