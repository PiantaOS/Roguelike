namespace Roguelike {
    internal record struct Box {
        public Vector2Int TopLeftCorner;
        public int SizeX;
        public int SizeY;
        public int x1 => TopLeftCorner.x;
        public int x2 => TopLeftCorner.x + SizeX;
        public int y1 => TopLeftCorner.y;
        public int y2 => TopLeftCorner.y + SizeY;
        public Box(Vector2Int topLeftCorener, int sizeX, int sizeY) {
            TopLeftCorner = topLeftCorener;
            SizeX = sizeX;
            SizeY = sizeY;
        }
    }
}
