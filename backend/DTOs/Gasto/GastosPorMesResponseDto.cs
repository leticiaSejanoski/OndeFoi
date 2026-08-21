namespace OndeFoi.DTOs;

public class GastosPorMesResponseDto{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public List<GastoResponseDto> Gastos { get; set; }
    public decimal Total { get; set; }
}