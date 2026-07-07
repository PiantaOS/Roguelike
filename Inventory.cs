using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class Inventory {
        private readonly Dictionary<ItemSlotType, PassiveItem> _passiveItems = new Dictionary<ItemSlotType, PassiveItem>();
        private readonly Player _player;
        private readonly List<PassiveItem> _heldPassives = [];
        public Inventory(Player player) {
            _passiveItems.Add(ItemSlotType.Chestplate, new EmptyItem());
            _passiveItems.Add(ItemSlotType.Ring, new EmptyItem());
            _passiveItems.Add(ItemSlotType.Greaves, new EmptyItem());
            _passiveItems.Add(ItemSlotType.Helmet, new EmptyItem());
            _passiveItems.Add(ItemSlotType.Weapon, new EmptyItem());
            _passiveItems.Add(ItemSlotType.Gauntlets, new EmptyItem());
            _passiveItems.Add(ItemSlotType.Shield, new EmptyItem());
            _player = player;

            //_heldPassives.Add(new ChestplateItem("Leather Armor", 3));
           // SwapEquippedItem(ItemSlotType.Chestplate, _heldPassives[0]);
            //_heldPassives.Add(new RingItem("Ring of Fortitude", 10));
           // _heldPassives.Add(new ChestplateItem("Iron Armor", 4));
        }

        public PassiveItem SwapEquippedItem(ItemSlotType slot, PassiveItem newItem) {
            PassiveItem oldItem = _passiveItems[slot];
            oldItem.ToggleEquipped(_player);

            newItem.ToggleEquipped(_player);
            _passiveItems[slot] = newItem;

            return oldItem;
        }

        public void PickupItem(PassiveItem item) {
            _heldPassives.Add(item);
        }

        public string RenderInventory(int highlightedItem) {
            StringBuilder s = new StringBuilder();
            highlightedItem = Math.Clamp(highlightedItem, 0, _heldPassives.Count - 1); 
            s.Append("Equipment: ");
            s.AppendLine();
            for (int i = 0; i < _heldPassives.Count; i++) {
                s.Append(i == highlightedItem ? '*' : '-');

                s.Append(_heldPassives[i].Name);

                s.Append(_heldPassives[i].IsEquipped() ? "  E" : "  U");
                s.AppendLine();
            }
            s.AppendLine();
            s.Append("Stats:");
            s.AppendLine();
            s.Append("Damage: ");
            s.Append(_player.Damage);
            s.AppendLine();
            s.Append("Damage Reduction: ");
            s.Append(_player.DamageReduction);
            s.AppendLine();
            s.Append("Max Health: ");
            s.Append(_player.MaxHealth);
            s.AppendLine();

            return s.ToString();
        }

        public void EquipItemAtIndex(int index) {
            index = Math.Clamp(index, 0, _heldPassives.Count);
            PassiveItem item = _heldPassives[index];

            if (_passiveItems[item.SlotType] == item) {
                SwapEquippedItem(item.SlotType, new EmptyItem());
                return;
            }

            SwapEquippedItem(item.SlotType, item);
        }
    }
}
