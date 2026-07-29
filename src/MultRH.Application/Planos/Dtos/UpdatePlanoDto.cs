using MultRH.Domain.Enums;

namespace MultRH.Application.Planos.Dtos
{
    public class UpdatePlanoDto
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? Valor { get; set; }
        public Periodicidade? Periodicidade { get; set; }
        public bool? Ativo { get; set; } = false;
    }
}
