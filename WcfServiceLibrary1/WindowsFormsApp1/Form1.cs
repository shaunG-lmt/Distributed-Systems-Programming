using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void translate_Click(object sender, EventArgs e)
        {
            ServiceReference1.TranslationServiceClient Translator = new ServiceReference1.TranslationServiceClient();

            returnValueTB.Text = Translator.Translate(userInputTB.Text);
        }
    }
}
