using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.Model;

namespace The_Nth_D
{
	class Camera
	{
		Map map;
		List<Entity> entities;
		Form1 form;


		public Camera(Map map, List<Entity> entities, Form1 form)
		{
			this.map = map;
			this.entities = entities;
			this.form = form;
		}

		public void drawFromCenter(Graphics graphics, int centerX, int centerY)
		{
			//Transform the point 1/2 a screen to the top left to be the top left.
			draw(graphics, centerX - form.Width / 2, centerY - form.Height / 2);
		}

		public void draw(Graphics graphics, int left, int top)
		{
			graphics.Clear(Color.White);
			drawMap(graphics, left, top);

			foreach (Entity entity in entities)
			{
				int screenX = (int)entity.x - left;
				int screenY = (int)entity.y - top;
				entity.Draw(graphics, screenX, screenY);
			}
		}

		public void drawMap(Graphics graphics, int left, int top)
		{
			int blockSize = Block.blockSize;

			int xOffset = left / blockSize;
			int yOffset = top / blockSize;

			int screenBlockWidth = form.Width / blockSize;
			int screenBlockHeight = form.Height / blockSize;

			for (int i = xOffset; i < screenBlockWidth + xOffset; i++)
				for (int j = yOffset; j < screenBlockHeight + yOffset; j++)
				{
					if (map[i, j].filled)
					{
						graphics.FillRectangle(map[i, j].brush, blockSize * (i - xOffset), blockSize * (j - yOffset), blockSize, blockSize);
					}
				}
		}
	}
}
