using KoffieMachineDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Decorators
{
    public abstract class BaseDrinkDecorator : IDrink
    {
        protected IDrink Drink;

        public BaseDrinkDecorator(IDrink drink)
        {
            this.Drink = drink;
        }

        public virtual string Name
        {
            get { return Drink.Name; }
            set { Drink.Name = value; }
        }

        public virtual double Price
        {
            get { return Drink.Price;  }
            set { Drink.Price = value;  }
        }

        public virtual Strength DrinkStrength
        {
            get { return Drink.DrinkStrength; }
            set { Drink.DrinkStrength = value; }
        }

        public virtual Amount SugarAmount
        {
            get { return Drink.SugarAmount; }
            set { Drink.SugarAmount = value; }
        }

        public virtual Amount MilkAmount
        {
            get { return Drink.MilkAmount; }
            set { Drink.MilkAmount = value; }
        }

        public virtual bool HasSugar
        {   get { return Drink.HasSugar; }
            set { Drink.HasSugar = value; }
        }

        public virtual double SugarPrice
        {
            get { return Drink.SugarPrice; }
            set { Drink.SugarPrice = value;  }
        }

        public virtual double MilkPrice
        {
            get { return Drink.MilkPrice; }
            set { Drink.MilkPrice = value; }
        }

        public virtual bool HasMilk
        {
            get { return Drink.HasMilk; }
            set { Drink.HasMilk = value; }
        }

        public virtual double GetPrice()
        {
            return this.Drink.GetPrice();
        }

        public virtual void LogDrinkMaking(ICollection<string> log)
        {
            Drink.LogDrinkMaking(log);
        }

        public virtual void LogSelect(ICollection<string> log)
        {
            Drink.LogSelect(log);
        }
    }
}
