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
					storage.GetObject(i).DrawCircle();
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
		public virtual void SetCoords(int xCoord, int yCoord)
		{
			Console.WriteLine("Object");
		}
		public virtual void DrawCircle()
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
	}
}
