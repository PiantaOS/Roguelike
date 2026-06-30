using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class Player {
        Vector2Int currentPosition;
        TileType stoodOnTile;
        public Player(Vector2Int position, TileType tileSpawnedOn) {
            currentPosition = position;
            stoodOnTile = tileSpawnedOn;
        }
        public bool ReadInput(char input, Grid grid) {
            switch(input) {
                case 'l':
                    return TryMove(new Vector2Int(1, 0), grid); ;
                case 'm':
                    return TryMove(new Vector2Int(0, 1), grid); ;
                case 'j': 
                    return TryMove(new Vector2Int(-1, 0), grid); ;
                case 'i':
                    return TryMove(new Vector2Int(0, -1), grid); ;
                case 'k':
                    //stay in place but pass time
                    return true;
            }
            return false;
        }
        private bool TryMove(Vector2Int direction, Grid grid) {
            TileType nextTile = grid.GetTileFromCoord(currentPosition + direction);
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
