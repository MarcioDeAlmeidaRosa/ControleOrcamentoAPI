namespace ControleOrcamentoAPI.Models
{
    public class Usuario : Entity
    {
        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        //Usado para definir o papel do usuário no sistema ADMIN/USER
        public string Rolle { get; set; }
    }
}
