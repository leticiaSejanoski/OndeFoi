namespace OndeFoi.DTOs
{
    public class EditarGastoDto
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
    }
}