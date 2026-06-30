using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal static class GridGen {
        static int maxIterations = 10;
        //No safety against being too close to the wall

        public static Player SpawnPlayer(Grid grid) {
            List<Vector2Int> validTiles = new List<Vector2Int>();
            for(int i = 0; i < grid.GetDimension(0); i++) {
                for(int j = 0; j < grid.GetDimension(1); j++) {
                    if(grid.GetTileFromCoord(new Vector2Int(i, j)) == TileType.Floor) validTiles.Add(new Vector2Int(i, j));
                }
            }
            Random rand = new Random();
            int randomIndex = rand.Next(0, validTiles.Count);
            Player player = new Player(validTiles[randomIndex], TileType.Floor);
            grid.SetTileAtCoord(validTiles[randomIndex], TileType.Player);

            return player;
        }
        public static List<Tile> GenerateRoom(RoomParameters param) {
            List<Tile> room = new List<Tile>();
            for (int i = 0; i < param.SizeY; i++) {
                for(int j = 0; j < param.SizeX; j++) {
                    if(i == 0 || i + 1 == param.SizeY) {
                        //On top or bottom
                        Tile tile = new Tile(new Vector2Int(param.TopLeftCorner.x + j, param.TopLeftCorner.y + i), TileType.Wall);
                        room.Add(tile);
                    }
                    else {
                        if(j == 0 || j + 1 == param.SizeX) {
                            //On left or right
                            Tile tile = new Tile(new Vector2Int(param.TopLeftCorner.x + j, param.TopLeftCorner.y + i), TileType.Wall);
                            room.Add(tile);
                        }
                        else {
                            //Floor
                            Tile tile = new Tile(new Vector2Int(param.TopLeftCorner.x + j, param.TopLeftCorner.y + i), TileType.Floor);
                            room.Add(tile);
                        }
                        
                    }
                }
            }
            return room;
        }
        public static Grid GenerateGridWithNumRooms(int x, int y, int numRooms, int minRoomSize, int maxRoomSize) {
            Grid grid = new Grid(x, y);

            List<RoomParameters> rooms = new List<RoomParameters>();
            for(int i = 0; i < numRooms; i++) {
                Random rand = new Random();
                int roomSize = rand.Next(minRoomSize, maxRoomSize);

                RoomParameters testRoom = GetRandomRoom(roomSize, grid.GetDimension(0) - 1, grid.GetDimension(1) - 1);
                int iterations = 0;
                while(OverlapsWithRooms(testRoom, rooms)) {
                    if (iterations >= maxIterations) break;
                    iterations++;
                    testRoom = GetRandomRoom(roomSize, grid.GetDimension(0) - 1, grid.GetDimension(1) - 1);
                }
                rooms.Add(testRoom);
            }
            for(int i = 0; i < rooms.Count; i++) {
                grid.AddRoom(GenerateRoom(rooms[i]));
            }
            return grid;
        }
        private static RoomParameters GetRandomRoom(int roomSize, int boundRight, int boundDown) {
            Random rand = new Random();
            int roomX = rand.Next(0, (boundRight - roomSize));
            int roomY = rand.Next(0, (boundDown - roomSize));
            Vector2Int topLeft = new Vector2Int(roomX, roomY);

            RoomParameters room = new RoomParameters(topLeft, roomSize, roomSize);
            return room;
        }
        private static bool OverlapsWithRooms(RoomParameters room, List<RoomParameters> rooms) {
            for (int j = 0; j < rooms.Count; j++) {
                if (DoRoomsOverlap(room, rooms[j])) {
                    return true;
                }
            }
            return false;
        }
        public static bool DoRoomsOverlap(RoomParameters room1, RoomParameters room2) {
            //https://stackoverflow.com/questions/306316/determine-if-two-rectangles-overlap-each-other
            if (room1.TopLeftCorner.x < room2.TopLeftCorner.x + room2.SizeX && room1.TopLeftCorner.x + room1.SizeX > room2.TopLeftCorner.x &&
                room1.TopLeftCorner.y < room2.TopLeftCorner.y + room2.SizeY && room1.TopLeftCorner.y + room1.SizeY > room2.TopLeftCorner.y) {
                return true;
            }
            return false;
        }
    }
}
