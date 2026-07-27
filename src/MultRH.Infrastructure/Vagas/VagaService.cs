using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultRH.Application.Vagas;
using MultRH.Application.Vagas.Dtos;
using MultRH.Domain.Entities;
using MultRH.Infrastructure.Data;

namespace MultRH.Infrastructure.Vagas
{
    public class VagaService : IVagaService
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public VagaService(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<VagaDto>> GetAll()
        {
            var vagas = await _context.Vagas.ToListAsync();
            return _mapper.Map<List<VagaDto>>(vagas);
        }

        public async Task<VagaDto?> GetById(int id)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            return vaga is null ? null : _mapper.Map<VagaDto>(vaga);
        }

        public async Task<VagaDto> Create(CreateVagaDto dto)
        {
            var vaga = _mapper.Map<Vaga>(dto);
            _context.Vagas.Add(vaga);
            await _context.SaveChangesAsync();
            return _mapper.Map<VagaDto>(vaga);
        }

        public async Task<bool> Update(int id, UpdateVagaDto dto)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga is null) return false;

            _mapper.Map(dto, vaga);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga is null) return false;

            _context.Vagas.Remove(vaga);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
