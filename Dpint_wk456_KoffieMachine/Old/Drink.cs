using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class Drink : IDrink
    {
        public Drink(bool hasSugar, bool hasMilk, Amount sugarAmount, Amount milkAmount, Strength drinkStrength)
        {
            this.HasSugar = hasSugar;
            this.HasMilk = hasMilk;
            this.MilkAmount = milkAmount;
            this.SugarAmount = sugarAmount;
            this.Price = BaseDrinkPrice;
            this.DrinkStrength = drinkStrength;
         }


        public static readonly double SugarPrice = 0.1;
        public static readonly double MilkPrice = 0.15;
        protected const double BaseDrinkPrice = 1.0;
        public virtual string Name { get; set; }

        public bool HasSugar { get; set; }
        public Amount SugarAmount { get; set; }
        public Amount MilkAmount { get; set; }
        public bool HasMilk { get; set; }
        public double Price { get; set; }
        public Strength DrinkStrength { get; set; }

        public double GetPrice()
        {
            return this.Price;
        }

        public void LogDrinkMaking(ICollection<string> log)
        {
           log.Add($"Making {this.Name}...");
           log.Add($"Heating up...");
        }
    }
}
