using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSalaCinema
{
    public class SalaCinema : EntidadeBase
    {
        private readonly int _capacidade;
        private readonly int _assentosDisponiveis;

        

        public SalaCinema(int capacidade, int assentosDisponiveis)
        {
            _capacidade = capacidade;
            _assentosDisponiveis = assentosDisponiveis;
        }

        public override string ToString()
        {
            return "Sala Id: " + id + Environment.NewLine +
                "Capacidade da sala: " + _capacidade + Environment.NewLine +
                "Assentos disponiveis: " + _assentosDisponiveis + Environment.NewLine;
        }
    }
}
