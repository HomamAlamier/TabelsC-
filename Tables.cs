using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
class Tables
{
    public int WindowsMode_FindAndReplace = 10020;
    public int WindowsMode_FindOnly = 10300;
    public struct mytextbox
    {
        public Point location;
        public int width;
        public int height;
        public int allHeight;
        public int allWidth;
        public Color BackColor;
        public Font TextFont;
    }
    public struct mystract
    {
        public string[,] DataBase;
        public int height;
        public int width;
        public mytextbox[,] points;
        public System.Drawing.Point point;
        public int TableX;
        public int TableY;
        public Form InForm;
        public int FoucsI;
        public int FoucsJ;
    }
    public static bool IsCreated = false;
    static bool clicked = false;
    public static mystract Table;
    public static int TableCount = 0;
    public void CreateTable(int Height, int Width, Form frm, int x, int y)
    {
        if (IsCreated == false)
            IsCreated = true;
        else
            return;
        Table.DataBase = new string[Height + 1, Width + 1];
        Table.height = Height;
        Table.width = Width;
        Table.point = new System.Drawing.Point(x, y);
        Table.InForm = frm;
        Table.TableX = x;
        Table.TableY = y;
    }
    public void DrawTable(int width, int height, bool redraw = false)
    {
        if (IsCreated == false) return;
        int x = Table.TableX;
        int y = Table.TableY;
        if (redraw == false) Table.points = new mytextbox[Table.height + 1, Table.width + 1];
        int w = (width / (Table.width));
        int h = 30;
        int hh = h;
        h = h * Table.height;
        int wr = (width % (Table.width));
        Graphics g;
        g = Table.InForm.CreateGraphics();
        g.Clear(Color.White);
        Pen myPen = new Pen(Color.Black);
        myPen.Width = 1;
        int x2 = 0, y2;
        int cWidth = w * Table.width;
        for (int i = 0; i <= Table.height; i++)
        {
            for (int j = 0; j <= Table.width; j++)
            {
                if (redraw == false) Table.points[i, j].TextFont = new Font("Arial", 10);
                Table.points[i, j].allWidth = width;
                Table.points[i, j].allHeight = height;
                //TextBox tx = Table.TB[0, j].Tag as TextBox;
                Table.points[i, j].location = new Point(x2, 0);
                x2 = w * j;
                g.DrawLine(myPen, x2, y, x2, y + h);
                Table.points[i, j].width = w;
                y2 = hh * i;
                y2 += y;
                g.DrawLine(myPen, x, y2, cWidth, y2);
                Table.points[i, j].location = new Point(Table.points[i, j].location.X, y2);
                Table.points[i, j].height = hh;
            }
        }
        g.DrawLine(myPen, x, y, cWidth, y);
        g.DrawLine(myPen, x, y, x, y + h);
        g.DrawLine(myPen, x, y + h, cWidth, y + h);
        for (int i = 0; i <= Table.height; i++)
        {
            for (int j = 0; j <= Table.width; j++)
            {

                if (Table.DataBase[i, j] != null)
                {

                    SolidBrush br = new SolidBrush(Color.Black);
                    string tT = Table.DataBase[i, j];
                    int z = Convert.ToInt32(Table.points[i, j].TextFont.Size);
                    z -= 3;
                    int max = Table.points[i, j].width / z;
                    if (max > 3 && tT.Length > max) tT = tT.Substring(0, max - 3) + "...";
                    g.DrawString(tT, Table.points[i, j].TextFont, br, new Point(Table.points[i, j].location.X, (Table.points[i, j].location.Y - Table.points[i, j].height) + 2));
                }
            }
        }
        for (int i = 0; i <= Table.height; i++)
        {
            for (int j = 0; j <= Table.width; j++)
            {

                Color c = Color.FromArgb(0, 0, 0, 0);
                if (Table.points[i, j].BackColor.IsEmpty == true)
                    Table.points[i, j].BackColor = Color.White;
                else
                    SetCellColor(Table.points[i, j].BackColor, i, j);
            }
        }
        if (redraw == false)
        {
            Table.FoucsI = 0;
            Table.FoucsJ = 1;
        }
        int xR = Table.points[Table.FoucsI, Table.FoucsJ].location.X;
        int yR = Table.points[Table.FoucsI, Table.FoucsJ].location.Y;
        int xR2 = Table.points[Table.FoucsI, Table.FoucsJ].width;
        int yR2 = Table.points[Table.FoucsI, Table.FoucsJ].height - 10;
        Rectangle rec = new Rectangle(xR, yR, xR2, yR2 + 10);
        g = Table.InForm.CreateGraphics();
        int R, G, B;
        if (Table.points[Table.FoucsI, Table.FoucsJ].BackColor.R > 10)
            R = Table.points[Table.FoucsI, Table.FoucsJ].BackColor.R - 10;
        else
            R = Table.points[Table.FoucsI, Table.FoucsJ].BackColor.R+10;
        if (Table.points[Table.FoucsI, Table.FoucsJ].BackColor.G > 10)
            G = Table.points[Table.FoucsI, Table.FoucsJ].BackColor.G - 10;
        else
            G = Table.points[Table.FoucsI, Table.FoucsJ].BackColor.G+10;
        if (Table.points[Table.FoucsI, Table.FoucsJ].BackColor.B > 10)
            B = Table.points[Table.FoucsI, Table.FoucsJ].BackColor.B - 10;
        else
            B = Table.points[Table.FoucsI, Table.FoucsJ].BackColor.B+10;
        Pen pn = new Pen(Color.FromArgb(255, R, G, B));
        pn.Width = 2;
        g.DrawRectangle(pn, rec);
    }
    public Point GetSelectedCell()
    {
        if (IsCreated == false) return new Point(0, 0);
        int i, j;
        i = Table.FoucsI;
        j = Table.FoucsJ;
        return new Point(i, j);
    }
    public void selectCell(int i, int j)
    {
        if (IsCreated == false) return;
        Table.FoucsI = i;
        Table.FoucsJ = j;
        DrawTable(Table.points[0, 0].allWidth, Table.points[0, 0].allHeight, true);
    }
    public Point GetCellAxis(int i,int j)
    {
        return Table.points[i, j].location;
    }
    public Point GetTableAxis()
    {
        return new Point(Table.width, Table.height);
    }
        public void ClickOnTable(int x,int y)
        {
            if (IsCreated == false) return;
            if (clicked==false )
            {
                for (int i = 0; i <= Table.height ; i++)
                {
                    for (int j = 1; j <= Table.width; j++)
                    {
                        if (Table.points[i,j].location.X < x && (Table.points[i,j].location.X + Table.points[i,j].width) > x && (Table.points[i,j].location.Y) > y)
                        {
                           //params
                            TextBox tx = new TextBox();
                        tx.MinimumSize = new Size(Table.points[i, j].width, Table.points[i, j].height + 1);
                            tx.Height = Table.points[i,j].height + 1;
                            tx.Width = Table.points[i,j].width;
                        tx.Location = new Point(Table.points[i, j].location.X + 1, Table.points[i, j].location.Y - Table.points[i, j].height);
                            //tx.Multiline = true;
                            tx.Leave += new EventHandler(textVlick_Leave);
                            tx.KeyPress += new KeyPressEventHandler(textClick_KeyPress);
                            tx.TextChanged += new EventHandler(textClick_TextChanged);
                            tx.TabIndex = j;
                            tx.Name = i.ToString();
                            tx.BackColor = Color.LawnGreen;
                            tx.Text = Table.DataBase[i, j];
                        tx.Font = Table.points[i, j].TextFont;
                            // add
                            Table.InForm.Controls.Add(tx); 
                            tx.TextAlign = HorizontalAlignment.Center;
                            tx.Focus();
                        clicked = true;
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
            DrawTable( Table.points[0, 1].allWidth, Table.points[0, 1].allHeight,true);
        clicked = false;
        }
        private void textClick_KeyPress(object sender,KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                TextBox tx = (TextBox)sender;
                tx.Dispose();
                DrawTable( Table.points[0, 1].allWidth, Table.points[0, 1].allHeight,true);
            clicked = false;
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
        public void findCell(string input)
        {
            if (IsCreated == false) return;
            int i=0, j=0;
            for ( i = 0; i <= Table.height ; i++)
            {
                for (j = 0; j < Table.width ; j++)
                {
                    if (Table.DataBase[i, j] != null && Table.DataBase[i,j].IndexOf(input) >=0 )
                    {
                    SetCellColor(Color.LightCyan, i, j);
                    }
                }
            }
        }
    private void SetCellColor(Color col,int i,int j)
    {
        if (IsCreated == false) return;
        Table.points[i, j].BackColor = col;
        Graphics g = Table.InForm.CreateGraphics();
        int x = Table.points[i, j].location.X + 1;
        int y = Table.points[i, j].location.Y - Table.points[i, j].height;
        y++;
        Rectangle rec = new Rectangle(new Point(x, y), new Size(Table.points[i, j].width - 1, Table.points[i, j].height - 1));
        Region r = new Region(rec);
        g.FillRegion(new SolidBrush(col), r);
        string tT = Table.DataBase[i, j];
        int z = Convert.ToInt32(Table.points[i, j].TextFont.Size);
        z-=3;
        int max = Table.points[i, j].width / z;
        if (max > 3 && tT != null && tT.Length > max) tT = tT.Substring(0, max - 3) + "...";
        g.DrawString(tT, Table.points[i,j].TextFont, new SolidBrush(Color.Black), new Point(x, y));
    }
    public void ColorCell(Color col,int x , int y)
    {
        if (IsCreated == false) return;
        int i = 0, j = 0;
        for (i = 0; i < Table.height; i++)
        {
            for (j = 0; j < Table.width; j++)
            {
                if (Table.points[i,j].location.X <= x && (Table.points[i, j].location.Y - Table.points[i, j].height ) <=y && x <= (Table.points[i, j].location.X+ Table.points[i, j].width))
                {
                    SetCellColor(Color.Red, i, j);
                }
            }
        }
    }
    public void FontCell(Font fnt, int x, int y)
    {
        if (IsCreated == false) return;
        int i = 0, j = 0;
        for (i = 0; i < Table.height; i++)
        {
            for (j = 0; j < Table.width; j++)
            {
                if (Table.points[i, j].location.X <= x && (Table.points[i, j].location.Y - Table.points[i, j].height) <= y && x <= (Table.points[i, j].location.X + Table.points[i, j].width))
                {
                    Table.points[i, j].TextFont = fnt;
                }
            }
        }
        DrawTable(Table.points[0, 0].allWidth, Table.points[0, 0].allHeight, true);
    }
    public void findAndreplace(string input,string input2)
        {
            if (IsCreated == false) return;
            int i = 0, j = 0;
            for (i = 0; i <= Table.height; i++)
            {
                for (j = 0; j < Table.width; j++)
                {
                    if (Table.DataBase[i, j] != null && Table.DataBase[i, j].IndexOf(input) >= 0)
                    {
                        Table.DataBase[i, j] = input2;
                    }
                }
            }
            DrawTable(Table.points[0, 0].allWidth, Table.points[0, 0].allHeight,true);
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
            if (IsCreated == false) return;
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
        public void openFormFile(string filename,Form frm)
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
            CreateTable(h, w, frm, 1,25);
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
                        Table.DataBase[i, j] = dat;
                    else
                        Table.DataBase[i, j] = dat;
                    pointer += co;
                    oldpointer = pointer;
                }
            }
            DrawTable(Table.InForm.Width - 20, Table.InForm.Height - 1);
        }

        public void CreateFindWindow(int WindowsMode)
        {
            if (IsCreated == false) return;
            System.Drawing.Size size =new Size();
            if (WindowsMode == WindowsMode_FindOnly)
                size = new System.Drawing.Size(200, 80);
            else
                size = new System.Drawing.Size(200, 130);
            Form inputBox = new Form();
            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            if (WindowsMode == WindowsMode_FindOnly)
                inputBox.Text = "Find";
            else
                inputBox.Text = "Find and replace";
            Label lbl = new Label();
            lbl.Text = "Enter a search key word :";
            lbl.Location = new Point(5, 5);
            lbl.Height = 20;
            inputBox.Controls.Add(lbl);
            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10 , 23);
            textBox.Location = new System.Drawing.Point(5, 25);
            textBox.Text = "";
            inputBox.Controls.Add(textBox);
            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            if (WindowsMode == WindowsMode_FindOnly)
                okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 49);
            else
                okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 99);
            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            if (WindowsMode == WindowsMode_FindOnly)
                cancelButton.Location = new System.Drawing.Point(size.Width - 80, 49);
            else
                cancelButton.Location = new System.Drawing.Point(size.Width - 80, 99);
            TextBox text2 = new TextBox();
            if (WindowsMode == WindowsMode_FindAndReplace)
            {
                Label lbl2 = new Label();
                lbl2.Text = "Enter a replace key word :";
                lbl2.Location = new Point(5, 48);
                lbl2.Height = 20;
                inputBox.Controls.Add(lbl2);
                text2.Width = textBox.Width;
                text2.Location = new Point(5, 68);
                inputBox.Controls.Add(text2);
            }
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.Controls.Add(okButton);
            inputBox.Controls.Add(cancelButton);
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;
            inputBox.ShowDialog();
            if (WindowsMode == WindowsMode_FindOnly)
                findCell(textBox.Text);
            else
                findAndreplace(textBox.Text, text2.Text);
        }  
    }

