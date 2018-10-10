﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.Model;
using The_Nth_D.World;

namespace The_Nth_D
{
	[Serializable]
	public class Map
	{
		public Block[,] map;
		public string name;
		public string version;

		Block nullBlock;

		public Map(int x, int y, string name)
		{
			this.name = name;
			map = new Block[x, y];
			nullBlock = new Block(true, Color.Black);

			for (int i = 0; i < x; i++)
				for (int j = 0; j < y; j++)
				{
					map[i, j] = new Block(false, Color.White);
				}
		}

		public Map(Block[,] blocks, MapInfo mapInfo)
		{
			this.map = blocks;
			applyMapInfo(mapInfo);
		}

		public MapInfo getMapInfo()
		{
			return new MapInfo(name, version);
		}

		public void applyMapInfo(MapInfo mapInfo)
		{
			name = mapInfo.name;
			version = mapInfo.version;
		}

		public Map onDeseralized()
		{
			nullBlock = new Block(true, Color.Black);
			for (int i = 0; i < GetLength(0); i++)
				for (int j = 0; j < GetLength(1); j++)
				{
					map[i, j].onDeseralized();
				}
			return this;
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
				return nullBlock;
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
