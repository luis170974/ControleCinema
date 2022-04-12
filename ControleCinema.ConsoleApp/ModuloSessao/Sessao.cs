using System;
using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloIngresso;
using ControleCinema.ConsoleApp.ModuloSalaCinema;
using ControleCinema.ConsoleApp.ModuloFilme;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class Sessao : EntidadeBase
    {
        public Ingresso _ingressosVendidos;
        public SalaCinema _salaCinema;
        public Filme _filme;
        private readonly bool _statusSessao;
        private readonly DateTime _horarioFilme;

        public Sessao(SalaCinema salaCinema, DateTime horarioDoFilme, Filme filme)
        {
            _salaCinema = salaCinema;
            _horarioFilme = horarioDoFilme;
            _filme = filme;

        }

        public override string ToString()
        {
            return "Sessão Id: " + id + Environment.NewLine +
                "Sala: " + _salaCinema + Environment.NewLine +
                "Filme: " + _filme + Environment.NewLine +
                "Horario do filme: " + _horarioFilme + Environment.NewLine;
        }

    }
}
