using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.Model
{
	class EvilBox : Entity
	{
		Entity target;
		int speed = 1;
		public EvilBox(Bitmap sprite, int x, int y, Entity target, int speed) : base(sprite, x, y)
		{
			this.target = target;
			this.speed = speed;
		}

		public void Move()
		{
			if (target.x > x)
				x += speed;

			if (target.y > y)
				y += speed;

			if (target.x < x)
				x -= speed;

			if (target.y < y)
				y -= speed;
		}

		public override void onTick()
		{
			Move();
		}
	}
}
