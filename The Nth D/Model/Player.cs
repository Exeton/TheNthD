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

			handelPhysics(ref velocityX, 0, map);
			handelPhysics(ref velocityY, 1, map);
		}

		public void handelPhysics(ref int velocity, int dimension, Block[,] map)
		{

			if (velocity > 0)
				velocity--;
			if (velocity < 0)
				velocity++;

			if (velocity == 0)//Yes, this must go after friction calculations
				return;

			int spriteSizeOnAxis = getSize(dimension);

			Vector2 velocityVec = Form1.velocityAndDimensionToVector(1, dimension, velocity);//If Velocity is passed in instead of 1, if velocity is negative, it'll get canceled out
			Vector2 positionVec = new Vector2(x, y);
			Vector2 spriteOffsetVec = Form1.velocityAndDimensionToVector(velocity, dimension, spriteSizeOnAxis - 1);
			Vector2 perpVector = Block.blockSize * Vector2.Normalize(Form1.positivePerpindicularVector(velocityVec));

			positionVec +=velocityVec;//Prevent clipping issues

			if (velocity > 0)
				positionVec += spriteOffsetVec;
			
			for (int i = 0; i < spriteSizeOnAxis / Block.blockSize; i++)
			{
				if (map[(int)positionVec.X / Block.blockSize, (int)positionVec.Y / Block.blockSize].filled == true)
				{
					movePlayerToBlockEdge(velocity, dimension);//Remove redundant calls to this. Like if the player's on the ground
					return;
				}

				positionVec += perpVector;
			}

			addVelocityVector(velocityVec);
		}

		public void movePlayerToBlockEdge(int velocity, int dimension)
		{
			//Set the players position to 9, or 0

			int pos = (int)getEdge(velocity, dimension);//Round float to whole number
			int lastDigit = pos % 10;

			if (velocity > 0)
			{
				int composite = 9 - lastDigit;
				addPos(composite, dimension);
			}
			else
			{
				addPos(-lastDigit, dimension);
			}
		}

		public void addVelocityVector(Vector2 vector)
		{
			x += vector.X;
			y += vector.Y;
		}

		public override void onTick(Block[,] map)
		{
			handelPhysics(map);
		}
	}
}
