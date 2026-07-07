namespace Roguelike {
    internal class Player {
        private Vector2Int _currentPosition;
        private TileType _stoodOnTile;
        private int _health;
        public int MaxHealth { get; set; }
        public Inventory Inventory { get; set; }
        public int DamageReduction { get; set; }
        public int Damage { get; set; }
        //Can change with equipment
        public bool ReachedExit = false;
        public Player(Vector2Int position, TileType tileSpawnedOn, int health, int damage) {
            _currentPosition = position;
            _stoodOnTile = tileSpawnedOn;
            _health = health;
            Damage = damage;
            MaxHealth = health;
            Inventory = new Inventory(this);
        }

        public void TakeDamage(Grid grid, int damageAmount) {
            grid.WriteLog($"Took {damageAmount} damage");
            _health -= damageAmount - DamageReduction;
        }
        public bool IsAlive() {
            return _health > 0;
        }
        public bool ReadInput(char input, Grid grid) {
            switch (input) {
                case 'l':
                    return TryMove(new Vector2Int(1, 0), grid, grid.Enemies);
                case 'm':
                    return TryMove(new Vector2Int(0, 1), grid, grid.Enemies);
                case 'j':
                    return TryMove(new Vector2Int(-1, 0), grid, grid.Enemies);
                case 'i':
                    return TryMove(new Vector2Int(0, -1), grid, grid.Enemies);
                case 'k':
                    return true;
            }
            return false;
        }
        public int GetHealth() { return _health; }

        public void AddHealth(int amount) {
            _health += amount;
            _health = Math.Clamp(_health, 0, MaxHealth);
        }
        private bool TryMove(Vector2Int direction, Grid grid, List<Enemy> enemies) {
            TileType nextTile = grid.GetTileFromCoord(_currentPosition + direction);
            if (nextTile == TileType.Player) {
                return true;
            }
            if (nextTile == TileType.Enemy) {
                foreach (Enemy enemy in enemies) {
                    if (enemy.GetPosition() == _currentPosition + direction) enemy.TakeDamage(Damage, grid);
                }
                return true;
            }
            switch (nextTile) {
                case TileType.Empty:
                    break;
                case TileType.Wall:
                    return false;
                //case TileType.Exit:
                //    grid =
                case TileType.Floor:
                case TileType.Corridor:
                case TileType.Door:
                    Move(_currentPosition + direction, nextTile, grid);
                    return true;
                case TileType.Item:
                    grid.WriteLog("Picked up an item");
                    Inventory.PickupItem(grid.TakeItemFromCoord(_currentPosition + direction));
                    return true;
                case TileType.Exit:
                    ReachedExit = true;
                    return true;
                case TileType.Heal:
                    grid.WriteLog("Healed 25 health");
                    AddHealth(25);
                    Move(_currentPosition + direction, TileType.Floor, grid);
                    return true;
                case TileType.Player:
                case TileType.Enemy:
                default:
                    break;
            }
            return false;
        }
        private void Move(Vector2Int targetPosition, TileType nextTile, Grid grid) {
            grid.SetTileAtCoord(_currentPosition, _stoodOnTile);
            _stoodOnTile = nextTile;

            _currentPosition = targetPosition;
            grid.SetTileAtCoord(targetPosition, TileType.Player);
        }

        public void SetNewPosition(Vector2Int newPos) { _currentPosition = newPos; }
        public Vector2Int GetPosition() { return _currentPosition; }
    }
}
