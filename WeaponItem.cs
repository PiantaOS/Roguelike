using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class WeaponItem : PassiveItem {
        protected List<string> _materialNames = [
            "Leather",
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
            "the Forge"
        ];

        protected List<string> _weaponTypes = [
            "Sword", "Greatsword", "Cutlass", "Polearm", "Halberd", "Axe", "Hammer", "Dagger", "Knife", "Blade", "Club",
            "Staff", "Katana", "Mace", "Spear", "Hatchet", "Falchion", "Morningstar", "Pick", "Scythe", "Kukri",
            "Shovel", "Brass Knuckles"
        ];
        public WeaponItem(string name, int magnitude) {
            Name = name;
            _magnitude = magnitude;
            SlotType = ItemSlotType.Weapon;
        }

        public WeaponItem(int floor) {
            SlotType = ItemSlotType.Weapon;
            SetRandomStatMagnitude(floor);
            SetRandomName();
        }
        protected override void ApplyStatBoost(Player player) {
            player.Damage += _magnitude;
        }

        protected override void RemoveStatBoost(Player player) {
            player.Damage -= _magnitude;
        }

        public override void SetRandomStatMagnitude(int floor) {
            int baseStat = (floor + 1) * 25;
            baseStat += new Random().Next(-floor * 10, floor * 10);
            _magnitude = baseStat;
        }

        public override void SetRandomName() {
            StringBuilder s = new StringBuilder();
            Random r = new Random();
            s.Append(_materialNames[r.Next(_materialNames.Count)]);
            s.Append(" ");
            s.Append(_weaponTypes[r.Next(_weaponTypes.Count)]);
            s.Append(" of ");
            s.Append(_verbs[r.Next(_verbs.Count)]);

            Name = s.ToString();
        }
    }
}
