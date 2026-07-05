namespace Roguelike {
    internal struct Tile {
        public Vector2Int Coordinate;
        public TileType TileType;

        public Tile(Vector2Int coordinate, TileType tileType) {
            this.Coordinate = coordinate;
            this.TileType = tileType;
        }
    }
}
