using System.ComponentModel.DataAnnotations;


namespace BaseModel
{
    public class Cliente
    {
        public int ClienteID { get; set; }
        [Required]
        public string Nome { get; set; }
        [Display(Name = "CPF/CNPJ")]
        public string Cpf_Cnpj { get; set; }
        public string Obs { get; set; }
        [Required]
        public bool Inativo { get; set; }
    }
}
