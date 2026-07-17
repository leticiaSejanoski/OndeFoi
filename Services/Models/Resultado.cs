using OndeFoi.Models;

namespace OndeFoi.Services
{
    public class Resultado
    {
        public bool Sucesso { get; set; }
        public List<string> Erros { get; set; } = new();
        public Categoria? Categoria { get; set; }

        public static Resultado Ok()
        {
            return new Resultado
            {
                Sucesso = true
            };
        }
        public static Resultado Ok(Categoria categoria)
        {
            return new Resultado
            {
                Sucesso = true,
                Categoria = categoria
            };
        }

        public static Resultado Erro(List<string> erros)
        {
            return new Resultado
            {
                Sucesso = false,
                Erros = erros
            };
        }
            public static Resultado Erro(string erro)
        {
            return new Resultado
            {
                Sucesso = false,
                Erros = new List<string> {erro}
            };
        }
    }
}