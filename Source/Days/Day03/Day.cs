using System;
using System.Diagnostics;
using System.Linq;

class Day03 : BaseDay {
    const char Tree = '#';
    public override string Run(int part, string rawData) {

        char[][] hill = rawData.Lines().Select(x => x.ToCharArray()).ToArray();

        if (part == 1) {
            return CountTrees(hill, 3, 1).ToString();
        }
        else {
            int[,] paths = {
                {1,1},
                {3,1 },
                {5,1 },
                {7,1 },
                {1,2 }
            };

            long product = 1;

            for(int i = 0; i < paths.GetLength(0); i++) {
                int trees = CountTrees(hill, paths[i, 0], paths[i, 1]);
                product *= trees;
            }
            //816675328 too low due to int overflow
            return product.ToString();
        }
    }


    int CountTrees(char[][] hill, int xStep, int yStep) {
        int y = 0;
        int x = 0;
        int count = 0;
        int width = hill[0].Length;
        int height = hill.Length;

        while (y < height) {
            if (hill[y][x] == Tree) {
                count++;
            }
            y += yStep;
            x = (x + xStep) % width;
        }

        return count;
    }
}