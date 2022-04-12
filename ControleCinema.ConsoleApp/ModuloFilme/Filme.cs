using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class Filme : EntidadeBase
    {
        private readonly string _nomeFilme;
        private readonly string _duracaoFilme;
        public Genero _genero;



        public Filme(string nomeFilme, string duracaoFilme, Genero genero)
        {
            _nomeFilme = nomeFilme;
            _duracaoFilme = duracaoFilme;
            _genero = genero;

        }

        public override string ToString()
        {
            return "Filme Id: " + id + Environment.NewLine +
                "Nome do filme: " + _nomeFilme + Environment.NewLine +
                "Duração do filme: " + _duracaoFilme + Environment.NewLine +
                "Genero do filme: " + _genero + Environment.NewLine;
        }
    }
}
