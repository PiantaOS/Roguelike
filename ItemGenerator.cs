using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class ItemGenerator {
        public ItemGenerator() { }

        public static List<PassiveItem> GeneratePassiveItems(int amount, int floor) {
            List<PassiveItem> items = new List<PassiveItem>();
            for (int i = 0; i < amount; i++) {
                int type = new Random().Next(0, Enum.GetNames(typeof(ItemSlotType)).Length);

                switch (type) {
                    case 0:
                        items.Add(new ChestplateItem(floor));
                        break;
                    case 1:
                        items.Add(new GreavesItem(floor));
                        break;
                    case 2:
                        items.Add(new RingItem(floor));
                        break;
                    case 3:
                        items.Add(new HelmetItem(floor));
                        break;
                    case 4:
                        items.Add(new GauntletsItem(floor));
                        break;
                    case 5:
                        items.Add(new ShieldItem(floor));
                        break;
                    case 6:
                        items.Add(new WeaponItem(floor));
                        break;
                }
            }

            return items;
        }

    }
}
