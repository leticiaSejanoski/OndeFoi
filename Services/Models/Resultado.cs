using OndeFoi.Models;

namespace OndeFoi.Services
{
    public class Resultado<T>
    {
        public bool Sucesso { get; set; }
        public List<string> Erros { get; set; } = new();
        public T? Dado { get; set; }

        public static Resultado<T> Ok()
        {
            return new Resultado<T>
            {
                Sucesso = true
            };
        }
        public static Resultado<T> Ok(T dado)
        {
            return new Resultado<T>
            {
                Sucesso = true,
                Dado = dado
            };
        }

        public static Resultado<T> Erro(List<string> erros)
        {
            return new Resultado<T>
            {
                Sucesso = false,
                Erros = erros
            };
        }
            public static Resultado<T> Erro(string erro)
        {
            return new Resultado<T>
            {
                Sucesso = false,
                Erros = new List<string> {erro}
            };
        }
    }
}