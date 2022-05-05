using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Helper
{
    internal class CategoryHelper
    {
        private static Dictionary<string, string> map = new Dictionary<string, string>();

        public static void PopulateMap()
        {
            map.Add("dough", "pâte");
            map.Add("base", "base");
            map.Add("cheese", "fromage");
            map.Add("sauce", "sauce");
            map.Add("meat", "viande");
            map.Add("accompaniments", "accompagnements");
            map.Add("drink", "boisson");
            map.Add("dessert", "dessert");

            map.Add("pâte", "dough");
            map.Add("fromage", "cheese");
            map.Add("viande", "meat");
            map.Add("accompagnements", "accompaniments");
            map.Add("boisson", "drink");
        }


        public static string TranslateCategory(string category)
        {
            string result = map.GetValueOrDefault(category.ToLower());
            return result;
        }

    }
}
