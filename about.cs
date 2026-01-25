using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIAutomation
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();
            
        }

        private void about_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void CloseOnClick(object sender, EventArgs e)
        {
            this.Close(); // Close the form when clicked
        }
    }
}
