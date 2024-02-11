using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;


namespace Dominio
{
    public class Articulo
    { 
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        public string Imagen { get; set; }
        public decimal Precio { get; set; }
        [DisplayName("Marca")]
        public Marca DescripcionM { get; set; }
        [DisplayName("Categoría")]
        public Categoria DescripcionC { get; set; }

        public static implicit operator Articulo(List<Articulo> v)
        {
            throw new NotImplementedException();
        }

        // crear clase Marca(seleccionable de una lista desplegable). e importar.
        // crear clase Categoría(seleccionable de una lista desplegable. e importar.
    }
}
