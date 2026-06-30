using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal struct RoomParameters {
        public Vector2Int TopLeftCorner;
        public int SizeX;
        public int SizeY;

        public RoomParameters(Vector2Int topLeftCorener, int sizeX, int sizeY) {
            TopLeftCorner = topLeftCorener;
            SizeX = sizeX;
            SizeY = sizeY;
        }
    }
}
