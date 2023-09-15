using AutoMapper;
using GestaoProduto.Domain.Entities;
using GestaoProjeto.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.AutoMapper
{
    public class DomainToApplication : Profile
    {
        public DomainToApplication()
        {
            CreateMap<Produto, ProdutoViewModel>();

            CreateMap<Produto, NovoProdutoViewModel>();

            CreateMap<Fornecedor, FornecedorViewModel>();

            CreateMap<Fornecedor, NovoFornecedorViewModel>();

            CreateMap<Categoria, CategoriaViewModel>();

            CreateMap<Categoria, NovaCategoriaViewModel>();
        }
    }
}
