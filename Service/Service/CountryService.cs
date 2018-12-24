using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using AutoMapper;
using Entity;
using Entity.Model;
using Infrastructure;
using Repository.Interface;
using Service.Interface;

namespace Service.Service
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public ApplicationResult GetCountries()
        {
            return ApplicationResult.Ok(_countryRepository.Gets(x=>!x.IsDeleted).OrderBy(x=>x.Name));
        }
    }
}
