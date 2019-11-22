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
    public partial class frmTipoDespesa : Form
    {
        private BindingSource bnTipoDesp = new BindingSource();
        private bool bInclusao = false;
        private DataSet dsTipoDesp = new DataSet();
        public frmTipoDespesa()
        {
            InitializeComponent();
        }
        private void FrmTipoDespesa_Load(object sender, EventArgs e)
        {
            try {
                TipoDespesa tipoDespesa = new TipoDespesa();
                dsTipoDesp.Tables.Add(tipoDespesa.Listar());
                bnTipoDesp.DataSource = dsTipoDesp.Tables["TIPODESPESA"];
                dgvTipoDespesa.DataSource = bnTipoDesp;
                bnvTipoDespesa.BindingSource = bnTipoDesp;
                txtIdDespesa.DataBindings.Add("TEXT", bnTipoDesp, "id_tipodespesa");
                txtTipoDespesa.DataBindings.Add("TEXT", bnTipoDesp, "nome_tipodespesa");
                //lock controls
                txtIdDespesa.ReadOnly = true;
                txtTipoDespesa.ReadOnly = true;
                btnSalvar.Enabled = false;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnNovoRegistro_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                tabControl1.SelectTab(1);
            }
            bnTipoDesp.AddNew();
            txtTipoDespesa.ReadOnly = false;
            txtTipoDespesa.Focus();
            btnSalvar.Enabled = true;
            btnAlterar.Enabled = false;
            btnNovoRegistro.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;
            bInclusao = true; ;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            // validar os dados
            if (txtTipoDespesa.Text == "")
            {
                MessageBox.Show("Nome do Tipo de Despesa inválido!");
            }
            else
            {
                TipoDespesa tipoDespesa = new TipoDespesa();
                tipoDespesa.IdTipoDespesa = Convert.ToInt16(txtIdDespesa.Text);
                tipoDespesa.NomeTipoDespesa = txtTipoDespesa.Text;
                if (bInclusao)
                {
                    if (tipoDespesa.Salvar() > 0)
                    {
                        MessageBox.Show("Tipo de Despesa adicionado com sucesso!");
                        btnSalvar.Enabled = false;
                        txtIdDespesa.ReadOnly = true;
                        txtTipoDespesa.ReadOnly = true;
                        btnSalvar.Enabled = false;
                        btnAlterar.Enabled = true;
                        btnNovoRegistro.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = false;
                        bInclusao = false;
                        // recarrega o grid
                        dsTipoDesp.Tables.Clear();
                        dsTipoDesp.Tables.Add(tipoDespesa.Listar());
                        bnTipoDesp.DataSource = dsTipoDesp.Tables["TIPODESPESA"];
                        tabControl1.SelectTab(0);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar Tipo de Despesa!");
                    }
                }
                else
                {
                    if (tipoDespesa.Alterar() > 0)
                    {
                        MessageBox.Show("Tipo de Despesa alterado com sucesso!");
                        dsTipoDesp.Tables.Clear();
                        dsTipoDesp.Tables.Add(tipoDespesa.Listar());
                        txtIdDespesa.ReadOnly = true;
                        txtTipoDespesa.ReadOnly = true;
                        btnSalvar.Enabled = false;
                        btnAlterar.Enabled = true;
                        btnNovoRegistro.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = false;
                        tabControl1.SelectTab(0);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar Tipo de Despesa!");
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
            txtTipoDespesa.ReadOnly = false;
            txtTipoDespesa.Focus();
            btnSalvar.Enabled = true;
            btnAlterar.Enabled = false;
            btnNovoRegistro.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;
            bInclusao = false;
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                tabControl1.SelectTab(1);
            }
            if (MessageBox.Show("Confirma exclusão?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                TipoDespesa tipoDespesa = new TipoDespesa();
                tipoDespesa.IdTipoDespesa = Convert.ToInt16(txtIdDespesa.Text);
                tipoDespesa.NomeTipoDespesa = txtTipoDespesa.Text;
                if (tipoDespesa.Excluir() > 0)
                {
                    TipoDespesa T = new TipoDespesa();
                    dsTipoDesp.Tables.Clear();
                    dsTipoDesp.Tables.Add(T.Listar());
                    bnTipoDesp.DataSource = dsTipoDesp.Tables["TipoDespesa"];
                    tabControl1.SelectTab(0);
                    MessageBox.Show("Tipo de Despesa excluído com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao excluir Tipo de Despesa!");
                }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            bnTipoDesp.CancelEdit();
            btnSalvar.Enabled = false;
            txtTipoDespesa.ReadOnly = true;
            btnAlterar.Enabled = true;
            btnNovoRegistro.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
