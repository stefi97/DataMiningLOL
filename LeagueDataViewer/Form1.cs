using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using LiveCharts;
using LiveCharts.WinForms;
using System.IO;
using System.Threading;

namespace LeagueDataViewer
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
            var skinManager = MaterialSkin.MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            skinManager.ColorScheme = new MaterialSkin.ColorScheme
                (Primary.Red800, Primary.Red900, Primary.Red900, Accent.Red700, TextShade.BLACK);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logoPic.ImageLocation = "./images/riot.png"; //path to image
            logoPic.BackColor = Color.Transparent;
            ShowHeroes();
        }
        private void ShowHeroes()
        {
            string[] filePaths = GetImages();
            int j = 0;
            int k = 0;
            for (int i = 0; i < filePaths.Length; i++)
            {

                Panel panel = new Panel
                {
                    Size= new Size(120, 120),
                    Name = $"panel{filePaths[i].Substring(19).Split('.')[0]}",
                    Location = new Point(130*j, k * 130)
                };

                j++;
                if (j == 9)
                {
                    j = 0;
                    k++;
                }
                PictureBox pic = new PictureBox
                {
                    ImageLocation = filePaths[i],
                    Name= filePaths[i].Substring(19).Split('.')[0],
                    Size = new Size(100, 100),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                Label label = new Label
                {
                    Size = new System.Drawing.Size(100, 20),
                    BorderStyle = BorderStyle.FixedSingle,
                    Text = filePaths[i].Substring(19).Split('.')[0],
                    Location = new Point(0, 100),
                    Dock = DockStyle.None,
                    AutoSize = false,
                    ForeColor = System.Drawing.Color.White,
                    Font = new Font("Roboto", 12, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pic.Click += new EventHandler(Onb2Click);
                panel.Controls.Add(pic);
                panel.Controls.Add(label);
                panelHeroes.Controls.Add(panel);
            }
            string ceva = filePaths[0].Substring(19).Split('.')[0];
        }
        private string[] GetImages()
        {
            string path = "./images/champions";
            try
            {
                string[] filePaths = Directory.GetFiles(path, "*.png");

                return filePaths;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return null;
        }
        void Onb2Click(object sender, EventArgs e )
        {
            PictureBox clicked = (PictureBox)sender;
            string name = clicked.Name;
            this.Hide();
            Champion f2 = new Champion(name);
            f2.ShowDialog();
        }
       
    }

    public partial class MyPanel : Panel
    {
       
        public MyPanel()
            : base()
        {
            
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Refresh();
            Thread.Sleep(1);
            base.OnMouseWheel(e);
        }
       
    }
}
