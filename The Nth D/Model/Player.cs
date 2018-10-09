using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.Model;

namespace The_Nth_D
{
	class Player : EntityWithPhysics, I2DMovementController
	{
		public static int maxJumpTimer = 25;
		int jumpTimer;


		int movementSpeedCap = 10;
		int movementSpeed = 2;
		int verticalMovementSpeed = 10;


		public Player(Bitmap sprite, int x, int y) : base(sprite, x, y)
		{
		}

		public void handelRightInput()
		{
			//Creates movement speed cap and momentum effect
			velocityX = Math.Min(movementSpeedCap, velocityX + movementSpeed);
		}

		public void handelLeftInput()
		{
			velocityX = Math.Max(-movementSpeedCap, velocityX -movementSpeed);
		}

		public void handelUpInput()
		{
			if (onBlock())
			{
				jumpTimer = maxJumpTimer;
			}
			if (jumpTimer >= 0)
				velocityY = -verticalMovementSpeed;
		}

		public override void onTickHook(Map map)
		{
			jumpTimer--;
		}

		public void handelDownInput()
		{
			//Tie this into acceleration so that different frecquencys of input updates won't affect the fall time.
			velocityY = movementSpeed; 
		}

		public override void onTileCollosion(int dimension, int velocity)
		{
			base.onTileCollosion(dimension, velocity);
			jumpTimer = 0;
		}
	}
}
