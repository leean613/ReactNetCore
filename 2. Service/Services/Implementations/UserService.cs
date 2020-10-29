using Abstractions.Interfaces;
using AutoMapper;
using Common.Exceptions;
using Common.Helpers;
using DTOs.React;
using DTOs.React.User;
using DTOs.Share;
using Entities.React;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Implementations.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private IRepository<User> _userRepository => _unitOfWork.GetRepository<User>();

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            CheckDuplicateUser(dto);

            var newUser = await _userRepository.InsertAsync(
                new User
                {
                    UserName = dto.UserName,
                    Password = LoginHelper.EncryptPassword("1"),
                    Role = dto.Role,
                });

            await _unitOfWork.CompleteAsync();

            return await GetUserAsync(newUser.Id);
        }

        private void CheckDuplicateUser(CreateUserDto dto)
        {
            var existUser = _userRepository.GetFirstOrDefault(x => x.UserName.Trim().ToUpper() == dto.UserName.Trim().ToUpper());

            if (existUser != null)
            {
                throw new BusinessException("Tên đăng nhập của bạn trùng với tên đăng nhập của nhân viên khác!");
            }
        }

        public async Task<UserDto> GetUserAsync(Guid userId)
        {
            var userFromDb = await _userRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == userId);

            return _mapper.Map<UserDto>(userFromDb);
        }

        public async Task<IPagedResultDto<UserDto>> FilterUsersAsync(PagedResultRequestDto pagedResultRequest, FilterUsersDto filter)
        {
            return await _userRepository
                .GetAll()
                .SearchByFields(filter.SearchTerm, x => x.UserName)
                .OrderBy(x => x.CreateDate)
                .GetPagedResultAsync(pagedResultRequest.Page, pagedResultRequest.PageSize, x => _mapper.Map<UserDto>(x));
        }

        public async Task<UserDto> UpdateUserAsync(UpdateUserDto dto)
        {
            var userFromDb = _userRepository
                .GetAll()
                .FirstOrDefault(x => x.Id == dto.Id);

            var user = _mapper.Map<User>(dto);

            await _userRepository.UpdateAsync(user);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UserDto>();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
