using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class Grid {
        private Tile[,] tiles;
        public Grid(int x, int y) {
            tiles = new Tile[x, y];

            for(int i = 0; i < tiles.GetLength(0); i++) {
                for(int j = 0; j < tiles.GetLength(1); j++) {
                    tiles[i, j].TileType = TileType.Empty;
                }
            }
        }
        public int GetDimension(int dimension) {
            return tiles.GetLength(dimension);
        }
        public TileType GetTileFromCoord(Vector2Int coord) {
            return tiles[coord.x, coord.y].TileType;
        }
        public void AddRoom(List<Tile> room) {
            for (int i = 0; i < room.Count; i++) {
                SetTileAtCoord(room[i].Coordinate, room[i].TileType);
            }
        }
        public bool SetTileAtCoord(Vector2Int coord, TileType tileType) {
            //if (tiles[coord.x, coord.y].TileType == TileType.Player) return false;

            tiles[coord.x, coord.y].TileType = tileType;
            return true;
        }
    }
}
