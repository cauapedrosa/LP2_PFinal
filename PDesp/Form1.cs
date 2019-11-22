using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
namespace PDesp
{
    public partial class frmPrincipal : Form
    {
        public static SqlConnection conexao;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection("Data Source=LENOVO-XWING;Initial Catalog=lp2;Integrated Security=True");
                conexao.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro de banco de dados =/" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Outros Erros =/" + ex.Message);
            }
        }
        private void CadastrosToolStripMenuItem_Click(object sender, EventArgs e)   {   }

        private void SairToolStripMenuItem_Click(object sender, EventArgs e)    {   this.Close();   }

        private void SobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 frmSobre = new AboutBox1();
            frmSobre.Show();
        }

        private void MembroFamiliarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMembro frmMem = new frmMembro();
            frmMem.MdiParent = this;
            frmMem.WindowState = FormWindowState.Maximized;
            frmMem.Show();
        }

        private void TipoDeDespesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTipoDespesa frmTDes = new frmTipoDespesa();
            frmTDes.MdiParent = this;
            frmTDes.WindowState = FormWindowState.Maximized;
            frmTDes.Show();
        }

        private void DespesasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDespesa frmDespesa = new frmDespesa();
            frmDespesa.MdiParent = this;
            frmDespesa.WindowState = FormWindowState.Maximized;
            frmDespesa.Show();
        }
    }
}
