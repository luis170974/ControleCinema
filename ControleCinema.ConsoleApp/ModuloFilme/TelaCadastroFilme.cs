using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class TelaCadastroFilme : TelaBase , ITelaCadastravel
    {
        private readonly Notificador _notificador;
        private readonly IRepositorio<Filme> _repositorioFilme;
        private readonly IRepositorio<Genero> _repositorioGenero;
        private readonly TelaCadastroGenero _telaCadastroGenero;


        public TelaCadastroFilme( IRepositorio<Filme> repositorioFilme, Notificador notificador, IRepositorio<Genero> repositorioGenero, TelaCadastroGenero telaGenero) : base("Cadastro de Filme")
        {
            _repositorioFilme = repositorioFilme;
            _notificador = notificador;
            _repositorioGenero = repositorioGenero;
            _telaCadastroGenero = telaGenero;

        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Filme");

            Filme novoFilme = ObterFilme();

            _repositorioFilme.Inserir(novoFilme);

            _notificador.ApresentarMensagem("Filme cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Filme");

            bool temFilmeCadastrado = VisualizarRegistros("Pesquisando");

            if (temFilmeCadastrado == false)
            {
                _notificador.ApresentarMensagem("Nenhum filme cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroRegistro();

            Filme filmeAtualizado = ObterFilme();

            bool conseguiuEditar = _repositorioFilme.Editar(numeroFilme, filmeAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Filme editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Filme");

            bool temFilmeRegistrado = VisualizarRegistros("Pesquisando");

            if (temFilmeRegistrado == false)
            {
                _notificador.ApresentarMensagem("Nenhum filme cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioFilme.Excluir(numeroFilme);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Filme excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Filmes");

            List<Filme> filmes = _repositorioFilme.SelecionarTodos();

            if (filmes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum filme disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Filme filme in filmes)
                Console.WriteLine(filme.ToString());

            Console.ReadLine();

            return true;
        }

        private Filme ObterFilme()
        {

            Console.Write("Digite o nome do filme: ");
            string nomeFilme = Console.ReadLine();

            Console.Write("Digite a duração do filme: ");
            string duracao = Console.ReadLine();


            Console.WriteLine("Escolha um genero disponivel");

            bool mostrarGenero = _telaCadastroGenero.VisualizarRegistros("Tela");

            int idGeneroSelecionado = int.Parse(Console.ReadLine());

            Genero genero = _repositorioGenero.SelecionarRegistro(idGeneroSelecionado);

            return new Filme(nomeFilme, duracao, genero);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroFilmeEncontrado;

            do
            {
                Console.Write("Digite o ID do Filme que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroFilmeEncontrado = _repositorioFilme.ExisteRegistro(numeroRegistro);

                if (numeroFilmeEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Filme não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroFilmeEncontrado == false);

            return numeroRegistro;
        }




    }
}
