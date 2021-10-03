using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace projeto_integrador
{
    public partial class telaPrincipal : Form
    {
        public telaPrincipal()
        {
            InitializeComponent();
        }

        private void cadastroDePeçasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastrodePecas cadastro = new cadastrodePecas();
            cadastro.ShowDialog();
        }


        private void sairToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void cadastroDePeçasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            cadastrodePecas cadastro = new cadastrodePecas();
            cadastro.ShowDialog();
        }
    }
}
