using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.CreatedInterviews)
                .Include(u => u.ConductedInterviews)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _context.Users
                .Include(u => u.CreatedInterviews)
                .Include(u => u.ConductedInterviews)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> CreateAsync(CreateUserRequest request)
        {
            
            var emailExists = await _context.Users.AnyAsync(u => u.Email == request.Email); 
            if (emailExists)
                throw new ArgumentException($"User with email {request.Email} already exists");

            var user = _mapper.Map<User>(request);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(Guid id, UpdateUserRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            _mapper.Map(request, user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.CreatedInterviews)
                .Include(u => u.ConductedInterviews)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            // Проверяем, есть ли связанные интервью
            if (user.CreatedInterviews.Any() || user.ConductedInterviews.Any())
                throw new InvalidOperationException("Cannot delete user with associated interviews");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _context.Users
                .Include(u => u.CreatedInterviews)
                .Include(u => u.ConductedInterviews)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new KeyNotFoundException($"User with email {email} not found");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetByRoleAsync(string role)
        {
            if (!Enum.TryParse<UserRole>(role, true, out var userRole))
                throw new ArgumentException($"Invalid role: {role}");

            var users = await _context.Users
                .Include(u => u.CreatedInterviews)
                .Include(u => u.ConductedInterviews)
                .Where(u => u.Role == userRole)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}