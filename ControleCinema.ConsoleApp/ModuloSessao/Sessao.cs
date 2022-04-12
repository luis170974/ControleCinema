using System;
using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloIngresso;
using ControleCinema.ConsoleApp.ModuloSalaCinema;
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
        private readonly bool _statusSessao;
        private readonly DateTime _horarioFilme;

        public Sessao(SalaCinema salaCinema, DateTime horarioDoFilme)
        {
            _salaCinema = salaCinema;
            _horarioFilme = horarioDoFilme;

        }



    }
}
