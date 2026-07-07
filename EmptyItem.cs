using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class EmptyItem : PassiveItem {
        public EmptyItem() {
            return;
        }
        protected override void ApplyStatBoost(Player player) {
            return;
        }

        protected override void RemoveStatBoost(Player player) {
            return;
        }

        public override void SetRandomStatMagnitude(int floor) {
            return;
        }

        public override void SetRandomName() {
            return;
        }
    }
}
