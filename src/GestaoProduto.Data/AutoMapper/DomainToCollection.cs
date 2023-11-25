using AutoMapper;
using GestaoProduto.Data.Providers.MongoDb.Collections;
using GestaoProduto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Data.AutoMapper
{
    public class DomainToCollection : Profile
    {
        public DomainToCollection()
        {
            CreateMap<Produto, ProdutoCollection>();
            CreateMap<Fornecedor, FornecedorCollection>();
            CreateMap<Categoria, CategoriaCollection>();
            CreateMap<Usuario, UsuarioCollection>();
        }
    }
}
