using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Entity.Model;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.Extensions.Configuration;
using Repository.Interface;

namespace Service.Service
{
    public class InitService : IInitService
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IConfiguration _configuration;
        private readonly ICountryRepository _countryRepository;

        public InitService(IConfigurationRepository configurationRepository, IConfiguration configuration, ICountryRepository countryRepository)
        {
            _configurationRepository = configurationRepository;
            _configuration = configuration;
            _countryRepository = countryRepository;
        }

        public void InitConfigurations()
        {
            var enums = typeof(ConfigurationEnum.Key).GetFields();
            foreach (var item in enums)
            {
                var configAttribute = item.GetCustomAttributes().FirstOrDefault() as ConfigurationGroupAttribute;
                if (configAttribute != null)
                {
                    var value = (ConfigurationEnum.Key)Enum.Parse(typeof(ConfigurationEnum.Key), item.Name);
                    if (!_configurationRepository.Gets(x => x.Group == configAttribute.Group && x.Key == value).Any())
                    {
                        _configurationRepository.Insert(new Configuration()
                        {
                            Group = configAttribute.Group,
                            Key = value,
                            Value = configAttribute.DefaultValue
                        });
                    }
                }
            }
        }

        public void InitCountry()
        {
            var data = _configuration.GetSection("Country").AsEnumerable().Select(x => x.Value);
            var countries = (from item in data
                where !string.IsNullOrEmpty(item)
                select new Country
                {
                    Name = item
                }).ToList();

            var entities = _countryRepository.Gets(x => true);
            var existCountries = entities.Select(x => x.Name);

            var newCountries = (from c in countries
                where !existCountries.Contains(c.Name)
                select new Country
                {
                    Name = c.Name
                }).ToList();

            if (newCountries.Any())
                _countryRepository.Insert(newCountries);
        }
    }
}
