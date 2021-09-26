using Microsoft.EntityFrameworkCore;
using RealEstateApi.Controllers;
using RealEstateApi.Models;
using System;
using Xunit;

namespace UnitTest
{
    public class RealEstateControllerTest
    {
        private readonly RealEstateController _sut;
        

        public RealEstateControllerTest()
        {

        }

        public DbContextOptions<AppDbContext> DummyOptions { get; } = new DbContextOptionsBuilder<AppDbContext>().Options;

    }
}
