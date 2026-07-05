namespace Roguelike {
    internal class Player(Vector2Int position, TileType tileSpawnedOn, int health, int damage) {
        private Vector2Int _currentPosition = position;
        private TileType _stoodOnTile = tileSpawnedOn;
        private int _health = health;
        private readonly int _maxHealth = health;

        private readonly int _damage = damage;
        //Can change with equipment

        public void TakeDamage(int damageAmount) {
            _health -= damageAmount;
        }
        public bool IsAlive() {
            return _health > 0;
        }
        public bool ReadInput(char input, Grid grid) {
            switch (input) {
                case 'l':
                    return TryMove(new Vector2Int(1, 0), grid, grid.Enemies); ;
                case 'm':
                    return TryMove(new Vector2Int(0, 1), grid, grid.Enemies); ;
                case 'j':
                    return TryMove(new Vector2Int(-1, 0), grid, grid.Enemies); ;
                case 'i':
                    return TryMove(new Vector2Int(0, -1), grid, grid.Enemies); ;
                case 'k':
                    return true;
            }
            return false;
        }
        public int GetHealth() { return _health; }
        public int GetMaxHealth() { return _maxHealth; }
        private bool TryMove(Vector2Int direction, Grid grid, List<Enemy> enemies) {
            TileType nextTile = grid.GetTileFromCoord(_currentPosition + direction);
            if (nextTile == TileType.Player) {
                return true;
            }
            if (nextTile == TileType.Enemy) {
                foreach (Enemy enemy in enemies) {
                    if (enemy.GetPosition() == _currentPosition + direction) enemy.TakeDamage(_damage, grid);
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
                case TileType.Player:
                case TileType.Enemy:
                case TileType.Exit:
                default:
                    return false;
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
