
using AcessoDadosSIM;
using DataAccessDP;
using NegocioSIGeP.AntigasTemporarias;
using NegocioSIGeP.DadosPessoais;
using NegocioSIGeP.Digitalizacao;
using NegocioSIGeP.Distribuicao;
using NegocioSIGeP.Enquadramento;
using NegocioSIGeP.EscalaHierarquica;
using NegocioSIGeP.HistoricoMilitarDP;
using NegocioSIGeP.Seguranca;
using Organon;
using Organon.AcessoDados;
using Organon.Apresentacao;
using Organon.Processo;
using Organon.Seguranca;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Interop;

namespace NegocioSIGeP.SituacaoMilitar
{
    /// <summary>
    /// Classe persistente do modelo Organon. Gerada automaticamente (alterar a documentação).
    /// </summary>
    [Persistente]
    [Apresentacao("Inclusão")]
    public partial class Inclusao : Entidade, ITramitavel
    {
        #region Propriedades
        ////Foto do Militar armazenada no SIM.
        //public FotoMilitar foto
        //{
        //    get
        //    {
        //        Imagem retorno = Imagem.novo();
        //        retorno.conteudo = Foto.recuperarFoto(this.nip);
        //        retorno.formato = Formato.certificar("image/jpeg", "jpeg");
        //        return retorno;
        //    }
        //}
        /// <summary>
        /// Número de Identificação do Militar
        /// </summary>
        [Persistente]
        [Chave(1)]
        [Apresentacao("NIP")]
        public string nip
        {
            get { return (string)_persistencia["nip"]; }
            set { _persistencia["nip"] = value; }
        }

        /// <summary>
        /// Nome do Militar.
        /// </summary>
        [Persistente]
        [Apresentacao("Militar")]
        public string nome
        {
            get { return (string)_persistencia["nome"]; }
            set { _persistencia["nome"] = value; }
        }

        /// <summary>
        /// Nome de Guerra Militar.
        /// </summary>
        [Persistente]
        [Apresentacao("Nome de guerra")]
        public string nomeGuerra
        {
            get { return (string)_persistencia["nomeGuerra"]; }
            set { _persistencia["nomeGuerra"] = value; }
        }

        /// <summary>
        /// Militar do SIGeP
        /// </summary>
        [Persistente]
        [Chave(0)]
        [Apresentacao("Militar do SIGeP")]
        [Dominio("militaresPossiveis")]
        public MilitarMB militar
        {
            get { return (MilitarMB)_persistencia["militar"]; }
            set { _persistencia["militar"] = value; }
        }

        /// <summary>
        /// Data em que o Militar foi incluído no SIGeP.
        /// </summary>
        [Persistente]
        [Apresentacao("Data de inclusão")]
        public DateTime dataInclusao
        {
            get { return (DateTime)_persistencia["dataInclusao"]; }
            set { _persistencia["dataInclusao"] = value; }
        }

        /// <summary>
        /// Sexo do Militar
        /// </summary>
        [Persistente]
        [Apresentacao("Sexo")]
        [Dominio("sexosPossiveis")]
        public Sexo sexo
        {
            get { return (Sexo)_persistencia["sexo"]; }
            set { _persistencia["sexo"] = value; }
        }

        /// <summary>
        /// CPF do Militar.
        /// </summary>
        [Persistente]
        [Apresentacao("CPF")]
        public string CPF
        {
            get { return (string)_persistencia["CPF"]; }
            set { _persistencia["CPF"] = value; }
        }

        /// <summary>
        /// Data de nascimento do Militar.
        /// </summary>
        [Persistente]
        [Apresentacao("Data de Nascimento")]
        public DateTime dataNascimento
        {
            get { return (DateTime)_persistencia["dataNascimento"]; }
            set { _persistencia["dataNascimento"] = value; }
        }

        /// <summary>
        /// Tipo sanguineo do Militar
        /// </summary>
        [Persistente]
        [Apresentacao("Tipo sanguíneo")]
        [Dominio("tiposSanguineosPossiveis")]
        public TipoSanguineo tipoSanguineo
        {
            get { return (TipoSanguineo)_persistencia["tipoSanguineo"]; }
            set { _persistencia["tipoSanguineo"] = value; }
        }

        /// <summary>
        /// Fator RH
        /// </summary>
        [Persistente]
        [Apresentacao("Fator RH")]
        [Dominio("fatoresRHPossiveis")]
        public FatorRH fatorRH
        {
            get { return (FatorRH)_persistencia["fatorRH"]; }
            set { _persistencia["fatorRH"] = value; }
        }

        /// <summary>
        /// Naturalidade do Militar.
        /// </summary>
        [Persistente]
        [Apresentacao("Naturalidade")]
        [Dominio("ufsPossiveis")]
        public UF naturalidade
        {
            get { return (UF)_persistencia["naturalidade"]; }
            set { _persistencia["naturalidade"] = value; }
        }

        /// <summary>
        /// Estado Civil do Militar
        /// </summary>
        [Persistente]
        [Apresentacao("Estado Civil")]
        [Dominio("estadosCivisPossiveis")]
        public EstadoCivil estadoCivil
        {
            get { return (EstadoCivil)_persistencia["estadoCivil"]; }
            set { _persistencia["estadoCivil"] = value; }
        }

        /// <summary>
        /// Especialidade do Miliatr.
        /// </summary>
        [Persistente]
        [Apresentacao("Especialidade")]
        [Dominio("especialidadesPossiveis")]
        public Especialidade especialidade
        {
            get { return (Especialidade)_persistencia["especialidade"]; }
            set { _persistencia["especialidade"] = value; }
        }

        /// <summary>
        /// Nome do Pai do Militar.
        /// </summary>
        [Persistente]
        [Apresentacao("Nome do Pai")]
        public string nomePai
        {
            get { return (string)_persistencia["nomePai"]; }
            set { _persistencia["nomePai"] = value; }
        }        /// <summary>

        /// <summary>
        /// Nome da Mãe do Militar.
        /// </summary>
        [Persistente]
        [Apresentacao("Nome da Mãe")]
        public string nomeMae
        {
            get { return (string)_persistencia["nomeMae"]; }
            set { _persistencia["nomeMae"] = value; }
        }        /// <summary>

        /// <summary>
        /// PASEP do Militar.
        /// </summary>
        [Persistente]
        [Apresentacao("PASEP")]
        public string PASEP
        {
            get { return (string)_persistencia["PASEP"]; }
            set { _persistencia["PASEP"] = value; }
        }        /// <summary>

        /// <summary>
        /// Título eleioral do Militar
        /// </summary>
        [Persistente]
        [Apresentacao("Título Eleitoral")]
        [Dominio("titulosEleitoraisPossiveis")]
        public TituloEleitoral tituloEleitoral
        {
            get { return (TituloEleitoral)_persistencia["tituloEleitoral"]; }
            set { _persistencia["tituloEleitoral"] = value; }
        }

        /// <summary>
        /// Endereço do Militar
        /// </summary>
        [Persistente]
        [Apresentacao("Endereço")]
        [Dominio("enderecosPossiveis")]
        public Endereco endereco
        {
            get { return (Endereco)_persistencia["endereco"]; }
            set { _persistencia["endereco"] = value; }
        }

        /// <summary>
        /// Posto ou Graduação
        /// </summary>
        [Persistente]
        [Apresentacao("Posto/Graduãção")]
        [Dominio("postosGraduacoesPossiveis")]
        public PostoGraduacao postoGraduacao
        {
            get { return (PostoGraduacao)_persistencia["postoGraduacao"]; }
            set { _persistencia["postoGraduacao"] = value; }
        }

        /// <summary>
        /// Corpo do Militar
        /// </summary>
        [Persistente]
        [Apresentacao("Corpo")]
        [Dominio("corposPossiveis")]
        public Corpo corpo
        {
            get { return (Corpo)_persistencia["corpo"]; }
            set { _persistencia["corpo"] = value; }
        }

        /// <summary>
        /// Quadro do Militar.
        /// </summary>
        [Persistente]
        [Apresentacao("Quadro")]
        [Dominio("quadrosPossiveis")]
        public Quadro quadro
        {
            get { return (Quadro)_persistencia["quadro"]; }
            set { _persistencia["quadro"] = value; }
        }

        /// <summary>
        /// Data de incorporação do militar
        /// </summary>
        [Persistente]
        [Apresentacao("Data de Incorporação")]
        public DateTime incorporacao
        {
            get { return (DateTime)_persistencia["incorporacao"]; }
            set { _persistencia["incorporacao"] = value; }
        }

