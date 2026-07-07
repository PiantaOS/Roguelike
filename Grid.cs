namespace Roguelike {
    internal class Grid {
        private Tile[,] tiles;
        private bool[,] gridVision;
        public List<Enemy> Enemies { get; set;  } = [];

        public Grid(int x, int y) {
            tiles = new Tile[x, y];

            for (int i = 0; i < tiles.GetLength(0); i++) {
                for (int j = 0; j < tiles.GetLength(1); j++) {
                    tiles[i, j].TileType = TileType.Empty;
                }
            }
        }
        public void InitializeEnemies(int numEnemies) {
            for (int i = 0; i < numEnemies; i++) {
                Enemy enemy = GridGen.SpawnEnemy(this, 50, 25);
                Enemies.Add(enemy);
            }
        }
        public int GetDimension(int dimension) {
            return tiles.GetLength(dimension);
        }
        public TileType GetTileFromCoord(Vector2Int coord) {
            return tiles[coord.x, coord.y].TileType;
        }
        public void AddTiles(List<Tile> tiles) {
            for (int i = 0; i < tiles.Count; i++) {
                SetTileAtCoord(tiles[i].Coordinate, tiles[i].TileType);
            }
        }
        //public void SpawnItem(Item item)
        public bool SetTileAtCoord(Vector2Int coord, TileType tileType) {
            tiles[coord.x, coord.y].TileType = tileType;
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
