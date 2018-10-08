using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.Model
{
	abstract class Entity
	{
		public Bitmap sprite;
		int health;

		public int x;
		public int y;

		public Entity(Bitmap sprite, int x, int y)
		{
			this.sprite = sprite;
			this.x = x;
			this.y = y;
		}

		public virtual void Draw(Graphics g)
		{
			g.DrawImage(sprite, x, y);
		}

		public abstract void onTick();
	}
}
