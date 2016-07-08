using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;
using System.Threading;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Practice
{
    public partial class Form1 : Form
    {
        public Graphics g;
        List<Figure> MainFigures = new List<Figure>();
        ResourceManager LocRM = new ResourceManager("Practice.Form1", typeof(Form1).Assembly);
        CultureInfo ci = new CultureInfo("en-EN");
        public Form1()
        {
            InitializeComponent();

        }

        private void bCircle_Click(object sender, EventArgs e)
        {
            int yMax = pbMain.Height;
            int xMax = pbMain.Width;
            Circle circle = new Circle(xMax, yMax );
            circle.Id = MainFigures.Count();
            treeViewMain.Nodes.Add(LocRM.GetString("bCircle", ci) + circle.Id);
            MainFigures.Add(circle);
            pbMain.Invalidate();
            
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            foreach (Figure f in MainFigures)
            {
                f.Draw(e.Graphics);
                f.Move(pbMain.Width, pbMain.Height);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            string stopFigure = treeViewMain.SelectedNode.Text;
            string stopFigureNumber = Regex.Match(stopFigure, @"\d+").Value;
            int stopFigureIndex = Convert.ToInt32(stopFigureNumber);
            var figure = MainFigures[stopFigureIndex];
            figure.IsMoved = !(figure.IsMoved);
            bStop.Text = figure.IsMoved ? LocRM.GetString("bStop", ci) : LocRM.GetString("StartButton", ci);

        }

        private void bTriangle_Click(object sender, EventArgs e)
        {
            int yMax = pbMain.Height;
            int xMax = pbMain.Width;
            Triangle triangle = new Triangle(xMax, yMax);
            triangle.Id = MainFigures.Count();
            treeViewMain.Nodes.Add(LocRM.GetString("b" + triangle.GetType().Name, ci) + triangle.Id);
            MainFigures.Add(triangle);
            pbMain.Invalidate();

        }

        private void bRectangle_Click(object sender, EventArgs e)
        {
            int yMax = pbMain.Height;
            int xMax = pbMain.Width;
            Rectangle rectangle = new Rectangle(xMax, yMax);
            rectangle.Id = MainFigures.Count();
            treeViewMain.Nodes.Add(LocRM.GetString("b" + rectangle.GetType().Name, ci) + rectangle.Id);
            MainFigures.Add(rectangle);
            pbMain.Invalidate();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                MainFigures = Support.Deserealize(of.FileName);
                treeViewMain.Nodes.Clear();
                foreach (Figure f in MainFigures)
                {
                    treeViewMain.Nodes.Add(LocRM.GetString("b" + f.GetType().Name, ci) + f.Id.ToString());
                }
                pbMain.Invalidate();
            }


        }


        private void binToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                Support.SerializeToBin(MainFigures, sf.FileName);
            }
        }
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalizeTreeView("en-EN");
            ci = new CultureInfo("en-EN");
            
            foreach (Control c in this.Controls)
            {
                foreach (Control c1 in c.Controls)
                {
                    c1.Text = LocRM.GetString(c1.Name, ci);
                }
            }

        }

        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalizeTreeView("ru-RU");
            ci = new CultureInfo("ru-RU");
            foreach (Control c in this.Controls)
            {
                foreach (Control c1 in c.Controls)
                {
                    c1.Text = LocRM.GetString(c1.Name, ci);
                }
            }

            
        }

        private void LocalizeTreeView(string lang)
        {
            foreach (TreeNode node in treeViewMain.Nodes)
            {
                    node.Text = node.Text.Replace(LocRM.GetString("bCircle", ci), LocRM.GetString("bCircle", new CultureInfo(lang)));
                    node.Text = node.Text.Replace(LocRM.GetString("bTriangle", ci), LocRM.GetString("bTriangle", new CultureInfo(lang)));
                    node.Text = node.Text.Replace(LocRM.GetString("bRectangle", ci), LocRM.GetString("bRectangle", new CultureInfo(lang)));
            }
            treeViewMain.Update();
        }

        private void treeViewMain_AfterSelect(object sender, TreeViewEventArgs e)
        {

            string selectedFigure = treeViewMain.SelectedNode.Text;
            string selectedFigureNumber = Regex.Match(selectedFigure, @"\d+").Value;
            int selectedFigureIndex = Convert.ToInt32(selectedFigureNumber);
            if (!MainFigures[selectedFigureIndex].IsMoved) bStop.Text = LocRM.GetString("StartButton", ci);
            if (MainFigures[selectedFigureIndex].IsMoved) bStop.Text = LocRM.GetString("bStop", ci);
        }

        private void jsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                Support.SerializeToJson(MainFigures, sf.FileName);
            }
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                Support.SerializeToXml(MainFigures, sf.FileName);
            }
        }
    }
}
