using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApp4
{ 
    class Tables
    {
        public struct pos
        {
            public int cellX;
            public int cellY;
        }
        public struct mytextbox
        {
            public Point location;
            public int width;
            public int height;
            public int allHeight;
            public int allWidth;
        }
        public struct mystract
        {
           public string[,] DataBase;
            public int height;
            public int width;
            public mytextbox[,] points;
            public System.Drawing.Point point;
 
            public Form1 InForm;
        }
        static bool clicked = false;
       public static mystract Table ;
        public static int TableCount = 0;
        public void CreateTable(int Height,int Width,Form1 frm,int mode,string[] titles = null, int x = 0, int y = 0)
        {
            Table.DataBase = new string[Height + 1, Width +1];
            Table.height = Height;
            Table.width = Width;
            Table.point = new System.Drawing.Point(x, y);
            Table.InForm = frm;
        }
        public void DrawTable(int width,int height,int x,int y)
        {
            Table.points = new mytextbox[ Table.height, Table.width];
            int w = (width / (Table.width)) ;
            int h = (height / (Table.height)) ;

            int hh = h;
            h = h * Table.height;
            int wr = (width % (Table.width));
            Graphics g;
            
            g = Table.InForm.CreateGraphics();
            g.Clear(Color.White);
            Pen myPen = new Pen(Color.Black);
            myPen.Width = 1;
            g.DrawLine(myPen, x, y, x + width, y);
            g.DrawLine(myPen, x, y, x , y+h);
            g.DrawLine(myPen, x, y+h , x+width , y + h);
            g.DrawLine(myPen, x + width, y, x + width, y + h);
            int x2=0, y2;
            for (int i = 0; i < Table.height; i++)
            {
                for (int j = 0; j < Table.width; j++)
                {
                    Table.points[i, j].allWidth = width;
                    Table.points[i, j].allHeight = height;
                    //TextBox tx = Table.TB[0, j].Tag as TextBox;
                    Table.points[i, j].location = new Point(x2, 0);
                    x2 = w * j;
                    g.DrawLine(myPen, x2, y, x2, y + h);
                    Table.points[i, j].width = w;
                    y2 = hh * i;
                    y2 += y;
                    g.DrawLine(myPen, x, y2, x + width, y2);
                    Table.points[i,j].location = new Point(Table.points[i,j].location.X, y2);
                    Table.points[i,j].height = hh;
                }
            }
            for (int i = 0; i < Table.height ; i++)
            {
                for (int j = 0; j < Table.width ; j++)
                {

                if (Table.DataBase[i,j] != null)
                    {
                        string tT= Table.DataBase[i, j];
                        Font fnt = new Font("Arial",10);
                        SolidBrush br = new SolidBrush(Color.Black) ;
                        int max = Table.points[i, j].width / 10;
                        if (tT.Length > max) tT=tT.Substring(0, max) + "...";
                        g.DrawString(tT, fnt, br,new Point(Table.points[i, j].location.X , (Table.points[i, j].location.Y- Table.points[i, j].height) + 2));
                    }

                }
            }

        }
        public void ClickOnTable(int x,int y)
        {
            if (clicked==false )
            {
                for (int i = 0; i < Table.height ; i++)
                {
                    for (int j = 1; j < Table.width; j++)
                    {
                        if (Table.points[i,j].location.X <= x && (Table.points[i,j].location.X + Table.points[i,j].width) >= x && (Table.points[i,j].location.Y) >= y)
                        {
                           //params
                            TextBox tx = new TextBox();
                            tx.Height = Table.points[i,j].height + 1;
                            tx.Width = Table.points[i,j].width;
                            tx.Location = new Point(Table.points[i, j].location.X + 1, Table.points[i, j].location.Y - Table.points[i, j].height);
                            tx.Multiline = true;
                            tx.Leave += new EventHandler(textVlick_Leave);
                            tx.KeyPress += new KeyPressEventHandler(textClick_KeyPress);
                            tx.TextChanged += new EventHandler(textClick_TextChanged);
                            tx.TabIndex = j;
                            tx.Name = i.ToString();
                            tx.BackColor = Color.LawnGreen;
                            tx.Text = Table.DataBase[i, j];
                            // add
                            Table.InForm.Controls.Add(tx); 
                            tx.TextAlign = HorizontalAlignment.Center;
                            tx.Focus();
                            return;
                        }
                    }
                }
            }
        }
        private void textClick_TextChanged(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;
            int i = int.Parse(tx.Name);
            Table.DataBase[i, tx.TabIndex] = tx.Text;
        }
        private void textVlick_Leave(object sender,EventArgs e)
        {
            TextBox tx = (TextBox)sender;
            tx.Dispose();
            DrawTable( Table.points[0, 1].allWidth, Table.points[0, 1].allHeight, Table.points[0, 1].location.X, 25);
        }
        private void textClick_KeyPress(object sender,KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                TextBox tx = (TextBox)sender;
                tx.Dispose();
                DrawTable( Table.points[0, 1].allWidth, Table.points[0, 1].allHeight, Table.points[0, 1].location.X, 25);
            }
            }

        public void ResizeColomun(int newWidth,int colNum)
        {

        }
        private string[,] SwapArrays_Database(int newHeight,int newWidth, string[,] old)
        {
            string[,] temp = new string[newHeight,newWidth ];
            temp = old;
            old = new string[newHeight,newWidth ];
            return temp;
        }
        public int GetWidth()
        {
            return Table.points[0,0].allWidth;
        }
        public void findCell(string input,int X=0,int Y =0 )
        {
            int i=0, j=0;
            for ( i = 0; i <= Table.height ; i++)
            {
                for (j = 0; j < Table.width ; j++)
                {
                    if (Table.DataBase[i, j] != null && Table.DataBase[i,j].IndexOf(input) >=0 )
                    {
                        TextBox tx = new TextBox();
                        
                    }
                }
            }
        }
        public void findAndreplace(string input,string input2)
        {
            int i = 0, j = 0;
            for (i = 0; i <= Table.height; i++)
            {
                for (j = 0; j < Table.width; j++)
                {
                    if (Table.DataBase[i, j] != null && Table.DataBase[i, j].IndexOf(input) >= 0)
                    {

                    }
                }
            }
        }
        public int GetHeight()
        {
            return (19 * Table.height) + 19;
        }
        public int GetX()
        {
            return Table.point.X;
        }
        public int GetY()
        {
            return Table.point.Y;
        }
        public void saveToFile(string filename)
        {
            System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.OpenOrCreate);
            string x = Table.height + ":" + Table.width + "/" ;
            for (int i = 0; i <= Table.height ; i++)
            {
                for (int j = 0; j <= Table.width ; j++)
                {
                    if (i >=0  && j >= 1)
                    {
                        string u = Table.DataBase[i, j];
                        if (u == null) u = "";
                            x += u.Length + ":" + u;
                    }
                }
            }
            Byte[] info = new UTF8Encoding(true).GetBytes(x);
            fs.Write(info, 0, info.Length);
            fs.Close();
        }
        public void openFormFile(string filename,Form1 frm)
        {
            System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.OpenOrCreate);
            byte[] b = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding(true);
            string x;
            fs.Read(b, 0, b.Length);
            fs.Close();
            x = temp.GetString(b);
            string u = x.Substring(0, x.IndexOf('/'));
            int offset = x.IndexOf('/')+1;
            string da = x.Substring(offset ,x.Length-(offset));
            int f1 = u.IndexOf(':');
            int f2 = u.Length;
            int h = int.Parse( u.Substring(0, f1));
            int w = int.Parse(u.Substring(f1+1 , f2-(f1+1)));
            CreateTable(h, w, frm, 1,null,1,25);
            int pointer =0 ;
            int oldpointer=0;
            for (int i =0; i <= h; i++)
            {
                for (int j = 1; j <= w; j++)
                {
                    pointer = da.IndexOf(':', oldpointer+1);
                    if (pointer < 0) break;
                    if (oldpointer == 0) oldpointer = -1;
                    int co = int.Parse(da.Substring(oldpointer + 1, pointer - (oldpointer + 1)));
                    string dat = da.Substring(pointer + 1, co);
                    if (i == 0)
                        addData(dat, i, j, false );
                    else
                        addData(dat, i, j);
                    pointer += co;
                    oldpointer = pointer;
                }
            }

            

        }
        private void addData(string input,int x,int y,bool enabl = true )
        {
            Table.DataBase[x, y] = input;
        }
    }
}
