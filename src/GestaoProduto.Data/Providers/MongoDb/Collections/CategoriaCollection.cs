using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Data.Providers.MongoDb.Collections
{
    [BsonCollection("Categoria")]
    public class CategoriaCollection : Document
    {
        public int Codigo { get;  set; }
        public string Descricao { get;  set; }
    }
}
