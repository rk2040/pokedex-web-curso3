using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pokemon
    {
        public int Id {  get; set; }
        // Para que el nombre de las columnas no tome el nombre de las propiedades y podamos darle un nombre que querramos a las columnas
        // Metodo DisplayName. Cada uno va arriba de la propiedad que queremos que se muestre otro nombre en lugar del de la propiedad
        [DisplayName("Número")]
        public int Numero {  get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public Elemento Tipo { get; set; }
        public Elemento Debilidad { get; set; }
        public bool Activo { get; set; }

    }
}
