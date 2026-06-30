
using Roguelike;

GridRender render = new GridRender();
render.SetTileAppearance(TileType.Empty, '_');
render.SetTileAppearance(TileType.Player, '@');
render.SetTileAppearance(TileType.Enemy, 'X');
render.SetTileAppearance(TileType.Floor, '.');

int fullblockUnicode = int.Parse("2588", System.Globalization.NumberStyles.HexNumber);
render.SetTileAppearance(TileType.Wall, Convert.ToChar(fullblockUnicode));

reset:;
Grid grid = GridGen.GenerateGridWithNumRooms(50, 25, 1, 24, 25);

Player player = GridGen.SpawnPlayer(grid, 100, 25);
List<Enemy> enemies = new List<Enemy>();

int numEnemies = 2;
for(int i = 0; i < numEnemies; i++) {
    Enemy enemy = GridGen.SpawnEnemy(grid, 50, 25);
    enemies.Add(enemy);
}
Console.Clear();
Console.WriteLine(render.GetGridAsText(grid));
Console.Write("HP: " + player.GetHealth().ToString() + "/" + player.GetMaxHealth().ToString());
while (true) {
    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

    char input = keyInfo.KeyChar;
    if (keyInfo.Key == ConsoleKey.Escape) break;
    if (keyInfo.Key == ConsoleKey.Tab) goto reset;
    while (!player.ReadInput(input, grid, enemies)) {
        keyInfo = Console.ReadKey(true);

        input = keyInfo.KeyChar;
    }
    for(int i = 0; i < enemies.Count; i++) {
        if (enemies[i].IsAlive()) continue;
        enemies[i].RemoveSelf(grid);
        enemies.Remove(enemies[i]);
    }
    Render();
    for (int i = 0;i < enemies.Count; i++) {
        Enemy enemy = enemies[i];
        enemy.CalculateMoveBasic(player, grid);
    }
    Render();
    if (!player.IsAlive()) break;
}
//Could be moved to gridrender
void Render() {
    Console.Clear();
    Console.WriteLine(render.GetGridAsText(grid));
    Console.Write("HP: " + player.GetHealth().ToString() + "/" + player.GetMaxHealth().ToString());
}