namespace OndeFoi.DTOs
{
    public class DashboardResponseDto
    {
        public decimal Renda { get; set; }
        public decimal TotalGastosMesAtual { get; set; }
        public decimal Saldo { get; set; }

        public IEnumerable<GastoPorCategoria> TotalPorCategoria { get; set; }
    }
}