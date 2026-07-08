namespace Roguelike {
    internal class Grid {
        private readonly Tile[,] _tiles;
        public bool[,] GridVision { get; private set; }
        private readonly PassiveItem[,] _items;
        public List<Enemy> Enemies { get; set;  } = [];

        public Grid(int x, int y) {
            _tiles = new Tile[x, y];
            GridVision = new bool[x, y];
            _items = new PassiveItem[x, y];

            for (int i = 0; i < _tiles.GetLength(0); i++) {
                for (int j = 0; j < _tiles.GetLength(1); j++) {
                    _tiles[i, j].TileType = TileType.Empty;
                    GridVision[i, j] = false;
                    //Could populate passiveitems with empty items
                }
            }
        }
        public void InitializeEnemies(int numEnemies, int floor) {
            for (int i = 0; i < numEnemies; i++) {
                Enemy enemy = GridGen.SpawnEnemy(this, 50, 25, floor);
                Enemies.Add(enemy);
            }
        }
        public int GetDimension(int dimension) {
            return _tiles.GetLength(dimension);
        }
        public TileType GetTileFromCoord(Vector2Int coord) {
            return _tiles[coord.x, coord.y].TileType;
        }
        public void AddTiles(List<Tile> tiles) {
            for (int i = 0; i < tiles.Count; i++) {
                SetTileAtCoord(tiles[i].Coordinate, tiles[i].TileType);
            }
        }

        public void RevealAroundPlayer(Vector2Int playerPosition)
        {
            List<Vector2Int> positionsToCheck = [];
            positionsToCheck.Add(playerPosition + new Vector2Int(0, 3));
            positionsToCheck.Add(playerPosition + new Vector2Int(-1, 2));
            positionsToCheck.Add(playerPosition + new Vector2Int(0, 2));
            positionsToCheck.Add(playerPosition + new Vector2Int(1, 2));
            positionsToCheck.Add(playerPosition + new Vector2Int(-3, 1));
            positionsToCheck.Add(playerPosition + new Vector2Int(-2, 1));
            positionsToCheck.Add(playerPosition + new Vector2Int(-1, 1));
            positionsToCheck.Add(playerPosition + new Vector2Int(-0, 1));
            positionsToCheck.Add(playerPosition + new Vector2Int(1, 1));
            positionsToCheck.Add(playerPosition + new Vector2Int(2, 1));
            positionsToCheck.Add(playerPosition + new Vector2Int(3, 1));
            
            positionsToCheck.Add(playerPosition + new Vector2Int(-5, 0));
            positionsToCheck.Add(playerPosition + new Vector2Int(-4, 0));
            positionsToCheck.Add(playerPosition + new Vector2Int(-3, 0));
            positionsToCheck.Add(playerPosition + new Vector2Int(-2, 0));
            positionsToCheck.Add(playerPosition + new Vector2Int(-1, 0));
            positionsToCheck.Add(playerPosition + new Vector2Int(-0, 0));
            positionsToCheck.Add(playerPosition + new Vector2Int(1, 0));
            positionsToCheck.Add(playerPosition + new Vector2Int(2, 0));
            positionsToCheck.Add(playerPosition + new Vector2Int(3, 0));
            positionsToCheck.Add(playerPosition + new Vector2Int(4, 0));
            positionsToCheck.Add(playerPosition + new Vector2Int(5, 0));

            positionsToCheck.Add(playerPosition + new Vector2Int(0, -3));
            positionsToCheck.Add(playerPosition + new Vector2Int(-1, -2));
            positionsToCheck.Add(playerPosition + new Vector2Int(0, -2));
            positionsToCheck.Add(playerPosition + new Vector2Int(1, -2));
            positionsToCheck.Add(playerPosition + new Vector2Int(-3, -1));
            positionsToCheck.Add(playerPosition + new Vector2Int(-2, -1));
            positionsToCheck.Add(playerPosition + new Vector2Int(-1, -1));
            positionsToCheck.Add(playerPosition + new Vector2Int(-0, -1));
            positionsToCheck.Add(playerPosition + new Vector2Int(1, -1));
            positionsToCheck.Add(playerPosition + new Vector2Int(2, -1));
            positionsToCheck.Add(playerPosition + new Vector2Int(3, -1));
            
            positionsToCheck.Add(playerPosition);
            foreach (Vector2Int pos in positionsToCheck)
            {
                if (pos.x < 0 || pos.x > _tiles.GetLength(0) - 1|| pos.y < 0 || pos.y > _tiles.GetLength(1) - 1) {
                    continue;
                }

                GridVision[pos.x, pos.y] = true;
            }
        }
        public void SpawnItem(Vector2Int coord, PassiveItem item) {
            SetTileAtCoord(coord, TileType.Item);
            _items[coord.x, coord.y] = item;
        }

        public PassiveItem TakeItemFromCoord(Vector2Int coord) {
            PassiveItem item = _items[coord.x, coord.y];
            _items[coord.x, coord.y] = default;
            SetTileAtCoord(coord, TileType.Floor);
            return item;
        }
        public bool SetTileAtCoord(Vector2Int coord, TileType tileType) {
            _tiles[coord.x, coord.y].TileType = tileType;
            return true;
        }
        string log = "";
        public void WriteLog(string msg) {
            if (log != "") {
                log += "\n";
            }
            log += msg;
        }
        public string ReadLog() {
            string temp = log;
            log = "";
            return temp;
        }
    }
}
