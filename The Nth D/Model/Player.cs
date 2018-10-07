using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D
{
	class Player : I2DMovementController
	{
		public Bitmap sprite;
		int health;

		public int x;
		public int y;

		int velocityX;
		int velocityY;
		
		int movementSpeed = 10;

		public Player(Bitmap sprite, int x, int y)
		{
			this.sprite = sprite;
			this.x = x;
			this.y = y;
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

		
	}
}
