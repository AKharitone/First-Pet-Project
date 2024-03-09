using TemplateAPIServices.PublicModels;
using System.Threading.Tasks;

namespace TemplateAPIServices.Interfaces {
  public interface ILoginService {
    Task<string> CreateJwtToken(LoginModel loginModel);
  }
}
