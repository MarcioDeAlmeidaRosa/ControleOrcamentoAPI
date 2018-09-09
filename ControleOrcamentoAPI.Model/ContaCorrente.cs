namespace ControleOrcamentoAPI.Models
{
    public class ContaCorrente : Entity
    {
        public Agencia Agencia { get; set; }

        public string Numero { get; set; }

        public string DV { get; set; }
    }
}
