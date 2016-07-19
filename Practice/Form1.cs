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
using log4net;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Practice
{
    public partial class Form1 : Form
    {
        public Graphics g;
        volatile List<Figure> MainFigures = new List<Figure>();
        static object locker = new object();
        ResourceManager LocRM = new ResourceManager("Practice.Form1", typeof(Form1).Assembly);
        CultureInfo ci = new CultureInfo("en-EN");
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        Thread thread;
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        int speed = 10;
        public Form1()
        {
            InitializeComponent();
            AllocConsole();
            Console.WriteLine("cococo");
            log4net.Config.XmlConfigurator.Configure();
            thread = new Thread(MoveFigures);
            
            thread.Start();

        }

        private void bCircle_Click(object sender, EventArgs e)
        {
            int yMax = pbMain.Height;
            int xMax = pbMain.Width;
            Circle circle = new Circle(xMax, yMax);
            circle.Id = MainFigures.Count();
            treeViewMain.Nodes.Add(LocRM.GetString("bCircle", ci) + circle.Id);
            lock (MainFigures)
            {
                MainFigures.Add(circle);
                circle.CrossFigures += do_Cross;
            }
            pbMain.Invalidate();

        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            //lock (locker)
            {
                foreach (Figure f in MainFigures)
                {
                    f.Draw(e.Graphics);
                    //try
                    //{
                    //    f.Move(pbMain.Width, pbMain.Height);
                    //}
                    //catch (FigureOutOfPictureBoxException ex)
                    //{
                    //    //timer1.Stop();
                    //    //DialogResult result = MessageBox.Show(ex.Message);
                    //    //if (result == DialogResult.OK)
                    //    //{
                    //    //f.BackToPictureBox(pbMain.Width, pbMain.Height);
                    //    //timer1.Start();
                    //    //}
                    //    string str = "Figure " + f.Id + " is out of pb at " + DateTime.Now;
                    //    log.Info(str);
                    //    log.Error("", ex);
                    //    f.BackToPictureBox(pbMain.Width, pbMain.Height);

                    //}

                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CrossFigures(MainFigures, pbMain.Width, pbMain.Height);
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
            //MessageBox.Show(Figures.Triangle.ToString());

        }

        private void bTriangle_Click(object sender, EventArgs e)
        {
            int yMax = pbMain.Height;
            int xMax = pbMain.Width;
            Triangle triangle = new Triangle(xMax, yMax);
            triangle.Id = MainFigures.Count();
            treeViewMain.Nodes.Add(LocRM.GetString("b" + triangle.GetType().Name, ci) + triangle.Id);
            lock (MainFigures)
            {
                MainFigures.Add(triangle);
                triangle.CrossFigures += do_Cross;
            }
            pbMain.Invalidate();

        }

        private void bRectangle_Click(object sender, EventArgs e)
        {
            int yMax = pbMain.Height;
            int xMax = pbMain.Width;
            Rectangle rectangle = new Rectangle(xMax, yMax);
            rectangle.Id = MainFigures.Count();
            treeViewMain.Nodes.Add(LocRM.GetString("b" + rectangle.GetType().Name, ci) + rectangle.Id);
            lock (MainFigures)
            {
                MainFigures.Add(rectangle);
                rectangle.CrossFigures += do_Cross;
            }
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
            //TODO Исправить замену контролов в меню
            LocalizeTreeView("en-EN");
            ci = new CultureInfo("en-EN");
            ChangeLocalization();


        }

        private void ChangeLocalization()
        {
            foreach (Control c in this.Controls)
            {
                foreach (Control c1 in c.Controls)
                {
                    c1.Text = LocRM.GetString(c1.Name, ci);
                }
            }

            foreach (ToolStripDropDownItem item in menuStrip1.Items)
            {
                item.Text = LocRM.GetString(item.Name, ci);
                if (item is ToolStripDropDownItem)
                {
                    foreach (ToolStripItem dropDownItem in ((ToolStripDropDownItem)item).DropDownItems)
                    {
                        dropDownItem.Text = LocRM.GetString(dropDownItem.Name, ci);
                    }
                }
            }
        }

        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalizeTreeView("ru-RU");
            ci = new CultureInfo("ru-RU");
            ChangeLocalization();


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

        private void MoveFigures()
        {
            while (true)
            {
                //lock (MainFigures)
                {
                    Thread.Sleep(speed);
                    foreach (Figure f in MainFigures.ToArray<Figure>())
                    {
                        if (f != null)
                        {
                            try
                            {
                                f.Move(pbMain.Width, pbMain.Height);
                            }
                            catch (FigureOutOfPictureBoxException ex)
                            {
                                //timer1.Stop();
                                //DialogResult result = MessageBox.Show(ex.Message);
                                //if (result == DialogResult.OK)
                                //{
                                //    f.BackToPictureBox(pbMain.Width, pbMain.Height);
                                //    timer1.Start();
                                //    //}
                                string str = "Figure " + f.Id + " is out of pb at " + DateTime.Now;
                                log.Info(str);
                                log.Error("", ex);
                                f.BackToPictureBox(pbMain.Width, pbMain.Height);

                                }
                        }
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainFigures.Clear();
            treeViewMain.Nodes.Clear();
        }

        public void do_Cross(object sender, CrossFiguresEventArgs e)
        {
            Console.WriteLine("{0} at time {1}", e.Print(), DateTime.Now);
        }

        public static void CrossFigures(List<Figure> figures, int xMAx, int yMax)
        {
            if (figures.Count > 1)
            {
                foreach (Figure f1 in figures)
                {
                    foreach (Figure f2 in figures)
                    {
                        if (f1.Equals(f2)) continue;
                        if (CrossChecking.CrossByType(f1, f2, xMAx, yMax))
                            f1.DoCrossFigures(f1, f2);
                            
                    }
                }
            }
        }
    }
}
