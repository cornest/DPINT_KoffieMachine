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

        public const string COFFEE = "Coffee";
        public const string CAFE_AU_LAIT = "Café au Lait";
        public const string CAPPUCCINO = "Capuccino";
        public const string ESPRESSO = "Espresso";
        public const string WIENER_MELANGE = "Wiener Melange";

        public DrinkFactory()
        {
            DrinkOptions = new Dictionary<string, string>();
            DrinkOptions[COFFEE] = "Coffee description";
            DrinkOptions[CAFE_AU_LAIT] = "Café au lait description";
            DrinkOptions[CAPPUCCINO] = "Cappuccino description";
            DrinkOptions[ESPRESSO] = "Esspresso description";
            DrinkOptions[WIENER_MELANGE] = "Wiener Melange description";
        }

        public IDrink CreateDrink(string option, bool hasSugar, bool hasMilk, Amount sugarAmount, Amount milkAmount, Strength drinkStrength)
        {
            IDrink newDrink = new Drink(hasSugar, hasMilk, sugarAmount, milkAmount, drinkStrength);

            switch (option)
            {
             case COFFEE:
                        newDrink = new CoffeeDrinkDecorator(newDrink);
                        break;
                    case CAFE_AU_LAIT:
                        newDrink = new CafeAuLaitDrinkDecorator(newDrink);
                        break;
                    case CAPPUCCINO:
                        newDrink = new CappuccinoDrinkDecorator(newDrink);
                        break;
                    case ESPRESSO:
                        newDrink = new EspressoDrinkDecorator(newDrink);
                        break;
                    case WIENER_MELANGE:
                        newDrink = new WienerMelangeDrinkDecorator(newDrink);
                        break;
                    default:
                        break;
                }
            return newDrink;
        }
    }
}