        /// <summary>
        /// OM de incorporação
        /// </summary>
        [Persistente]
        [Apresentacao("OM de incorporação")]
        [Dominio("omsPossiveis")]
        public OM oMIncorporacao
        {
            get {
                //Início da alteração - tarefa #493 
                //Quintanilha
                //Tarefa iniciada em 04/03/2020

                OM retorno = (OM)_persistencia["oMIncorporacao"];
                if (((IObjeto)this).eNovo)
                {
                    return retorno;
                }
                else
                {  //corrigido o retorno quando a OM é nula #469 - CC(FN) Soransso -Início
                    if (retorno != null)
                    {
                        return retorno.recuperarSiglaTemporal(retorno, incorporacao);
                    }
                    else return null; //corrigido o retorno quando a OM é nula #469 - CC(FN) Soransso -Fim
                }
                //Fim da alteração - tarefa #493 
            }
            set { _persistencia["oMIncorporacao"] = value; }
        }

        /// <summary>
        /// Turma do Militar.
        /// </summary>
        [Persistente]
        [Apresentacao("Turma")]
        [Dominio("turmasPossiveis")]
        public Turma turma
        {
            get { return (Turma)_persistencia["turma"]; }
            set { _persistencia["turma"] = value; }
        }

        /// <summary>
        /// Situação da Pessoa Principal (SPP).
        /// </summary>
        [Persistente]
        [Apresentacao("Situação da Pessoa Principal")]
        [Dominio("SPPPossiveis")]
        public SituacaoPessoaPrincipal spp
        {
            get { return (SituacaoPessoaPrincipal)_persistencia["spp"]; }
            set { _persistencia["spp"] = value; }
        }

        /// <summary>
        /// Motivos SPP.
        /// </summary>
        [Persistente]
        [Apresentacao("Motivo SPP")]
        [Dominio("motivosSPPPossiveis")]
        public Motivo motivoSPP
        {
            get { return (Motivo)_persistencia["motivoSPP"]; }
            set { _persistencia["motivoSPP"] = value; }
        }

        /// <summary>
        /// Data da promoção.
        /// </summary>
        [Persistente]
        [Apresentacao("Data Promoção")]
        [Dominio("valoresPossiveis")]
        public DateTime dataPromocao
        {
            get { return (DateTime)_persistencia["dataPromocao"]; }
            set { _persistencia["dataPromocao"] = value; }
        }

        /// <summary>
        /// Posto da Promoção.
        /// </summary>
        [Persistente]
        [Apresentacao("Posto promoção")]
        [Dominio("postosPossiveis")]
        public PostoGraduacao postoPromocao
        {
            get { return (PostoGraduacao)_persistencia["postoPromocao"]; }
            set { _persistencia["postoPromocao"] = value; }
        }
        /// <summary>
        /// Número documento de promoção.
        /// </summary>
        [Persistente]
        [Apresentacao("Documento Promoção")]
        public string numeroDocumentoPromocao
        {
            get { return (string)_persistencia["numeroDocumentoPromocao"]; }
            set { _persistencia["numeroDocumentoPromocao"] = value; }
        }

        /// <summary>
        /// Data do documento de promoção
        /// </summary>
        [Persistente]
        [Apresentacao("Data do documento")]
        [Dominio("valoresPossiveis")]
        public DateTime dataDocumentoPromocao
        {
            get { return (DateTime)_persistencia["dataDocumentoPromocao"]; }
            set { _persistencia["dataDocumentoPromocao"] = value; }
        }

        /// <summary>
        /// OM Origem do documento de Promoção.
        /// </summary>
        [Persistente]
        [Apresentacao("OM origem")]
        [Dominio("omsOrigemDocumentosPromocaoPossiveis")]
        public OM origemDocumentoPromocao
        {
            get {
                //Início da alteração - tarefa #493 
                //Quintanilha
                //Tarefa iniciada em 04/03/2020

                OM retorno = (OM)_persistencia["origemDocumentoPromocao"];
                if (((IObjeto)this).eNovo)
                {
                    return retorno;
                }
                else
                {  //corrigido o retorno quando a OM é nula #469 - CC(FN) Soransso -Início
                    if (retorno != null)
                    {
                        return retorno.recuperarSiglaTemporal(retorno, dataDocumentoPromocao);
                    }
                    else return null; //corrigido o retorno quando a OM é nula #469 - CC(FN) Soransso -Fim
                }
                //Fim da alteração - tarefa #493 
            }
            set { _persistencia["origemDocumentoPromocao"] = value; }
        }

        /// <summary>
        /// Forma da Promoção
        /// </summary>
        [Persistente]
        [Apresentacao("Forma da Promoção")]
        [Dominio("formasPromocoesPossiveis")]
        public PromocaoForma formaPromocao
        {
            get { return (PromocaoForma)_persistencia["formaPromocao"]; }
            set { _persistencia["formaPromocao"] = value; }
        }

        /// <summary>
        /// Critério da Promoção.
        /// </summary>
        [Persistente]
        [Apresentacao("Critério")]
        [Dominio("criteriosPromocaoPossiveis")]
        public PromocaoCriterio criterioPromocao
        {
            get { return (PromocaoCriterio)_persistencia["criterioPromocao"]; }
            set { _persistencia["criterioPromocao"] = value; }
        }

        /// <summary>
        /// Tipo de documento de promoção.
        /// </summary>
        [Persistente]
        [Apresentacao("Tipo de documento")]
        [Dominio("tiposDocumentosPossiveis")]
        public AntigoTipoDocumento tipoDocumentoPromocao
        {
            get { return (AntigoTipoDocumento)_persistencia["tipoDocumentoPromocao"]; }
            set { _persistencia["tipoDocumentoPromocao"] = value; }
        }

        /// <summary>
        /// ORDMOV
        /// </summary>
        [Persistente]
        [Apresentacao("ORDMOV")]
        public string ordmov
        {
            get { return (string)_persistencia["ordmov"]; }
            set { _persistencia["ordmov"] = value; }
        }

        /// <summary>
        /// Data da Apresentação na OM destino
        /// </summary>
        [Persistente]
        [Apresentacao("Apresentação na OM")]
        public DateTime dataApresentacao
        {
            get { return (DateTime)_persistencia["dataApresentacao"]; }
            set { _persistencia["dataApresentacao"] = value; }
        }

        /// <summary>
        /// OM destino da inclusão
        /// </summary>
        [Persistente]
        [Apresentacao("OM destino")]
        [Dominio("omsDestinosPossiveis")]
        public OM omDestinio
        {
            get {
                //Início da alteração - tarefa #493 
                //Quintanilha
                //Tarefa iniciada em 04/03/2020

                OM retorno = (OM)_persistencia["omDestinio"];
                if (((IObjeto)this).eNovo)
                {
                    return retorno;
                }
                else
                {  //corrigido o retorno quando a OM é nula #469 - CC(FN) Soransso -Início
                    if (retorno != null)
                    {
                        return retorno.recuperarSiglaTemporal(retorno, dataApresentacao);
                    }
                    else return null; //corrigido o retorno quando a OM é nula #469 - CC(FN) Soransso -Fim
                }
                //Fim da alteração - tarefa #493 
            }
            set { _persistencia["omDestinio"] = value; }
        }

        /// <summary>
        /// Situação Funcional.
        /// </summary>
        [Persistente]
        [Apresentacao("Situação funcional")]
        [Dominio("situacaoFuncionaisPossiveis")]
        public SituacaoFuncional situacaoFuncional
        {
            get { return (SituacaoFuncional)_persistencia["situacaoFuncional"]; }
            set { _persistencia["situacaoFuncional"] = value; }
        }

        /// <summary>
        /// Habilitação inicial do Militar. 
        /// </summary>
        [Persistente]
        [Apresentacao("Habilitação Inicial")]
        [Dominio("habilitacoesPossiveis")]
        public TurmaHabilitacao habilitacao
        {
            get { return (TurmaHabilitacao)_persistencia["habilitacao"]; }
            set { _persistencia["habilitacao"] = value; }
        }

        /// <summary>
        /// Colocação na Turma.
        /// </summary>
        [Persistente]
        [Apresentacao("Colocação na Turma")]
        public int colocacao
        {
            get { return (int)_persistencia["colocacao"]; }
            set { _persistencia["colocacao"] = value; }
        }

        /// <summary>
        /// Historico Militar relativo à inclusao.
        /// </summary>
        [Persistente]
        [Apresentacao("Histórico Militar")]
        public HistoricoMilitar historicoMilitar
        {
            get { return (HistoricoMilitar)_persistencia["historicoMilitar"]; }
            set { _persistencia["historicoMilitar"] = value; }
        }

        /// <summary>
        /// Promoção
        /// </summary>
        [Persistente]
        [Apresentacao("Promoção")]
        public Promocao promocao
        {
            get { return (Promocao)_persistencia["promocao"]; }
            set { _persistencia["promocao"] = value; }
        }

        /// <summary>
        /// Comissão inicial.
        /// </summary>
        [Persistente]
        [Apresentacao("Comissão inicial")]
        public Comissao comissao
        {
            get { return (Comissao)_persistencia["comissao"]; }
            set { _persistencia["comissao"] = value; }
        }

