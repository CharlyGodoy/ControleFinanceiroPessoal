using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class SubGrupo
    {
        public int SubGrupoID { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public bool Inativo { get; set; }

        // Relacionamento com Grupo
        public int GrupoID { get; set; }
        public virtual Grupo _Grupo { get; set; }
    }
}
