using System.ComponentModel.DataAnnotations;

namespace BaseModel
{
    public class Fornecedor
    {
        public int FornecedorID { get; set; }
        [Required]
        public string Nome { get; set; }
        [Display(Name = "CPF/CNPJ")]
        public string Cpf_Cnpj { get; set; }
        public string Obs { get; set; }
        [Required]
        public bool Inativo { get; set; }
    }
}
