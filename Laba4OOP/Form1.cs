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
		Storage storage = new Storage(40);
		List<Group> groupList = new List<Group>();

		int numberOfObjects = 0;
		int numberOfEveryObject = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
			for (int i = 0; i < storage.GetCount(); i++)
			{
				//проверка на нажатие по фигуре
				if (storage.HaveObject(i))
				{
					if (storage.GetObject(i).CheckClickOnObject(e.X, e.Y))
						return;
				}
			}
			if(rbCircle.Checked)
				storage.SetObject(numberOfObjects, new CCircle(), numberOfEveryObject);
			if (rbSquare.Checked)
				storage.SetObject(numberOfObjects, new CSquare(), numberOfEveryObject);
			storage.GetObject(numberOfObjects).SetCoords(e.X, e.Y);
			numberOfObjects++;
			numberOfEveryObject++;
			Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
		{

			for (int i = 0; i < storage.GetCount(); i++)
            {
				if (storage.HaveObject(i))
				{
                    if (storage.GetObject(i).CheckChosen())
                        storage.GetObject(i).DrawRedObject();
                    else if (storage.GetObject(i).colored)
                        storage.GetObject(i).DrawGreenObject();
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
						storage.GetObject(i).CheckChangeChosen(e.X, e.Y);
						Invalidate();
					}
					else if (e.Button == System.Windows.Forms.MouseButtons.Left)
					{
						storage.GetObject(i).CheckChangeUnchosen();
						storage.GetObject(i).CheckChangeChosen(e.X, e.Y);
					}
					Invalidate();
				}
			}
		}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < storage.GetCount(); i++)
            {
				if (storage.HaveObject(i))
					if (storage.GetObject(i).CheckChosen())
					{
                        storage.GetObject(i).DeleteObject();
                        storage.DeleteObject(i);
						Invalidate();
					}
			}
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
			int object0 = 0;

			for (int i = 0; i < storage.GetCount(); i++)
			{
				if (storage.HaveObject(i))
				{
					if (storage.GetObject(i).CheckChosen())
					{
						object0 = i;
						break;
					}
				}
			}

			if (e.KeyData == Keys.Q)
			{
				for (int i = 0; i < storage.GetCount(); i++)
				{
					if (storage.HaveObject(i))
					{
						if (i == object0 - 1)
						{
							storage.GetObject(i).chosen = true;
						}
						else
							storage.GetObject(i).CheckChangeUnchosen();
					}
				}
				Invalidate();
			}

			if (e.KeyData == Keys.E)
			{
				for (int i = 0; i < storage.GetCount(); i++)
				{
					if (storage.HaveObject(i))
					{
						if (i == object0 + 1)
						{
							storage.GetObject(i).chosen = true;
						}
						else
							storage.GetObject(i).CheckChangeUnchosen();
					}
				}
				Invalidate();
			}

			if (e.KeyData == Keys.C)
			{
				for (int i = 0; i < storage.GetCount(); i++)
				{
					if(storage.HaveObject(i))
						if (storage.GetObject(i).CheckChosen())
						{
							if (rbGreen.Checked)
							{
								storage.GetObject(i).colored = true;
								storage.GetObject(i).CheckChangeUnchosen();
							}
						}
				}
				Invalidate();
			}

			if (e.KeyData == Keys.X)
			{
				for (int i = 0; i < storage.GetCount(); i++)
				{
					if (storage.HaveObject(i))
						if (storage.GetObject(i).CheckChosen())
						{
							storage.GetObject(i).SizeUpObject();
						}
				}
				Invalidate();
			}

			if (e.KeyData == Keys.Z)
			{
				for (int i = 0; i < storage.GetCount(); i++)
				{
					if (storage.HaveObject(i))
						if (storage.GetObject(i).CheckChosen())
						{
							storage.GetObject(i).SizeDownObject();
						}
				}
				Invalidate();
			}

			if (e.KeyData == Keys.A)
			{
				for (int i = 0; i < storage.GetCount(); i++)
				{
					if (storage.HaveObject(i))
						if (storage.GetObject(i).CheckChosen())
						{
							storage.GetObject(i).LeftObject();
						}
				}
				Invalidate();
			}

			if (e.KeyData == Keys.D)
			{
				for (int i = 0; i < storage.GetCount(); i++)
				{
					if (storage.HaveObject(i))
						if (storage.GetObject(i).CheckChosen())
						{
							storage.GetObject(i).RightObject();
						}
				}
				Invalidate();
			}

			if (e.KeyData == Keys.W)
			{
				for (int i = 0; i < storage.GetCount(); i++)
				{
					if (storage.HaveObject(i))
						if (storage.GetObject(i).CheckChosen())
						{
							storage.GetObject(i).DownObject();
						}
				}
				Invalidate();
			}

			if (e.KeyData == Keys.S)
			{
				for (int i = 0; i < storage.GetCount(); i++)
				{
					if (storage.HaveObject(i))
						if (storage.GetObject(i).CheckChosen())
						{
							storage.GetObject(i).UpObject();
						}
				}
				Invalidate();
			}

			if (e.KeyData == Keys.Delete)
			{
				for (int i = 0; i < storage.GetCount(); i++)
				{
					if (storage.HaveObject(i))
						if (storage.GetObject(i).CheckChosen())
						{
							storage.DeleteObject(i);
						}
				}
				Invalidate();
			}
		}

        private void btnGroup_Click(object sender, EventArgs e)
        {
			int chosenObjects = 0;
            for (int i = 0; i < storage.GetCount(); i++)
            {
				if (storage.HaveObject(i) && storage.GetObject(i).CheckChosen())
					chosenObjects++;
			}
			groupList.Add(new Group());
			for (int i = 0; i < storage.GetCount(); i++)
            {
				if(storage.HaveObject(i) && storage.GetObject(i).CheckChosen())
                {
					if(storage.GetObject(i) is Group) { 
                        for (int j = 0; j < groupList[storage.GetObject(i).numberInGroupList].GetLengthOfGroup(); j++)
                        {
							groupList[groupList.Count - 1].addObject(groupList[storage.GetObject(i).numberInGroupList].GetObject(j));
							groupList[groupList.Count - 1].numberInGroupList = groupList.Count - 1;
						
						}
						for (int j = 0; j < groupList[storage.GetObject(i).numberInGroupList].GetLengthOfGroup(); j++)
                        {
							groupList[groupList.Count - 1].GetObject(j).numberInGroupList = groupList.Count - 1;
						}
					}
					else 
					{
						groupList[groupList.Count - 1].addObject(storage.GetObject(i));
						for (int j = 0; j < groupList[storage.GetObject(i).numberInGroupList].GetLengthOfGroup(); j++)
                        {
							groupList[groupList.Count - 1].numberInGroupList = groupList.Count - 1;
							groupList[groupList.Count - 1].GetObject(j).numberInGroupList = groupList.Count - 1;
						}
					}
					storage.DeleteObject(i);
                }
			}
			storage.SetObject(numberOfObjects, groupList[groupList.Count - 1], numberOfEveryObject);
			numberOfObjects++;
			Invalidate();
		}

        private void btnUngroup_Click(object sender, EventArgs e)
        {
			int numberOfGroup = 0;

            for (int i = 0; i < groupList.Count; i++)
            {
				if (groupList[i].CheckChosen()) {
					numberOfGroup = i;
				}
            }

            for (int i = 0; i < groupList[numberOfGroup].GetLengthOfGroup(); i++)
            {
				storage.SetObject(numberOfObjects, groupList[numberOfGroup].GetObject(i), groupList[numberOfGroup].GetObject(i).numberOfObject);
				numberOfObjects++;
			}
			groupList[numberOfGroup].DeleteGroup();
			Invalidate();
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
		public void SetObject(int i, Object newObject, int numberOfEveryObject)
		{
			storage[i] = newObject;
			storage[i].numberOfObject = numberOfEveryObject;
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

	//класс группировки для паттерна Composite
	class Group : Object
	{
		Color color;
		Random rand = new Random();

		public Group()
        {
			color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
		}

		private List<Object> groupObj = new List<Object>();

		public int GetLengthOfGroup()
        {
			return groupObj.Count;
        }

		public void DeleteObject(Object obj)
		{
			groupObj.Remove(obj);
		}

		public void addObject(Object obj)
        {
			numberInGroupList = obj.numberInGroupList;
			groupObj.Add(obj);
			obj.chosen = false;
		}
		
		//рисование круга
		public override void DrawObject()
        {
			foreach(Object obj in groupObj) 
			{ 
				System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(color.ToArgb()));
				System.Drawing.Graphics formGraphics;
				formGraphics = Form.ActiveForm.CreateGraphics();
				if (obj is CCircle)
				{
					Rectangle ellipse = new Rectangle(obj.xCoord, obj.yCoord, obj.size, obj.size);
					formGraphics.DrawEllipse(myPen, ellipse);
				}
                else
                {
					Rectangle rtg = new Rectangle(obj.xCoord, obj.yCoord, obj.size, obj.size);
					formGraphics.DrawRectangle(myPen, rtg);
				}
				obj.DrawNumber(obj.numberOfObject);
					
				myPen.Dispose();
				formGraphics.Dispose();
			}
        }

		public override void DrawRedObject()
		{
			foreach (Object obj in groupObj)
			{
				obj.DrawRedObject();
			}
		}

		public override void DrawGreenObject()
		{
			foreach (Object obj in groupObj)
			{
				obj.DrawGreenObject();
			}
		}

		public override bool CheckClickOnObject(int x, int y)
        {
            foreach (Object obj in groupObj)
            {
				if (obj.CheckClickOnObject(x, y))
					return true;
            }
			return false;
		}

		public override void CheckChangeChosen(int x, int y)
        {
			foreach (Object obj in groupObj)
			{
				if (obj.CheckClickOnObject(x, y))
					foreach (Object obj2 in groupObj)
					{
						obj2.chosen = true;
					}
			}
		}

		public override bool CheckChosen()
		{
			foreach (Object obj in groupObj)
			{
				if (obj.CheckChosen())
					return true;
			}

			return false;
		}

		public override void CheckChangeUnchosen()
		{
			foreach (Object obj in groupObj)
			{
				obj.CheckChangeUnchosen();
			}
		}

		public override void UpObject()
		{
			foreach (Object obj in groupObj)
			{
				obj.UpObject();
			}
		}

		public override void DownObject()
		{
			foreach (Object obj in groupObj)
			{
				obj.DownObject();
			}
		}

		public override void RightObject()
		{
			foreach (Object obj in groupObj)
			{
				obj.RightObject();
			}
		}

		public override void LeftObject()
		{
			foreach (Object obj in groupObj)
			{
				obj.LeftObject();
			}
		}
		public override void SizeUpObject()
		{
			foreach (Object obj in groupObj)
			{
				obj.SizeUpObject();
			}
		}

		public Object GetObject(int i)
        {
			return groupObj[i];
        }

		public override void SizeDownObject()
		{
			foreach (Object obj in groupObj)
			{
				obj.SizeDownObject();
			}
		}

		public void DeleteGroup()
        {
			groupObj.Clear();
		}

		//удаление объекта из формы
		public override void DeleteObject()
		{
            foreach (Object obj in groupObj)
            {
				obj.DeleteObject();
            }
			DeleteGroup();
		}
	}

	//базовый класс
	class Object
	{
		public int xCoord;
		public int yCoord;
		public int size = 30;
		public int numberInGroupList;

		//метка выделенности
		public bool chosen = false;

		//метка окрашенности
		public bool colored = false;

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
		public virtual void DrawGreenObject()
		{
			Console.WriteLine("Object");
		}

		public virtual bool CheckChosen()
        {
			if (chosen)
				return true;
			else
				return false;
        }

		//проверка на нажатие на объекта
		public virtual bool CheckClickOnObject(int x, int y)
		{
			if (((x - size) < xCoord) && (x + size > xCoord) && ((y - size - size) < yCoord) && (y + size > yCoord))
				return true;
			else
				return false;
		}

		public virtual void CheckChangeChosen(int x, int y)
        {
			if(CheckClickOnObject(x, y))
				chosen = true;
        }

		public virtual void CheckChangeUnchosen()
		{
			chosen = false;
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

		public virtual void UpObject()
        {
			if (yCoord >= Form.ActiveForm.ClientSize.Height - size - 2)
				yCoord = Form.ActiveForm.ClientSize.Height - size - 2;
			yCoord++;
        }

		public virtual void DownObject()
        {
			if (yCoord <= 1)
				yCoord = 1;
			yCoord--;
        }

		public virtual void RightObject()
        {
			if (xCoord >= Form.ActiveForm.ClientSize.Width - size - 2)
				xCoord = Form.ActiveForm.ClientSize.Width - size - 2;
			xCoord++;
        }

		public virtual void LeftObject()
        {
			if (xCoord <= 1)
				xCoord = 1;
			xCoord--;
        }

		public virtual void SizeUpObject()
		{
			size++;
		}

		public virtual void SizeDownObject()
		{
			size--;
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

		public override void DrawGreenObject()
		{
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Green);
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
			chosen = false;
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
		public override void DrawGreenObject()
		{
			System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Green);
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
			chosen = false;
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
