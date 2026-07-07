namespace Roguelike {
    internal abstract class PassiveItem {
        protected bool _equipped = false;
        protected int _magnitude; // Amount item does something eg. Gives 5 more damage or gives 3 armor reduction
        public string Name { get; protected set; }
        protected PassiveItem(string name, int magnitude) {
            Name = name;
            _magnitude = magnitude;
        }

        protected PassiveItem() {
        }


        public ItemSlotType SlotType { get; protected set; }

        public void ToggleEquipped(Player player) {
            if(_equipped) {
                _equipped = false;
                RemoveStatBoost(player);
            }
            else {
                _equipped = true;
                ApplyStatBoost(player);
            }
        }
        public bool IsEquipped() => _equipped;
        protected abstract void ApplyStatBoost(Player player);

        protected abstract void RemoveStatBoost(Player player);
        public abstract void SetRandomStatMagnitude(int floor);
        public abstract void SetRandomName();
    }
}
