using System;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TemplateAPIServices.PublicModels;
using TemplateShared;

namespace TemplateAPIServices {
  public static class Tools {
    public static string GetHash(string text) {
      // SHA512 is disposable by inheritance.  
      using (var sha256 = SHA256.Create()) {
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
      }
    }

    public static string GetSalt() {
      byte[] bytes = new byte[128 / 8];
      using (var keyGenerator = RandomNumberGenerator.Create()) {
        keyGenerator.GetBytes(bytes);
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
      }
    }

    public static string CreatePassword(int length) // TODO
    {
      const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_";
      StringBuilder res = new StringBuilder();
      Random rnd = new Random();

      while (0 < length--) {
        res.Append(valid[rnd.Next(valid.Length)]);
      }

      return res.ToString();
    }
  }

  public static class IQueryableExtension {
    public static IQueryable<T> OrderByRequest<T>(this IQueryable<T> query, DataTablesRequest model) {
      if (model.Sort == null)
        return query;

      if (string.IsNullOrEmpty(model.Sort.Prop))
        return query;

      var prop = typeof(T).GetProperty(model.Sort.Prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

      if (prop == null)
        return query;

      if (string.IsNullOrEmpty(model.Sort.Order))
        return query;

      if (model.Sort.Order == "ascending")
        return query.OrderBy(e => prop.GetValue(e, null));
      else return query.OrderByDescending(e => prop.GetValue(e, null));
    }

    public static IQueryable<T> FilterByRequest<T>(this IQueryable<T> query, DataTablesRequest model) {
      if (model.Filters == null || model.Filters.Count <= 0)
        return query;

      foreach (var filter in model.Filters) {
        if (string.IsNullOrEmpty(filter.Value))
          continue;

        var prop = typeof(T).GetProperty(filter.Prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (prop == null)
          continue;



        //TODO: Add other types, possible subtypes
        switch (filter.Type) {
          case FilterType.String:
            query = query.Where(e =>
            !string.IsNullOrEmpty(((string)(prop.GetValue(e, null)))) ?
                ((string)(prop.GetValue(e, null))).ToLower().Contains(filter.Value.ToLower())
                : false
            );
            break;
          case FilterType.Boolean:
            query = query.Where(e => prop.GetValue(e, null).Equals(Convert.ChangeType(filter.Value, prop.GetValue(e, null).GetType())));
            break;
          default:
            break;
        }
      }
      return query;
    }
  }

    public static class StringExtensions
    {
        public static bool IsValidUserName(this string userName)
        {
            return Regex.IsMatch(userName, Configuration.UserValidationPattern);
        }

        public static bool IsValidPassword(this string password)
        {
            return Regex.IsMatch(password, Configuration.PasswordValidationPattern);
        }

        public static bool IsValidEmail(this string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidBuilding(this string buildingNumber)
        {
            return Regex.IsMatch(buildingNumber, Configuration.BuildingValidationPattern);
        }
        public static bool IsValidZip(this string zip)
        {
            return Regex.IsMatch(zip, Configuration.ZipValidationPattern);
        }
    }
}