using System;
using System.Collections.Generic;
using System.Text;
using AdvertisingServer.Models;
using AdvertisingServer.UnitTests.DataFixture;
using Microsoft.AspNetCore.Mvc;
//using MyTested.AspNetCore.Mvc;
//using MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers;

namespace AdvertisingServer.UnitTests
{
    public class ApiTestBase<T> where T : ControllerBase
    {
        //protected readonly IControllerBuilder<T> Controller;

        protected ApiTestBase()
        {
            //Controller = MyMvc.Controller<T>();
        }
    }
}
