using System.Linq;
using TemplateAPIModel;
using TemplateAPIServices;

namespace TemplateAPI.Seeds {
  public static class Seed {
    public static void initSeed(TemplateContext context) {

        //if (context.User.FirstOrDefault() == null)
        //{
        //    var salt = Tools.GetSalt();

        //    var user = new User
        //    {
        //        FirstName = "Admin",
        //        LastName = "Admin",
        //        MiddleName = "",
        //        Number = "admin01",
        //        UserName = "admin",
        //        PasswordHash = Tools.GetHash("test1234" + "admin" + salt),
        //        PasswordSalt = salt,
        //    };

        //    context.User.Add(user);
        //    context.SaveChanges();
        //}

    }
  }
}
