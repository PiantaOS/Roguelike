
namespace Roguelike {
    internal static class GridGen {
        public static Player SpawnPlayer(Grid grid, int health, int damage) {
            List<Vector2Int> validTiles = [];
            for (int i = 0; i < grid.GetDimension(0); i++) {
                for (int j = 0; j < grid.GetDimension(1); j++) {
                    if (grid.GetTileFromCoord(new Vector2Int(i, j)) == TileType.Floor) validTiles.Add(new Vector2Int(i, j));
                }
            }
            Random rand = new Random();

            int randomIndex = rand.Next(0, validTiles.Count);
            Player player = new Player(validTiles[randomIndex], TileType.Floor, health, damage);
            grid.SetTileAtCoord(validTiles[randomIndex], TileType.Player);

            return player;
        }

        public static void SpawnItems(Grid grid, int numItems) {
            List<Vector2Int> validTiles = [];
            for (int i = 0; i < grid.GetDimension(0); i++) {
                for (int j = 0; j < grid.GetDimension(1); j++) {
                    if (grid.GetTileFromCoord(new Vector2Int(i, j)) == TileType.Floor) validTiles.Add(new Vector2Int(i, j));
                }
            }
            Random rand = new Random();

            List<PassiveItem> items = ItemGenerator.GeneratePassiveItems(numItems);
            for (int i = 0; i < items.Count; i++) {
                int randomIndex = rand.Next(validTiles.Count);
                grid.SpawnItem(validTiles[randomIndex], items[i]); // No distinction between any item and any other. Give grid a lookup table containing each item and create a function of grid called set tile at coord
            }
        }
        public static Player SpawnPlayer(Player player, Grid grid, int health, int damage) {
            List<Vector2Int> validTiles = new List<Vector2Int>();
            for (int i = 0; i < grid.GetDimension(0); i++) {
                for (int j = 0; j < grid.GetDimension(1); j++) {
                    if (grid.GetTileFromCoord(new Vector2Int(i, j)) == TileType.Floor) validTiles.Add(new Vector2Int(i, j));
                }
            }
            Random rand = new Random();
            int randomIndex = rand.Next(0, validTiles.Count);
            player.SetNewPosition(validTiles[randomIndex]);
            grid.SetTileAtCoord(validTiles[randomIndex], TileType.Player);

            return player;
        }
        public static Enemy SpawnEnemy(Grid grid, int health, int damage) {
            List<Vector2Int> validTiles = [];
            for (int i = 0; i < grid.GetDimension(0); i++) {
                for (int j = 0; j < grid.GetDimension(1); j++) {
                    if (grid.GetTileFromCoord(new Vector2Int(i, j)) == TileType.Floor) validTiles.Add(new Vector2Int(i, j));
                }
            }
            Random rand = new Random();
            int randomIndex = rand.Next(0, validTiles.Count);
            Enemy enemy = new Enemy(validTiles[randomIndex], TileType.Floor, health, damage);
            grid.SetTileAtCoord(validTiles[randomIndex], TileType.Enemy);

            return enemy;
        }
        public static List<Tile> GenerateRoomTiles(Box param) {
            List<Tile> room = [];
            for (int i = 0; i < param.SizeY; i++) {
                for (int j = 0; j < param.SizeX; j++) {
                    if (i == 0 || i + 1 == param.SizeY) {
                        //On top or bottom
                        Tile tile = new Tile(new Vector2Int(param.TopLeftCorner.x + j, param.TopLeftCorner.y + i), TileType.Wall);
                        room.Add(tile);
                    }
                    else {
                        if (j == 0 || j + 1 == param.SizeX) {
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
        public static void AddExit(Vector2Int coords, Grid grid) {
            grid.SetTileAtCoord(coords, TileType.Exit);
        }
        
        //6 is the minimum room size that looks good
        public static List<Tile> GenerateCorridorDiagonal(Vector2Int pos1, Vector2Int pos2) {
            // https://bfnightly.bracketproductions.com/chapter_25.html?highlight=draw%20corridor#adding-in-corridors
            List<Tile> corridor = [];

            while (pos1.x != pos2.x || pos1.y != pos2.y) {
                Vector2Int direction = pos2 - pos1;
                if (Math.Abs(direction.x) > Math.Abs(direction.y)) {
                    pos1 += new Vector2Int(direction.Clamp().x, 0);
                }
                else {
                    pos1 += new Vector2Int(0, direction.Clamp().y);
                }

                corridor.Add(new Tile(pos1, TileType.Corridor));
            }
            return corridor;
        }
        public static List<Tile> GenerateDoors(Vector2Int pos1, Vector2Int pos2) {
            List<Tile> doors = [];
            doors.Add(new Tile(pos1, TileType.Door));
            doors.Add(new Tile(pos2, TileType.Door));
            return doors;
        }
        private static List<Tile> GenerateCorridorZip(Vector2Int pos1, Vector2Int pos2) {
            // https://bfnightly.bracketproductions.com/chapter_25.html?highlight=draw%20corridor#adding-in-corridors
            List<Tile> corridor = [];

            int distX = pos2.x - pos1.x;
            int distY = pos2.y - pos1.y;
            corridor.Add(new Tile(pos1, TileType.Corridor));
            if (Math.Abs(distX) > Math.Abs(distY)) {
                int zip = Math.Abs(distX) / 2;//new Random().Next(1, Math.Abs(distX) - 1);
                while (Math.Abs(distX) > 0) {
                    pos1.x += Math.Clamp(distX, -1, 1);
                    corridor.Add(new Tile(pos1, TileType.Corridor));
                    distX -= Math.Clamp(distX, -1, 1);
                    if (zip == Math.Abs(distX)) break;
                }
                while (Math.Abs(distY) > 0) {
                    pos1.y += Math.Clamp(distY, -1, 1);
                    corridor.Add(new Tile(pos1, TileType.Corridor));
                    distY -= Math.Clamp(distY, -1, 1);
                }
                while (Math.Abs(distX) > 0) {
                    pos1.x += Math.Clamp(distX, -1, 1);
                    corridor.Add(new Tile(pos1, TileType.Corridor));
                    distX -= Math.Clamp(distX, -1, 1);
                }
            }
            else {
                int zip = Math.Abs(distY) / 2;//new Random().Next(1, Math.Abs(distY));
                while (Math.Abs(distY) > 0) {
                    pos1.y += Math.Clamp(distY, -1, 1);
                    corridor.Add(new Tile(pos1, TileType.Corridor));
                    distY -= Math.Clamp(distY, -1, 1);
                    if (zip == Math.Abs(distY)) break;
                }
                while (Math.Abs(distX) > 0) {
                    pos1.x += Math.Clamp(distX, -1, 1);
                    corridor.Add(new Tile(pos1, TileType.Corridor));
                    distX -= Math.Clamp(distX, -1, 1);
                }
                while (Math.Abs(distY) > 0) {
                    pos1.y += Math.Clamp(distY, -1, 1);
                    corridor.Add(new Tile(pos1, TileType.Corridor));
                    distY -= Math.Clamp(distY, -1, 1);
                }
            }
            corridor.Add(new Tile(pos2, TileType.Corridor));
            return corridor;
        }
        public static Grid GenerateSegmentedGrid(int x, int y, int segmentDivisions, int minRoomSize, int maxRoomSize, int roomSpawnPercent) {
            Grid grid = new Grid(x, y);
            List<Box> segments = GetSegments(segmentDivisions, x, y);
            List<Box> rooms = [];
            foreach (Box t in segments) {
                //if (rand.Next(0, 100) > roomSpawnPercent) continue;
                Box newRoom = GetRandomRectangleRoomInBounds(t, minRoomSize, maxRoomSize);
                rooms.Add(newRoom);
            }

            foreach (Box t in rooms) {
                grid.AddTiles(GenerateRoomTiles(t));
            }

            for (int i = 0; i < rooms.Count; i++) {
                Vector2Int coords = GetCoordFromIndex(i, segmentDivisions);
                Vector2Int[] validDoorways = GetValidDoorways(rooms[i], rooms);
                if (coords.x != 0) {
                    Vector2Int door = validDoorways[0];
                    Vector2Int otherDoor = GetValidDoorways(rooms[i - 1], rooms)[1];
                    grid.AddTiles(GenerateDoors(door, otherDoor));
                    grid.AddTiles(GenerateCorridorZip(door - new Vector2Int(1, 0), otherDoor + new Vector2Int(1, 0)));
                }
                if (coords.y != 0) {
                    Vector2Int door = validDoorways[2];
                    Vector2Int otherDoor = GetValidDoorways(rooms[i - segmentDivisions], rooms)[3];
                    grid.AddTiles(GenerateDoors(door, otherDoor));
                    grid.AddTiles(GenerateCorridorZip(door - new Vector2Int(0, 1), otherDoor + new Vector2Int(0, 1)));
                }
            }
            return grid;
        }
        private static Vector2Int GetCoordFromIndex(int index, int numSegments) {
            int x = (index) % numSegments;
            int y = (int)Math.Floor(index / (double)numSegments);

            return new Vector2Int(x, y);
        }
        private static Vector2Int[] GetValidDoorways(Box room, List<Box> rooms) {
            Vector2Int[] validDoorways = new Vector2Int[4];
            Random random = new Random();

            int index = rooms.IndexOf(room);
            int numSegments = (int)Math.Sqrt(rooms.Count);

            Vector2Int coords = GetCoordFromIndex(index, numSegments); // Check math
            if (coords.x != 0) {
                Vector2Int leftWall = new Vector2Int(room.X1, random.Next(room.Y1 + 1, room.Y2 - 1));
                validDoorways[0] = leftWall;
            }
            else validDoorways[0] = Vector2Int.Zero;

            if (coords.x != numSegments - 1) {
                Vector2Int rightWall = new Vector2Int(room.X2 - 1, random.Next(room.Y1 + 1, room.Y2 - 1));
                validDoorways[1] = rightWall;
            }
            else validDoorways[1] = Vector2Int.Zero;

            if (coords.y != 0) {
                Vector2Int topWall = new Vector2Int(random.Next(room.X1 + 1, room.X2 - 1), room.Y1);
                validDoorways[2] = topWall;
            }
            else validDoorways[2] = Vector2Int.Zero;

            if (coords.y != numSegments - 1) {
                Vector2Int bottomWall = new Vector2Int(random.Next(room.X1 + 1, room.X2 - 1), room.Y2 - 1);
                validDoorways[3] = bottomWall;
            }
            else validDoorways[3] = Vector2Int.Zero;

            return validDoorways;
        }
        private static List<Box> GetSegments(int segmentDivisions, int x, int y) {
            int segmentXLength = x / segmentDivisions;
            int segmentYLength = y / segmentDivisions;

            List<Box> segments = new List<Box>();
            for (int i = 0; i < segmentDivisions; i++) {
                for (int j = 0; j < segmentDivisions; j++) {
                    Vector2Int topLeftCorner = new Vector2Int(segmentXLength * j, segmentYLength * i);
                    Box segment = new Box(topLeftCorner, segmentXLength, segmentYLength);

                    segments.Add(segment);
                }
            }

            return segments;
        }
        private static Box GetRandomSquareRoomInBounds(Box bounds, int minSize, int maxSize) {
            Random random = new Random();
            int size = random.Next(minSize, maxSize);

            int topLeftX = random.Next(bounds.X1, bounds.X2 - size);
            int topLeftY = random.Next(bounds.Y1, bounds.Y2 - (size / 2) + 1);
            Vector2Int topLeft = new Vector2Int(topLeftX, topLeftY);

            Box room = new Box(topLeft, size, (size / 2) + 1);
            return room;
        }
        private static Box GetRandomRectangleRoomInBounds(Box bounds, int minSize, int maxSize) {
            Random random = new Random();
            int sizeX = random.Next(minSize, maxSize);
            int sizeY = random.Next(minSize, maxSize) / 3;
            int topLeftX = random.Next(bounds.X1, bounds.X2 - sizeX);
            int topLeftY = random.Next(bounds.Y1, bounds.Y2 - sizeY);
            Vector2Int topLeft = new Vector2Int(topLeftX, topLeftY);

            Box room = new Box(topLeft, sizeX, sizeY);
            return room;
        }

    }
}
