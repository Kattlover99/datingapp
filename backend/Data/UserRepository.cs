using AutoMapper;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;
using DatingApp.Interface;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private IMapper mapper;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public UserRepository(DataContext context, IMapper mapper) : this(context)
        {
            this.mapper = mapper;
        }

        public async Task<MemberDto> GetUserByIdAsync(int id)
        {
            AppUser user = await _context.Users.FindAsync(id);
            return new MemberDto(user);
        }

        public async Task<AppUser> GetAppUserByIdAsync(int id)
        {
            AppUser user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<MemberDto> GetUserByUsernameAsync(string username)
        {
            MemberDto dto = new MemberDto();
            AppUser entity = await _context.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username);
            dto = entityToDto(entity, dto);
            return dto;
        }

        private MemberDto entityToDto(AppUser entity, MemberDto dto)
        {
            dto = new MemberDto(entity);
            return dto;

        }

        public async Task<IEnumerable<MemberDto>> GetUsersAsync()
        {
            List<AppUser> list = await _context.Users
                .Take(4)
                .Skip(4)
                .Include(p => p.Photos).ToListAsync();
            return list.Select(user => new MemberDto(user)).ToList();
        }

        public async void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public bool UpdateUser(string username, MemberUpdateDto dto)
        {
            AppUser entity = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (entity == null) return false;

            if (dto.Interests == entity.Interests &&
                dto.Introduction == entity.Introduction &&
                dto.Country == entity.Country &&
                dto.City == entity.City &&
                dto.LookingFor == entity.LookingFor
                ) { return false; }

            entity.Introduction = dto.Introduction;
            entity.LookingFor = dto.LookingFor;
            entity.Interests = dto.Interests;
            entity.City = dto.City;
            entity.Country = dto.Country;

            _context.Update(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<AppUser> GetAppUserByUsernameAsync(string username)
        {
            AppUser entity = await _context.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username);
            return entity;
        }

        public async Task<bool> SaveAppUser(AppUser user)
        {
            if (user == null) return false;
            _context.Update(user);
            _context.SaveChanges();
            return true;
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users.Include(p => p.Photos).AsQueryable();

            var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MaxAge - 1));
            var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MinAge));

            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            query = query.Where(u => u.Gender == userParams.Gender);
            query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.LastActive)
            };

            var dto = query.Select(appUser => new MemberDto(appUser));

            return await PagedList<MemberDto>.CreateAsync(
                dto, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<string> GetUserGender(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username)
                .Select(x => x.Gender).FirstOrDefaultAsync();
        }
    }
}
