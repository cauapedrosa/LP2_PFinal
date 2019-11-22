using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PDesp
{
    class Despesa
    {
        private int idDespesa;
        private int tipodespesa_id_tipodespesa;
        private int membro_id_membro;
        private DateTime dataDespesa;
        private double valorDespesa;
        private string obsDespesa;

        public int IdDespesa
        {
            get
            {
                return idDespesa;
            }
            set
            {
                idDespesa = value;
            }
        }
        public int Tipodespesa_id_tipodespesa
        {
            get
            {
                return tipodespesa_id_tipodespesa;
            }
            set
            {
                tipodespesa_id_tipodespesa = value;
            }
        }
        public int Membro_id_membro
        {
            get
            {
                return membro_id_membro;
            }
            set
            {
                membro_id_membro = value;
            }
        }
        public DateTime DataDespesa
        {
            get
            {
                return dataDespesa;
            }
            set
            {
                dataDespesa = value;
            }
        }
        public double ValorDespesa
        {
            get
            {
                return valorDespesa;
            }
            set
            {
                valorDespesa = value;
            }
        }
        public string ObsDespesa
        {
            get
            {
                return obsDespesa;
            }
            set
            {
                obsDespesa = value;
            }
        }
        public DataTable Listar()
        {
            SqlDataAdapter da_despesa;
            DataTable dt_despesa = new DataTable();
            try
            {
                da_despesa = new SqlDataAdapter("SELECT * FROM DESPESA", frmPrincipal.conexao);
                da_despesa.Fill(dt_despesa);
                da_despesa.FillSchema(dt_despesa, SchemaType.Source);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt_despesa;
        }
        public int Salvar()
        {
            int retorno = 0;
            try {
                SqlCommand mycommand;
                int nRowsAffected;
                mycommand = new SqlCommand("INSERT INTO DESPESA VALUES (@tipodespesa_id_tipodespesa,@membro_id_membro,@data_despesa,@valor_despesa,@obs_despesa)", frmPrincipal.conexao);
                mycommand.Parameters.Add(new SqlParameter("@tipodespesa_id_tipodespesa", SqlDbType.Int)); ;
                mycommand.Parameters.Add(new SqlParameter("@membro_id_membro", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@data_despesa", SqlDbType.Date));
                mycommand.Parameters.Add(new SqlParameter("@valor_despesa", SqlDbType.Real));
                mycommand.Parameters.Add(new SqlParameter("@obs_despesa", SqlDbType.VarChar));
                mycommand.Parameters["@tipodespesa_id_tipodespesa"].Value = tipodespesa_id_tipodespesa;
                mycommand.Parameters["@membro_id_membro"].Value = membro_id_membro;
                mycommand.Parameters["@data_despesa"].Value = dataDespesa;
                mycommand.Parameters["@valor_despesa"].Value = valorDespesa;
                mycommand.Parameters["@obs_despesa"].Value = obsDespesa;
                nRowsAffected = mycommand.ExecuteNonQuery();
                if (nRowsAffected > 0)
                {
                    retorno = nRowsAffected;
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
                mycommand = new SqlCommand("UPDATE DESPESA SET tipodespesa_id_tipodespesa = @tipodespesa_id_tipodespesa,membro_id_membro = @membro_id_membro,data_despesa = @data_despesa,valor_despesa = @valor_despesa,obs_despesa = @obs_despesa WHERE id_despesa = @id_despesa", frmPrincipal.conexao);
                mycommand.Parameters.Add(new SqlParameter("@id_despesa", SqlDbType.Int)); ;
                mycommand.Parameters.Add(new SqlParameter("@tipodespesa_id_tipodespesa", SqlDbType.Int)); ;
                mycommand.Parameters.Add(new SqlParameter("@membro_id_membro", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@data_despesa", SqlDbType.Date));
                mycommand.Parameters.Add(new SqlParameter("@valor_despesa", SqlDbType.Real));
                mycommand.Parameters.Add(new SqlParameter("@obs_despesa", SqlDbType.VarChar));
                mycommand.Parameters["@id_despesa"].Value = idDespesa;
                mycommand.Parameters["@tipodespesa_id_tipodespesa"].Value = tipodespesa_id_tipodespesa;
                mycommand.Parameters["@membro_id_membro"].Value = membro_id_membro;
                mycommand.Parameters["@data_despesa"].Value = dataDespesa;
                mycommand.Parameters["@valor_despesa"].Value = valorDespesa;
                mycommand.Parameters["@obs_despesa"].Value = obsDespesa;
                nReg = mycommand.ExecuteNonQuery();
                if (nReg > 0)
                {
                    retorno = nReg;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        public int Excluir()
        {
            int nReg = 0;
            try {
                SqlCommand mycommand;
                mycommand = new SqlCommand("DELETE FROM DESPESA WHERE id_despesa=@id_despesa", frmPrincipal.conexao);
                mycommand.Parameters.Add(new SqlParameter("@id_despesa", SqlDbType.Int)); ;
                mycommand.Parameters["@id_despesa"].Value = idDespesa;
                nReg = mycommand.ExecuteNonQuery();
            }
            catch (Exception ex) {
                throw ex;
            }
            return nReg;
        }

    }
}
