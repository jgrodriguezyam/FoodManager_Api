using System.Collections.Generic;
using System.Linq;
using System.Net;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Users;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Utils;
using FoodManager.Model;
using FoodManager.Model.IHmac;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Users;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserQuery _userQuery;
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;
        private readonly IHmacHelper _hmacHelper;
        private readonly IUserFactory _userFactory;

        public UserService(IUserQuery userQuery, IUserRepository userRepository, IUserValidator userValidator, IHmacHelper hmacHelper, IUserFactory userFactory)
        {
            _userQuery = userQuery;
            _userRepository = userRepository;
            _userValidator = userValidator;
            _hmacHelper = hmacHelper;
            _userFactory = userFactory;
        }

        public FindUsersResponse Find(FindUsersRequest request)
        {
            try
            {
                _userQuery.WithOnlyActivated(true);
                _userQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _userQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _userQuery.WithDealer(request.DealerId);
                _userQuery.WithName(request.Name);
                _userQuery.WithUserName(request.UserName);
                _userQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _userQuery.TotalRecords();
                _userQuery.Paginate(request.StartPage, request.EndPage);
                var users = _userQuery.Execute();

                return new FindUsersResponse
                {
                    Users = TypeAdapter.Adapt<List<UserResponse>>(users),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(UserRequest request)
        {
            try
            {
                var user = TypeAdapter.Adapt<User>(request);
                _userValidator.ValidateAndThrowException(user, "Base");
                user.EncryptPassword();
                _userRepository.Add(user);
                return new CreateResponse(user.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(UserRequest request)
        {
            try
            {
                var currentUser = _userRepository.FindBy(request.Id);
                currentUser.ThrowExceptionIfRecordIsNull();
                var userToCopy = TypeAdapter.Adapt<User>(request);
                TypeAdapter.Adapt(userToCopy, currentUser);
                _userValidator.ValidateAndThrowException(currentUser, "Base");
                _userRepository.Update(currentUser);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.User Get(GetUserRequest request)
        {
            try
            {
                var user = _userRepository.FindBy(request.Id);
                user.ThrowExceptionIfRecordIsNull();
                return _userFactory.Execute(user);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteUserRequest request)
        {
            try
            {
                var user = _userRepository.FindBy(request.Id);
                user.ThrowExceptionIfRecordIsNull();
                _userRepository.Remove(user);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public LoginUserResponse Login(LoginUserRequest request)
        {
            try
            {
                var encryptPassword = Cryptography.Encrypt(request.Password);
                var userToUpdate = _userRepository.FindBy(user => user.UserName == request.UserName && user.Password == encryptPassword).FirstOrDefault();
                userToUpdate.ThrowExceptionIfIsNull(HttpStatusCode.Unauthorized, "Credenciales invalidas");
                userToUpdate.Login();
                _hmacHelper.UpdateHmacOfUser(userToUpdate);

                return new LoginUserResponse
                {
                    UserId = userToUpdate.Id,
                    PublicKey = userToUpdate.PublicKey
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Logout(LogoutUserRequest request)
        {
            try
            {
                var user = _userRepository.FindBy(request.Id);
                user.ThrowExceptionIfRecordIsNull();
                user.Logout();
                _hmacHelper.UpdateHmacOfUser(user);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse ChangePassword(ChangeUserPasswordRequest request)
        {
            try
            {
                var encryptOldPassword = Cryptography.Encrypt(request.OldPassword);
                var userToUpdate = _userRepository.FindBy(user => user.UserName == request.UserName && user.Password == encryptOldPassword).FirstOrDefault();
                userToUpdate.ThrowExceptionIfIsNull(HttpStatusCode.Unauthorized, "Credenciales invalidas");
                userToUpdate.Password = request.NewPassword;
                userToUpdate.EncryptPassword();
                _userRepository.Update(userToUpdate);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            try
            {
                var user = _userRepository.FindBy(request.Id);
                user.ThrowExceptionIfRecordIsNull();
                if (user.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(request.Status);
                user.Status = request.Status;
                _userRepository.Update(user);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}