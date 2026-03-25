using System;
using System.Collections.Generic;
using System.Text;
using Organon.Armazenagem.Relacional;
using System.Data;

namespace DataAccessDP
{
    public class PessoaDPMMDA
    {

        #region atributos
        private string _nip;
        private string _nomeGuerra;
        private string _postoGraduacao;
        private string _corpo;
        private string _quadro;
        private string _especialidade;
        private string _nome;
        private string _oMAtual;
        private string _numeroIdentidade;
        private int _orgaoIdentidade;


        private string _nomePai;
        private string _nomeMae;
        private DateTime _dataIngresso;
        private DateTime _dataDesligamento;
        private string _sexo;
        private string _CPF;
        private string _PISPASEP;
        private string _naturalidade;
        private int _estadoCivil;
        private string _email;
        private string _logradouro;
        private DateTime _dataNascimento;
        private string _numero;
        private string _complemento;
        private string _bairro;
        private string _municipio;
        private string _UFEndereco;
        private int _cep;
        private string _situacaoAtividade;
        private string _numeroTituloEleitor;
        private string _zonaTituloEleitor;
        private string _secaoTituloEleitor;
        private string _tipoSanguineo;
        private string _fatorRH;
        private IList<HisMilDPMMDA> _historicosDPMMDA;

        public IList<HisMilDPMMDA> HistoricosDPMMDA
        {
            get
            {
                if (_historicosDPMMDA == null)
                {
                    _historicosDPMMDA = HisMilDPMMDA.colecao(this.Nip);
                }
                return _historicosDPMMDA;
            }
        }
        
        public string Especialidade
        {
            get { return _especialidade; }
            set { _especialidade = value; }
        }
        public string Quadro
        {
            get { return _quadro; }
            set { _quadro = value; }
        }
         public string OMAtual
        {
            get { return _oMAtual; }
            set { _oMAtual = value; }
        }
      
        public string Corpo
        {
            get { return _corpo; }
            set { _corpo = value; }
        }

        public DateTime DataIngresso
        {
            get { return _dataIngresso; }
            set { _dataIngresso = value; }
        }

        public DateTime DataDesligamento
        {
            get { return _dataDesligamento; }
            set { _dataDesligamento = value; }
        }

        public string NomePai
        {
            get { return _nomePai; }
            set { _nomePai = value; }
        }
        public string PostoGraduacao
        {
            get { return _postoGraduacao; }
            set { _postoGraduacao = value; }
        }

        public string NomeMae
        {
            get { return _nomeMae; }
            set { _nomeMae = value; }
        }

        public DateTime DataNascimento
        {
            get { return _dataNascimento; }
            set { _dataNascimento = value; }
        }

        public string Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }

