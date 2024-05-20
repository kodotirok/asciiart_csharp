using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace actuallyascii
{
    public partial class Form1 : Form
    {
        Img image;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Select file";
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "png|*.png";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string s = ofd.FileName;
                textBox1.Text = s.Substring(s.LastIndexOf("\\") + 1);
                Bitmap bitmap = new Bitmap(ofd.FileName);
                image = new Img(bitmap);

                
            }
        }

        private void btn_ascii_Click(object sender, EventArgs e)
        {
            string text = image.ascii();
            using (StreamWriter sw = new StreamWriter(".\\out.txt"))
            {
                sw.Write(text);
            }
            MessageBox.Show("OK");

        }
    }

    public class Img
    {
        private Bitmap bitmap;

        public Img(Bitmap img)
        {
            bitmap = img;
        }

        public string ascii()
        {
            string s = "";

            double brightness;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    brightness = pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114;

                    if (brightness > 0 && brightness <= 50) s += "&";
                    else if (brightness > 50 && brightness <= 100) s += "\\";
                    else if (brightness > 100 && brightness <= 150) s += "$";
                    else if (brightness > 150 && brightness <= 200) s += "%";
                    else if (brightness > 200 && brightness <= 255) s += "@";
                }

                s += "\n";
            }

            return s;
        }
    }
}
