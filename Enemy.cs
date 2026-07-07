namespace Roguelike {
    internal class Enemy(Vector2Int position, TileType tileSpawnedOn, int health, int damage) {
        private Vector2Int _currentPosition = position;
        private TileType _stoodOnTile = tileSpawnedOn;
        private int _health = health;
        public bool IsActive { get; private set; }
        private readonly int _damage = damage;
        public void TakeDamage(int damageAmount, Grid grid) {
            _health -= damageAmount;
        }
        public bool IsAlive() {
            return _health > 0;
        }

        public void CheckIfSeen(Grid grid) {
            if (grid.GridVision[_currentPosition.x, _currentPosition.y]) IsActive = true;
        }
        public void RemoveSelf(Grid grid) {
            grid.SetTileAtCoord(_currentPosition, _stoodOnTile);
        }
        public Vector2Int GetPosition() { return _currentPosition; }
        public void CalculateMoveBasic(Player player, Grid grid) {
            Vector2Int direction = player.GetPosition() - _currentPosition;
            if (Math.Abs(direction.x) > Math.Abs(direction.y)) {
                if (TryMove(new Vector2Int(direction.Clamp().x, 0), player, grid)) { return; }
            }
            if (TryMove(new Vector2Int(0, direction.Clamp().y), player, grid)) { return; }
            TryMove(Vector2Int.Zero, player, grid);
        }
        private bool TryMove(Vector2Int direction, Player player, Grid grid) {
            TileType nextTile = grid.GetTileFromCoord(_currentPosition + direction);
            if (nextTile == TileType.Enemy) {
                return false;
            }
            if (nextTile == TileType.Player) {
                player.TakeDamage(grid, _damage);
                return true;
            }
            switch (nextTile) {
                case TileType.Empty:
                    break;
                case TileType.Enemy:
                    return false;
                case TileType.Wall:
                    return false;
                case TileType.Floor:
                case TileType.Corridor:
                case TileType.Door:
                    Move(_currentPosition + direction, nextTile, grid);
                    return true;
            }
            return false;
        }
        private void Move(Vector2Int targetPosition, TileType nextTile, Grid grid) {
            grid.SetTileAtCoord(_currentPosition, _stoodOnTile);
            _stoodOnTile = nextTile;

            _currentPosition = targetPosition;
            grid.SetTileAtCoord(targetPosition, TileType.Enemy);
        }
    }
}
