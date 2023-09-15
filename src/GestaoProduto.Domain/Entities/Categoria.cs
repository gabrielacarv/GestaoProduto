using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Domain.Entities
{
    public class Categoria
    {
        #region Construtores
        public Categoria(int codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        #endregion

        #region Propriedades

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }

        #endregion

        #region Comportamentos
        public void AlterarDescricao(string novaDescricao)
        {
            Descricao = novaDescricao;
        }

        #endregion
    }
}
