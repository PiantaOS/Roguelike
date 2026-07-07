using System.Text;
using Pastel;
namespace Roguelike {
    internal class GridRender {
        private readonly Dictionary<TileType, (char, ConsoleColor)> _tileAppearances = new Dictionary<TileType, (char, ConsoleColor)>();
        public void SetTileAppearance(TileType tileType, (char value, ConsoleColor color) properties) {
            _tileAppearances.Add(tileType, properties);
        }
        public string GetGridAsText(Grid grid) {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < grid.GetDimension(1); i++) {
                for (int j = 0; j < grid.GetDimension(0); j++) {
                    if (!grid.GridVision[j, i]) {
                        stringBuilder.Append(' ');
                        continue;
                    }
                    _tileAppearances.TryGetValue(grid.GetTileFromCoord(new Vector2Int(j, i)), out (char value, ConsoleColor color) properties);
                    stringBuilder.Append(properties.value.ToString().Pastel(properties.color));
                }
                stringBuilder.AppendLine();
            }

            string result = stringBuilder.ToString();
            return result;
        }
        public string GetGridAsTextWithCoords(Grid grid) {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < grid.GetDimension(1); i++) {
                for (int j = 0; j < grid.GetDimension(0); j++) {
                    _tileAppearances.TryGetValue(grid.GetTileFromCoord(new Vector2Int(j, i)), out (char value, ConsoleColor color) properties);
                    if (grid.GetTileFromCoord(new Vector2Int(j, i)) == TileType.Empty) {
                        stringBuilder.Append((j % 5) + 1);
                        continue;
                    }
                    stringBuilder.Append(properties.value.ToString().Pastel(properties.color));
                }
                stringBuilder.AppendLine();
            }

            string result = stringBuilder.ToString();
            return result;
        }
    }
}
