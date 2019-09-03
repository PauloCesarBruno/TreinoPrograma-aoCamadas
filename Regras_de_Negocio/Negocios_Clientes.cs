using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using DTO;
using Conexao_SqlServer;

namespace Regras_de_Negocio
{
    public class Negocios_Clientes
    {
        Conect conect = new Conect();

        public String Inserir_Clientes(Clientes clientes)
        {
            try
            {
                conect.LimparParametros();
                // Não é Necessario o IdCliente, pois a chave é Auto Incremento.
                conect.AddParametros("@Nome", clientes.Nome);
                conect.AddParametros("@CPF", clientes.CPF);
                conect.AddParametros("@DataNascimento", clientes.DataNascimento);
                conect.AddParametros("@LimiteDeCredito", clientes.LimiteDeCredito);
                String IdClientes = conect.ExecutaManipulacao(CommandType.StoredProcedure, "uspClienteInserir").ToString();
                conect.FecharConexao();
                return IdClientes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public String Alterar_Clientes(Clientes clientes)
        {
            try
            {
                conect.LimparParametros();
                conect.AddParametros("@IdCliente", clientes.IdCliente);
                conect.AddParametros("@Nome", clientes.Nome);
                conect.AddParametros("@CPF", clientes.CPF);
                conect.AddParametros("@DataNascimento", clientes.DataNascimento);
                conect.AddParametros("@LimiteDeCredito", clientes.LimiteDeCredito);
                String IdCliente = conect.ExecutaManipulacao(CommandType.StoredProcedure, "uspClienteAlterar").ToString();
                conect.FecharConexao();
                return IdCliente;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public String Excluir_Clientes(Clientes clientes)
        {
            try
            {
                conect.LimparParametros();
                conect.AddParametros("@IdCliente", clientes.IdCliente);
                String IdCliente = conect.ExecutaManipulacao(CommandType.StoredProcedure, "uspClienteExcluir").ToString();
                conect.FecharConexao();
                return IdCliente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Clientes_Colecao BuscaPorCodigo(int IdCliente)
        {
            try
            {               
                Clientes_Colecao Colecao = new Clientes_Colecao();
                conect.LimparParametros();
                conect.AddParametros("@IdCliente", IdCliente);
                DataTable dt = conect.ExecutaConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorId");

                foreach (DataRow linhas in dt.Rows)
                {
                    Clientes clientes = new Clientes();
                    clientes.IdCliente = Convert.ToInt32(linhas["IdCliente"]);
                    clientes.Nome = Convert.ToString(linhas["Nome"]);
                    clientes.CPF = Convert.ToString(linhas["CPF"]);
                    clientes.DataNascimento = Convert.ToString(linhas["DataNascimento"]);
                    clientes.LimiteDeCredito = Convert.ToDecimal(linhas["LimiteDeCredito"]);
                    Colecao.Add(clientes);
                    conect.FecharConexao();
                }
                return Colecao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Clientes_Colecao BuscaPorNome(String Nome)
        {
            try
            {
                Clientes_Colecao Colecao = new Clientes_Colecao();
                conect.LimparParametros();
                conect.AddParametros("@Nome", Nome);
                DataTable dt = conect.ExecutaConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorNome");

                foreach (DataRow linhas in dt. Rows)
                {
                    Clientes clientes = new Clientes();
                    clientes.IdCliente = Convert.ToInt32(linhas["IdCliente"]);
                    clientes.Nome = Convert.ToString(linhas["Nome"]);
                    clientes.CPF = Convert.ToString(linhas["CPF"]);
                    clientes.DataNascimento = Convert.ToString(linhas["DataNascimento"]);
                    clientes.LimiteDeCredito = Convert.ToDecimal(linhas["LimiteDeCredito"]);
                    Colecao.Add(clientes);
                    conect.FecharConexao();
                }
                return Colecao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

// Não é Necessario o IdCliente, pois a chave é Auto Incremento.

// ================================================//
// uspClienteInserir
// uspClienteAlterar
// uspClienteExcluir
// uspClienteConsultarPorId
// uspClienteConsultarPorNome
// ================================================//

// Inserir_Clientes
// Alterar_Clientes
// Excluir_Clientes
// BuscaPorCodigo
// BuscaPorNome


