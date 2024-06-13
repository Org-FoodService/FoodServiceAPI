using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodServiceAPI.Core.Service;
using FoodServiceAPI.Core.Wrapper.Interface;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodServiceApi.Tests.TestsBase
{
    public class AuthTestsBase
    {
        protected readonly SignUpDto SignUpDto;
        protected readonly SignInDto SignInDto;
        protected readonly ClientUser ExistingClientUser;
        protected readonly ClientUser ClientUser;

        public AuthTestsBase()
        {
            SignUpDto = new SignUpDto
            {
                Username = "newuser",
                Password = "Password123!",
                ConfirmPassword = "Password123!",
                PhoneNumber = "11911112222",
                Email = "newuser@example.com",
                CpfCnpj = "12345678901"
            };
            SignInDto = new SignInDto
            {
                Username = "user1",
                Password = "Password123!"
            };

            ClientUser = new ClientUser
            {
                UserName = "user1",
                Email = "user1@example.com",
                CpfCnpj = "12345678901"
            };
            ExistingClientUser = new ClientUser
            {
                UserName = "existinguser",
                Email = "existinguser@example.com",
                CpfCnpj = "12345678901"
            };
        }
    }
}

