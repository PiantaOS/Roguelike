using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal struct Vector2Int {
        public int x;
        public int y;

        public Vector2Int(int x, int y) {
            this.x = x;
            this.y = y;
        }
        public override string ToString() {
            return x.ToString() + ", " + y.ToString();
        }
        public static Vector2Int operator+(Vector2Int lhs, Vector2Int rhs) {
            return new Vector2Int(lhs.x + rhs.x, lhs.y + rhs.y);
        }
        public static Vector2Int operator -(Vector2Int lhs, Vector2Int rhs) {
            return new Vector2Int(lhs.x - rhs.x, lhs.y - rhs.y);
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
