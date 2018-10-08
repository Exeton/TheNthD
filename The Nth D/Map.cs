using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.Model;

namespace The_Nth_D
{
	class Map
	{
		Block[,] map;

		public Map(int x, int y)
		{
			map = new Block[x, y];

			for (int i = 0; i < x; i++)
				for (int j = 0; j < y; j++)
				{
					map[i, j] = new Block(false, null);
				}
		}

		public int GetLength(int dimension)
		{
			return map.GetLength(dimension);
		}

		public Block this[int x, int y] {
			get
			{
				if (insideMap(x, y))
					return map[x, y];
				return new Block(true, Brushes.Pink);
			}
			set
			{
				if (insideMap(x, y))
					map[x, y] = value;
			}
		}

		public bool insideMap(int x, int y)
		{
			if (x < 0 || y < 0)
				return false;
			if (x >= map.GetLength(0) || y >= map.GetLength(1))
				return false;

			return true;
		}

	}
}
