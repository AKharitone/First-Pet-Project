using TemplateAPIModel;
using TemplateAPIServices.PublicModels;
using TemplateShared;

namespace TemplateAPIServices.Extensions
{
    public static class UserExtensions
    {
        public static UserModel MapDbModel(User dbUser)
        {
            if (dbUser == null)
                return null;

            return new UserModel
            {
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                MiddleName = dbUser.MiddleName,
                LastName = dbUser.LastName,
                Number = dbUser.Number,
                UserName = dbUser.UserName,
                AddressModel = AddressExtensions.MapDbModel(dbUser.Address)
            };
        }

        public static User MapViewModel(UserModel userModel)
        {
            return new User
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                MiddleName = userModel.MiddleName,
                LastName = userModel.LastName,
                Number = userModel.Number,
                UserName = userModel.UserName
            };
        }

        public static void ValidateUser(UserModel user)
        {
            if (string.IsNullOrEmpty(user.UserName))
                user.ModelStatus |= UserModelStatus.EmptyUsername;
            else if (!user.UserName.IsValidUserName())
                user.ModelStatus |= UserModelStatus.InvalidUsername;

            if (string.IsNullOrEmpty(user.Password))
                user.ModelStatus |= UserModelStatus.EmptyPassword;
            else if (!user.Password.IsValidPassword())
                user.ModelStatus |= UserModelStatus.InvalidPassword;
        }
    }
}