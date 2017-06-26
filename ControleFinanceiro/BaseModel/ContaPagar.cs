using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class ContaPagar
    {
        public int ContaPagarID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Valor { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Valor Pago")]
        public double Valor_Pago { get; set; }
        [Display(Name = "Data de Inclusão")]
        [DataType(DataType.Date)]
        public DateTime Data_Inclusao { get; set; }
        [Display(Name = "Data de Vencimento")]
        [DataType(DataType.Date)]        
        public DateTime Data_Vencimento { get; set; }
        [Display(Name = "Data de Pagamento")]
        [DataType(DataType.Date)]
        public DateTime Data_Pagamento { get; set; }
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

        // --- Relacionamento Fornecedor ---
        public int FornecedorID { get; set; }
        public virtual Fornecedor _Fornecedor { get; set; }

    }
}
