using System.Collections.Generic;
using System.Linq;

namespace csharpcore
{
    public class GildedRose
    {
        IList<Item> Items;
        IList<string> ItemsThatIncreaseQuality;
        IList<string> ItemsThatDecreaseQuality;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
            ItemsThatIncreaseQuality = new List<string>
            {
                "Aged Brie",
                "Backstage passes to a TAFKAL80ETC concert"
            };
            ItemsThatDecreaseQuality = new List<string>
            {
                "+5 Dexterity Vest",
                "Elixir of the Mongoose",
                "Conjured Mana Cake"
            };
        }

        public void UpdateQuality()
        {
            var itemsWithoutSulfuras = Items.Where(item => !IsSulfuras(item)).ToList();

            itemsWithoutSulfuras.ForEach(item =>
            {
                ChangeQuality(item);
                item.SellIn--;
            });

            itemsWithoutSulfuras.Where(item => item.Quality < 0 || (IsBackstage(item) && item.SellIn < 0))
                .ToList().ForEach(item => item.Quality = 0);

            itemsWithoutSulfuras.Where(item => item.Quality > 50).ToList().ForEach(item => item.Quality = 50);
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
            var quality = GetBaseQuality(item);

            if (IsBackstage(item))
            {
                if (item.SellIn <= 5)
                    quality += 2;
                else if (item.SellIn <= 10)
                    quality++;
            }

            item.Quality += quality;
        }
        private void DecreaseQuality(Item item)
        {
            var quality = GetBaseQuality(item);

            if (IsConjured(item))
                quality *= 2;

            item.Quality -= quality;
        }
        private int GetBaseQuality(Item item)
        {
            return (item.SellIn > 0) ? 1 : 2;
        }
        private bool IsSulfuras(Item item)
        {
            return item.Name == "Sulfuras, Hand of Ragnaros";
        }
        private bool IsBackstage(Item item)
        {
            return item.Name == ItemsThatIncreaseQuality[1];
        }
        private bool IsConjured(Item item)
        {
            return item.Name == ItemsThatDecreaseQuality[2];
        }
    }
}
