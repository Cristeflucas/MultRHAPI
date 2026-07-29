using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultRH.Application.Planos;
using MultRH.Application.Planos.Dtos;
using MultRH.Domain.Entities;
using MultRH.Infrastructure.Data;

namespace MultRH.Infrastructure.Planos
{
    public class PlanoService : IPlanoService
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public PlanoService(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PlanoDto>> GetAll()
        {
            var planos = await _context.Planos.ToListAsync();
            return _mapper.Map<List<PlanoDto>>(planos);
        }

        public async Task<PlanoDto?> GetById(int id)
        {
            var plano = await _context.Planos.FindAsync(id);
            return plano == null ? null : _mapper.Map<PlanoDto>(plano);
        }

        public async Task<PlanoDto> Create(CreatePlanoDto dto)
        {
            var plano = _mapper.Map<Plano>(dto);
            plano.Ativo = true;
            _context.Planos.Add(plano);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlanoDto>(plano);
        }

        public async Task<bool> Update(int id, UpdatePlanoDto dto)
        {
            var plano = await _context.Planos.FindAsync(id);
            if (plano is null) return false;

            if (dto.Nome is not null) plano.Nome = dto.Nome;
            if (dto.Descricao is not null) plano.Descricao = dto.Descricao;
            if (dto.Valor.HasValue) plano.Valor = dto.Valor.Value;
            if (dto.Periodicidade.HasValue) plano.Periodicidade = dto.Periodicidade.Value;
            if (dto.Ativo.HasValue) plano.Ativo = dto.Ativo.Value;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