        /// <summary>
        /// Situação Funcional inicial.
        /// </summary>
        [Persistente]
        [Apresentacao("Situação Funcional inicial")]
        public SituacaoFuncionalComissao sfc
        {
            get { return (SituacaoFuncionalComissao)_persistencia["sfc"]; }
            set { _persistencia["sfc"] = value; }
        }

        /// <summary>
        /// Turma Militar
        /// </summary>
        [Persistente]
        [Apresentacao("Turma Militar")]
        public TurmaMilitar turmaMilitar
        {
            get { return (TurmaMilitar)_persistencia["turmaMilitar"]; }
            set { _persistencia["turmaMilitar"] = value; }
        }

        
        private PessoaDPMMDA _pessoaDPMMDA;
        [Apresentacao("Histórico DPMMDA")]
        public string HistoricosDPMMDA
        {
            get
            {
                string retorno = string.Empty;
                _pessoaDPMMDA = PessoaDPMMDA.fabricar(this.nip);
                if (_pessoaDPMMDA != null)
                {
                    retorno = string.Concat(retorno, "<TABLE width=100% border=1><TR><TH>Data</TH><TH>SPP</TH><TH>Motivo</TH></TR>");
                    foreach (HisMilDPMMDA hist in _pessoaDPMMDA.HistoricosDPMMDA)
                    {
                        retorno = string.Concat(retorno, "<TR>");
                        retorno = string.Concat(retorno, "<TD>", hist.DataHisMil.ToString("d"), "</TD>");
                        retorno = string.Concat(retorno, "<TD>", hist.SPP, "</TD>");
                        retorno = string.Concat(retorno, "<TD>", hist.Motivo, "</TD>");
                        retorno = string.Concat(retorno, "</TR>");
                    }
                    retorno = string.Concat(retorno, "</TABLE>");

                }
                return retorno;
            }
        }


        #endregion

        #region Propriedades Não Persistentes
        public DateTime dtPromocao { get; set; }
        
        [Dominio("cursosPossiveis")]
        public string curso { get; set; }

        [Dominio ("turmasLotePossiveis")]
        public string turmaLote { get; set; }

        [Dominio("anosPossiveis")]
        public string anoLote { get; set; }

        bool ehLote = false;

        #endregion


        #region Métodos de Objeto

        protected override bool guardar()
        {
            bool retorno = base.guardar();
            return retorno;
        }

        protected override List<string> erros()
        {
            return errosInterno();
        }
        private List<string> errosInterno()
        {
            List<string> retorno = base.erros();
            if (string.IsNullOrEmpty(nip))
            {
                retorno.Add("NIP não informado.");
            }
            else if (nip.Length != 8)
            {
                retorno.Add("NIP inválido. Digite apenas números com 8 dígitos.");
            }
            //Tarefa 509 - Rosario - Início
            if (string.IsNullOrEmpty(CPF))
            {
                retorno.Add("CPF não informado.");
            }
            else if (CPF.Length != 11)
            {
                retorno.Add("CPF inválido. Digite apenas números com 11 dígitos.");
            }
            //Tarefa 509 - Rosario - Fim
            //# 142 Início
            /*if (retorno.Count == 0)
            {
                Inclusao temp = localizar("nip", nip);
                if ((temp != null) && (temp.identificador != identificador))
                {
                    retorno.Add("Nip já utilizado.");
                }
            }*/
            //# 142 Fimif ((militarSIGeP != null) && (SituacaoAtividade.inativa != null))
            if (postoGraduacao == null)
            {
                retorno.Add("Posto/Graduação não informado.");
            }
            if (incorporacao == DateTime.MinValue)
            {
                retorno.Add("Data de incorporação não informada.");
            }

            //if (((incorporacao - dataNascimento).Days / 365) < 18)
            //{
            //    retorno.Add("Data de incorporação não compatível.");
            //}

            //if (((dataInclusao - dataNascimento).Days / 365) < 18)
            //{
            //    retorno.Add("Data de nascimento não compatível.");
            //}

            if (dataNascimento == DateTime.MinValue)
            {
                retorno.Add("Data de nascimento não informada.");
            }
            if (corpo == null)
            {
                retorno.Add("Corpo não informado.");
            }
            if (string.IsNullOrEmpty(nome))
            {
                retorno.Add("Nome não informado.");
            }
            if (string.IsNullOrEmpty(nomeGuerra))
            {
                retorno.Add("Nome de Guerra não informado.");
            }
            if (quadro == null)
            {
                retorno.Add("Quadro não informado.");
            }
            if (turma == null)
            {
                retorno.Add("Turma não informada.");
            }
            if (oMIncorporacao == null)
            {
                retorno.Add("OM de incorporação não informada.");
            }
            if (postoPromocao == null || !postosGraduacoesPossiveis().contem(postoPromocao))
            {
                retorno.Add("Posto da Promoção incompatível.");
            }
            if (dataPromocao == DateTime.MinValue)
            {
                retorno.Add("Data da Promoção não informada.");
            }
            if (dataDocumentoPromocao == DateTime.MinValue)
            {
                retorno.Add("Data do documento da Promoção não informada.");
            }
            if (string.IsNullOrEmpty(numeroDocumentoPromocao))
            {
                retorno.Add("Número do documento de Promoção não informado.");
            }
            if (sexo == null)
            {
                retorno.Add("Sexo não informado.");
            }
            if (formaPromocao == null)
            {
                retorno.Add("Forma da Promoção incompatível.");
            }
            if (criterioPromocao == null)
            {
                retorno.Add("Critério da Promoção incompatível.");
            }
            if (origemDocumentoPromocao == null)
            {
                retorno.Add("OM origem do documento da Promoção incompatível.");
            }
            if (tipoDocumentoPromocao == null)
            {
                retorno.Add("Tipo de documento de Promoção incompatível.");
            }
            if (string.IsNullOrEmpty(ordmov))
            {
                retorno.Add("ORDMOV incompatível.");
            }
            if (omDestinio == null)
            {
                retorno.Add("OM destino incompatível.");
            }
            if (situacaoFuncional == null)
            {
                retorno.Add("Situação Funcional incompatível.");
            }
            if (dataApresentacao == DateTime.MinValue)
            {
                retorno.Add("Data apresentação não informada");
            }
            if (spp == null)
            {
                retorno.Add("SPP não informada.");
            }
            if (motivoSPP == null)
            {
                retorno.Add("Motivo da SPP não informado.");
            }
            if (colocacao == 0)
            {
                retorno.Add("Colocação na Turma não informada.");
            }
            if (habilitacao == null)
            {
                retorno.Add("Habilitação não informada.");
            }
            //else
            //{
            //    retorno.Add("Não é possível alterar os dados da Inclusão, o Militar já foi incluído no SIGeP. Utilize as funcionalidades normais para alteração dos dados do Militar.");
            //}
            return retorno;

        }
        List<string> errosCadastramento()
        {
            List<string> retorno = new List<string>();
            return retorno;
        }
        List<string> errosComplementacao()
        {
            List<string> retorno = new List<string>();
            return retorno;
        }

