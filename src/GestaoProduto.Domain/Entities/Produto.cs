using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestaoProduto.Domain.Entities
{
    public class Produto
    {
        #region - Construtores
        public Produto(int codigo, string nome, string descricao, bool ativo, decimal valor, DateTime dataCadastro, int estoque, int estoqueMinimo)
        {
            Codigo = codigo;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = dataCadastro;
            Estoque = estoque;
            EstoqueMinimo = estoqueMinimo;
        }

        #endregion

        #region - Propriedades
        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public int Estoque { get; private set; }
        public int EstoqueMinimo { get; set; }

        #endregion

        #region - Comportamentos    

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void AlterarDescricao(string novaDescricao)
        {
            Descricao = novaDescricao;
        }

        public void AlterarNome(string novoNome)
        {
            Nome = novoNome;
        }

        public void AtualizarEstoque(int quantidade)
        {
            Estoque += quantidade;
        }

        public bool PossuiEstoque(int quantidade) => Estoque >= quantidade;

        public void AlterarPreco(decimal valor)
        {
            Valor = valor;
        }

        public void AlterarEstoqueMinimo(int quant)
        {
            EstoqueMinimo = quant;
        }

        #endregion
    }
}
