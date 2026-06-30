using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class GridRender {
        private Dictionary<TileType, char> _tileAppearances = new Dictionary<TileType, char>();
        public void SetTileAppearance(TileType tileType, char value) {
            _tileAppearances.Add(tileType, value);
        }
        public string GetGridAsText(Grid grid) {
            StringBuilder stringBuilder = new StringBuilder();
            for(int i = 0; i < grid.GetDimension(1); i++) {
                for(int j = 0; j < grid.GetDimension(0); j++) {
                    char desiredChar;
                    _tileAppearances.TryGetValue(grid.GetTileFromCoord(new Vector2Int(j, i)), out desiredChar);

                    stringBuilder.Append(desiredChar);
                }
                stringBuilder.AppendLine();
            }
            
            string result = stringBuilder.ToString();
            return result;
        }
    }
}
