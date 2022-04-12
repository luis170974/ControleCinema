using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloSalaCinema;
using ControleCinema.ConsoleApp.ModuloIngresso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class TelaCadastroSessao : TelaBase
    {
        private readonly IRepositorio<Sessao> _repositorioSessao;
        private readonly Notificador _notificador;
        private readonly IRepositorio<SalaCinema> _repositorioSalaCinema;
        private readonly TelaCadastroSalaCinema _telaCadastroSalaCinema;
        private readonly IRepositorio<Ingresso> _repositorioIngresso;
        private readonly Ingresso _ingresso;


        public TelaCadastroSessao(IRepositorio<Sessao> sessao, Notificador notificador, IRepositorio<SalaCinema> repositorioSalaCinema, TelaCadastroSalaCinema telaCadastroSalaCinema, IRepositorio<Ingresso> ingressos, Ingresso ingresso)
            : base("Cadastro de Sala de Cinema")
        {

            _repositorioSessao = sessao;
            _notificador = notificador;
            _repositorioSalaCinema = repositorioSalaCinema;
            _telaCadastroSalaCinema = telaCadastroSalaCinema;
            _repositorioIngresso = ingressos;
            _ingresso = ingresso;


        }


        public void Inserir()
        {
            MostrarTitulo("Cadastro de Sessão");

            Sessao novaSessao = ObterSessao();

            _repositorioSessao.Inserir(novaSessao);

            _notificador.ApresentarMensagem("Sessão cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Sessão");

            bool temSessaoCadastrada = VisualizarRegistros("Pesquisando");

            if (temSessaoCadastrada == false)
            {
                _notificador.ApresentarMensagem("Nenhuma sessão cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroSessao = ObterNumeroSessao();

            Sessao sessaoAtualizada = ObterSessao();

            bool conseguiuEditar = _repositorioSessao.Editar(numeroSessao, sessaoAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sessão editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Sessão");

            bool temFilmeRegistrado = VisualizarRegistros("Pesquisando");

            if (temFilmeRegistrado == false)
            {
                _notificador.ApresentarMensagem("Nenhum filme cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroSessao = ObterNumeroSessao();

            bool conseguiuExcluir = _repositorioSessao.Excluir(numeroSessao);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sessão excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Sessões");

            List<Sessao> sessoes = _repositorioSessao.SelecionarTodos();

            if (sessoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma sessão disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sessao sessao in sessoes)
                Console.WriteLine(sessao.ToString());

            Console.ReadLine();

            return true;
        }

        private Sessao ObterSessao()
        {
            //mostrar capacidade da sala
    

            Console.WriteLine("Digite a sala escolhida: ");

            bool mostrarSalas = _telaCadastroSalaCinema.VisualizarRegistros("Tela");

            int salaEscolhida = int.Parse(Console.ReadLine());

            SalaCinema mostraSalaEscolhida = _repositorioSalaCinema.SelecionarRegistro(salaEscolhida);

            Console.WriteLine("Digite o horario da sessão: ");

            DateTime horario = Convert.ToDateTime(Console.ReadLine());

            return new Sessao(mostraSalaEscolhida, horario);
        }
        

        public int ObterNumeroSessao()
        {
            int numeroRegistro;
            bool numeroSessaoEncontrado;

            do
            {
                Console.Write("Digite o ID do Filme que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroSessaoEncontrado = _repositorioSessao.ExisteRegistro(numeroRegistro);

                if (numeroSessaoEncontrado == false)
                    _notificador.ApresentarMensagem("ID da sessão não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroSessaoEncontrado == false);

            return numeroRegistro;
        }
    }
}
