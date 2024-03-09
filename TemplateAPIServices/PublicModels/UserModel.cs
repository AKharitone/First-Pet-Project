using TemplateAPIModel;
using TemplateShared;

namespace TemplateAPIServices.PublicModels {
  public class UserModel {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Number { get; set; }
    public UserModelStatus ModelStatus { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public AddressModel AddressModel { get; set; }
    public HealthCardModel HealthCardModel { get; set; }
  }
}