        public object localizarMilitarDP(ISessao umaSessao)
        {
            IList<string> erros = new List<string>();
            PessoaDPMMDA pessoaDP = null;
            Militar militarSIGeP = null;            
            MilitarExtraCFN militarExtraCFNSIGeP = null;
            if (string.IsNullOrEmpty(this.nip) || this.nip.Length != 8)
            {
                erros.Add("NIP inválido, informe o NIP no formato 99999999 sem pontos");
            }
            //#294 - Ten Rosario - Início
            else
            {
                militarSIGeP = Militar.localizar("nip", this.nip);
                if ((militarSIGeP != null) && (militarSIGeP.situacaoAtividade != null && militarSIGeP.situacaoAtividade.eAtiva))
                {
                    erros.Add("Militar do CFN já incluído no SIGeP.");
                }
                else
                {
                    militarExtraCFNSIGeP = MilitarExtraCFN.localizar("nip", this.nip);
                    if (militarExtraCFNSIGeP != null)
                    {
                        erros.Add("Militar Extra-CFN já incluído no SIGeP.");
                    }
                    else
                    {
                        try
                        {
                            pessoaDP = PessoaDPMMDA.fabricar(this.nip);
                        }
                        catch (Exception e)
                        {
                            erros.Add("Ocorreu um problema na conexão com a DPMM. Tente novamente mais tarde, caso o erro persista entre em contato com o CPesFN.");
                        }
                    }
                }
            }
            if (erros.Count == 0)
            {
                if (pessoaDP != null)
                {
                    //if (militarSIGeP != null)
                    //{
                    //    UsuarioSIGeP usuarioSIGeP = UsuarioSIGeP.localizar("militar.nip", militarSIGeP.nip);
                    //    if (usuarioSIGeP != null)
                    //    {
                    //        ((IObjeto)usuarioSIGeP).excluir(umaSessao);
                    //    }
                    //}
            //#294 - Ten Rosario - Fim
                    this._pessoaDPMMDA = pessoaDP;
                    this.nome = pessoaDP.Nome;
                    this.nomeGuerra = pessoaDP.NomeGuerra;
                    if (!string.IsNullOrEmpty(pessoaDP.Sexo))
                    {
                        this.sexo = Sexo.localizar("sigla", pessoaDP.Sexo.Trim());
                    }
                    this.CPF = pessoaDP.CPF;
                    this.dataNascimento = pessoaDP.DataNascimento;
                    this.incorporacao = pessoaDP.DataIngresso;
                    if (!string.IsNullOrEmpty(pessoaDP.TipoSanguineo))
                    {
                        this.tipoSanguineo = TipoSanguineo.localizar("sigla", pessoaDP.TipoSanguineo.Trim());
                    }
                    if (!string.IsNullOrEmpty(pessoaDP.FatorRH))
                    {
                        this.fatorRH = FatorRH.localizar("sigla", pessoaDP.FatorRH.Trim());
                    }
                    if (!string.IsNullOrEmpty(pessoaDP.Naturalidade))
                    {
                        this.naturalidade = UF.localizar("abreviatura", pessoaDP.Naturalidade.Trim());
                    }
                    this.estadoCivil = this.estadoCivilSIGeP(pessoaDP);
                    if (!string.IsNullOrEmpty(pessoaDP.NomePai))
                    {
                        this.nomePai = pessoaDP.NomePai.Trim();
                    }
                    if (!string.IsNullOrEmpty(pessoaDP.NomeMae))
                    {
                        this.nomeMae = pessoaDP.NomeMae.Trim();
                    }
                    if (!string.IsNullOrEmpty(pessoaDP.PISPASEP))
                    {
                        this.PASEP = pessoaDP.PISPASEP.Trim();
                    }
                    if (this.tituloEleitoral == null)
                    {
                        this.tituloEleitoral = TituloEleitoral.novo();
                    }
                    this.tituloEleitoral = montarTituloEleitoral(this.tituloEleitoral, pessoaDP);
                    //if (this.endereco == null)
                    //{
                    //    this.endereco = Endereco.novo();
                    //}
                    //this.endereco = montarEndereco(this.endereco, pessoaDP);
                    if (!string.IsNullOrEmpty(pessoaDP.PostoGraduacao))
                    {
                        this.postoGraduacao = PostoGraduacao.localizar("codigoDP", pessoaDP.PostoGraduacao.Trim());
                    }
                    if (!string.IsNullOrEmpty(pessoaDP.Corpo))
                    {
                        this.corpo = Corpo.localizar("abreviatura", pessoaDP.Corpo.Trim());
                    }
                    if (!string.IsNullOrEmpty(pessoaDP.Especialidade))
                    {
                        this.especialidade = Especialidade.localizar("abreviatura", pessoaDP.Especialidade.Trim());
                    }
                    if (!string.IsNullOrEmpty(pessoaDP.Quadro))
                    {
                        this.quadro = Quadro.localizar("codigoDP", pessoaDP.Quadro.Trim());
                    }
                    return this;
                }
                else
                {
                    erros.Add("Militar não cadastrado na DPMM.");
                }
            }
            return erros;
        }

        public object militaresLote(ISessao umaSessao)
        {
            guardarTurma();

            return true;
        }

        public void guardarTurma()
        {
            Turma turma = Turma.novo();
            bool podeGuardar = true;

            turma.sigla = $"{curso} Turma {turmaLote}/{anoLote}";
            turma.descricao = turma.sigla;
            turma.nivelHierarquico = NivelHierarquico.praca;

            podeGuardar = podeGuardar && (((IObjeto)turma).erros().Count == 0);

            if (podeGuardar)
            {
                ((IObjeto)turma).guardar();
            }
        }


        private EstadoCivil estadoCivilSIGeP(PessoaDPMMDA umaPessoaDPMM)
        {
            EstadoCivil estadoCivil = null;
            switch (umaPessoaDPMM.EstadoCivil)
            {
                case 1: estadoCivil = EstadoCivil.solteiro; break;
                case 2: estadoCivil = EstadoCivil.casado; break;
                case 3: estadoCivil = EstadoCivil.viuvo; break;
                case 4: estadoCivil = EstadoCivil.separado; break;
                case 5: estadoCivil = EstadoCivil.divorciado; break;
                default: break;
            }
            return estadoCivil;
        }

        
        private bool podeIncluir()
        {
            bool retorno = true;
            return retorno;
        }
        public IColecao militaresPossiveis()
        {
            IColecao retorno = Militar.colecao();
            return retorno;
        }
        public IColecao sexosPossiveis()
        {
            IColecao retorno = Sexo.colecao();
            return retorno;
        }
        public IColecao fatoresRHPossiveis()
        {
            IColecao retorno = FatorRH.colecao();
            return retorno;
        }

        public IColecao tiposSanguineosPossiveis()
        {
            IColecao retorno = TipoSanguineo.colecao();
            return retorno;
        }
        public IColecao ufsPossiveis()
        {
            IColecao retorno = UF.colecao();
            return retorno;
        }
        public IColecao estadosCivisPossiveis()
        {
            IColecao retorno = EstadoCivil.colecao();
            return retorno;
        }

        public IColecao titulosEleitoraisPossiveis()
        {
            IColecao retorno = TituloEleitoral.colecao();
            return retorno;
        }
        public IColecao enderecosPossiveis()
        {
            IColecao retorno = Endereco.colecao();
            return retorno;
        }
        public IColecao turmasPossiveis()
        {
            IColecao retorno = Turma.colecao();
            if (this.postoGraduacao != null)
            {
                retorno.adicionarCondicaoFixa("nivelHierarquico", Operador.igual, this.postoGraduacao.circulo.nivel);
            }
            return retorno;
        }
        public IColecao omsPossiveis()
        {
            IColecao retorno = OM.colecao();
            if (postoGraduacao != null)
            {
                if (postoGraduacao.circulo.nivel == NivelHierarquico.oficial)
                {
                    retorno.adicionarCondicaoFixa("codigo", Operador.contidoEm, new string[] { "62200", "62300", "62400" });
                }
                else
                {
                    retorno.adicionarCondicaoFixa("codigo", Operador.contidoEm, new string[] { "32100", "87900", "32200" });
                }
            }
            return retorno;
        }
        public IColecao habilitacoesPossiveis()
        {
            IColecao retorno = TurmaHabilitacao.colecao();
            return retorno;
        }

        private TituloEleitoral montarTituloEleitoral(TituloEleitoral umTituloEleitoral, PessoaDPMMDA umaPessoaDPMM)
        {
            if (!string.IsNullOrEmpty(umaPessoaDPMM.NumeroTituloEleitor))
            {
                umTituloEleitoral.inscricao = umaPessoaDPMM.NumeroTituloEleitor.Trim();
            }
            else
            {
                umTituloEleitoral.inscricao = "N/C";
            }
            if (!string.IsNullOrEmpty(umaPessoaDPMM.SecaoTituloEleitor))
            {
                umTituloEleitoral.secao = Convert.ToInt32(Regex.Replace(umaPessoaDPMM.SecaoTituloEleitor.Trim(), @"\s+", ""));
            }
            if (!string.IsNullOrEmpty(umaPessoaDPMM.ZonaTituloEleitor))
            {
                umTituloEleitoral.zona = Convert.ToInt32(Regex.Replace(umaPessoaDPMM.ZonaTituloEleitor.Trim(), @"\s+", ""));
            }
            return umTituloEleitoral;
        }
        private Endereco montarEndereco(Endereco umEndereco, PessoaDPMMDA umaPessoaDPMM)
        {
            if (!string.IsNullOrEmpty(umaPessoaDPMM.Bairro))
            {
                umEndereco.bairro = umaPessoaDPMM.Bairro.Trim();
            }
            if (umaPessoaDPMM.Cep > 0)
            {
                umEndereco.cep = umaPessoaDPMM.Cep.ToString();
            }
            if (!string.IsNullOrEmpty(umaPessoaDPMM.Complemento))
            {
                umEndereco.complemento = umaPessoaDPMM.Complemento;
            }
            if (!string.IsNullOrEmpty(umaPessoaDPMM.Logradouro))
            {
                umEndereco.logradouro = umaPessoaDPMM.Logradouro;
            }
            else
            {
                umEndereco.logradouro = "N/C";
            }
            if (!string.IsNullOrEmpty(umaPessoaDPMM.Municipio))
            {
                umEndereco.municipio = umaPessoaDPMM.Municipio.Trim();
            }
            if (!string.IsNullOrEmpty(umaPessoaDPMM.Numero))
            {
                umEndereco.numero = umaPessoaDPMM.Numero.Trim();
            }
            if (!string.IsNullOrEmpty(umaPessoaDPMM.UFEndereco))
            {
                umEndereco.uf = UF.localizar("abreviatura", umaPessoaDPMM.UFEndereco.Trim());
            }
            umEndereco.militar = this.militar;
            return umEndereco;
        }
        public IColecao postosGraduacoesPossiveis()
        {
            IColecao retorno = PostoGraduacao.colecao();
            return retorno;
        }
        public IColecao corposPossiveis()
        {
            IColecao retorno = Corpo.colecao();
            return retorno;
        }
        public IColecao especialidadesPossiveis()
        {
            IColecao retorno = Especialidade.colecao();
            return retorno;
        }
        public IColecao quadrosPossiveis()
        {
            IColecao retorno = Quadro.colecao();
            return retorno;
        }
        public IColecao SPPPossiveis()
        {
            IColecao retorno = SituacaoPessoaPrincipal.colecao();
            return retorno;
        }
        public IColecao motivosSPPPossiveis()
        {
            IColecao retorno = Motivo.colecao();
            if (this.spp != null)
            {
                retorno.adicionarCondicaoFixa("situacoesPessoasPropriaMotivos.situacaoPessoaPrincipal", Operador.contidoEm, this.spp);
            }
            return retorno;
        }
        public IColecao criteriosPromocaoPossiveis()
        {
            IColecao retorno = PromocaoCriterio.colecao();
            return retorno;
        }

