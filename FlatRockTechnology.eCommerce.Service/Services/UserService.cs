using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Contracts.Patterns;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Core.Exceptions;
using FlatRockTechnology.eCommerce.Core.Models.User;

namespace FlatRockTechnology.eCommerce.Service.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository userRepository;
		private readonly IUnitOfWork unitOfWork;

		public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
		{
			this.userRepository = userRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<UserModel> GetByIdAsync(Guid id)
		{
			var user = await userRepository.GetByIdAsync(id);

			if (user == null)
			{
				throw new ItemNotFoundException();
			}

			return new UserModel
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				IsActive = user.IsActive
			};
		}

		public async Task<IEnumerable<UserModel>> GetAllAsync()
		{
			var users = await userRepository.GetAllAsync();

			if (!users.Any())
			{
				throw new EmptyCollectionException();
			}

			return users.Select(u => new UserModel
			{
				Id = u.Id,
				UserName = u.UserName,
				Email = u.Email,
				IsActive = u.IsActive
			})
			.ToList();
		}

		public async Task<UserModel> CreateAsync(UserModel userModel)
		{
			var user = new UserEntity
			{
				UserName = userModel.UserName,
				Email = userModel.Email
			};

			await userRepository.CreateAsync(user);
			await unitOfWork.CompleteAsync();

			return userModel;
		}

		public async Task<UserModel> EditAsync(UserModel userModel)
		{
			var user = new UserEntity
			{
				Id = userModel.Id,
				UserName = userModel.UserName,
				Email = userModel.Email,
				FirstName = userModel.FirstName,
				LastName = userModel.LastName
			};

			userRepository.Edit(user);
			await unitOfWork.CompleteAsync();

			return userModel;
		}

		public async Task DeleteByIdAsync(Guid id)
		{
			var user = await userRepository.GetByIdAsync(id);

			if (user == null || !user.IsActive)
			{
				throw new ItemNotFoundException();
			}

			user.IsActive = false;
			await unitOfWork.CompleteAsync();
		}
	}
}