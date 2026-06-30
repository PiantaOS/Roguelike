
using Roguelike;

GridRender render = new GridRender();
render.SetTileAppearance(TileType.Empty, '_');
render.SetTileAppearance(TileType.Player, '@');
render.SetTileAppearance(TileType.Floor, '.');

int fullblockUnicode = int.Parse("2588", System.Globalization.NumberStyles.HexNumber);
render.SetTileAppearance(TileType.Wall, Convert.ToChar(fullblockUnicode));

Grid grid = GridGen.GenerateGridWithNumRooms(50, 25, 4, 7, 10);

Player player = GridGen.SpawnPlayer(grid);


while (true) {
    Console.Clear();
    Console.WriteLine(render.GetGridAsText(grid));

    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

    char input = keyInfo.KeyChar;
    if (keyInfo.Key == ConsoleKey.Escape) break;

    while(!player.ReadInput(input, grid)) {
        keyInfo = Console.ReadKey(true);

        input = keyInfo.KeyChar;
    }

    Console.Clear();
    Console.WriteLine(render.GetGridAsText(grid));

}