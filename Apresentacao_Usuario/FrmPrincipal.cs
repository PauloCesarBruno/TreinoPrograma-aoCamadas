using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DTO;
using Regras_de_Negocio;
using Conexao_SqlServer;

namespace Apresentacao_Usuario
{
    public partial class FrmPrincipal : Form
    {
        Conect conect = new Conect();

        public FrmPrincipal()
        {
            InitializeComponent();
            dgvClientes.AutoGenerateColumns = false;
        }

        private void Limpa_campos()
        {
            CarregarGrid();
            txtCodigo.Text = String.Empty;
            txtNome.Text = String.Empty;
            maskCpf.Text = String.Empty;
            maskData.Text = String.Empty;
            txtLimite.Text = String.Empty;
            txtBuscaNome.Text = String.Empty;
            txtBuscaId.Text = String.Empty;
            txtBuscaNome.Focus();
        }

      private void CarregarGrid()
        {
            try
            {
                Clientes_Colecao Ccolecao = new Clientes_Colecao();
                Negocios_Clientes Nclientes = new Negocios_Clientes();
                Ccolecao = Nclientes.BuscaPorNome(txtBuscaNome.Text);
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = Ccolecao;
                dgvClientes.Update();
                dgvClientes.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO, Detalhe: " + ex.Message);
            }
        }

        private void BuscaId()
        {
            try
            {
                Clientes_Colecao Ccolecao = new Clientes_Colecao();
                Negocios_Clientes Nclientes = new Negocios_Clientes();
                Ccolecao = Nclientes.BuscaPorCodigo(Convert.ToInt32(txtBuscaId.Text));
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = Ccolecao;
                dgvClientes.Update();
                dgvClientes.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO, Detalhe: " + ex.Message);
            }
        }

        private void Inserir()
        {
            try
            {
                Clientes clientes = new Clientes();
                Negocios_Clientes Nclientes = new Negocios_Clientes();
                // Não precisa Id cliente --> Chave primária = Auto Incremento.
                clientes.Nome = txtNome.Text;
                clientes.CPF = maskCpf.Text;
                clientes.DataNascimento = maskData.Text;
                clientes.LimiteDeCredito = Convert.ToDecimal(txtLimite.Text);
                Nclientes.Inserir_Clientes(clientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO, Detalhe: " + ex.Message);
            }
        }

        private void Altera()
        {
            try
            {
                Clientes clientes = new Clientes();
                Negocios_Clientes Nclientes = new Negocios_Clientes();
                clientes.IdCliente = Convert.ToInt32(txtCodigo.Text);
                clientes.Nome = txtNome.Text;
                clientes.CPF = maskCpf.Text;
                clientes.DataNascimento = maskData.Text;
                clientes.LimiteDeCredito = Convert.ToDecimal(txtLimite.Text);
                Nclientes.Alterar_Clientes(clientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO, Detalhe: " + ex.Message);
            }
        }

        private void Excluir()
        {
            try
            {
                Clientes clientes = new Clientes();
                Negocios_Clientes Nclientes = new Negocios_Clientes();
                clientes.IdCliente = Convert.ToInt32(txtCodigo.Text);
                Nclientes.Excluir_Clientes(clientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO, Detalhe: " + ex.Message);
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            CarregarGrid();


            //this.reportViewer1.RefreshReport();
        }

        private void txtBuscaNome_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscaNome.Text == String.Empty)
            {
                btnAltera.Enabled = false;
                btnDeleta.Enabled = false;
                CarregarGrid();
            }
            else
            {
                CarregarGrid();
                btnAltera.Enabled = true;
                btnDeleta.Enabled = true;
            }
            if (dgvClientes.Rows.Count == 0)
            {
                MessageBox.Show("Nenhum Registro encontrado Com o Nome Informado !!!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBuscaNome.Text = String.Empty;
                txtBuscaId.Text = String.Empty;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtBuscaId.Text == String.Empty)
            {
                MessageBox.Show("Nehum Código Informado !!!", "AVISO:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBuscaId.Focus();
                return;
            }
            btnAltera.Enabled = true;
            btnDeleta.Enabled = true;
            BuscaId();
            if (dgvClientes.Rows.Count == 0)
            {
                MessageBox.Show("Nenhum Registro encontrado para o código informado, Clique no botão (Atualizar) para recarregar os dados !!!", "SISTEMA HOSPITALAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBuscaNome.Text = String.Empty;
                txtBuscaId.Text = String.Empty;
                btnAltera.Enabled = false;
                btnDeleta.Enabled = false;
                btnLimpar.Focus();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult perg = MessageBox.Show("Deseja realmente sair da Aplicação Sim ou Não ?", "SAIDA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (perg != DialogResult.Yes)
            {
                MessageBox.Show("OK, Mantendo Aplicação !", "SAIDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBuscaNome.Focus();
            }
            else
            {
                MessageBox.Show("OK, Até a Próxima !", "SAIDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        private void btnInsere_Click(object sender, EventArgs e)
        {
            Inserir();
            Limpa_campos();
            btnInsere.Enabled = false;
        }

        private void dgvClientes_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow linha = this.dgvClientes.Rows[e.RowIndex];
            txtCodigo.Text = linha.Cells[0].Value.ToString();
            txtNome.Text = linha.Cells[1].Value.ToString();
            maskCpf.Text = linha.Cells[2].Value.ToString();
            maskData.Text = linha.Cells[3].Value.ToString();
            txtLimite.Text = String.Format("{0:N}", linha.Cells[4].Value);
        }

        private void btnAltera_Click(object sender, EventArgs e)
        {
            MessageBox.Show("êxito na Alteração do Registro !!!", "ALTERAÇÃO DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Altera();
            Limpa_campos();
            btnAltera.Enabled = false;
            btnDeleta.Enabled = false;
            txtBuscaNome.Focus();
        }

        private void btnDeleta_Click(object sender, EventArgs e)
        {
            DialogResult perg = MessageBox.Show("Deseja realmente apagar este registro Sim ou Não ?", "EXCLUSÃO DE REGISTRO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (perg != DialogResult.Yes)
            {
                MessageBox.Show("OK, Mantendo o registro !!!", "EXCLUSÃO DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpa_campos();
                btnDeleta.Enabled = false;
                btnAltera.Enabled = false;
                txtBuscaNome.Focus();
                return;
            }
            else
            {
                MessageBox.Show("Registro Apagado com êxito !!!", "EXCLUSÃO DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Excluir();
                Limpa_campos();
                btnDeleta.Enabled = false;
                btnAltera.Enabled = false;
                txtBuscaNome.Focus();
            }
        }

        private void frmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpa_campos();
            CarregarGrid();
            btnAltera.Enabled = false;
            btnDeleta.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnInsere.Enabled = true;
            Limpa_campos();
            txtNome.Focus();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmImpressao imp = new frmImpressao();
            imp.Show();
        }
    }
}

//===============================================================//
// CarregarGrid
// BuscaId
// Inserir
// Altera
// Excluir
// Não precisa Id cliente --> Chave primária = Auto Incremento.


//===============================================================//
// Evento CellEnter do Dgv.
//-------------------------.

//DataGridViewRow linha = this.dgvClientes.Rows[e.RowIndex];
//txtCodigo.Text = linha.Cells[0].Value.ToString();
//txtNome.Text = linha.Cells[1].Value.ToString();
//maskCpf.Text = linha.Cells[2].Value.ToString();
//maskData.Text = linha.Cells[3].Value.ToString();
//txtLimite.Text = String.Format("{0:N}", linha.Cells[4].Value);
