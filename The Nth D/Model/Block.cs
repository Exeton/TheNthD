using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.Model
{
	public class Block
	{
		public static int blockSize = 10;

		public bool filled;
		public Brush color;

		public Block(bool filled, Brush color)
		{
			this.filled = filled;
			this.color = color;
		}
	}
}
