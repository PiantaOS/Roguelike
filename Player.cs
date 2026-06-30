using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class Player {
        Vector2Int currentPosition;
        TileType stoodOnTile;
        int health;
        int maxHealth;
        int damage;//Can change with equipment
        public Player(Vector2Int position, TileType tileSpawnedOn, int health, int damage) {
            currentPosition = position;
            stoodOnTile = tileSpawnedOn;
            this.health = health;
            this.maxHealth = health;
            this.damage = damage;
        }
        public void TakeDamage(int damage) {
            health -= damage;
        }
        public bool IsAlive() {
            if (health <= 0) return false;
            return true;
        }
        public bool ReadInput(char input, Grid grid, List<Enemy> enemies) {
            switch(input) {
                case 'l':
                    return TryMove(new Vector2Int(1, 0), grid, enemies); ;
                case 'm':
                    return TryMove(new Vector2Int(0, 1), grid, enemies); ;
                case 'j': 
                    return TryMove(new Vector2Int(-1, 0), grid, enemies); ;
                case 'i':
                    return TryMove(new Vector2Int(0, -1), grid, enemies); ;
                case 'k':
                    return true;
            }
            return false;
        }
        public int GetHealth() { return health; }
        public int GetMaxHealth() {  return maxHealth; }
        private bool TryMove(Vector2Int direction, Grid grid, List<Enemy> enemies) {
            TileType nextTile = grid.GetTileFromCoord(currentPosition + direction);
            if(nextTile == TileType.Player) {
                return true;
            }
            if (nextTile == TileType.Enemy) {
                foreach(Enemy enemy in enemies) {
                    if (enemy.GetPosition() == currentPosition + direction) enemy.TakeDamage(damage, grid);
                }
                return true;
            }
            switch (nextTile) {
                case TileType.Empty:
                    Console.WriteLine("Player tried to move to empty tile");
                    break;
                case TileType.Wall:
                    return false;
                case TileType.Floor:
                    Move(currentPosition + direction, nextTile, grid);
                    return true;

            }
            return false;
        }
        private void Move(Vector2Int targetPosition, TileType nextTile, Grid grid) {
            grid.SetTileAtCoord(currentPosition, stoodOnTile);
            stoodOnTile = nextTile;

            currentPosition = targetPosition;
            grid.SetTileAtCoord(targetPosition, TileType.Player);
        }
        public Vector2Int GetPosition() { return currentPosition; }
    }
}
