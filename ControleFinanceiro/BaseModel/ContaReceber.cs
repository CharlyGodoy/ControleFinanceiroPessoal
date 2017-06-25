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
        public double Valor { get; set; }
        [Required]
        public Cliente Cliente { get; set; }
        public string Descricao { get; set; }
        public double Valor_Recebido { get; set; }
        public DateTime Data_Inclusao { get; set; }
        [Required]
        public DateTime Data_PrevRecebimento { get; set; }
        public DateTime Data_Recebimento { get; set; }
        [Required]
        public bool Baixado { get; set; }
        [Required]
        public bool Liquidado { get; set; }

        // --- Relacionamento Grupo ---
        [Required]
        public int GrupoID { get; set; }
        [Required]
        public Grupo _Grupo { get; set; }

        public ContaReceber()
        {
            Data_Inclusao = DateTime.Now;
        }
    }
}
