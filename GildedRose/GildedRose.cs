using System.Collections.Generic;
using System.Linq;

namespace csharpcore
{
    public class GildedRose
    {
        IList<Item> Items;
        IList<string> ItemsThatIncreaseQuality;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
            ItemsThatIncreaseQuality = new List<string>
            {
                "Aged Brie",
                "Backstage passes to a TAFKAL80ETC concert",
            };
        }

        public void UpdateQuality()
        {
            Items.Where(item => item.Name != "Sulfuras, Hand of Ragnaros").ToList().ForEach(item=>
            {
                item.SellIn--;
                ChangeQuality(item);
            });
        }
        private void ChangeQuality(Item item)
        {
            if (ItemsThatIncreaseQuality.Contains(item.Name))
                IncreaseQuality(item);
            else
                DecreaseQuality(item);
        }
        private void IncreaseQuality(Item item)
        {
            int quality = (item.SellIn >= 0) ? 1 : 2;

            if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                if (item.SellIn < 5)
                    quality += 2;
                else if (item.SellIn < 10)
                    quality ++;

                if (item.SellIn < 0)
                {
                    item.Quality = 0;
                    return;
                }
            }

            item.Quality += quality;

            if (item.Quality > 50)
                item.Quality = 50;
        }
        private void DecreaseQuality(Item item)
        {
            int quality = (item.SellIn >= 0) ? 1 : 2;

            if (item.Name == "Conjured Mana Cake")
                quality *= 2;

            item.Quality -= quality;

            if (item.Quality < 0)
                item.Quality = 0;
        }
    }
}
