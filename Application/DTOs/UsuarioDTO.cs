namespace Application.DTOs
{
    public class UsuarioDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string SenhaHash { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
