namespace ControleOrcamentoAPI.Models
{
    public class UsuarioAutenticado : Entity
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public override string ToString()
        {
            return base.ToString();//TODO: REECREVER
        }
    }
}
