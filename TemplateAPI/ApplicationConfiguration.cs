using System;
using Microsoft.Extensions.Configuration;
using TemplateAPIServices.Interfaces;

namespace TemplateAPI {
  public class ApplicationConfiguration : IApplicationConfiguration {
    private readonly IConfiguration _configuration;

    public ApplicationConfiguration(IConfiguration configuration) {
      _configuration = configuration;
    }

    public string JwtSecurityKey => ReadKey(nameof(JwtSecurityKey));

    private string ReadKey(string settingName) {
      string value = _configuration[settingName];
      if (String.IsNullOrEmpty(value))
        throw new ApplicationException($"Setting {settingName} not found.");

      return value;
    }
  }
}