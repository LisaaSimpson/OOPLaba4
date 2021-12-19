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
		//хранилище объектов
		Storage storage = new Storage(30);
		int numberOfObjects = 0;
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
					if (storage.GetObject(i).CheckClickOnObject(e.X, e.Y))
						return;
				}
			}
			if(comboBox1.SelectedIndex == 0)
				storage.SetObject(numberOfObjects, new CCircle());
			else
				storage.SetObject(numberOfObjects, new CSquare());
			storage.GetObject(numberOfObjects).SetCoords(e.X, e.Y);
			numberOfObjects++;
			Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
		{
			for (int i = 0; i < storage.GetCount(); i++)
            {
				if (storage.HaveObject(i))
				{
					if (storage.GetObject(i).chosen)
						storage.GetObject(i).DrawRedObject();
					else
					{
						storage.GetObject(i).DrawObject();
					}
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
						if (storage.GetObject(i).CheckClickOnObject(e.X, e.Y))
						{
							storage.GetObject(i).chosen = true;
							Invalidate();
						}
					}
					else if (e.Button == System.Windows.Forms.MouseButtons.Left)
						if (storage.HaveObject(i))
						{
							if (storage.GetObject(i).CheckClickOnObject(e.X, e.Y))
								storage.GetObject(i).chosen = true;
							else
								storage.GetObject(i).chosen = false;
							Invalidate();
						}
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
                        storage.GetObject(i).DeleteObject();
                        storage.DeleteObject(i);
						Invalidate();
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

		//конструктор
		public Storage(int size)
		{
			this.size = size;
			storage = new Object[size];
		}

		//добавить объект к хранилище
		public void SetObject(int i, Object newObject)
		{
			storage[i] = newObject;
			storage[i].numberOfObject = i;
            for (int j = 0; j < storage.Length; j++)
            {
				if (storage[j] != null)
					storage[j].chosen = false;
            }
			storage[i].chosen = true;
		}

		//ссылка на объект хранилище
		public Object GetObject(int i)
		{
			return storage[i];
		}

		//количество объектов в хранилище
		public int GetCount()
		{
			return size;
		}

		//удалить объект из хранилища 
		public void DeleteObject(int i)
		{
			storage[i] = null;
		}

		//полностью удалить объект
		public void DestroyObject(int i)
		{
			if (size != 0)
			{
				DeleteObject(i);
			}
		}

		//проверка на наличие объекта в хранилище
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
		protected int xCoord;
		protected int yCoord;
		protected int size = 30;

		//метка выделенности
		public bool chosen = false;

		//номер объекта
		public int numberOfObject = 0;

		//задать координаты
		public void SetCoords(int xCoord, int yCoord)
		{
			this.xCoord = xCoord;
			this.yCoord = yCoord;
		}

		//нарисовать черный объект
		public virtual void DrawObject()
		{
			Console.WriteLine("Object");
		}

		//нарисовать красный объект
		public virtual void DrawRedObject()
		{
			Console.WriteLine("Object");
		}

		//проверка на нажатие на объекта
		public bool CheckClickOnObject(int x, int y)
		{
			if (((x - size) < xCoord) && (x + size > xCoord) && ((y - size - size) < yCoord) && (y + size > yCoord))
				return true;
			else
				return false;
		}

		//удаление объекта из формы
		public virtual void DeleteObject()
		{
			Console.WriteLine("Object");
		}

		//рисование номера объекта
		public void DrawNumber(int numberOfCircle)
		{
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			string drawString = numberOfCircle.ToString();
			System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 14);
			System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
			System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
			formGraphics.DrawString(drawString, drawFont, drawBrush, xCoord - size / 2, yCoord - size / 2, drawFormat);
			drawFont.Dispose();
			drawBrush.Dispose();
			formGraphics.Dispose();
		}

		//удаление номера объекта
		public void DeleteNumber(int numberOfCircle)
		{
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			string drawString = numberOfCircle.ToString();
			System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 14);
			System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
			System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
			formGraphics.DrawString(drawString, drawFont, drawBrush, xCoord - size / 2, yCoord - size / 2, drawFormat);
			drawFont.Dispose();
			drawBrush.Dispose();
			formGraphics.Dispose();
		}
	}

	class CCircle : Object
	{

		public override void DrawObject()
        {
			//рисование круга
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			Rectangle ellipse = new Rectangle(xCoord, yCoord, size, size);
			formGraphics.DrawEllipse(myPen, ellipse);
			DrawNumber(this.numberOfObject);

			myPen.Dispose();
			formGraphics.Dispose();
		}

		public override void DrawRedObject()
		{
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			Rectangle ellipse = new Rectangle(xCoord, yCoord, size, size);
			formGraphics.DrawEllipse(myPen, ellipse);
			DrawNumber(this.numberOfObject);

			myPen.Dispose();
			formGraphics.Dispose();
		}

		public override void DeleteObject()
		{
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.White);
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			Rectangle ellipse = new Rectangle(xCoord, yCoord, size, size);
			formGraphics.DrawEllipse(myPen, ellipse);
			DeleteNumber(this.numberOfObject);

			myPen.Dispose();
			formGraphics.Dispose();
		}
	}

	class CSquare : Object
	{

		public override void DrawObject()
		{
			//рисование квадрата
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			Rectangle rtg = new Rectangle(xCoord, yCoord, size, size);
			formGraphics.DrawRectangle(myPen, rtg);
			DrawNumber(this.numberOfObject);

			myPen.Dispose();
			formGraphics.Dispose();
		}

		public override void DrawRedObject()
		{
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			Rectangle rtg = new Rectangle(xCoord, yCoord, size, size);
			formGraphics.DrawRectangle(myPen, rtg);
			DrawNumber(this.numberOfObject);

			myPen.Dispose();
			formGraphics.Dispose();
		}

		public override void DeleteObject()
		{
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.White);
			System.Drawing.Graphics formGraphics;
			formGraphics = Form.ActiveForm.CreateGraphics();
			Rectangle rtg = new Rectangle(xCoord, yCoord, size, size);
			formGraphics.DrawRectangle(myPen, rtg);
			DeleteNumber(this.numberOfObject);

			myPen.Dispose();
			formGraphics.Dispose();
		}
	}
}
