using Systems_Project_Spring_2023.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Systems_Project_Spring_2023.Models
{
    public class ItemKit
    {
        public List<Item> Items { get; set; }
        public List<Kit> Kits { get; set; }

        public ItemKit(List<Item> items, List<Kit> kits)
        {
            Items = items;
            Kits = kits;
        }

        /*public List<Item> FilterItemsByCategory(string category)
        {
            return Items.Where(i => i.Category == category).ToList();
        }

        public List<Kit> FilterKitsByPrice(double minPrice, double maxPrice)
        {
            return Kits.Where(k => k.Price >= minPrice && k.Price <= maxPrice).ToList();
        }*/
    }

}