        public IColecao formasPromocoesPossiveis()
        {
            IColecao retorno = PromocaoForma.colecao();
            return retorno;
        }

        public IColecao omsOrigemDocumentosPromocaoPossiveis()
        {
            IColecao retorno = OM.colecao();
            return retorno;
        }

        public IColecao postosPossiveis()
        {
            IColecao retorno = PostoGraduacao.colecao();
            if (postoGraduacao != null)
            {
                if (postoGraduacao.circulo.nivel == NivelHierarquico.oficial)
                {
                    retorno.adicionarCondicaoFixa("hierarquia", Operador.igual, postoGraduacao.hierarquia + 1);
                }
                else if (postoGraduacao.circulo.nivel == NivelHierarquico.praca)
                {
                    int[] hierarquias = new int[2];
                    hierarquias[0] = postoGraduacao.hierarquia + 1;
                    hierarquias[1] = postoGraduacao.hierarquia + 2;
                    retorno.adicionarCondicaoFixa("hierarquia", Operador.contidoEm, hierarquias);
                }
            }
            return retorno;
        }
        public IColecao tiposDocumentosPossiveis()
        {
            IColecao retorno = AntigasTemporarias.AntigoTipoDocumento.colecao();
            return retorno;
        }
        public IColecao situacaoFuncionaisPossiveis()
        {
            IColecao retorno = SituacaoFuncional.colecao();
            retorno.adicionarCondicao("extincao", Operador.maiorIgual, DateTime.Today);
            return retorno;
        }

        public IColecao omsDestinosPossiveis()
        {
            IColecao retorno = OM.colecao();
            return retorno;
        }

        public IList cursosPossiveis()
        {
            IList retorno = new List<string>();
            retorno.Add("C-FSD-FN");
            retorno.Add("C-FSG-MU");           
            return retorno;
        }

        public IList turmasLotePossiveis()
        {
            IList retorno = new List<string>();
            retorno.Add("I");
            retorno.Add("II");
            return retorno;
        }

        public IList anosPossiveis()
        {
            IList retorno = new List<string>();
            int quantidade = 10;
            int anoAtual = DateTime.Now.Year;

            foreach (var ano in Enumerable.Range(anoAtual, quantidade))
            {
                retorno.Add(ano.ToString());
            }

            return retorno;
        }
        protected override void atribuirValoresIniciais()
        {
            this.dataInclusao = DateTime.Today;
        }
        public override IObjeto repetir()
        {
            Inclusao retorno = Inclusao.novo();
            retorno.corpo = this.corpo;
            retorno.criterioPromocao = this.criterioPromocao;
            retorno.dataDocumentoPromocao = this.dataDocumentoPromocao;
            retorno.dataPromocao = this.dataPromocao;
            retorno.especialidade = this.especialidade;
            retorno.formaPromocao = this.formaPromocao;
            retorno.motivoSPP = this.motivoSPP;
            retorno.numeroDocumentoPromocao = this.numeroDocumentoPromocao;
            retorno.origemDocumentoPromocao = this.origemDocumentoPromocao;
            retorno.postoGraduacao = this.postoGraduacao;
            retorno.postoPromocao = this.postoPromocao;
            retorno.quadro = this.quadro;
            retorno.spp = this.spp;
            retorno.turma = this.turma;
            retorno.dataApresentacao = this.dataApresentacao;
            retorno.omDestinio = this.omDestinio;
            retorno.tipoDocumentoPromocao = this.tipoDocumentoPromocao;
            retorno.situacaoFuncional = this.situacaoFuncional;
            retorno.colocacao = this.colocacao + 1;
            return retorno;
        }
        bool persistirObjetosDependentes()
        {
            bool retorno = false;
            if (errosInterno().Count == 0)
            {
                //#142 Início  
                Militar militarSIGeP = null;
                militarSIGeP = Militar.localizar("nip", this.nip);
                //#294 - Ten Rosario - Início
                if ((militarSIGeP != null) && (militarSIGeP.situacaoAtividade != null && !militarSIGeP.situacaoAtividade.eAtiva))
                {
                //#294 - Ten Rosario - Início
                    militarSIGeP.nip = nip + "i";
                    militarSIGeP.oMAtual = null;
                    ((IObjeto)militarSIGeP).guardar();
                    this.militar = null;
                }
                //#142 Fim 

                MilitarMB umMilitar = null;
                if (this.militar == null)
                {
                    umMilitar = Militar.novo();
                }
                else
                {
                    umMilitar = this.militar;
                }
                umMilitar.corpo = this.corpo;
                umMilitar.cpf = this.CPF;
                umMilitar.dataIncorporacao = this.incorporacao;
                umMilitar.dataNascimento = this.dataNascimento;
                umMilitar.nascimento = this.dataNascimento;
                umMilitar.naturalidade = this.naturalidade;
                umMilitar.nip = this.nip;
                umMilitar.nome = this.nome;
                umMilitar.nomeGuerra = this.nomeGuerra;
                umMilitar.nomeMae = this.nomeMae;
                umMilitar.nomePai = this.nomePai;
                umMilitar.postoAtual = this.postoGraduacao;
                umMilitar.quadro = this.quadro;
                umMilitar.sexo = this.sexo;
                umMilitar.situacaoAtividade = SituacaoAtividade.ativa;
                umMilitar.tipoSanguineo = this.tipoSanguineo;
                umMilitar.fatorRH = this.fatorRH;
                umMilitar.estadoCivil = this.estadoCivil;
                umMilitar.PASEP = this.PASEP;
                umMilitar.tituloEleitoral = this.tituloEleitoral;
                if (((IObjeto)umMilitar).guardar())
                {
                    HistoricoMilitar umHistoricoMilitar = null;
                    Promocao umaPromocao = null;
                    Comissao umaComissao = null;
                    SituacaoFuncionalComissao umaSFC = null;
                    TurmaMilitar umaTurmaMilitar = null;
                    umMilitar = (Militar)Militar.localizar(umMilitar.identificador);

                    //this.endereco.militar = umMilitar;
                    if (this.historicoMilitar == null)
                    {
                        umHistoricoMilitar = HistoricoMilitar.novo();
                    }
                    else
                    {
                        umHistoricoMilitar = this.historicoMilitar;
                    }
                    umHistoricoMilitar.autor = Autor.localizar("oM", origemDocumentoPromocao);
                    umHistoricoMilitar.inicio = this.dataPromocao;
                    umHistoricoMilitar.militar = umMilitar;
                    umHistoricoMilitar.motivo = this.motivoSPP;
                    umHistoricoMilitar.numeroAto = this.numeroDocumentoPromocao;
                    umHistoricoMilitar.numeroBoletim = "0";
                    umHistoricoMilitar.situacaoPessoaPrincipal = this.spp;
                    umHistoricoMilitar.tipoDocumento = this.tipoDocumentoPromocao;

                    if (this.promocao == null)
                    {
                        umaPromocao = Promocao.novo();
                    }
                    else
                    {
                        umaPromocao = this.promocao;
                    }
                    umaPromocao.data = this.dataPromocao;
                    umaPromocao.dataDocumento = this.dataDocumentoPromocao;
                    umaPromocao.documento = this.numeroDocumentoPromocao;
                    umaPromocao.militar = umMilitar;
                    umaPromocao.origemDocumento = this.origemDocumentoPromocao;
                    umaPromocao.postoGraduacao = this.postoPromocao;
                    umaPromocao.promocaoCriterio = this.criterioPromocao;
                    umaPromocao.promocaoForma = this.formaPromocao;
                    umaPromocao.tipoDocumento = this.tipoDocumentoPromocao;

                    if (this.comissao == null)
                    {
                        umaComissao = Comissao.novo();
                    }
                    else
                    {
                        umaComissao = this.comissao;
                    }
                    umaComissao.militar = umMilitar;
                    umaComissao.dataApresentacao = this.dataApresentacao;
                    umaComissao.dataOrdmov = this.dataDocumentoPromocao;
                    umaComissao.eOMAtual = true;
                    umaComissao.motivo = MotivoMovimentacao.localizar("abreviatura", "INTERESSE DE SERVIÇO");
                    umaComissao.ordmov = this.ordmov;
                    umaComissao.origem = this.omDestinio;
                    umaComissao.tipoMovimentacao = TipoMovimentacao.E1;

                    if (this.sfc == null)
                    {
                        umaSFC = SituacaoFuncionalComissao.novo();
                    }
                    else
                    {
                        umaSFC = this.sfc;
                    }
                    umaSFC.comissao = umaComissao;
                    umaSFC.inicio = this.dataApresentacao;
                    umaSFC.situacaoFuncional = this.situacaoFuncional;

                    if (this.turmaMilitar == null)
                    {
                        umaTurmaMilitar = TurmaMilitar.novo();
                    }
                    else
                    {
                        umaTurmaMilitar = this.turmaMilitar;
                    }
                    umaTurmaMilitar.colocacao = this.colocacao;
                    umaTurmaMilitar.habilitacao = this.habilitacao;
                    umaTurmaMilitar.militar = umMilitar;
                    umaTurmaMilitar.OMIncorporacao = this.oMIncorporacao;
                    umaTurmaMilitar.turma = this.turma;

                    //umMilitar.oMAtual = umaComissao.origem;
                    //this.adicionarObjetoDependente(this.militar);
                    bool podeGuardar = true;

                    podeGuardar = podeGuardar && (((IObjeto)umHistoricoMilitar).erros().Count == 0);
                    podeGuardar = podeGuardar && (((IObjeto)umaPromocao).erros().Count == 0);
                    podeGuardar = podeGuardar && (((IObjeto)umaComissao).erros().Count == 0);
                    podeGuardar = podeGuardar && (((IObjeto)umaSFC).erros().Count == 0);
                    podeGuardar = podeGuardar && (((IObjeto)umaTurmaMilitar).erros().Count == 0);

                    if (podeGuardar)
                    {
                        ((IObjeto)umHistoricoMilitar).guardar();
                        ((IObjeto)umaPromocao).guardar();
                        ((IObjeto)umaComissao).guardar();
                        ((IObjeto)umaSFC).guardar();
                        ((IObjeto)umaTurmaMilitar).guardar();
                        umMilitar.vinculoAtual = umaComissao;
                        ((IObjeto)umMilitar).guardar();

                        this.militar = umMilitar;
                        this.historicoMilitar = umHistoricoMilitar;
                        this.promocao = umaPromocao;
                        this.comissao = umaComissao;
                        this.sfc = umaSFC;
                        this.turmaMilitar = umaTurmaMilitar;
                        retorno = base.guardar();
                    }
                    UsuarioSIGeP usuario = UsuarioSIGeP.novo();
                    ((IUsuario)usuario).celula = this.omDestinio;
                    usuario.pessoa = this.militar;
                    usuario.identificacao = this.militar.nip;
                    ((IObjeto)usuario).guardar();
                    if (!((IObjeto)usuario).guardar())
                    {
                        usuario = null;
                    }

                    if (usuario != null)
                    {
                        Perfil perfil = Perfil.localizar("abreviatura", "militarCFN");
                        if (perfil != null)
                        {
                            IColecao autorizacoes = perfil.autorizacoes;
                            autorizacoes.adicionarCondicao("usuario", usuario);
                            if (autorizacoes.Count < 1)
                            {
                                autorizacoes.elementoPadrao.guardar();
                            }
                        }
                    }
                    SituacaoSMO smoNova = SituacaoSMO.novo();
                    if ((dataInclusao == DateTime.Today && postoGraduacao.sigla == "SD") || (dataInclusao == DateTime.Today && postoGraduacao.sigla == "3SG" && especialidade.abreviatura == "MU"))
                    {

                        smoNova.militar = umMilitar;
                        smoNova.faseAtual = Fase.localizar("abreviatura", "docSMO");
                        smoNova.eHInclusao = true;
                        ((IObjeto)smoNova).guardar();
                    }

                }

            }


            
            return retorno;
        }

      
        public object adicionarMilitar(Acao umaAcao)
        {
            object retorno = true;
            if (umaAcao == Acao.encaminhar)
            {
                List<string> listaErros = new List<string>();
                if (listaErros.Count > 0)
                {
                    retorno = listaErros;
                }
                else
                {
                   retorno = persistirObjetosDependentes();
                }
           
            }
            
