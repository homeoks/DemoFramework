using System;
using System.Linq;
using Entity;
using Entity.Base;
using Microsoft.EntityFrameworkCore;
using MmFramework.Controllers;
using Repository.Interface;
using Service.Interface;
using Service.Service;
using Xunit;

namespace XUnitTest
{
    public class UnitTest1
    {
        public ICountryService _CountryService;
        public IUserService _UserService;
        [Fact]
        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder(new DbContextOptions<ApplicationDbContext>()).UseInMemoryDatabase("dbTest");
            var context=new ApplicationDbContext(builder.Options);
            InitTempData(context);
            context.SaveChanges();

            var repo = _CountryService;
            
            var messs = context.Messages.Where(x => true).ToList();
            Assert.True(messs.Any());
        }

        private void InitTempData(ApplicationDbContext context)
        {
            for (int i = 1; i < 10; ++i)
            {
                var mess = new Message()
                {
                    Id = i,
                    DateReceive = DateTimeOffset.Now,
                    ToUser = "To User" + i,
                    FromUser = "From User" + i,
                    Content = "Content" + i,
                };
                context.Messages.Add(mess);
            }

        }
    }
}
