using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Interface
{
    public interface IDrink
    {
        string Name { get; set; }

        bool HasSugar { get; set; }
        Amount SugarAmount { get; set; }
        Amount MilkAmount { get; set; }

        bool HasMilk { get; set; }


        double Price { get; set; }

        double GetPrice();

        Strength DrinkStrength { get; set; }



        void LogDrinkMaking(ICollection<string> log);

    }
}
