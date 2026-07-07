

using System.ComponentModel.Design;

namespace Roguelike {
    internal class Game {
        private GridRender _render;
        private Player _player;
        private Grid _currentGrid;
        private bool _running;
        private bool _inInventory = false;
        private int _floor;
        public Game() {
            Initialize();
        }

        public void Initialize() {
            _currentGrid = GridGen.GenerateSegmentedGrid(75, 25, 3, 14, 18, 95);

            _render = new GridRender();
            _render.SetTileAppearance(TileType.Empty, (' ', ConsoleColor.White));
            _render.SetTileAppearance(TileType.Player, ('@', ConsoleColor.Blue));
            _render.SetTileAppearance(TileType.Enemy, ('X', ConsoleColor.Red));
            _render.SetTileAppearance(TileType.Floor, ('.', ConsoleColor.Gray));
            _render.SetTileAppearance(TileType.Corridor, ('#', ConsoleColor.Yellow));
            _render.SetTileAppearance(TileType.Door, ('+', ConsoleColor.DarkYellow));
            _render.SetTileAppearance(TileType.Exit, ('E', ConsoleColor.Green));
            _render.SetTileAppearance(TileType.Item, ('I', ConsoleColor.DarkMagenta));
            _render.SetTileAppearance(TileType.Heal, ('H', ConsoleColor.Magenta));

            int fullblockUnicode = int.Parse("2588", System.Globalization.NumberStyles.HexNumber);
            _render.SetTileAppearance(TileType.Wall, (Convert.ToChar(fullblockUnicode), ConsoleColor.Gray));
            _running = true;

            _player = GridGen.SpawnPlayer(_currentGrid, 100, 25);
            GridGen.SpawnItems(_currentGrid, 4, _floor);
            const int numEnemies = 2;
            _currentGrid.InitializeEnemies(numEnemies, _floor);
            GridGen.SpawnExit(_currentGrid, 20, _player.GetPosition());
            GridGen.GenerateHeals(_currentGrid, 2);
            Play();
        }

        private void Play() {
            Render();
            while (_running) {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                char input = keyInfo.KeyChar;
                if (keyInfo.Key == ConsoleKey.Escape) break;
                if (keyInfo.Key == ConsoleKey.Tab) {
                    _running = false;
                    Initialize();
                }

                if (keyInfo.Key == ConsoleKey.W) {
                    
                    _inInventory = true;
                    int currentItem = 0;
                    while (_inInventory) {
                        Console.Clear();
                        Console.WriteLine(_player.Inventory.RenderInventory(currentItem));

                        ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                        char key = pressedKey.KeyChar;
                        if (pressedKey.Key == ConsoleKey.W) {
                            _inInventory = false;
                            break;
                        }
                        if (pressedKey.Key == ConsoleKey.M || pressedKey.Key == ConsoleKey.L) currentItem++;
                        if (pressedKey.Key == ConsoleKey.I || pressedKey.Key == ConsoleKey.J) currentItem--;
                        if (pressedKey.Key == ConsoleKey.K) _player.Inventory.EquipItemAtIndex(currentItem);
                    }

                    Render();
                    continue;
                }
                while (!_player.ReadInput(input, _currentGrid)) {
                    keyInfo = Console.ReadKey(true);

                    input = keyInfo.KeyChar;
                }

                List<Enemy> enemies = _currentGrid.Enemies;
                for (int i = 0; i < enemies.Count; i++) {
                    if (enemies[i].IsAlive()) continue;
                    _currentGrid.WriteLog("Killed an enemy");
                    enemies[i].RemoveSelf(_currentGrid);
                    enemies.Remove(enemies[i]);
                }
                foreach (Enemy enemy in enemies) {
                    enemy.CheckIfSeen(_currentGrid);
                    if (!enemy.IsActive) continue;
                    enemy.CalculateMoveBasic(_player, _currentGrid);
                }

                Render();
                if (_player.ReachedExit) {
                    _floor++;
                    _player.ReachedExit = false;
                    _currentGrid = GridGen.GenerateSegmentedGrid(75, 25, 3, 14, 18, 95);
                    GridGen.SpawnPlayer(_player, _currentGrid);
                    GridGen.SpawnItems(_currentGrid, 4, _floor);
                    _currentGrid.InitializeEnemies(3 + _floor, _floor);
                    GridGen.SpawnExit(_currentGrid, 20, _player.GetPosition());
                    GridGen.GenerateHeals(_currentGrid, 2 + 1 * _floor);

                    Render();
                    continue;
                }
                if (!_player.IsAlive()) break;
            }
        }

        private void Render() {
            _currentGrid.RevealAroundPlayer(_player.GetPosition());

            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.WriteLine(_render.GetGridAsText(_currentGrid));
            Console.WriteLine("HP: " + _player.GetHealth().ToString() + "/" + _player.MaxHealth.ToString());
            Console.Write("Log: " + _currentGrid.ReadLog());
        }
    }

}
