using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSalaCinema
{
    public class TelaCadastroSalaCinema : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<SalaCinema> _repositorioSalaCinema;
        private readonly Notificador _notificador;

        public TelaCadastroSalaCinema(IRepositorio<SalaCinema> salaCinema, Notificador notificador)
            : base ("Cadastro de Sala de Cinema")
        {
            _repositorioSalaCinema = salaCinema;
            _notificador = notificador;

        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Sala");

            SalaCinema novaSala = ObterSala();

            _repositorioSalaCinema.Inserir(novaSala);

            _notificador.ApresentarMensagem("Sala cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Sala Cinema");

            bool temSalaCadastrada = VisualizarRegistros("Pesquisando");

            if (temSalaCadastrada == false)
            {
                _notificador.ApresentarMensagem("Nenhuma sala cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroSala = ObterNumeroRegistro();

            SalaCinema salaAtualizada = ObterSala();

            bool conseguiuEditar = _repositorioSalaCinema.Editar(numeroSala, salaAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala de cinema editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Sala");

            bool temSalaRegistrada = VisualizarRegistros("Pesquisando");

            if (temSalaRegistrada == false)
            {
                _notificador.ApresentarMensagem("Nenhuma sala cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroSala = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioSalaCinema.Excluir(numeroSala);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala de Cinema excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Salas");

            List<SalaCinema> salas = _repositorioSalaCinema.SelecionarTodos();

            if (salas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma sala disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (SalaCinema sala in salas)
                Console.WriteLine(sala.ToString());

            Console.ReadLine();

            return true;
        }

        private SalaCinema ObterSala()
        {
            Console.Write("Digite a capacidade: ");
            int capacidadeSala = int.Parse(Console.ReadLine());

            Console.Write("Digite a quantidade de assentos disponiveis: ");
            int assentosDisponiveis = int.Parse(Console.ReadLine());


            return new SalaCinema(capacidadeSala, assentosDisponiveis);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroSalaEncontrada;

            do
            {
                Console.Write("Digite o ID da sala que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroSalaEncontrada = _repositorioSalaCinema.ExisteRegistro(numeroRegistro);

                if (numeroSalaEncontrada == false)
                    _notificador.ApresentarMensagem("ID da sala não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroSalaEncontrada == false);

            return numeroRegistro;
        }
    }
}
