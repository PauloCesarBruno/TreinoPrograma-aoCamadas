using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apresentacao_Usuario
{
    public partial class frmImpressao : Form
    {
        public frmImpressao()
        {
            InitializeComponent();
        }

        private void frmImpressao_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'DBClienteDataSet.tblClientes'. Você pode movê-la ou removê-la conforme necessário.
            this.tblClientesTableAdapter.Fill(this.DBClienteDataSet.tblClientes);

            this.reportViewer1.RefreshReport();
        }
    }
}
