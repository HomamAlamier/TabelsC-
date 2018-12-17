using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{ 
    class Tables
    {
        public struct pos
        {
            public int cellX;
            public int cellY;
        }
        public struct mystract
        {
           public TextBox[,] TB;
           public string[,] DataBase;
            public pos[] TBPos;
            public int height;
            public int width;
            public System.Drawing.Point point;
            public int TBwidth;
        }
       public static mystract[] Table ;
        public static int TableCount = 0;
        public void CreateTable(int Height,int Width,Form1 frm,int mode,string[] titles = null, int x = 0, int y = 0)
        {
            if (TableCount > 0)
            {
                TableCount++;
                Table = SwapArrays_mystract(TableCount, Table);
            }
            else
            {
                Table = new mystract[1];
               
            }
            Table[TableCount].TB = new TextBox[Height+1, Width+1];
            Table[TableCount].DataBase = new string[Height + 1, Width +1];
            Table[TableCount].TBPos = new pos[Height + 1+Width ];
            Table[TableCount].height = Height;
            Table[TableCount].width = Width;
            Table[TableCount].point = new System.Drawing.Point(x, y);
            Table[TableCount].TBwidth = 100;
        int textboxcount = 0;
            // int x = 0, y = 0;
            int oldx = x;
        for (int i = 0; i <= Height; i++)
         {
                for (int j = 0; j <= Width ; j++)
                {
                    TextBox tx = new TextBox();
                    // Default Parameters
                    tx.TextAlign = HorizontalAlignment.Center;
                    tx.Location = new System.Drawing.Point(x, y);
                    tx.Height = 20;
                    tx.BorderStyle = BorderStyle.FixedSingle;
                    tx.Name = Convert.ToString(i);
                    tx.TabIndex = j;
                    tx.TextChanged += new EventHandler(TB_TextChanged);
                    // Spesific Parameters
                    if (j > 0)
                        tx.Width = 100;
                    else
                    {
                        tx.Width = 30;
                        tx.Enabled = false;
                        if (textboxcount >0) tx.Text = Convert.ToString( textboxcount);
                    }
                                
                if (textboxcount == 0 && mode ==1)
                 {
                        tx.Enabled = false;
                        if (titles != null && j <= titles.Length && mode == 1 && j >0) tx.Text = titles[j-1];
                 } 
                 // return Parameters
                 x += tx.Width-1;
                 frm.Controls.Add(tx);
                 Table[TableCount].TB[i, j] = new TextBox();
                 Table[TableCount].TB[i, j].Tag = tx;

                }
                    textboxcount++;
                    y += 19;
                    x = oldx;
                if (titles != null)
                {
                    int z;
                    if (Width < titles.Length)
                        z = Width;
                    else
                        z = titles.Length;
                    for (int f = 0; f < z ; f++)
                    {
                        Table[TableCount].DataBase[0, f] = titles[f];
                    }
                }
                }

            TableCount++;
        }
        public void ResizeTable( int newWidth,int newY=0, int newHeight = 0, int tableIndex = 0)
        {
            int o = TableCount;
            int w = (newWidth / (Table[tableIndex].width))-4;
            int x = (Table[tableIndex].point.X );
            int y;
            if (newY == 0)
                y = Table[tableIndex].point.Y;
            else
                y = newY;
            Table[tableIndex].TBwidth = w;
            for (int i = 0; i <= Table[tableIndex].height ; i++)
            {
                x = Table[tableIndex].point.X+29;
                for (int j = 0; j <= Table[tableIndex].width ; j++)
                {
                    TextBox tx = Table[tableIndex].TB[i, j].Tag as TextBox;
                    if (j > 0)
                    {
                        tx.Width = w;
                        tx.Location = new System.Drawing.Point(x, y);
                        x += tx.Width - 1;
                    }
                    else
                        tx.Location = new System.Drawing.Point(tx.Location.X, y);
                }
                y += 19;
            }
        }
        public void ResizeColomun(int newWidth,int colNum, int tableIndex = 0)
        {
            int o = TableCount;
            int w = newWidth ;
            TextBox txx = Table[tableIndex].TB[tableIndex, colNum].Tag as TextBox;
            int x = (txx.Location.X);
             int y = txx.Location.Y;
            for (int i = 0; i <= Table[tableIndex].height; i++)
            {
                int xx = (x + newWidth ) - 1;
                for (int j = 0; j <= Table[tableIndex].width; j++)
                {
                    TextBox tx = Table[tableIndex].TB[i, j].Tag as TextBox;
                    if (colNum == j)
                        tx.Width = w;
                    if ( j > colNum)
                    { 
                        tx.Location = new System.Drawing.Point(xx, tx.Location.Y );
                            xx += tx.Width - 1;
                    }
                }
            }
        }
        private mystract[] SwapArrays_mystract(int newMax,mystract[] old)
        {
            mystract[] temp = new mystract[newMax];
            temp = old;
            old = new mystract[newMax] ;
            return temp;
        }
        private string[,] SwapArrays_Database(int newHeight,int newWidth, string[,] old)
        {
            string[,] temp = new string[newHeight,newWidth ];
            temp = old;
            old = new string[newHeight,newWidth ];
            return temp;
        }
        public int GetWidth(int tableIndex = 0)
        {
            int temp=0;
            for (int i = 0; i <= Table[tableIndex].width ; i++)
            {
                TextBox tx = Table[tableIndex].TB[0, i].Tag as TextBox;
                temp += tx.Width-1;
            }
            return temp;
        }
        public void findCell(string input,int X=0,int Y =0, int tableIndex = 0)
        {
            int i=0, j=0;
            for ( i = 0; i <= Table[tableIndex].height ; i++)
            {
                for (j = 0; j < Table[tableIndex].width ; j++)
                {
                    if (Table[tableIndex].DataBase[i, j] != null && Table[tableIndex].DataBase[i,j].IndexOf(input) >=0 )
                    {
                        TextBox tx = Table[tableIndex].TB[i, j].Tag as TextBox;
                        X = i;
                        Y = j;
                        tx.BackColor = System.Drawing.Color.LightCyan;
                    }
                }
            }
        }
        public void findAndreplace(string input,string input2,int tableIndex =0)
        {
            int i = 0, j = 0;
            for (i = 0; i <= Table[tableIndex].height; i++)
            {
                for (j = 0; j < Table[tableIndex].width; j++)
                {
                    if (Table[tableIndex].DataBase[i, j] != null && Table[tableIndex].DataBase[i, j].IndexOf(input) >= 0)
                    {
                        TextBox tx = Table[tableIndex].TB[i, j].Tag as TextBox;
                        tx.Text = input2;
                    }
                }
            }
        }
        public int GetHeight(int tableIndex = 0)
        {
            return (19 * Table[tableIndex].height) + 19;
        }
        public int GetX(int tableIndex = 0)
        {
            return Table[tableIndex].point.X;
        }
        public int GetY(int tableIndex = 0)
        {
            return Table[tableIndex].point.Y;
        }
        private void TB_TextChanged(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;
            int x;
            if (TableCount > 0)
                x = TableCount - 1;
            else
                x = TableCount;
            Table[x].DataBase[int.Parse(tx.Name), tx.TabIndex] = tx.Text;
        }
        public void saveToFile(string filename,int tableIndex =0)
        {
            System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.OpenOrCreate);
            string x = Table[tableIndex].height + ":" + Table[tableIndex].width + "/" ;
            for (int i = 0; i <= Table[tableIndex].height ; i++)
            {
                for (int j = 0; j <= Table[tableIndex].width ; j++)
                {
                    if (i >=0  && j >= 1)
                    {
                        string u = Table[tableIndex].DataBase[i, j];
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
        private void addData(string input,int x,int y,bool enabl = true ,int tableIndex =0)
        {
            Table[tableIndex].DataBase[x, y] = input;
            TextBox tx = Table[tableIndex].TB[x, y].Tag as TextBox;
            tx.Text = input;
            tx.Enabled = enabl;
        }
    }
}
