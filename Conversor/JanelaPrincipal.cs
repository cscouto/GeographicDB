using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Catfood.Shapefile;
using DotSpatial.Topology;
using DotSpatial.Data;
using EGIS.ShapeFileLib;


namespace Conversor
{
    public partial class JanelaPrincipal : Form
    {
        public JanelaPrincipal()
        {
            InitializeComponent();
        }

        private void importarDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new Form1().ShowDialog();
            }catch(Exception ex)
            {

            }
            
        }

        private void exportarDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ExportarPage().ShowDialog();
        }
    }
}
