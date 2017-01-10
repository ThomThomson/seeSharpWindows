using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureViewer {
    public partial class picViewer : Form {
        public picViewer() {
            InitializeComponent();
        }

        private void tableLayout_Paint(object sender, PaintEventArgs e) {

        }

        private void btnShowPic_Click(object sender, EventArgs e) {
            if(this.openFileDialog1.ShowDialog() == DialogResult.OK) {
                this.pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void btnClear_Click(object sender, EventArgs e) {
            this.pictureBox1.Image = null;
        }

        private void btnSetBGColor_Click(object sender, EventArgs e) {
            if(this.colorDialog1.ShowDialog() == DialogResult.OK) {
                this.pictureBox1.BackColor = colorDialog1.Color;
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void stretchBox_CheckedChanged(object sender, EventArgs e) {
            if (this.stretchBox.Checked) {
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }else {
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            }
        }
    }
}
