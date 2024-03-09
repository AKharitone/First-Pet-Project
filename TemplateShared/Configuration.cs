namespace TemplateShared {
  public static class Configuration {
    public static readonly string UserValidationPattern = "^[-0-9A-Za-z_]{5,}$";
    public static readonly string PasswordValidationPattern = "^.{8,}$";
    public static readonly string ZipValidationPattern = "^[0-9]{5}$";
    public static readonly string BuildingValidationPattern = "^[0-9]{1,}$|^[0-9]{1,}/[0-9]{1,}$";
  }
}
