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
			velocityY = movementSpeed;
		}
		public void handelDownInput()
		{
			//Tie this into acceleration so that different frecquencys of input updates won't affect the fall time.
			velocityY = -movementSpeed; 
		}

		public void handelPhysics()
		{
			if (velocityX > 0)
				velocityX--;
			if (velocityX < 0)
				velocityX++;

			if (velocityY > 0)
				velocityY--;
			if (velocityY < 0)
				velocityY++;


			x += velocityX;
			y -= velocityY; //Changes down to negative and up to positive
		}

		public override void onTick()
		{
			handelPhysics();
		}
	}
}
