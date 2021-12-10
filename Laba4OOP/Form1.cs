using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba4OOP
{
    public partial class Form1 : Form
    {
		Storage storage = new Storage(30);
		int numberOfCircles = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
			for (int i = 0; i < storage.GetCount(); i++)
			{
				if (storage.HaveObject(i))
				{
					if (storage.GetObject(i).CheckClickOnCircle(e.X, e.Y))
						return;
				}
			}
			storage.SetObject(numberOfCircles, new CCircle());
			storage.GetObject(numberOfCircles).SetCoords(e.X, e.Y);
			numberOfCircles++;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
		{
			for (int i = 0; i < storage.GetCount(); i++)
            {
				if (storage.HaveObject(i))
				{
					if(storage.GetObject(i).chosen)
						storage.GetObject(i).DrawRedCircle();
					else
						storage.GetObject(i).DrawCircle();
				}
			}
		}

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
			for (int i = 0; i < storage.GetCount(); i++)
			{
				if (storage.HaveObject(i))
				{
					if (Control.ModifierKeys == Keys.Control && e.Button == System.Windows.Forms.MouseButtons.Left)
					{
						if (storage.GetObject(i).CheckClickOnCircle(e.X, e.Y))
							storage.GetObject(i).chosen = true;
					}
					else if(e.Button == System.Windows.Forms.MouseButtons.Left)
						if (storage.HaveObject(i))
							if (storage.GetObject(i).CheckClickOnCircle(e.X, e.Y))
								storage.GetObject(i).chosen = true;
							else
								storage.GetObject(i).chosen = false;
				}
			}
		}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < storage.GetCount(); i++)
            {
				if (storage.HaveObject(i))
					if (storage.GetObject(i).chosen)
					{
                        storage.GetObject(i).DeleteCircle();
                        storage.DeleteObject(i);
					}
			}
        }
    }

    class Storage
	{
		private int size;
		Object[] storage;

		Storage()
		{
			size = 0;
		}

		public Storage(int size)
		{
			this.size = size;
			storage = new Object[size];
		}

		public void SetObject(int i, Object newObject)
		{
			storage[i] = newObject;
            for (int j = 0; j < storage.Length; j++)
            {
				if (storage[j] != null)
					storage[j].chosen = false;
            }
			storage[i].chosen = true;
		}

		public Object GetObject(int i)
		{
			return storage[i];
		}

		public int GetCount()
		{
			return size;
		}

		public void DeleteObject(int i)
		{
			storage[i] = null;
		}

		public void DestroyObject(int i)
		{
			if (size != 0)
			{
				DeleteObject(i);
			}
		}

		public bool HaveObject(int i)
		{
			if (storage[i] != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	//базовый класс
	class Object
	{
		public bool chosen = false;
		public virtual void SetCoords(int xCoord, int yCoord)
		{
			Console.WriteLine("Object");
		}
		public virtual void DrawCircle()
		{
			Console.WriteLine("Object");
		}
		public virtual void DrawRedCircle()
		{
			Console.WriteLine("Object");
		}

		public virtual bool CheckClickOnCircle(int x, int y)
		{
			return false;
		}
		public virtual void DeleteCircle()
		{
			Console.WriteLine("Object");
		}
	}

	class CCircle : Object
	{
		private int xCoord;
		private int yCoord;
        private int circleWidth = 30;
        private int circleHeight = 30;

        public override void SetCoords(int xCoord, int yCoord)
        {
			this.xCoord = xCoord;
			this.yCoord = yCoord;
        }

		public override void DrawCircle()
        {
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			Rectangle ellipse = new Rectangle(xCoord, yCoord, circleWidth, circleHeight);
			formGraphics.DrawEllipse(myPen, ellipse);
			myPen.Dispose();
			formGraphics.Dispose();
		}

		public override void DrawRedCircle()
		{
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			Rectangle ellipse = new Rectangle(xCoord, yCoord, circleWidth, circleHeight);
			formGraphics.DrawEllipse(myPen, ellipse);
			myPen.Dispose();
			formGraphics.Dispose();
		}

		public override void DeleteCircle()
		{
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.White);
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			Rectangle ellipse = new Rectangle(xCoord, yCoord, circleWidth, circleHeight);
			formGraphics.DrawEllipse(myPen, ellipse);
			myPen.Dispose();
			formGraphics.Dispose();
		}

		public override bool CheckClickOnCircle(int x, int y)
		{
			if (((x - circleWidth) < xCoord) && (x + circleWidth > xCoord) && ((y - circleWidth - circleWidth) < yCoord) && (y + circleWidth > yCoord))
				return true;
			else
				return false;
		}
	}
}