        public string CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }
        public string PISPASEP
        {
            get { return _PISPASEP; }
            set { _PISPASEP = value; }
        }
        public string NumeroIdentidade
        {
            get { return _numeroIdentidade; }
            set { _numeroIdentidade = value; }
        }

        public int OrgaoIdentidade
        {
            get { return _orgaoIdentidade; }
            set { _orgaoIdentidade = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Naturalidade
        {
            get { return _naturalidade; }
            set { _naturalidade = value; }
        }

        public int EstadoCivil
        {
            get { return _estadoCivil; }
            set { _estadoCivil = value; }
        }

        public string Logradouro
        {
            get { return _logradouro; }
            set { _logradouro = value; }
        }

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public string Complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }

        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }

        public string Municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }

        public string UFEndereco
        {
            get { return _UFEndereco; }
            set { _UFEndereco = value; }
        }

        public int Cep
        {
            get { return _cep; }
            set { _cep = value; }
        }

        public string SituacaoAtividade
        {
            get { return _situacaoAtividade; }
            set { _situacaoAtividade = value; }
        }


        #endregion
        public string Nip
        {
            get { return _nip; }
            set { _nip = value; }
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public string NomeGuerra
        {
            get { return _nomeGuerra; }
            set { _nomeGuerra = value; }
        }
        public string NumeroTituloEleitor
        {
            get { return _numeroTituloEleitor; }
            set { _numeroTituloEleitor = value; }
        }
        public string ZonaTituloEleitor
        {
            get { return _zonaTituloEleitor; }
            set { _zonaTituloEleitor = value; }
        }
        public string SecaoTituloEleitor
        {
            get { return _secaoTituloEleitor; }
            set { _secaoTituloEleitor = value; }
        }
        public string TipoSanguineo
        {
            get { return _tipoSanguineo; }
            set { _tipoSanguineo = value; }
        }
        public string FatorRH
        {
            get { return _fatorRH; }
            set { _fatorRH = value; }
        }
        /// <summary>
        /// Retorna um dicionário com os pares Proriedade e Valor
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> listaPropriedade()
        {
            IDictionary<string, string> retorno = new Dictionary<string, string>(); 
            retorno.Add("NIP", this.Nip);
            retorno.Add("Nome", this.Nome);
            retorno.Add("Nome de Guerra", this.NomeGuerra);
            retorno.Add("Posto/Grad", this.PostoGraduacao);
            retorno.Add("Corpo", this.Corpo);
            retorno.Add("quadro", this.Quadro);
            retorno.Add("Especialidade", this.Especialidade);
            retorno.Add("OM Atual", this.OMAtual);
            retorno.Add("Data Dslg", this.DataDesligamento.ToShortDateString());
            retorno.Add("Situação", this.SituacaoAtividade);
            retorno.Add("Data Ingresso", this.DataIngresso.ToShortDateString());
            retorno.Add("sexo", this.Sexo);
            retorno.Add("Data nascimento", this.DataNascimento.ToShortDateString());
            retorno.Add("TS", this.TipoSanguineo);
            retorno.Add("Fator RH", this.FatorRH);
            retorno.Add("Estado Civil", this.EstadoCivil.ToString());
            retorno.Add("Naturalidade", this.Naturalidade);
            retorno.Add("CPF", this.CPF);
            retorno.Add("PISPASEP", this.PISPASEP);
            retorno.Add("Nome Mãe", this.NomeMae);
            retorno.Add("Nome Pai", this.NomePai);
            retorno.Add("Número Idt", this.NumeroIdentidade);
            retorno.Add("emissor Identidade", this.OrgaoIdentidade.ToString());
            retorno.Add("E-Mail", this.Email);
            retorno.Add("Logradouro", this.Logradouro);
            retorno.Add("Número", this.Numero);
            retorno.Add("Complemento", this.Complemento);
            retorno.Add("Bairro", this.Bairro);
            retorno.Add("Município", this.Municipio);
            retorno.Add("UF end", this.UFEndereco);
            retorno.Add("CEP", this.Cep.ToString());
            retorno.Add("Número Título", this.NumeroTituloEleitor);
            retorno.Add("Seção eleitoral", this.SecaoTituloEleitor);
            retorno.Add("Zona", this.ZonaTituloEleitor);
            return retorno;
        }

        public static PessoaDPMMDA fabricar(string umNIP)
        {
            PessoaDPMMDA retorno = null;
            BancoRelacionalExterno banco = BancoRelacionalExterno.fabricar(modelo.Oracle);
            DataTable registros = null;
            //#185 Início
            BancoRelacional bancorelacional = new BancoRelacional();
            banco.textoConexao = bancorelacional.conectionStringBDPes;
            //banco.textoConexao = "Data Source= (DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = bd-bdpes.dpmm.mb)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = PROD)));User Id=NAVAL_BDI;Password=SELVA;";
            //#185 Fim
            try
            {
                registros = banco.consultar("SELECT * FROM VIEW_PESSOA_NAVAL WHERE NRONIP=" + umNIP.ToString());
            }
            catch { return retorno; }
            if (registros!=null && registros.Rows.Count>0)
            {
                DataRow linha = registros.Rows[0];
                retorno = new PessoaDPMMDA();
                retorno._nip = Convert.ToString(testarCampos(linha["NRONIP"]));
                retorno._dataIngresso = Convert.ToDateTime(testarCampos(linha["DATINGRESSO"]));
                retorno._dataDesligamento = Convert.ToDateTime(testarCampos(linha["DATDESLIGAMENTO"]));
                retorno._nomeGuerra = Convert.ToString(testarCampos(linha["NOMEGUERRA"]));
                retorno._postoGraduacao = Convert.ToString(testarCampos(linha["CODPOSTOGRADUACAO"]));
                retorno._corpo = Convert.ToString(testarCampos(linha["CODCORPO"]));
                retorno._quadro = Convert.ToString(testarCampos(linha["QUADRO"]));
                retorno._especialidade = Convert.ToString(testarCampos(linha["ESPECIALIDADE"]));
                retorno._nome = Convert.ToString(testarCampos(linha["NOMPESSOA"]));
                retorno._oMAtual = Convert.ToString(testarCampos(linha["OMATUAL"]));
                retorno._nomePai = Convert.ToString(testarCampos(linha["NOMPAI"]));
                retorno._nomeMae = Convert.ToString(testarCampos(linha["NOMMAE"]));
                retorno._dataNascimento = Convert.ToDateTime(testarCampos(linha["DATNASCIMENTO"]));
                retorno._sexo = Convert.ToString(testarCampos(linha["TIPSEXO"]));
                retorno._CPF = Convert.ToString(testarCampos(linha["NROCPF"]));
                retorno._PISPASEP = Convert.ToString(testarCampos(linha["NROPISPASEP"]));
                retorno._numeroIdentidade = Convert.ToString(testarCampos(linha["NROIDENTIDADE"]));
                retorno._orgaoIdentidade = Convert.ToInt32(testarCampos(linha["ORGAOIDENT"]));
                retorno._email = Convert.ToString(testarCampos(linha["TXTEMAIL"]));
                retorno._naturalidade = Convert.ToString(testarCampos(linha["NATURALIDADE"]));
                retorno._estadoCivil = Convert.ToInt32(testarCampos(linha["NROESTADOCIVIL"]));
                retorno._logradouro = Convert.ToString(testarCampos(linha["TXTLOGRADOURO"]));
                retorno._numero = Convert.ToString(testarCampos(linha["TXTNUMERO"]));
                retorno._complemento = Convert.ToString(testarCampos(linha["TXTCOMPLEMENTO"]));
                retorno._bairro = Convert.ToString(testarCampos(linha["TXTBAIRRO"]));
                retorno._municipio = Convert.ToString(testarCampos(linha["TXTMUNICIPIO"]));
                retorno._UFEndereco = Convert.ToString(testarCampos(linha["UFENDERECO"]));
                retorno._cep = Convert.ToInt32(testarCampos(linha["TXTCEP"]));
                retorno._situacaoAtividade = Convert.ToString(testarCampos(linha["DSCCLASSE"]));
                retorno._numeroTituloEleitor = Convert.ToString(testarCampos(linha["NROTITULOELEITOR"]));
                retorno._zonaTituloEleitor = Convert.ToString(testarCampos(linha["NROZONAELEITORAL"]));
                retorno._secaoTituloEleitor = Convert.ToString(testarCampos(linha["NROSECAOELEITORAL"]));
                retorno._tipoSanguineo = Convert.ToString(testarCampos(linha["TIPSANGUINEO"]));
                retorno._fatorRH = Convert.ToString(testarCampos(linha["TIPFATORRH"]));
            }
            else
            {
                banco = BancoRelacionalExterno.fabricar(modelo.Oracle);
                //banco.textoConexao = "User ID=NAVAL_BDI;PASSWORD=selva;Data Source=DPMM";
                //#185 Início
                BancoRelacional bancorelacional2 = new BancoRelacional();
                banco.textoConexao = bancorelacional2.conectionStringBDPes;
                //banco.textoConexao = "Data Source= (DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = bd-bdpes.dpmm.mb)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = PROD)));User Id=NAVAL_BDI;Password=SELVA;";
                //#185 Fim
                try
                {
                    registros = banco.consultar("SELECT * FROM VIEW_PESSOA_MILITAR WHERE NRONIP=" + umNIP.ToString());
                }
                catch { return retorno; }
                if (registros != null && registros.Rows.Count > 0)
                {
                    DataRow linha = registros.Rows[0];
                    retorno = new PessoaDPMMDA();
                    retorno._nip = Convert.ToString(testarCampos(linha["NRONIP"]));
                    retorno._dataIngresso = Convert.ToDateTime(testarCampos(linha["DATINGRESSO"]));
                    retorno._dataDesligamento = Convert.ToDateTime(testarCampos(linha["DATDESLIGAMENTO"]));
                    retorno._nomeGuerra = Convert.ToString(testarCampos(linha["NOMEGUERRA"]));
                    retorno._postoGraduacao = Convert.ToString(testarCampos(linha["CODPOSTOGRADUACAO"]));
                    retorno._corpo = Convert.ToString(testarCampos(linha["CODCORPO"]));
                    retorno._quadro = Convert.ToString(testarCampos(linha["QUADRO"]));
                    retorno._especialidade = Convert.ToString(testarCampos(linha["ESPECIALIDADE"]));
                    retorno._nome = Convert.ToString(testarCampos(linha["NOMPESSOA"]));
                    retorno._oMAtual = Convert.ToString(testarCampos(linha["OMATUAL"]));
                    retorno._dataNascimento = Convert.ToDateTime(testarCampos(linha["DATNASCIMENTO"]));
                    retorno._sexo = Convert.ToString(testarCampos(linha["TIPSEXO"]));
                    retorno._naturalidade = Convert.ToString(testarCampos(linha["NATURALIDADE"]));
                    retorno._estadoCivil = Convert.ToInt32(testarCampos(linha["NROESTADOCIVIL"]));
                    retorno._situacaoAtividade = Convert.ToString(testarCampos(linha["DSCCLASSE"]));
                }
            }
            return retorno;
        }

        private static object testarCampos(object umCampo)
        {
            object retorno = umCampo;
            if (umCampo is DBNull)
            {
                retorno = null;
            }
            return retorno;

        }

        public static bool existeNIP(string umNIP)
        {
            bool retorno = false;
            BancoRelacionalExterno banco = BancoRelacionalExterno.fabricar(modelo.Oracle);
            DataTable registros = null;
            //banco.textoConexao = "User ID=NAVAL_BDI;PASSWORD=selva;Data Source=DPMM";
            //#185 Início
            BancoRelacional bancorelacional = new BancoRelacional();
            banco.textoConexao = bancorelacional.conectionStringBDPes;
            //banco.textoConexao = "Data Source= (DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = bd-bdpes.dpmm.mb)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = PROD)));User Id=NAVAL_BDI;Password=SELVA;";
            //#185 Fim

            try
            {
                registros = banco.consultar("SELECT * FROM VIEW_PESSOA_MILITAR WHERE NRONIP=" + umNIP.ToString());
            }
            catch { return retorno; }
            if (registros != null && registros.Rows.Count > 0)
            {
                retorno = true;
            }
            return retorno;
        }

        public static IList<string> nomesEncontrados(string umNome)
        {
            IList<string> retorno = new List<string>();
            BancoRelacionalExterno banco = BancoRelacionalExterno.fabricar(modelo.Oracle);
            DataTable registros = null;
            //banco.textoConexao = "User ID=NAVAL_BDI;PASSWORD=selva;Data Source=DPMM";
            //#185 Início
            BancoRelacional bancorelacional = new BancoRelacional();
            banco.textoConexao = bancorelacional.conectionStringBDPes;
            //banco.textoConexao = "Data Source= (DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = bd-bdpes.dpmm.mb)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = PROD)));User Id=NAVAL_BDI;Password=SELVA;";
            //#185 Fim

            try
            {
                registros = banco.consultar("SELECT * FROM VIEW_PESSOA_MILITAR WHERE NOMPESSOA LIKE '%" + umNome.Trim() + "%' AND NOMPESSOA IS NOT NULL ORDER BY NOMPESSOA");
            }
            catch { return retorno; }
            if (registros != null && registros.Rows.Count > 0)
            {
                for (int x = 0; x < registros.Rows.Count; x++)
                {
                    string texto = string.Empty;
                    DataRow linha = registros.Rows[x];
                    string nomePessoa = Convert.ToString(testarCampos(linha["NOMPESSOA"]));
                    texto = string.Concat(texto, Convert.ToString(testarCampos(linha["NRONIP"])), " - ");
                    texto = string.Concat(texto, nomePessoa , " - ");
                    texto = string.Concat(texto, Convert.ToString(testarCampos(linha["CODPOSTOGRADUACAO"])), " - ");
                    texto = string.Concat(texto, Convert.ToString(testarCampos(linha["CODCORPO"])), " - ");
                    texto = string.Concat(texto, Convert.ToString(testarCampos(linha["QUADRO"])), " - ");
                    texto = string.Concat(texto, Convert.ToString(testarCampos(linha["ESPECIALIDADE"])), " - ");
                    texto = string.Concat(texto, Convert.ToDateTime(testarCampos(linha["DATNASCIMENTO"])).ToShortDateString(), " - ");
                    texto = string.Concat(texto, Convert.ToDateTime(testarCampos(linha["DATINGRESSO"])).ToShortDateString());
                    
                    if (!string.IsNullOrEmpty(nomePessoa))
                    {
                        retorno.Add(texto);
                    }
                }
            }
            return retorno;
        }

    }
}
