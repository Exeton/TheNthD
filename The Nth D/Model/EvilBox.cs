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
		float speedSquared = 1;
		float speedValue = 1;
		bool dynamicSpeed;
		private float speed
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

		public EvilBox(Bitmap sprite, int x, int y, Entity target, float speed, bool dynamicSpeed) : base(sprite, x, y)
		{
			this.target = target;
			this.speed = speed;
			this.dynamicSpeed = dynamicSpeed;
		}

		public void Move()
		{
			//Enemy should move in a straight line towards the enemy at a constant speed
			//movementX^2 + movementY^2 = movementSpeed^2
			//movementY / movementX = slope of a line drawn between the evil box and the target

			//Solve the system of equations to get the dX and dY

			float dx = target.x - x;
			float dy = target.y - y;

			float distApprox = Math.Abs(dx) + Math.Abs(dy);

			if (dynamicSpeed)
			{
				int val = 50;
				if (distApprox > 50)
				{
					speed = 5 + (float)Math.Sqrt(distApprox / 100);
				}
				else
				{
					speed = 5 - (float)Math.Sqrt((50 - distApprox / 100));

					if (speed < 0)
						speed = 0;
				}
			}

			float changeInX;
			float changeInY;

			if (target.x - x == 0)
			{
				changeInX = 0;
				changeInY = speedSquared;
			}
			else
			{
				float slope = dy / dx;

				changeInX = (float)Math.Sqrt(speedSquared / (1 + slope * slope));

				if (changeInX > speed - 0.01f)//Prevents taking the negative of a square root
					changeInX = speed - 0.01f;

				float insideVal = speedSquared - changeInX * changeInX;
				if (insideVal < 0)
					insideVal = 0;
				changeInY = (float)Math.Sqrt(insideVal);
			}

			if (x > target.x)
				changeInX = -changeInX;
			if (y > target.y)
				changeInY = -changeInY;

			x += changeInX;
			y += changeInY;

			if (float.IsNaN(x) || float.IsNaN(y))
			{
				bool flat = true;
			}
		}

		public override void onTick()
		{
			Move();
		}
	}
}
