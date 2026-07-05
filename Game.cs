using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    internal class Game(GridRender render, Player player, bool running, Grid currentGrid) {
        private GridRender _render = render;
        private Player _player = player;
        private bool _running = running;
        private Grid _currentGrid = currentGrid;

        public void Initialize() {
            _render = new GridRender();
            _render.SetTileAppearance(TileType.Empty, ('_', ConsoleColor.White));
            _render.SetTileAppearance(TileType.Player, ('@', ConsoleColor.Blue));
            _render.SetTileAppearance(TileType.Enemy, ('X', ConsoleColor.Red));
            _render.SetTileAppearance(TileType.Floor, ('.', ConsoleColor.Gray));
            _render.SetTileAppearance(TileType.Corridor, ('#', ConsoleColor.Yellow));
            _render.SetTileAppearance(TileType.Door, ('+', ConsoleColor.DarkYellow));

            int fullblockUnicode = int.Parse("2588", System.Globalization.NumberStyles.HexNumber);
            _render.SetTileAppearance(TileType.Wall, (Convert.ToChar(fullblockUnicode), ConsoleColor.Gray));

            _currentGrid = GridGen.GenerateSegmentedGrid(75, 25, 3, 14, 18, 95);
            _player = GridGen.SpawnPlayer(_currentGrid, 100, 25);

            const int numEnemies = 2;
            _currentGrid.InitializeEnemies(numEnemies);

            Play();
        }


        private void Play() {
            Render();
            while (true) {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                char input = keyInfo.KeyChar;
                if (keyInfo.Key == ConsoleKey.Escape) break;
                if (keyInfo.Key == ConsoleKey.Tab) {
                    _running = false;
                    Initialize();
                }
                while (!_player.ReadInput(input, _currentGrid)) {
                    keyInfo = Console.ReadKey(true);

                    input = keyInfo.KeyChar;
                }

                List<Enemy> enemies = _currentGrid.Enemies;
                for (int i = 0; i < enemies.Count; i++) {
                    if (enemies[i].IsAlive()) continue;
                    enemies[i].RemoveSelf(_currentGrid);
                    enemies.Remove(enemies[i]);
                }
                foreach (Enemy enemy in enemies) {
                    enemy.CalculateMoveBasic(_player, _currentGrid);
                }
                Render();
                if (!_player.IsAlive()) break;
            }
        }

        private void Render() {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.WriteLine(_render.GetGridAsText(_currentGrid));
            Console.WriteLine("HP: " + _player.GetHealth().ToString() + "/" + _player.GetMaxHealth().ToString());
            Console.Write("Log: " + _currentGrid.ReadLog());
        }
    }

}
