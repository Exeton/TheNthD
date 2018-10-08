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
		int speedSquared = 1;
		int speedValue = 1;
		private int speed
		{
			get
			{
				return speedValue;
			}
			set
			{
				speedValue = value;
				speedSquared = speedValue * speedValue;
			}
		}

		public EvilBox(Bitmap sprite, int x, int y, Entity target, int speed) : base(sprite, x, y)
		{
			this.target = target;
			this.speed = speed;
		}

		public void Move()
		{
			//Enemy should move in a straight line towards the enemy at a constant speed
			//movementX^2 + movementY^2 = movementSpeed^2
			//movementY / movementX = slope of a line drawn between the evil box and the target

			//Solve the system of equations to get the dX and dY

			float changeInX;
			float changeInY;

			if (target.x - x == 0)
			{
				changeInX = 0;
				changeInY = speedSquared;
			}
			else
			{
				float slope = (target.y - y) / (target.x - x);

				changeInX = (float)Math.Sqrt(speedSquared / (1 + slope * slope));
				changeInY = (float)Math.Sqrt(speedSquared - changeInX * changeInX);
			}

			if (x > target.x)
				changeInX = -changeInX;
			if (y > target.y)
				changeInY = -changeInY;

			x += changeInX;
			y += changeInY;
		}

		public override void onTick()
		{
			Move();
		}
	}
}
