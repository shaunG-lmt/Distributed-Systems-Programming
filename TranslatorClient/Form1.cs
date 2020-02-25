﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using TranslationInterface;
namespace TranslatorClient
{
    public partial class Form1 : Form
    {
        ITranslation translatorObject = null;
        public Form1()
        {
            InitializeComponent();
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, false);

            translatorObject = (ITranslation)Activator.GetObject(typeof(ITranslation), "tcp://localhost:5000/Translate");
        }

        private void translateBtn_Click(object sender, EventArgs e)
        {
            textBox2.Text = translatorObject.Translate(textBox1.Text);
        }
    }
}
