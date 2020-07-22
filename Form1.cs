using System;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ZCA1
{
    public partial class Form1 : Form
    {
        private string fileName;
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            int bc = 1;
            if (radioButton2.Checked)
            {
                bc = 2;
            }
            else if (radioButton3.Checked)
            {
                bc = 3;
            }
            Controller controller = new Controller((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, bc);
            pictureBox1.Image = controller.GenerateBitmap();
            pictureBox1.Refresh();
            button1.Enabled = true;
            fileName = numericUpDown1.Value.ToString();
            switch (bc)
            {
                case 1:
                    fileName += "staly";
                    break;
                case 2:
                    fileName += "zawijany";
                    break;
                case 3:
                    fileName += "odbijajacy";
                    break;
            }
            fileName += numericUpDown3.Value.ToString() + "x" + numericUpDown2.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Obraz PNG|*.PNG";
            dialog.FileName = fileName;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(dialog.FileName, ImageFormat.Png);
            }
        }
    }
}
