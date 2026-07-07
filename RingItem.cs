using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class RingItem : PassiveItem {
        public RingItem(string name, int magnitude) {
            Name = name;
            _magnitude = magnitude;
            SlotType = ItemSlotType.Ring;
        }
        protected override void ApplyStatBoost(Player player) {
            //throw new NotImplementedException();
        }

        protected override void RemoveStatBoost(Player player) {
            //throw new NotImplementedException();
        }
    }
}
