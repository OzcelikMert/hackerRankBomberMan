using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Please enter numerically how many rows and columns you want the grid to \nconsist of and how many seconds you want it to last: ");
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int r = Convert.ToInt32(firstMultipleInput[0]);
            int c = Convert.ToInt32(firstMultipleInput[1]);
            int n = Convert.ToInt32(firstMultipleInput[2]);

            if (n > 1000000000)
            {
                Environment.Exit(0);
            }

            List<string> grid = new List<string>();
            for (int i = 0; i < r; i++)
            {
                Console.Write("Please indicate empty places (.) and bombs (O) on the grid: ");
                string gridItem = Console.ReadLine();
                if (gridItem.Length == c)
                {
                    grid.Add(gridItem);
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            BomberMan.init(r, c, n, grid);

            Console.ReadLine();
        }
    }

    public class IGridBombLocation
    {
        public int gridIndex { get; set; }
        public int stringIndex { get; set; }
    }

    class BomberMan
    {
        private static List<IGridBombLocation> findBombLocations(int r, int c, List<string> grid)
        {
            List<IGridBombLocation> bombLocations = new List<IGridBombLocation>();

            for (int x = 0; x < r; x++)
            {
                for (int y = 0; y < c; y++)
                {
                    if (grid[x][y] == 'O')
                    {
                        bombLocations.Add(new IGridBombLocation { gridIndex = x, stringIndex = y });

                        if (x > 0) bombLocations.Add(new IGridBombLocation { gridIndex = x - 1, stringIndex = y }); // Top
                        if (x < r - 1) bombLocations.Add(new IGridBombLocation { gridIndex = x + 1, stringIndex = y }); // Bottom
                        if (y > 0) bombLocations.Add(new IGridBombLocation { gridIndex = x, stringIndex = y - 1 }); // Left
                        if (y < c - 1) bombLocations.Add(new IGridBombLocation { gridIndex = x, stringIndex = y + 1 }); // Right
                    }
                }
            }

            return bombLocations;
        }

        public static void init(int r, int c, int n, List<string> grid)
        {
            List<IGridBombLocation> bombLocations = findBombLocations(r, c, grid); //new List<IGridBombLocation>();

            int repeatNumber = 4;
            int seconds = n < repeatNumber ? n : (n % repeatNumber) + repeatNumber;
            
            for (int i = 1; i <= seconds; i++)
            {
                if (i % 2 == 0)
                {
                    bombLocations = findBombLocations(r, c, grid);

                    for (int x = 0; x < r; x++)
                    {
                        grid[x] = grid[x].Replace(".", "O");
                    }
                }
                else if (i > 1 && i % 2 == 1)
                {
                    foreach (var bombLocation in bombLocations)
                    {
                        grid[bombLocation.gridIndex] = grid[bombLocation.gridIndex].Remove(bombLocation.stringIndex, 1).Insert(bombLocation.stringIndex, ".");
                    }
                }
            }

            showGrid(grid);
        }

        public static void showGrid(List<string> grid) {
            Console.WriteLine(String.Join("\n", grid));
        }
    }
}
