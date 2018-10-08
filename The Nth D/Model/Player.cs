using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.Model;

namespace The_Nth_D
{
	class Player : Entity, I2DMovementController
	{
		int velocityX;
		int velocityY;
		
		int movementSpeed = 10;

		public Player(Bitmap sprite, int x, int y) : base(sprite, x, y)
		{
		}

		public void handelLeftInput()
		{
			velocityX = -movementSpeed;
		}
		public void handelRightInput()
		{
			velocityX = movementSpeed;
		}
		public void handelUpInput()
		{
			velocityY = -movementSpeed;
		}
		public void handelDownInput()
		{
			//Tie this into acceleration so that different frecquencys of input updates won't affect the fall time.
			velocityY = movementSpeed; 
		}

		public void handelPhysics(Block[,] map)
		{


			velocityY += 2;

			if (velocityY > 10)
				velocityY = 10;

			if (velocityX > 0)
				velocityX--;
			if (velocityX < 0)
				velocityX++;

			if (velocityY > 0)
				velocityY--;
			if (velocityY < 0)
				velocityY++;

			applyXVelocity(velocityX, map);
			applyYVelocity(velocityY, map);
		}

		public void applyXVelocity(int velocity, Block[,] map)
		{
			int hitboxX = (int)x;
			if (velocity > 0)
			{
				hitboxX += sprite.Width;
			}

			for (int i = 0; i < sprite.Height / 10; i++)
			{
				int blockX = (int)(hitboxX / 10);
				int blockY = (int)(y / 10 + i);

				if (map[blockX, blockY].filled == true)
					return;
			}
			x += velocity;
		}


		public void applyYVelocity(int velocity, Block[,] map)
		{
			int hitboxY = (int)y;
			if (velocity > 0)
			{
				hitboxY += sprite.Height;
			}

			for (int i = 0; i < sprite.Width / 10; i++)
			{
				int blockX = (int)(x / 10 + i);
				int blockY = (int)(hitboxY / 10);

				if (map[blockX, blockY].filled == true)
					return;
			}
			y += velocity;
		}

		public override void onTick(Block[,] map)
		{
			handelPhysics(map);
		}
	}
}
