
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; 

namespace projeto_integrador
{
    public partial class cadastrodePecas : Form
    {
        public cadastrodePecas()
        {
            InitializeComponent();
        }

        private MySqlBaseConnectionStringBuilder conexaobanco()
        {
            MySqlBaseConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();  //strig de conexão
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "estoquep";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";
            conexaoBD.SslMode = 0;

            return conexaoBD;

        }




        private void button1_Click(object sender, EventArgs e) //botão voltar segunda tela
        {
            Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e) //evento click chama a função limparcampos. 
        {
            limparcampos();

        }

        private void limparcampos() //função limpar campos
        {
            txtpartnunber.Clear();
            txtnome.Clear();
            txtdescricao.Clear();
            mtbdata.Clear();


        }
        private void cadastrodePecas_Load(object sender, EventArgs e) //evento load que chama a função atualizarDataGrid e atualiza os itens do DataGrid
        {
            atualizarDataGrid();

        }

        private void atualizarDataGrid() // função que atualiza o DataGrid
        {
            MySqlBaseConnectionStringBuilder conexaoBD = conexaobanco(); // variavel recebe a função com a string de conexão
            MySqlConnection realizaconexaoBD = new MySqlConnection(conexaoBD.ToString()); // faz a conexão
            try
            {
                realizaconexaoBD.Open(); // abre a conexão

                MySqlCommand comandoMysql = realizaconexaoBD.CreateCommand();
                comandoMysql.CommandText = "SELECT * FROM peca"; // seleciona a tabela peça
                MySqlDataReader reader = comandoMysql.ExecuteReader();

                dgpeca.Rows.Clear();

                while (reader.Read()) // faz a leitura das linhas da tabela
                {
                    DataGridViewRow Row = (DataGridViewRow)dgpeca.Rows[0].Clone(); // insere no DataGrid
                    Row.Cells[0].Value = reader.GetString(0);
                    Row.Cells[1].Value = reader.GetString(1);
                    Row.Cells[2].Value = reader.GetString(2);
                    Row.Cells[3].Value = reader.GetString(3);
                    Row.Cells[4].Value = reader.GetString(4);
                    dgpeca.Rows.Add(Row);
                }

                realizaconexaoBD.Close(); // encerra a conexão
                


            }
            catch (Exception ex) //excessão caso não consiga conexão com o banco
            {
                MessageBox.Show("Cant not open conection !");
                Console.WriteLine(ex.Message);

            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e) //evento do botão adicionar
        {
            MySqlBaseConnectionStringBuilder conexaoBD = conexaobanco();
            MySqlConnection realizaconexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaconexaoBD.Open();

                MySqlCommand comandoMysql = realizaconexaoBD.CreateCommand();        // insere os valores nos campos no banco
                comandoMysql.CommandText = "INSERT INTO peca VALUES(0, '"+txtnome.Text+"', '"+txtdescricao.Text+"','"+mtbdata.Text+ "',' + mcbpeca.Text + ')";
                MySqlDataReader reader = comandoMysql.ExecuteReader();

                dgpeca.Rows.Clear();
                realizaconexaoBD.Close();
                MessageBox.Show("iserido com sucesso!");
                atualizarDataGrid();

                while (reader.Read())
                {
                    DataGridViewRow Row = (DataGridViewRow)dgpeca.Rows[0].Clone();
                    Row.Cells[0].Value = reader.GetString(0);
                    Row.Cells[1].Value = reader.GetString(1);
                    Row.Cells[2].Value = reader.GetString(2);
                    Row.Cells[3].Value = reader.GetString(3);
                    Row.Cells[4].Value = reader.GetString(4);
                    dgpeca.Rows.Add(Row);
                }


               
            }

            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);

            }

        }

        
        private void dgpeca_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgpeca.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {

                dgpeca.CurrentRow.Selected = true; //preenche os textbox com os valores das celulas selecionadas no DataGrid
                txtpartnunber.Text = dgpeca.Rows[e.RowIndex].Cells["Col1"].FormattedValue.ToString();
                txtnome.Text = dgpeca.Rows[e.RowIndex].Cells["Col2"].FormattedValue.ToString();
                txtdescricao.Text = dgpeca.Rows[e.RowIndex].Cells["Col3"].FormattedValue.ToString();
                mtbdata.Text = dgpeca.Rows[e.RowIndex].Cells["Col4"].FormattedValue.ToString();
                cbpeca.Text = dgpeca.Rows[e.RowIndex].Cells["Col5"].FormattedValue.ToString();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e) // evento do botão editar
        {
            MySqlBaseConnectionStringBuilder conexaoBD = conexaobanco();
            MySqlConnection realizaconexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaconexaoBD.Open();

                MySqlCommand comandoMysql = realizaconexaoBD.CreateCommand();
                comandoMysql.CommandText = "UPDATE peca SET nomePeca ='" + txtnome.Text + "'," +
                    "descricaoPeca =  '" + txtdescricao.Text + "'," +
                    "dataEntrada = '" + mtbdata.Text + "', " +
                    "WHERE parteNunber = " + txtpartnunber.Text +"";
                comandoMysql.ExecuteNonQuery();

               
                realizaconexaoBD.Close();
                MessageBox.Show("atualizado com sucesso!");
                atualizarDataGrid();
                limparcampos();
                
            }

            catch (Exception ex) //excessão casso não consiga realizar a conexão com o banco
            {
                
                Console.WriteLine(ex.Message);

            }

        }

        private void btnApagar_Click(object sender, EventArgs e) // evento do botão apagar
        {
            MySqlBaseConnectionStringBuilder conexaoBD = conexaobanco();
            MySqlConnection realizaconexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaconexaoBD.Open();

                MySqlCommand comandoMysql = realizaconexaoBD.CreateCommand();
                comandoMysql.CommandText = "UPDATE peca SET ativoPeca = 0  where parteNunber = " + txtpartnunber.Text + ""; //atualiza a tabela peça e o ativopeca com o
                    
                comandoMysql.ExecuteNonQuery();


                realizaconexaoBD.Close();
                MessageBox.Show("deletado com sucesso!");
                atualizarDataGrid();
                limparcampos();

            }

            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);

            }
        }
    }
     
}



