using System.ComponentModel.DataAnnotations;

namespace BaseModel
{
    public class Grupo
    {
        public int GrupoID { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public bool Inativo { get; set; }

        // --- Relacionamento SubGrupo ---
        [Display(Name = "Grupo Pai")]
        public string Grupo_ID_Pai { get; set; }

    }
}
