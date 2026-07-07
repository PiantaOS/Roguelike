using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class RingItem : PassiveItem {
        protected List<string> _materialNames = [
            "Iron",
            "Silver",
            "Golden",
            "Wooden",
            "Mythril",
            "Titanium",
            "Copper",
            "Diamond",
            "Steel"
        ];

        protected List<string> _verbs = [
            "Hunting",
            "Honor",
            "Fortitude",
            "Wrath",
            "Greed",
            "Pride",
            "Sin",
            "Destruction",
            "Glory",
            "Heroism",
            "Fate",
            "Purity",
            "Power",
            "Life",
            "Balance",
            "Reality",
            "Energy",
            "Protection",
            "Eminence",
            "Valor",
            "Truth",
            "Deception",
            "Fury",
            "Zeal",
            "Precision",
            "Battle",
            "War",
            "Mystery",
            "Drive",
            "Lack",
            "Imbalance",
            "Luck",
            "Earth",
            "Stone",
            "the Forge",
            "Curiosity",
        ];
        public RingItem(string name, int magnitude) {
            Name = name;
            _magnitude = magnitude;
            SlotType = ItemSlotType.Ring;
        }

        public RingItem(int floor) {
            SetRandomName();
            SetRandomStatMagnitude(floor);
            SlotType = ItemSlotType.Ring;
        }
        protected override void ApplyStatBoost(Player player) {
            player.MaxHealth += _magnitude;
            player.AddHealth(_magnitude);
        }

        protected override void RemoveStatBoost(Player player) {
            player.MaxHealth -= _magnitude;
            player.AddHealth(-_magnitude); // Probably shoulnd't do this
        }

        public override void SetRandomStatMagnitude(int floor) {
            int baseStat = (floor + 1) * 7;
            baseStat += new Random().Next(-floor * 2, floor * 3);
            _magnitude = baseStat;
        }

        public override void SetRandomName() {
            StringBuilder s = new StringBuilder();
            Random r = new Random();
            s.Append(_materialNames[r.Next(_materialNames.Count)]);
            s.Append(" Ring of ");
            s.Append(_verbs[r.Next(_verbs.Count)]);

            Name = s.ToString();
        }
    }
}
