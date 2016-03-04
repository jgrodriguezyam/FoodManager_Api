using FoodManager.Services.Interfaces;

namespace FoodManager.Services.Implements
{
    public class UserService : IUserService
    {
        //private readonly IUserRepository _userRepository;
        //private readonly IUserQuery _userQuery;
        //private readonly IHmacHelper _hmacHelper;
        //private readonly IUserValidator _userValidator;

        //public UserService(IUserRepository userRepository, IUserQuery userQuery, IHmacHelper hmacHelper, IUserValidator userValidator)
        //{
        //    _userRepository = userRepository;
        //    _userQuery = userQuery;
        //    _hmacHelper = hmacHelper;
        //    _userValidator = userValidator;
        //}

        //public FindUsersResponse Find(FindUsersRequest request)
        //{
        //    try
        //    {
        //        _userQuery.WithName(request.Name);
        //        _userQuery.WithUserName(request.UserName);
        //        _userQuery.WithOnlyActivatedStatus(true);
        //        _userQuery.WithBranch(request.BranchId);
        //        _userQuery.WithCode(request.Code);
        //        _userQuery.Sort(request.Sort, request.SortBy);
        //        _userQuery.Paginate(request.StartPage, request.EndPage);
        //        int totalRecords;
        //        var users = _userQuery.Execute<User>(out totalRecords);

        //        return new FindUsersResponse
        //        {
        //            Users = users.ConvertModelToDtos(),
        //            TotalRecords = totalRecords
        //        };
        //    }
        //    catch (DataAccessException)
        //    {
        //        throw new SurveyEngineApplicationException();
        //    }
        //}

        //public CreateResponse Create(UserRequest request)
        //{
        //    try
        //    {
        //        var userToCreate = request.ConvertToModel();
        //        _userValidator.ValidateAndThrowException(userToCreate, "Base");
        //        userToCreate.EncryptPassword();
        //        _userRepository.Add(userToCreate);
        //        return new CreateResponse(userToCreate.Id);
        //    }
        //    catch (DataAccessException)
        //    {
        //        throw new SurveyEngineApplicationException();
        //    }
        //}

        //public SuccessResponse Update(UserRequest request)
        //{
        //    try
        //    {
        //        var currentUser = _userRepository.FindBy(request.Id);
        //        currentUser.ThrowExceptionIfIsNull("Usuario no encontrado");
        //        var userToUpdate = request.ConvertToModel();
        //        currentUser.CopyFrom(userToUpdate);
        //        _userValidator.ValidateAndThrowException(currentUser, "Base");
        //        _userRepository.Update(currentUser);
        //        return new SuccessResponse { IsSuccess = true };
        //    }
        //    catch (DataAccessException)
        //    {
        //        throw new SurveyEngineApplicationException();
        //    }
        //}

        //public DTO.User Get(GetUserRequest request)
        //{
        //    try
        //    {
        //        var user = _userRepository.FindBy(request.Id);
        //        user.ThrowExceptionIfIsNull("Usuario no encontrado");
        //        return user.ConverToDto();
        //    }
        //    catch (DataAccessException)
        //    {
        //        throw new SurveyEngineApplicationException();
        //    }
        //}

        //public SuccessResponse Delete(DeleteUserRequest request)
        //{
        //    try
        //    {
        //        var user = _userRepository.FindBy(request.Id);
        //        user.ThrowExceptionIfIsNull("Usuario no encontrado");
        //        _userRepository.Remove(user);
        //        return new SuccessResponse { IsSuccess = true };
        //    }
        //    catch (DataAccessException)
        //    {
        //        throw new SurveyEngineApplicationException();
        //    }
        //}

        //public LoginUserResponse Login(LoginUserRequest request)
        //{
        //    try
        //    {
        //        var encryptPassword = Cryptography.Encrypt(request.Password);
        //        var userToUpdate = _userRepository.FindBy(request.UserName, encryptPassword);
        //        userToUpdate.ThrowExceptionIfIsNull(HttpStatusCode.Unauthorized, "Credenciales invalidas");
        //        userToUpdate.Login();
        //        _hmacHelper.RefreshHmacOfUser(userToUpdate);

        //        return new LoginUserResponse
        //        {
        //            UserId = userToUpdate.Id,
        //            PublicKey = userToUpdate.PublicKey
        //        };
        //    }
        //    catch (DataAccessException)
        //    {
        //        throw new SurveyEngineApplicationException();
        //    }
        //}

        //public SuccessResponse Logout(LogoutUserRequest request)
        //{
        //    try
        //    {
        //        var userToUpdate = _userRepository.FindBy(request.Id);
        //        userToUpdate.ThrowExceptionIfIsNull("Usuario no encontrado");
        //        userToUpdate.Logout();
        //        _hmacHelper.RefreshHmacOfUser(userToUpdate);
        //        return new SuccessResponse { IsSuccess = true };
        //    }
        //    catch (DataAccessException)
        //    {
        //        throw new SurveyEngineApplicationException();
        //    }
        //}

        //public SuccessResponse ChangePassword(ChangeUserPasswordRequest request)
        //{
        //    try
        //    {
        //        var encryptOldPassword = Cryptography.Encrypt(request.OldPassword);
        //        var userToUpdate = _userRepository.FindBy(request.UserName, encryptOldPassword);
        //        userToUpdate.ThrowExceptionIfIsNull(HttpStatusCode.Unauthorized, "Credenciales invalidas");
        //        userToUpdate.Password = request.NewPassword;
        //        userToUpdate.EncryptPassword();
        //        _userRepository.Update(userToUpdate);
        //        return new SuccessResponse { IsSuccess = true };
        //    }
        //    catch (DataAccessException)
        //    {
        //        throw new SurveyEngineApplicationException();
        //    }
        //}
    }
}