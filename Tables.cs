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
        public struct mystract
        {
           public TextBox[,] TB;
           public string[,] DataBase;
            public int[] TBPos;
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
            Table[TableCount].DataBase = new string[Height + 1, Width ];
            Table[TableCount].TBPos = new int[Height + 1];
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
                    tx.Name = i.ToString();
                    tx.TabIndex = TableCount;
                    Table[TableCount].TBPos[i] = j;
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
                    for (int f = 0; f < titles.Length ; f++)
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
                      //  if (i > 0 )
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
           
           // temp--;
            return temp;
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
            Table[tx.TabIndex].DataBase[int.Parse(tx.Name), Table[tx.TabIndex].TBPos[int.Parse(tx.Name)]] = tx.Text;
        }
    }
}
