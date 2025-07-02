using Applications.DTO.Create;
using Applications.DTO.Response;
using AutoMapper;
using Repositories.Models;
using Repositories.Repositories;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");
            return _mapper.Map<UserDto>(user);
        }

        public async Task AddAsync(UserCreateDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Email))
                throw new ArgumentException("Email is required");
            if (string.IsNullOrWhiteSpace(userDto.Password))
                throw new ArgumentException("Password is required");

            var user = _mapper.Map<User>(userDto);
            user.CreatedAt = DateTime.UtcNow;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _unitOfWork.UserRepository.AddAsync(user);

            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, UserCreateDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Email))
                throw new ArgumentException("Email is required");
            if (string.IsNullOrWhiteSpace(userDto.Password))
                throw new ArgumentException("Password is required");

            var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found");

            // Cập nhật thông tin từ DTO
            _mapper.Map(userDto, existingUser);

            // Mã hóa mật khẩu mới
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            existingUser.UserID = id;

            await _unitOfWork.UserRepository.UpdateAsync(existingUser);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            await _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task<IEnumerable<UserDto>> GetByRoleAsync(string role)
        {
            var users = await _unitOfWork.UserRepository.GetByRoleAsync(role);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
