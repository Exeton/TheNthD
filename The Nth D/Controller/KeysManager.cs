using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.Controller
{
	class KeysManager
	{
		I2DMovementController movementController;

		public KeysManager(I2DMovementController movementController)
		{
			this.movementController = movementController;
		}

		//up down left right
		public bool[] keys = new bool[4];

		public void handelInput()
		{
			if (keys[0])
				movementController.handelUpInput();
			if (keys[1])
				movementController.handelDownInput();
			if (keys[2])
				movementController.handelLeftInput();
			if (keys[3])
				movementController.handelRightInput();
		}
	}
}
