using App.Class;
using App.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace App
{
    public partial class Form1 : Form
    {
        private string initialPath = string.Empty;
        private string fileNamePath = string.Empty;
        private Image imgOriginal;
        private Boolean canuse = false;

        private List<Nodo> nodos;
        Nodo selOrig;
        Nodo selNodo;
        Angulo angulo = new Angulo();

        public Form1()
        {
            InitializeComponent();
        }

        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFiles();
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            nodos = new List<Nodo>();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            openFiles();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG Image(*.png)|*.png";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Title = "Guardar imagen";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    using (var bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
                    {
                        pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                        bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                catch (ArgumentNullException ex)
                {
                    string err = string.Format("Error saving dicom dataset!\r\n\r\n{0}", ex.ToString());

                    MessageBox.Show(err, "Error");
                    return;
                }
                catch (ExternalException ex)
                {
                    String error = String.Format("Error al intentar guardar\r\n\r\n{0}", ex.ToString());
                    MessageBox.Show(error, "Error");
                }
            }
        }

        private void preferenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //App_config confg = new App_config(this);
            //confg.ShowDialog();
        }

        public void Dark()
        {
            toolStrip1.BackColor = Color.FromArgb(45, 45, 48);
            this.BackColor = Color.FromArgb(51, 51, 51);
            menuStrip.BackColor = Color.FromArgb(45, 45, 48);
            archiveStrip.ForeColor = Color.White;
            archiveStrip.BackColor = Color.FromArgb(45, 45, 48);
            editionnStrip.ForeColor = Color.White;
            helpStrip.ForeColor = Color.White;
        }

        private void modoOscuroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (modoOscuroToolStripMenuItem.Checked)
            {
                modoOscuroToolStripMenuItem.Checked = false;
            }
            else
            {
                modoOscuroToolStripMenuItem.Checked = true;
                Dark();
            }
        }

        private void angle_Tool_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                canuse = canuse == false ? true : false;
                nodos.Clear();
                updateFrame();
                label2.Text = "";
            }

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Nodo n = null;
            foreach (Nodo item in nodos)
            {
                if (item.Adentro(e.Location))
                {
                    n = item;
                    break;
                }
            }

            // conecta dos nodos
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                if (selOrig == null)
                {
                    selOrig = n;
                }
                else
                {
                    if (n != null) selOrig.ConectarA(n);
                    selOrig = null;
                    Refresh();
                }
            }

            // inicia movimiento del nodo
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                selNodo = n;
            }


            /*if (nodos.Count == 3)
            {
                Angulo test = new Angulo();
                //double res = test.angulocal(nodos[1].centro, a_points[0], a_points[2]);
                double resultado = test.calAngu(nodos[1].centro, nodos[0].centro, nodos[2].centro);
                label2.Text = resultado.ToString();
            }*/
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Nodo item in nodos)
            {
                item.DibujaArista(e.Graphics);
            }
            foreach (Nodo item in nodos)
            {
                item.DibujaNodo(e.Graphics);
            }

            /*Rectangle rect = new Rectangle(10, 10, 320, 320);
            e.Graphics.DrawArc(new Pen(new SolidBrush(Color.Black), 1), rect, 90, 180);
            e.Graphics.DrawArc(new Pen(new SolidBrush(Color.Blue), 1), rect, 270, 180);*/

            if (nodos.Count == 3)
            {
                double radians = angulo.VectorAngle(nodos);
                double degrees = radians / Math.PI * 180;
                if(degrees < 0)
                    degrees += 180;
                label1.Text = degrees.ToString("0.00") + "°";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        private List<String> fillData(Angulo test)
        {
            var list = new List<String>();
            list.Add(test.calculateAngle().ToString());
            return list;
        }

        //Actualiza el Frame cada vez que se ingresan los puntos
        private void updateFrame()
        {
            this.Invalidate();
            this.Update();
            this.Refresh();
        }

        /// <summary>
        /// Abre una ventana para seleccionar el archivo
        /// </summary>
        private void openFiles()
        {
            try
            {
                openFileImage.Filter = "Archivos de imagen (*.PNG;*.BMP;*.JPG;*.GIF)|*.PNG;*.BMP;*.JPG;*.GIF|Todos los archivos (*.*)|*.*";
                openFileImage.Title = "Abrir archivo";
                openFileImage.FileName = "";
                if (openFileImage.ShowDialog() == DialogResult.OK)
                {
                    this.initialPath = Path.GetDirectoryName(openFileImage.FileName);
                    this.fileNamePath = openFileImage.FileName;
                    this.imgOriginal = Image.FromFile(openFileImage.FileName);
                    pictureBox1.Image = imgOriginal;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Elija un archivo compatible", "Error de compatibilidad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Visible = true;
            form.Show();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (canuse)
            {
                if (nodos.Count < 3)
                {
                    nodos.Add(new Nodo(e.X, e.Y));
                    Refresh();
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selNodo == null) return;

            // mueve nodo
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                selNodo.Posicion(e.Location);
                Refresh();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //elimina el nodos seleccionado
            if (e.KeyCode == Keys.Delete)
            {
                if (selNodo != null)
                {
                    nodos.Remove(selNodo);
                    foreach (Nodo item in nodos)
                    {
                        item.Desconectar(selNodo);
                    }
                    Refresh();
                    selNodo = null;
                }
            }
        }
    }
}
