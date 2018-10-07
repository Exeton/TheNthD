using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using The_Nth_D.Controller;

namespace The_Nth_D
{
	public partial class Form1 : Form
	{
		KeysManager keyManager;
		Player player;
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Bitmap playerSprite;
			//playerSprite = new Bitmap(Bitmap.FromFile(@""));
			playerSprite = createBox(100, 100, Color.Black);

			player = new Player(playerSprite, 100, 600);
			keyManager = new KeysManager(player);

			Timer gameLoop = new Timer();
			gameLoop.Interval = 10;
			gameLoop.Tick += GameLoop_Tick;
			gameLoop.Start();

		}

		private void GameLoop_Tick(object sender, EventArgs e)
		{
			draw();
			keyManager.handelInput();
			player.handelPhysics();
		}

		private void draw()
		{
			Graphics screenGraphics = this.CreateGraphics();

			Bitmap buffer = new Bitmap(Width, Height);
			Graphics bufferGrpahics = Graphics.FromImage(buffer);

			bufferGrpahics.Clear(Color.White);
			bufferGrpahics.DrawImage(player.sprite, player.x, player.y);

			screenGraphics.DrawImage(buffer, 0, 0);
		}


		//Method Source https://stackoverflow.com/questions/1720160/how-do-i-fill-a-bitmap-with-a-solid-color
		private Bitmap createBox(int width, int height, Color color)
		{
			Bitmap Bmp = new Bitmap(width, height);
			using (Graphics gfx = Graphics.FromImage(Bmp))
			using (SolidBrush brush = new SolidBrush(color))
			{
				gfx.FillRectangle(brush, 0, 0, width, height);
			}

			return Bmp;
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			setKeyValue(e.KeyCode, false);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			setKeyValue(e.KeyCode, true);
		}

		private void setKeyValue(Keys keyCode, bool value)
		{
			switch (keyCode)
			{
				case Keys.W:
					keyManager.keys[0] = value;
					break;
				case Keys.S:
					keyManager.keys[1] = value;
					break;
				case Keys.A:
					keyManager.keys[2] = value;
					break;
				case Keys.D:
					keyManager.keys[3] = value;
					break;
			}
		}
	}
}
