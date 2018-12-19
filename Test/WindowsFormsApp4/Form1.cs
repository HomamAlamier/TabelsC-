using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        Tables t = new Tables();
        public Form1()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Form1_Resize);
            this.MouseClick += new MouseEventHandler(Form1_MouseClick);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
        }

        private void nToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.CreateTable(10, 10, this, 1, 25);
            t.DrawTable(this.Width - 20, this.Height - 45);
        }
       private void Form1_Resize(object sender, EventArgs e)
        {
            //   t.ResizeTable(this.Width - 1);
            t.DrawTable(this.Width - 20, this.Height - 45,true);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void Form1_MouseClick(object sender , MouseEventArgs  e )
        {
            t.ClickOnTable(e.Location.X, e.Location.Y);
        }
        private void Form1_KeyUp(object sender , KeyEventArgs  e )
        {
                Point ij = t.GetSelectedCell();
            Point tA = t.GetTableAxis();
           switch(e.KeyCode)
            {
                case Keys.Down:
                    {
                        if (ij.X != (tA.X - 1))
                            ij = new Point(ij.X + 1, ij.Y);
                        else
                            ij = new Point(0, ij.Y);
                        t.selectCell(ij.X, ij.Y);
                    }
                    return;
                case Keys.Up:
                    {
                        if (ij.X >0)
                            ij = new Point(ij.X- 1, ij.Y);
                        else
                            ij = new Point(tA.X-1, ij.Y);
                        t.selectCell(ij.X, ij.Y);
                    }
                    return;
                case Keys.Right:
                    {
                        if (ij.Y < tA.Y )
                            ij = new Point(ij.X , ij.Y+1);
                        else
                            ij = new Point(ij.X, 1);
                        t.selectCell(ij.X, ij.Y);
                    }return;
                case Keys.Left:
                    {
                        if (ij.Y > 1)
                            ij = new Point(ij.X , ij.Y- 1);
                        else
                            ij = new Point(ij.X, tA.Y);
                        t.selectCell(ij.X, ij.Y);
                        
                    }
                    return;
                case Keys.Enter: return;
            }
            Point CA = t.GetCellAxis(ij.X, ij.Y);
            t.ClickOnTable(CA.X+1, CA.Y+1);

        }
        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.CreateFindWindow(t.WindowsMode_FindOnly);
        
        }

        private void sARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.CreateFindWindow(t.WindowsMode_FindAndReplace);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt|*.txt";
            saveFileDialog1.ShowDialog();
            t.saveToFile(saveFileDialog1.FileName );
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            t.openFormFile(openFileDialog1.FileName,this);
        }

        private void jToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.FontCell(new Font("Arial", 15), 2, 26);
        }

        private void colorTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.ColorCell(Color.LightCyan, 2, 26);
        }

        private void fontTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
