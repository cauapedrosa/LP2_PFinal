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
    public partial class frmMembro : Form
    {
        private BindingSource bnMembro = new BindingSource();
        private bool bInclusao = false;
        private DataSet dsMembro = new DataSet();

        public frmMembro()
        {
            InitializeComponent();
        }

        private void FrmMembro_Load(object sender, EventArgs e)
        { 
            try
            {
                Membro Mem = new Membro();
                dsMembro.Tables.Add(Mem.Listar());
                bnMembro.DataSource = dsMembro.Tables["MEMBRO"];
                dgvMembro.DataSource = bnMembro;
                bnvMembro.BindingSource = bnMembro;

                txtIdMembro.DataBindings.Add("TEXT", bnMembro, "id_membro");
                txtNomeMembro.DataBindings.Add("TEXT", bnMembro, "nome_membro");
                txtPapelMembro.DataBindings.Add("TEXT", bnMembro, "papel_membro");
                //lock controls
                txtIdMembro.ReadOnly = true;
                txtNomeMembro.ReadOnly = true;
                txtPapelMembro.ReadOnly = true;
                btnSalvar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnNovoRegistro_Click_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                tabControl1.SelectTab(1);
            }

            bnMembro.AddNew();
            txtNomeMembro.ReadOnly = false;
            txtPapelMembro.ReadOnly = false;
            txtNomeMembro.Focus();
            btnSalvar.Enabled = true;
            btnAlterar.Enabled = false;
            btnNovoRegistro.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;

            bInclusao = true;
        }
        private void BtnSalvar_Click_1(object sender, EventArgs e)
        {
            // validar os dados
            if (txtNomeMembro.Text == "")
            {
                MessageBox.Show("Nome Membro inválido!");
            } else if(txtPapelMembro.Text == "")
            {
                MessageBox.Show("Papel Membro inválido!");
            }
            else
            {
                Membro RegMem = new Membro();

                RegMem.IdMembro = Convert.ToInt16(txtIdMembro.Text);
                RegMem.NomeMembro = txtNomeMembro.Text;
                RegMem.PapelMembro = txtPapelMembro.Text;

                if (bInclusao)
                {
                    if (RegMem.Salvar() > 0)
                    {
                        MessageBox.Show("Membro adicionado com sucesso!");

                        btnSalvar.Enabled = false;
                        txtIdMembro.ReadOnly = true;
                        txtNomeMembro.ReadOnly = true;
                        txtPapelMembro.ReadOnly = true;
                        btnSalvar.Enabled = false;
                        btnAlterar.Enabled = true;
                        btnNovoRegistro.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = false;

                        bInclusao = false;

                        // recarrega o grid
                        dsMembro.Tables.Clear();
                        dsMembro.Tables.Add(RegMem.Listar());
                        bnMembro.DataSource = dsMembro.Tables["Membro"];
                        tabControl1.SelectTab(0);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar Membro!");
                    }
                }
                else
                {
                    if (RegMem.Alterar() > 0)
                    {
                        MessageBox.Show("Membro alterado com sucesso!");

                        dsMembro.Tables.Clear();
                        dsMembro.Tables.Add(RegMem.Listar());
                        txtIdMembro.ReadOnly = true;
                        txtNomeMembro.ReadOnly = true;
                        txtPapelMembro.ReadOnly = true;
                        btnSalvar.Enabled = false;
                        btnAlterar.Enabled = true;
                        btnNovoRegistro.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = false;
                        tabControl1.SelectTab(0);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar Membro!");
                    }
                }
            }
        }
        private void BtnSair_Click_1(object sender, EventArgs e){this.Close();}
        private void DgvMembro_CellContentClick(object sender, DataGridViewCellEventArgs e){}

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                tabControl1.SelectTab(1);
            }

            txtNomeMembro.ReadOnly = false;
            txtPapelMembro.ReadOnly = false;
            txtNomeMembro.Focus();
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
                Membro RegMembro = new Membro();

                RegMembro.IdMembro = Convert.ToInt16(txtIdMembro.Text);
                RegMembro.NomeMembro = txtNomeMembro.Text;
                RegMembro.PapelMembro = txtPapelMembro.Text;

                if (RegMembro.Excluir() > 0)
                {
                    MessageBox.Show("Membro excluído com sucesso!");
                    Membro R = new Membro();
                    dsMembro.Tables.Clear();
                    dsMembro.Tables.Add(R.Listar());
                    bnMembro.DataSource = dsMembro.Tables["Membro"];
                    tabControl1.SelectTab(0);
                }
                else
                {
                    MessageBox.Show("Erro ao excluir Membro!");
                }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            bnMembro.CancelEdit();

            btnSalvar.Enabled = false;
            txtNomeMembro.ReadOnly = true;
            txtPapelMembro.ReadOnly = true;
            btnAlterar.Enabled = true;
            btnNovoRegistro.Enabled = true;
            btnExcluir.Enabled = true;
        }
    }
}
