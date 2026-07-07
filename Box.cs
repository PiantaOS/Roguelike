namespace Roguelike {
    internal record struct Box(Vector2Int TopLeftCorner, int SizeX, int SizeY) {
        public Vector2Int TopLeftCorner = TopLeftCorner;
        public int SizeX = SizeX;
        public int SizeY = SizeY;
        public int X1 => TopLeftCorner.x;
        public int X2 => TopLeftCorner.x + SizeX;
        public int Y1 => TopLeftCorner.y;
        public int Y2 => TopLeftCorner.y + SizeY;
    }
}
