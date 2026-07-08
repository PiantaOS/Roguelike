using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class ChestplateItem : PassiveItem {
        protected List<string> _materialNames = [
            "Leather", "Iron", "Silver", "Golden", "Wooden", "Mythril", "Titanium", "Copper", "Diamond", "Chainmail",
            "Steel"
        ];

        protected List<string> _verbs = [
            "Hunting", "Honor", "Fortitude", "Wrath", "Greed", "Pride", "Sin", "Destruction", "Glory", "Heroism",
            "Fate", "Purity", "Power", "Life", "Balance", "Reality", "Energy", "Protection", "Eminence", "Valor",
            "Truth", "Deception", "Fury", "Zeal", "Precision", "Battle", "War", "Mystery", "Drive", "Lack", "Imbalance",
            "Luck", "Earth", "Stone", "the Forge"
        ];
        public ChestplateItem(string name, int magnitude) {
            Name = name;
            _magnitude = magnitude;
            SlotType = ItemSlotType.Chestplate;
        }

        public ChestplateItem(int floor) {
            SlotType = ItemSlotType.Chestplate;
            SetRandomStatMagnitude(floor);
            SetRandomName();
        }
        protected override void ApplyStatBoost(Player player) {
            player.DamageReduction += _magnitude;
        }

        protected override void RemoveStatBoost(Player player) {
            player.DamageReduction -= _magnitude;
        }

        public override void SetRandomStatMagnitude(int floor) {
            int baseStat = (floor + 1) * 5;
            baseStat += new Random().Next(-floor * 4, floor * 5);
            _magnitude = baseStat;
        }

        public override void SetRandomName() {
            StringBuilder s = new StringBuilder();
            Random r = new Random();
            s.Append(_materialNames[r.Next(_materialNames.Count)]);
            s.Append(" Chestplate of ");
            s.Append(_verbs[r.Next(_verbs.Count)]);

            Name = s.ToString();
        }
    }
}
