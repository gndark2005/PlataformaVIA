namespace PlataformaVIA.Core.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Banco
    {
        [Key]
        public string Nit { get; set; }
        public int CodBanco { get; set; }
        public string Nombre { get; set; }
    }
}
