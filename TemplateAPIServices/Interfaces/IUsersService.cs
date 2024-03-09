using TemplateAPIServices.PublicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateAPIServices.Interfaces {
  public interface IUsersService {
    Task<List<UserModel>> FindAll();
    Task<UserModel> FindUserByNumber(string number);
    Task<UserModel> DeleteUser(int id);
    Task<UserModel> CreateNewUser(UserModel user);
    Task<UserModel> UpdateUser(UserModel user);
    Task<UserModel> UpdateUserPass(int id, string pass);
  }
}