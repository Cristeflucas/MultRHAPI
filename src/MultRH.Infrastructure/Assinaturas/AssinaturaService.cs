using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultRH.Application.Assinaturas;
using MultRH.Application.Assinaturas.Dtos;
using MultRH.Domain.Entities;
using MultRH.Domain.Enums;
using MultRH.Infrastructure.Data;


namespace MultRH.Infrastructure.Assinaturas
{
    public class AssinaturaService : IAssinaturaService
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public AssinaturaService(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<AssinaturaDto> Create(CreateAssinaturaDto dto)
        {
            var plano = await _context.Planos.FindAsync(dto.PlanoId)
                ?? throw new ApplicationException("Plano não encontrado.");

            var ativaExistente = await _context.Assinaturas
                .Where(a => a.UserId == dto.UserId && a.Status == StatusAssinatura.Ativa)
                .FirstOrDefaultAsync();

            if (ativaExistente is not null)
            {
                ativaExistente.Status = StatusAssinatura.Cancelada;
            }

            var inicio = DateTime.UtcNow;
            var expiracao = plano.Periodicidade == Periodicidade.anual ? inicio.AddYears(1) : inicio.AddMonths(1);

            var assinatura = new Assinatura
            {
                UserId = dto.UserId!,
                PlanoId = dto.PlanoId,
                DataInicio = inicio,
                DataExpiracao = expiracao,
                Status = StatusAssinatura.Ativa
            };

            _context.Assinaturas.Add(assinatura);
            await _context.SaveChangesAsync();

            return _mapper.Map<AssinaturaDto>(assinatura);
        }

        public async Task<bool> Cancelar(int id)
        {
            var assinatura = await _context.Assinaturas.FindAsync(id);
            if (assinatura is null) return false;

            assinatura.Status = StatusAssinatura.Cancelada;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AssinaturaDto?> GetAtivaPorUsuario(string userId)
        {
            var assinatura = await _context.Assinaturas
                .Include(a => a.Plano)
                .Where(a => a.UserId == userId && a.Status == StatusAssinatura.Ativa && a.DataExpiracao > DateTime.UtcNow)
                .FirstOrDefaultAsync();

            return assinatura is null ? null : _mapper.Map<AssinaturaDto>(assinatura);
        }

        public async Task<bool> TemAssinaturaAtiva(string  userId)
        {
            return await _context.Assinaturas
                .AnyAsync(a => a.UserId == userId && a.Status == StatusAssinatura.Ativa && a.DataExpiracao > DateTime.UtcNow);
        }
    }
}
