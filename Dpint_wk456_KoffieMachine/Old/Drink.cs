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
            this.Price = 1.0;
            this.DrinkStrength = drinkStrength;
            this.SugarPrice = 0.1;
            this.MilkPrice = 0.15;
         }

        public virtual string Name { get; set; }

        public bool HasSugar { get; set; }
        public Amount SugarAmount { get; set; }
        public Amount MilkAmount { get; set; }
        public bool HasMilk { get; set; }
        public double Price { get; set; }
        public Strength DrinkStrength { get; set; }
        public double SugarPrice { get; set; }
        public double MilkPrice { get; set; }

        public double GetPrice()
        {
            return this.Price;
        }

        public void LogDrinkMaking(ICollection<string> log)
        {
           log.Add($"Making {this.Name}...");
           log.Add($"Heating up...");
        }

        public void LogSelect(ICollection<string> log)
        {
            return;
        }
    }
}
