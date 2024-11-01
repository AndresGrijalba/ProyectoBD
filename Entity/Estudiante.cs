using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Estudiante
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public bool Habilitado { get; set; }
        public int BeneficioRecibidos { get; set; }
    }
}
