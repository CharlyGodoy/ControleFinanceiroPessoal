using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class ContaReceber
    {
        public int ContaReceberID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Valor { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Valor Recebido")]
        public double Valor_Recebido { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de Inclusão")]
        public DateTime Data_Inclusao { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data Prevista de Recebimento")]
        public DateTime Data_PrevRecebimento { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de Recebimento")]
        public DateTime Data_Recebimento { get; set; }
        [Required]
        public bool Baixado { get; set; }
        [Required]
        public bool Liquidado { get; set; }

        // --- Relacionamento Grupo ---
        public int GrupoID { get; set; }
        public virtual Grupo _Grupo { get; set; }

        //// --- Relacionamento SubGrupo ---
        //public int SubGrupoID { get; set; }
        //public virtual SubGrupo _SubGrupo { get; set; }

        // --- Relacionamento Cliente ---
        public int ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }

    }
}
