using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PDesp
{
    class TipoDespesa
    {
        private int idTipoDespesa;
        private string nomeTipoDespesa;

        public int IdTipoDespesa
        {
            get
            {
                return idTipoDespesa;
            }
            set
            {
                idTipoDespesa = value;
            }
        }
        public string NomeTipoDespesa
        {
            get
            {
                return nomeTipoDespesa;
            }
            set
            {
                nomeTipoDespesa = value;
            }
        }
        public DataTable Listar()
        {
            SqlDataAdapter da_tdesp;
            DataTable dt_tdesp = new DataTable();
            try
            {
                da_tdesp = new SqlDataAdapter("SELECT * FROM TIPODESPESA", frmPrincipal.conexao);
                da_tdesp.Fill(dt_tdesp);
                da_tdesp.FillSchema(dt_tdesp, SchemaType.Source);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt_tdesp;
        }
        public int Salvar()
        {
            int retorno = 0;
            try {
                SqlCommand mycommand;
                int nReg;
                mycommand = new SqlCommand("INSERT INTO TIPODESPESA VALUES (@nome_tipodespesa)", frmPrincipal.conexao);
                mycommand.Parameters.Add(new SqlParameter("@nome_tipodespesa", SqlDbType.VarChar));
                mycommand.Parameters["@nome_tipodespesa"].Value = nomeTipoDespesa;
                nReg = mycommand.ExecuteNonQuery();
                if (nReg > 0)
                {
                    retorno = nReg;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return retorno;
        }
        public int Alterar()
        {
            int retorno = 0;
            try
            {
                SqlCommand mycommand;
                int nReg = 0;
                mycommand = new SqlCommand("UPDATE TIPODESPESA SET nome_tipodespesa = @nome_tipodespesa WHERE id_tipodespesa = @id_tipodespesa", frmPrincipal.conexao);
                mycommand.Parameters.Add(new SqlParameter("@id_tipodespesa", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@nome_tipodespesa", SqlDbType.VarChar));
                mycommand.Parameters["@id_tipodespesa"].Value = idTipoDespesa;
                mycommand.Parameters["@nome_tipodespesa"].Value = nomeTipoDespesa;
                nReg = mycommand.ExecuteNonQuery();
                if (nReg > 0) {
                    retorno = nReg;
                }
            }
            catch (Exception ex) {
                throw ex;
            }

            return retorno;
        }
        public int Excluir()
        {
            int nReg = 0;
            try {
                SqlCommand mycommand;
                mycommand = new SqlCommand("DELETE FROM TIPODESPESA WHERE id_tipodespesa=@id_tipodespesa", frmPrincipal.conexao);
                mycommand.Parameters.Add(new SqlParameter("@id_tipodespesa", SqlDbType.Int));
                mycommand.Parameters["@id_tipodespesa"].Value = Convert.ToInt16(idTipoDespesa);
                nReg = mycommand.ExecuteNonQuery();
            }
            catch (Exception ex) {
                throw ex;
            }
            return nReg;
        }
    }
}
