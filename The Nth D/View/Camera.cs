using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using The_Nth_D.Model;
using The_Nth_D.View;
using The_Nth_D.View.MapCaching;

namespace The_Nth_D
{
	class Camera
	{
		Map map;
		List<Entity> entities;
		Form1 form;
		ArrayMapCacher arrayMapCacher;

		Stopwatch diagonsiticTimer = new Stopwatch();
		int drawnFrames = 0;

		public Camera(Map map, List<Entity> entities, Form1 form, ArrayMapCacher arrayMapCacher)
		{
			this.map = map;
			this.entities = entities;
			this.form = form;
			this.arrayMapCacher = arrayMapCacher;
		}


		public int toWorldX(int screenX, int cameraWorldX) {
			return screenX + (cameraWorldX - form.Width / 2);
		}

		public int toWorldY(int screenY, int cameraWorldY)
		{
			return screenY + (cameraWorldY - form.Height / 2);
		}

		public void drawFromCenter(Graphics graphics, int centerX, int centerY)
		{
			//Transform the point 1/2 a screen to the top left to be the top left.
			draw(graphics, centerX - form.Width / 2, centerY - form.Height / 2);
		}

		public void draw(Graphics graphics, int left, int top)
		{
			//runMapDrawTest(graphics, left, top);
			drawMap2(graphics, left, top);
			
			foreach (Entity entity in entities)
			{
				int screenX = (int)entity.x - left;
				int screenY = (int)entity.y - top;
				entity.Draw(graphics, screenX, screenY);
			}
		}

		private void runMapDrawTest(Graphics graphics, int left, int top)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			for (int i = 0; i < 10000; i++)
			{
				drawMap2(graphics, left, top);
			}

			stopwatch.Stop();
			long elasped = stopwatch.ElapsedMilliseconds;
			int breakpoint = 0;

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

		public void drawMap2(Graphics graphics, int left, int top)
		{
			int blockSize = Block.blockSize;

			int regionWidthInPixels = MapCacher.regionWidthInBlocks * blockSize;
			int regionHeightInPixels = MapCacher.regionHeightInBlocks * blockSize;

			int xOffset = toRegionCoord(left, regionWidthInPixels);
			int yOffset = toRegionCoord(top, regionHeightInPixels);

			int screenRegionWidth = form.Width / regionWidthInPixels;
			int screenRegionHeight = form.Height / regionHeightInPixels;

			int remX = left % regionWidthInPixels;
			int remY = top % regionHeightInPixels;

			int drawnMaps = 0;

			for (int i = 0; i < screenRegionWidth + 2; i++)
				for (int j = 0; j < screenRegionHeight + 2; j++)
				{
					int xPos = regionWidthInPixels * i - remX;
					if (remX < 0)
						xPos -= regionWidthInPixels;

					int yPos = regionHeightInPixels * j - remY;
					if (remY < 0)
						yPos -= regionHeightInPixels;



					if (xPos > form.Width || yPos > form.Height)
						continue;//Although system graphics likely already preforms culling, this prevents excess calls to mapCacher.getCachedRegion()

					Bitmap mapSection = arrayMapCacher.getCachedRegion(i + xOffset, j + yOffset);


					graphics.DrawImage(mapSection, xPos, yPos);
					drawnMaps++;
				}

			int k = drawnMaps;
		}

		//This method makes it so negative screenCoords don't round towards 0
		public int toRegionCoord(int screenCoord, int regionWidthInPixels)
		{
			if (screenCoord >= 0)
				return screenCoord / regionWidthInPixels;
			else if (screenCoord % regionWidthInPixels == 0)//Rounding will not occur if the screenCoord is a - multiple of the regionWidth
				return screenCoord / regionWidthInPixels;
			else
				return (screenCoord / regionWidthInPixels) - 1;
		}
	}
}