            return retorno;
        }
        #endregion

        #region Apresentação
        /// <summary>
        /// Sobrecarga do método da classe System.object.
        /// </summary>
        /// <returns>String que representa o objeto.</returns>
        public override string ToString()
        {
            return string.Concat(nip, " - ", nome);
        }
        /// <summary>
        /// String que identifica o objeto externamente.
        /// </summary>
        public override string identificacaoObjeto
        {
            get
            {
                return ToString();
            }
        }
        /// <summary>
        /// Retorna formatação para apresentação dos objetos da classe em controles do tipo grade.
        /// </summary>
        /// <returns>Formatação para apresentação dos objetos da classe em controles do tipo grade.</returns>
        public override Grid grid()
        {
            Grid retorno = Grid.fabricar("Inclusão", new string[] { "identificador" });
            retorno.adicionarDetalhe("nip", "NIP", typeof(string), null, GridDetalhe.enumFormato.elo);
            retorno.adicionarDetalhe("nome", "Nome", typeof(string), null, GridDetalhe.enumFormato.elo);
            retorno.adicionarDetalhe("dataInclusao", "Data Inclusão", typeof(DateTime), "d", GridDetalhe.enumFormato.elo);
            retorno.campoOrdenador = "dataInclusao";
            retorno.sentido = enumSentidos.Decrescente;
            return retorno;
        }
        /// <summary>
        /// Retorna formatação para edição dos objetos da classe em formulários genéricos.
        /// </summary>
        /// <returns>Formatação para edição dos objetos da classe em formulários genéricos.</returns>
        protected override Interacao interacao(bool umaOpcaoInteracao)
        {
            Interacao retorno = Interacao.fabricar(this, "Inclusão");
            retorno.adicionarDetalhe("foto", "militar.foto", "", typeof(FotoMilitar), null);

            InteracaoAba abaGeral = retorno.adicionarAba("abaGeral", "Inclusão Individual");
            
            retorno.permiteRepetir = true;
            abaGeral.adicionarDetalhe("nip", "nip", "NIP", typeof(string), null);
            if (this.militar == null)
            {
                abaGeral.adicionarDetalhe("localizarMilitarDP", "localizarMilitarDP", "Localizar Militar", null, "botao");
            }
            abaGeral.adicionarDetalhe("militar", "militar", "Militar", typeof(Militar), null).editavel = false;
            abaGeral.adicionarDetalhe("postoGraduacao", "postoGraduacao", "Posto/Graduação", typeof(PostoGraduacao), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("corpo", "corpo", "Corpo", typeof(Corpo), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("quadro", "quadro", "Quadro", typeof(Quadro), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("especialidade", "especialidade", "Especialidade", typeof(Especialidade), null);
            abaGeral.adicionarDetalhe("nome", "nome", "Nome", typeof(string), null);
            abaGeral.adicionarDetalhe("nomeGuerra", "nomeGuerra", "Nome de Guerra", typeof(string), null);
            abaGeral.adicionarDetalhe("dataInclusao", "dataInclusao", "Data inclusão", typeof(DateTime), "d").editavel = false;
            abaGeral.adicionarDetalhe("incorporacao", "incorporacao", "Data incorporação", typeof(DateTime), "d");
            abaGeral.adicionarDetalhe("turma", "turma", "Turma", typeof(Turma), null);
            abaGeral.adicionarDetalhe("colocacao", "colocacao", "Colocação", typeof(int), null);
            abaGeral.adicionarDetalhe("habilitacao", "habilitacao", "Habilitação", typeof(TurmaHabilitacao), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("oMIncorporacao", "oMIncorporacao", "OM Incorporação", typeof(OM), null);
            abaGeral.adicionarDetalhe("sexo", "sexo", "Sexo", typeof(Sexo), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("CPF", "CPF", "CPF", typeof(string), null);
            abaGeral.adicionarDetalhe("dataNascimento", "dataNascimento", "Data nascimento", typeof(DateTime), "d");
            abaGeral.adicionarDetalhe("tipoSanguineo", "tipoSanguineo", "Tipo Sanguineo", typeof(TipoSanguineo), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("fatorRH", "fatorRH", "Fator RH", typeof(FatorRH), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("naturalidade", "naturalidade", "Naturalidade", typeof(UF), null);
            abaGeral.adicionarDetalhe("estadoCivil", "estadoCivil", "Estado Civil", typeof(EstadoCivil), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("nomePai", "nomePai", "Nome do Pai", typeof(string), null);
            abaGeral.adicionarDetalhe("nomeMae", "nomeMae", "Nome da Mãe", typeof(string), null);
            abaGeral.adicionarDetalhe("PASEP", "PASEP", "PASEP", typeof(string), null);
            abaGeral.adicionarDetalhe("tituloEleitoral", "tituloEleitoral", "Título Eleitoral", typeof(TituloEleitoral), null);
            //abaGeral.adicionarDetalhe("endereco", "endereco", "Endereço", typeof(Endereco), null);
            abaGeral.adicionarDetalhe("spp", "spp", "SPP", typeof(SituacaoPessoaPrincipal), null);
            abaGeral.adicionarDetalhe("motivoSPP", "motivoSPP", "Motivo SPP", typeof(SituacaoPessoaPrincipalMotivo), null);
            abaGeral.adicionarDetalhe("dataPromocao", "dataPromocao", "Data Promocao", typeof(DateTime), null);
            abaGeral.adicionarDetalhe("postoPromocao", "postoPromocao", "Posto/Graduação", typeof(PostoGraduacao), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("formaPromocao", "formaPromocao", "Forma Promoção", typeof(PromocaoForma), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("criterioPromocao", "criterioPromocao", "Critério Promoção", typeof(PromocaoCriterio), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("numeroDocumentoPromocao", "numeroDocumentoPromocao", "Número documento Promoção", typeof(string), null);
            abaGeral.adicionarDetalhe("dataDocumentoPromocao", "dataDocumentoPromocao", "Data documento Promoção", typeof(DateTime), null);
            abaGeral.adicionarDetalhe("tipoDocumentoPromocao", "tipoDocumentoPromocao", "Tipo de documento Promoção", typeof(AntigoTipoDocumento), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("origemDocumentoPromocao", "origemDocumentoPromocao", "OM origem documento Promoção", typeof(OM), null);
            abaGeral.adicionarDetalhe("ordmov", "ordmov", "ORDMOV", typeof(string), null);
            abaGeral.adicionarDetalhe("dataApresentacao", "dataApresentacao", "Data apresentação", typeof(DateTime), null);
            abaGeral.adicionarDetalhe("omDestinio", "omDestinio", "OM destino", typeof(OM), null);
            abaGeral.adicionarDetalhe("situacaoFuncional", "situacaoFuncional", "Situação Funcional", typeof(SituacaoFuncional), null, enumFormatoInteracao.caixaCombinada);
            abaGeral.adicionarDetalhe("HistoricosDPMMDA", "HistoricosDPMMDA", "Histórico DP", typeof(string), null).editavel = false;
            abaGeral.selecionado = true;

            InteracaoAba abaLote = retorno.adicionarAba("abaLote", "Inclusão Lote");
            abaLote.adicionarDetalhe("dtPromocao", "dtPromocao", "Data da Promoção", typeof(DateTime), null);
            abaLote.adicionarDetalhe("curso", "curso", "Curso", typeof(string), null, enumFormatoInteracao.caixaCombinada);
            abaLote.adicionarDetalhe("turmaLote", "turmaLote", "Turma", typeof(string), null, enumFormatoInteracao.caixaCombinada);            
            abaLote.adicionarDetalhe("anoLote", "anoLote", "Ano", typeof(string), null, enumFormatoInteracao.caixaCombinada);
            abaLote.adicionarDetalhe("militaresLote", "militaresLote", "Importar", null, "botao");

            InteracaoAba abaTramitacao = retorno.adicionarAba("abaTramitacao", "Tramitação");
            abaTramitacao.adicionarDetalhe("faseAtual", "faseAtual", "Fase atual", typeof(Fase), null).editavel = false;
            abaTramitacao.adicionarDetalhe("tramitacao", "tramitacoes", "Tramitação", typeof(Colecao), null).editavel = false;

            return retorno;
        }
        /// <summary>
        /// Retorna formatação para seleção dos objetos da classe em filtros genéricos.
        /// </summary>
        /// <returns>formatação para seleção dos objetos da classe em filtros genéricos.</returns>
        public override Selecao selecao()
        {
            Selecao retorno = Selecao.fabricar(this, "Inclusão");
            retorno.adicionarDetalhe("nip", "nip", "Nip", typeof(string));
            retorno.adicionarDetalhe("nome", "nome", "Nome", typeof(string));
            retorno.adicionarDetalhe("nomeGuerra", "nomeGuerra", "Nome de guerra", typeof(string));
            retorno.adicionarDetalhe("militar", "militar", "Militar", typeof(Militar));
            retorno.adicionarDetalhe("dataInclusao", "dataInclusao", "Data inclusão", typeof(DateTime));
            retorno.adicionarDetalhe("sexo", "sexo", "Sexo", typeof(Sexo));
            retorno.adicionarDetalhe("CPF", "CPF", "CPF", typeof(string));
            retorno.adicionarDetalhe("dataNascimento", "dataNascimento", "Data de nascimento", typeof(DateTime));
            retorno.adicionarDetalhe("tipoSanguineo", "tipoSanguineo", "Tipo sanguíneo", typeof(TipoSanguineo));
            retorno.adicionarDetalhe("fatorRH", "fatorRH", "Fator RH", typeof(FatorRH));
            retorno.adicionarDetalhe("naturalidade", "naturalidade", "Naturalidade", typeof(string));
            retorno.adicionarDetalhe("estadoCivil", "estadoCivil", "Estado Civil", typeof(EstadoCivil));
            retorno.adicionarDetalhe("especialidade", "especialidade", "Especialidade", typeof(Especialidade));
            retorno.adicionarDetalhe("nomePai", "nomePai", "Nome do Pai", typeof(string));
            retorno.adicionarDetalhe("nomeMae", "nomeMae", "Nome da Mae", typeof(string));
            retorno.adicionarDetalhe("PASEP", "PASEP", "PASEP", typeof(string));
            retorno.adicionarDetalhe("tituloEleitoral", "tituloEleitoral", "Título Eleitoral", typeof(TituloEleitoral));
            retorno.adicionarDetalhe("endereco", "endereco", "Endereço", typeof(Endereco));
            retorno.adicionarDetalhe("postoGraduacao", "postoGraduacao", "Posto/Graduação", typeof(PostoGraduacao));
            retorno.adicionarDetalhe("corpo", "corpo", "Corpo", typeof(Corpo));
            retorno.adicionarDetalhe("quadro", "quadro", "quadro", typeof(Quadro));
            retorno.adicionarDetalhe("incorporacao", "incorporacao", "Date de Incorporação", typeof(DateTime));
            retorno.adicionarDetalhe("oMIncorporacao", "oMIncorporacao", "OM de Incorporação", typeof(OM));
            retorno.adicionarDetalhe("turma", "turma", "Turma", typeof(Turma));
            retorno.adicionarDetalhe("spp", "spp", "SPP", typeof(SituacaoPessoaPrincipal));
            retorno.adicionarDetalhe("motivoSpp", "motivoSpp", "MotivoSpp", typeof(SituacaoPessoaPrincipalMotivo));
            retorno.adicionarDetalhe("dataPromocao", "dataPromocao", "Data da Promoção", typeof(DateTime));
            retorno.adicionarDetalhe("postoPromocao", "postoPromocao", "Posto da Promocao", typeof(PostoGraduacao));
            retorno.adicionarDetalhe("numeroDocumentoPromocao", "numeroDocumentoPromocao", "Número do Documento da Promoção", typeof(string));
            retorno.adicionarDetalhe("dataDocumentoPromocao", "dataDocumentoPromocao", "Data do Documento da Promocao", typeof(DateTime));
            retorno.adicionarDetalhe("origemDocumentoPromocao", "origemDocumentoPromocao", "Origem do Documento da Promoção", typeof(OM));
            retorno.adicionarDetalhe("formaPromocao", "formaPromocao", "Forma da Promoção", typeof(PromocaoForma));
            retorno.adicionarDetalhe("criterioPromocao", "criterioPromocao", "Critério da Promoção", typeof(PromocaoCriterio));
            retorno.adicionarDetalhe("tipoDocumentoPromocao", "tipoDocumentoPromocao", "Tipo do Documento da Promoção", typeof(AntigoTipoDocumento));
            retorno.adicionarDetalhe("ordMov", "ordMov", "Número da OrdMov", typeof(string));
            retorno.adicionarDetalhe("dataApresentacao", "dataApresentacao", "Data de Apresentação", typeof(DateTime));
            retorno.adicionarDetalhe("oMDestinio", "oMDestinio", "OM Destíno", typeof(OM));
            retorno.adicionarDetalhe("situacaoFuncional", "situacaoFuncional", "Situação Funcional", typeof(SituacaoFuncional));
            retorno.adicionarDetalhe("habilitacao", "habilitacao", "Habilitação", typeof(TurmaHabilitacao));
            retorno.adicionarDetalhe("colocacao", "colocacao", "Colocação", typeof(int));
            retorno.adicionarDetalhe("historicoMilitar", "historicoMilitar", "Histórico Militar", typeof(HistoricoMilitar));
            retorno.adicionarDetalhe("promocao", "promocao", "Promoção", typeof(Promocao));
            retorno.adicionarDetalhe("comissao", "comissao", "Comissão", typeof(Comissao));
            retorno.adicionarDetalhe("sfc", "sfc", "Situação Funcional na Comissão", typeof(SituacaoFuncionalComissao));
            retorno.adicionarDetalhe("turmaMilitar", "turmaMilitar", "Turma do Militar", typeof(TurmaMilitar));
            retorno.adicionarDetalhe("boletim", "boletim", "Boletim reinclusão", typeof(string));
            retorno.adicionarDetalhe("faseAtual", "faseAtual", "Fase atual", typeof(Fase));
            retorno.adicionarDetalhe("tramitacoes", "tramitacoes", "Tramitações", typeof(Colecao));
            return retorno;
        }
        /// <summary>
        /// Retorna parâmetros para extração dos objetos da classe por engenhos genéricos.
        /// </summary>
        /// <returns>parâmetros para extração dos objetos da classe por engenhos genéricos.</returns>
        public override Extracao extracao()
        {
            Extracao retorno = Extracao.fabricar(this, "Inclusão");
            retorno.adicionarDetalhe("nip", "nip", "Nip", typeof(string));
            retorno.adicionarDetalhe("nome", "nome", "Nome", typeof(string));
            retorno.adicionarDetalhe("nomeGuerra", "nomeGuerra", "Nome de guerra", typeof(string));
            retorno.adicionarDetalhe("militar", "militar", "Militar", typeof(Militar));
            retorno.adicionarDetalhe("dataInclusao", "dataInclusao", "Data inclusão", typeof(DateTime));
            retorno.adicionarDetalhe("sexo", "sexo", "Sexo", typeof(Sexo));
            retorno.adicionarDetalhe("CPF", "CPF", "CPF", typeof(string));
            retorno.adicionarDetalhe("dataNascimento", "dataNascimento", "Data de nascimento", typeof(DateTime));
            retorno.adicionarDetalhe("tipoSanguineo", "tipoSanguineo", "Tipo sanguíneo", typeof(TipoSanguineo));
            retorno.adicionarDetalhe("fatorRH", "fatorRH", "Fator RH", typeof(FatorRH));
            retorno.adicionarDetalhe("naturalidade", "naturalidade", "Naturalidade", typeof(string));
            retorno.adicionarDetalhe("estadoCivil", "estadoCivil", "Estado Civil", typeof(EstadoCivil));
            retorno.adicionarDetalhe("especialidade", "especialidade", "Especialidade", typeof(Especialidade));
            retorno.adicionarDetalhe("nomePai", "nomePai", "Nome do Pai", typeof(string));
            retorno.adicionarDetalhe("nomeMae", "nomeMae", "Nome da Mae", typeof(string));
            retorno.adicionarDetalhe("PASEP", "PASEP", "PASEP", typeof(string));
            retorno.adicionarDetalhe("tituloEleitoral", "tituloEleitoral", "Título Eleitoral", typeof(TituloEleitoral));
            retorno.adicionarDetalhe("endereco", "endereco", "Endereço", typeof(Endereco));
            retorno.adicionarDetalhe("postoGraduacao", "postoGraduacao", "Posto/Graduação", typeof(PostoGraduacao));
            retorno.adicionarDetalhe("corpo", "corpo", "Corpo", typeof(Corpo));
            retorno.adicionarDetalhe("quadro", "quadro", "quadro", typeof(Quadro));
            retorno.adicionarDetalhe("incorporacao", "incorporacao", "Date de Incorporação", typeof(DateTime));
            retorno.adicionarDetalhe("oMIncorporacao", "oMIncorporacao", "OM de Incorporação", typeof(OM));
            retorno.adicionarDetalhe("turma", "turma", "Turma", typeof(Turma));
            retorno.adicionarDetalhe("spp", "spp", "SPP", typeof(SituacaoPessoaPrincipal));
            retorno.adicionarDetalhe("motivoSpp", "motivoSpp", "MotivoSpp", typeof(SituacaoPessoaPrincipalMotivo));
            retorno.adicionarDetalhe("dataPromocao", "dataPromocao", "Data da Promoção", typeof(DateTime));
            retorno.adicionarDetalhe("postoPromocao", "postoPromocao", "Posto da Promocao", typeof(PostoGraduacao));
            retorno.adicionarDetalhe("numeroDocumentoPromocao", "numeroDocumentoPromocao", "Número do Documento da Promoção", typeof(string));
            retorno.adicionarDetalhe("dataDocumentoPromocao", "dataDocumentoPromocao", "Data do Documento da Promocao", typeof(DateTime));
            retorno.adicionarDetalhe("origemDocumentoPromocao", "origemDocumentoPromocao", "Origem do Documento da Promoção", typeof(OM));
            retorno.adicionarDetalhe("formaPromocao", "formaPromocao", "Forma da Promoção", typeof(PromocaoForma));
            retorno.adicionarDetalhe("criterioPromocao", "criterioPromocao", "Critério da Promoção", typeof(PromocaoCriterio));
            retorno.adicionarDetalhe("tipoDocumentoPromocao", "tipoDocumentoPromocao", "Tipo do Documento da Promoção", typeof(AntigoTipoDocumento));
            retorno.adicionarDetalhe("ordMov", "ordMov", "Número da OrdMov", typeof(string));
            retorno.adicionarDetalhe("dataApresentacao", "dataApresentacao", "Data de Apresentação", typeof(DateTime));
            retorno.adicionarDetalhe("oMDestinio", "oMDestinio", "OM Destíno", typeof(OM));
            retorno.adicionarDetalhe("situacaoFuncional", "situacaoFuncional", "Situação Funcional", typeof(SituacaoFuncional));
            retorno.adicionarDetalhe("habilitacao", "habilitacao", "Habilitação", typeof(TurmaHabilitacao));
            retorno.adicionarDetalhe("colocacao", "colocacao", "Colocação", typeof(int));
            retorno.adicionarDetalhe("historicoMilitar", "historicoMilitar", "Histórico Militar", typeof(HistoricoMilitar));
            retorno.adicionarDetalhe("promocao", "promocao", "Promoção", typeof(Promocao));
            retorno.adicionarDetalhe("comissao", "comissao", "Comissão", typeof(Comissao));
            retorno.adicionarDetalhe("sfc", "sfc", "Situação Funcional na Comissão", typeof(SituacaoFuncionalComissao));
            retorno.adicionarDetalhe("turmaMilitar", "turmaMilitar", "Turma do Militar", typeof(TurmaMilitar));
            retorno.adicionarDetalhe("boletim", "boletim", "Boletim reinclusão", typeof(string));
            retorno.adicionarDetalhe("faseAtual", "faseAtual", "Fase atual", typeof(Fase));
            return retorno;
        }
        public override Selecao selecaoRapida()
        {
            Selecao retorno = Selecao.fabricar(this, "Inclusão");
            retorno.adicionarDetalhe("nip", "nip", "NIP", typeof(string));
            retorno.adicionarDetalhe("nome", "nome", "Nome", typeof(string));
            return retorno;
        }
        public override MigalhaPao migalha()
        {
            return MigalhaPao.fabricar(this, "Inclusão", this.militar);
        }
        #endregion

        #region ITramitavel Members

        Organon.Seguranca.Roteiro ITramitavel.roteiro
        {
            get
            {
                return roteiro;
            }
        }
        Fase ITramitavel.faseAtual
        {
            get
            {
                return faseAtual;
            }
            set
            {
                faseAtual = value;
            }
        }
        object ITramitavel.encaminhar(ISessao umaSessao)
        {
            return encaminhar(umaSessao);
        }
        object ITramitavel.devolver(ISessao umaSessao)
        {
            return devolver(umaSessao);
        }
        bool ITramitavel.podeEncaminhar(ISessao umaSessao)
        {
            return podeEncaminhar(umaSessao);
        }
        bool ITramitavel.podeDevolver(ISessao umaSessao)
        {
            return podeDevolver(umaSessao);
        }
        IColecao ITramitavel.tramitacoes
        {
            get { return tramitacoes; }
        }

        #endregion
    }
}
