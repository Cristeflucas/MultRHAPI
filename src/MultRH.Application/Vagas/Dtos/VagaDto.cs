namespace MultRH.Application.Vagas.Dtos
{
    public class VagaDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Empresa { get; set; }
        public string? Descricao { get; set; }
        public string? Endereco { get; set; }
        public string? PontoRef { get; set; }
        public DateOnly? DataEntrevista { get; set; }
        public TimeOnly? HorarioEntrevista { get; set; }
        public string? ResponsavelEntrevista { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
