namespace ControleOrcamentoAPI.Models
{
    public class Agencia: Entity
    {
        public Banco Banco { get; set; }

        public string Numero { get; set; }

        public string DV { get; set; }

        public string Descricao { get; set; }
    }
}
