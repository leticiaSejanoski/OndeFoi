namespace OndeFoi.DTOs
{
    public class CriarGastoDto
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }
    }
}