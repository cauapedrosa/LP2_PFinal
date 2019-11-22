using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDesp
{
    public partial class frmDespesa : Form
    {
        private BindingSource bnDespesa = new BindingSource();
        private bool bInclusao = false;
        private DataSet dsDespesa = new DataSet();
        // Membro
        private BindingSource bnMembro = new BindingSource();
        private DataSet dsMembro = new DataSet();
        // TipoDespesa
        private BindingSource bnTDesp = new BindingSource();
        private DataSet dsTDesp = new DataSet();

        public frmDespesa()
        {
            InitializeComponent();
        }

        private void FrmDespesa_Load(object sender, EventArgs e)
        {
            try
            {
                Despesa Des = new Despesa();
                dsDespesa.Tables.Add(Des.Listar());
                bnDespesa.DataSource = dsDespesa.Tables["DESPESA"];
                dgvDespesa.DataSource = bnDespesa;
                bnvDespesa.BindingSource = bnDespesa;

                txtIdDespesa.DataBindings.Add("TEXT", bnDespesa, "ID_DESPESA");
                dtpDataDespesa.DataBindings.Add("TEXT", bnDespesa, "DATA_DESPESA");
                txtValorDespesa.DataBindings.Add("TEXT", bnDespesa, "VALOR_DESPESA");
                txtObsDespesa.DataBindings.Add("TEXT", bnDespesa, "OBS_DESPESA");

                // Adicionar MEMBRO_ID_MEMBRO à cbxMembro
                Membro Mem = new Membro();
                dsMembro.Tables.Add(Mem.Listar());
                bnMembro.DataSource = dsMembro.Tables["MEMBRO"];
                cbxMembro.DataSource = dsMembro.Tables["MEMBRO"];
                cbxMembro.DisplayMember = "NOME_MEMBRO";
                cbxMembro.ValueMember = "ID_MEMBRO";
                cbxMembro.DataBindings.Add("SelectedValue", bnDespesa, "MEMBRO_ID_MEMBRO");

                // Adicionar TIPODESPESA_ID_TIPODESPESAs à cbxTipoDespesa
                TipoDespesa TDesp = new TipoDespesa();
                dsTDesp.Tables.Add(TDesp.Listar());
                bnTDesp.DataSource = dsTDesp.Tables["TIPODESPESA"];
                cbxTipoDespesa.DataSource = dsTDesp.Tables["TIPODESPESA"];
                cbxTipoDespesa.DisplayMember = "NOME_TIPODESPESA";
                cbxTipoDespesa.ValueMember = "ID_TIPODESPESA";
                cbxTipoDespesa.DataBindings.Add("SelectedValue", bnDespesa, "TIPODESPESA_ID_TIPODESPESA");

                lockControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BtnNovoRegistro_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) {
                tabControl1.SelectTab(1);
            }
            bnDespesa.AddNew();

            unlockControls();

            bInclusao = true;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            double tval = 0;
            // inicio validação de dados
            if (cbxMembro.SelectedItem == null) {
                MessageBox.Show("Membro inválido!");
            }
            else if (cbxTipoDespesa.SelectedItem == null)  {
                MessageBox.Show("Tipo de Despesa inválido!");
            }
            if (!double.TryParse(txtValorDespesa.Text, out tval)) {
                MessageBox.Show("Valor da Despesa inválido!");
            }
            else if (dtpDataDespesa.Value > DateTime.Now) {
                MessageBox.Show("Data de Despesa maior que a Data de agora!");
            }
            // fim da validação
            else {
                Despesa despesa = new Despesa();

                despesa.IdDespesa = Convert.ToInt16(txtIdDespesa.Text);
                despesa.Tipodespesa_id_tipodespesa = Convert.ToInt16(cbxTipoDespesa.SelectedValue);
                despesa.Membro_id_membro = Convert.ToInt16(cbxMembro.SelectedValue);
                despesa.DataDespesa = dtpDataDespesa.Value;
                despesa.ValorDespesa = tval;
                despesa.ObsDespesa = txtObsDespesa.Text;

                if (bInclusao)
                {
                    if (despesa.Salvar() > 0)
                    {
                        MessageBox.Show("Despesa adicionada com sucesso!");
                        lockControls();

                        bInclusao = false;

                        // recarrega o grid
                        dsDespesa.Tables.Clear();
                        dsDespesa.Tables.Add(despesa.Listar());
                        bnDespesa.DataSource = dsDespesa.Tables["DESPESA"];
                        tabControl1.SelectTab(0);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar Despesa!");
                    }
                }
                else
                {
                    if (despesa.Alterar() > 0)
                    {
                        MessageBox.Show("Despesa alterado com sucesso!");

                        lockControls();
                        dsDespesa.Tables.Clear();
                        dsDespesa.Tables.Add(despesa.Listar());
                        tabControl1.SelectTab(0);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar Despesa!");
                    }
                }
            }
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 0)
            {
                tabControl1.SelectTab(1);
            }

            unlockControls();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                tabControl1.SelectTab(1);
            }
            if (MessageBox.Show("Confirma exclusão?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Despesa despesa = new Despesa();
                despesa.IdDespesa = Convert.ToInt16(txtIdDespesa.Text);
                despesa.Tipodespesa_id_tipodespesa = Convert.ToInt32(cbxTipoDespesa.SelectedValue);
                despesa.Membro_id_membro = Convert.ToInt32(cbxMembro.SelectedValue);
                despesa.DataDespesa = dtpDataDespesa.Value;
                despesa.ValorDespesa = Convert.ToDouble(txtValorDespesa.Text);
                despesa.ObsDespesa = txtObsDespesa.Text;

                if (despesa.Excluir() > 0)
                {
                    MessageBox.Show("Despesa excluída com sucesso!");
                    Despesa T = new Despesa();
                    dsDespesa.Tables.Clear();
                    dsDespesa.Tables.Add(T.Listar());
                    bnDespesa.DataSource = dsDespesa.Tables["DESPESA"];
                    tabControl1.SelectTab(0);
                }
                else
                {
                    MessageBox.Show("Erro ao excluir Despesa!");
                }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            bnDespesa.CancelEdit();
            tabControl1.SelectTab(0);
            lockControls();
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            dgvDespesa.ClearSelection();
            dgvDespesa.CurrentCell = (dgvDespesa.FirstDisplayedCell);
        }

        private void lockControls()
        {
            txtIdDespesa.Enabled = false;
            cbxTipoDespesa.Enabled = false;
            cbxMembro.Enabled = false;
            dtpDataDespesa.Enabled = false;
            txtValorDespesa.Enabled = false;
            txtObsDespesa.Enabled = false;
            btnSalvar.Enabled = false;
            btnAlterar.Enabled = true;
            btnNovoRegistro.Enabled = true;
            btnExcluir.Enabled = true;
            btnCancelar.Enabled = false;
        }
        private void unlockControls()
        {
            txtIdDespesa.ReadOnly = true;
            txtIdDespesa.Enabled = true;
            cbxTipoDespesa.Enabled = true;
            cbxMembro.Enabled = true;
            dtpDataDespesa.Enabled = true;
            txtValorDespesa.Enabled = true;
            txtObsDespesa.Enabled = true;
            btnSalvar.Enabled = true;
            btnAlterar.Enabled = false;
            btnNovoRegistro.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;
        }
    }
}
