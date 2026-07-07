namespace Roguelike {
    internal struct Vector2Int {
        public int x;
        public int y;
        public static Vector2Int Zero => new Vector2Int(0, 0);

        public static float Distance(Vector2Int first, Vector2Int second) =>
            System.Numerics.Vector2.Distance(first.FloatVector, second.FloatVector);
        public System.Numerics.Vector2 FloatVector => new System.Numerics.Vector2((float)x, (float)y);
        public Vector2Int(int x, int y) {
            this.x = x;
            this.y = y;
        }
        public override string ToString() {
            return x.ToString() + ", " + y.ToString();
        }
        public Vector2Int Clamp() {
            return new Vector2Int(Math.Clamp(x, -1, 1), Math.Clamp(y, -1, 1));
        }
        public static Vector2Int operator +(Vector2Int lhs, Vector2Int rhs) {
            return new Vector2Int(lhs.x + rhs.x, lhs.y + rhs.y);
        }
        public static Vector2Int operator -(Vector2Int lhs, Vector2Int rhs) {
            return new Vector2Int(lhs.x - rhs.x, lhs.y - rhs.y);
        }
        public static bool operator ==(Vector2Int lhs, Vector2Int rhs) {
            if (lhs.x == rhs.x && lhs.y == rhs.y) return true;
            return false;
        }
        public static bool operator !=(Vector2Int lhs, Vector2Int rhs) {
            if (lhs.x == rhs.x && lhs.y == rhs.y) return false;
            return true;
        }
        public static Vector2Int operator *(Vector2Int lhs, int r) {
            return new Vector2Int(lhs.x * r, lhs.y * r);
        }
        public static Vector2Int operator /(Vector2Int lhs, int r) {
            return new Vector2Int(lhs.x / r, lhs.y / r);
        }
        public static System.Numerics.Vector2 operator *(Vector2Int lhs, float r) {
            return new System.Numerics.Vector2(lhs.x * r, lhs.y * r);
        }
    }
}
