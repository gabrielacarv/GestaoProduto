using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Data.Providers.MongoDb.Collections
{
    [BsonCollection("Usuario")]
    public class UsuarioCollection : Document
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
