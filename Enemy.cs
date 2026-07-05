namespace Roguelike {
    internal class Enemy {
        Vector2Int currentPosition;
        TileType stoodOnTile;
        int health;
        int damage;
        public Enemy(Vector2Int position, TileType tileSpawnedOn, int health, int damage) {
            currentPosition = position;
            stoodOnTile = tileSpawnedOn;
            this.health = health;
            this.damage = damage;
        }
        public void TakeDamage(int damage, Grid grid) {
            health -= damage;
        }
        public bool IsAlive() {
            if (health <= 0) return false;
            return true;
        }
        public void RemoveSelf(Grid grid) {
            grid.SetTileAtCoord(currentPosition, stoodOnTile);
        }
        public Vector2Int GetPosition() { return currentPosition; }
        public void CalculateMoveBasic(Player player, Grid grid) {
            Vector2Int direction = player.GetPosition() - currentPosition;
            if (Math.Abs(direction.x) > Math.Abs(direction.y)) {
                if (TryMove(new Vector2Int(direction.Clamp().x, 0), player, grid)) { return; }
            }
            if (TryMove(new Vector2Int(0, direction.Clamp().y), player, grid)) { return; }
            TryMove(Vector2Int.Zero, player, grid);
        }
        private bool TryMove(Vector2Int direction, Player player, Grid grid) {
            TileType nextTile = grid.GetTileFromCoord(currentPosition + direction);
            if (nextTile == TileType.Enemy) {
                return false;
            }
            if (nextTile == TileType.Player) {
                player.TakeDamage(damage);
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
                    Move(currentPosition + direction, nextTile, grid);
                    return true;
            }
            return false;
        }
        private void Move(Vector2Int targetPosition, TileType nextTile, Grid grid) {
            grid.SetTileAtCoord(currentPosition, stoodOnTile);
            stoodOnTile = nextTile;

            currentPosition = targetPosition;
            grid.SetTileAtCoord(targetPosition, TileType.Enemy);
        }
    }
}
