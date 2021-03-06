﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAuction.BusinessLayer.DataTransferObjects;
using WebAuction.BusinessLayer.DataTransferObjects.Common;
using WebAuction.BusinessLayer.DataTransferObjects.Filters;
using WebAuction.BusinessLayer.Facades;
using WebAuction.BusinessLayer.Services.UserLogins;
using WebAuction.BusinessLayer.Services.Users;
using WebAuction.BusinessLayer.Tests.FacadeTests.Common;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using Xunit;

namespace WebAuction.BusinessLayer.Tests.FacadeTests
{
    public class UserFacadeTests
    {
        [Fact]
        public async Task GetUserAccordingToEmailAsync_ExistingUser_ReturnCorrectUser()
        {
            const string userEmail = "test.user@email.com";

            var expectedUser = new UserDto
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = userEmail,
                Phone = "+420777123456"
            };

            var expectedQueryResult = new QueryResultDto<UserDto, UserFilterDto>
            {
                Items = new List<UserDto> {expectedUser}
            };

            var userFacade = CreateUserFacade(expectedQueryResult);

            var actualUser = await userFacade.GetUserAccordingToEmailAsync("sad");

            Assert.Equal(expectedUser, actualUser);
        }

        [Fact]
        public async Task GetAllUsersAsync_NoUsers_ReturnEmptyResult()
        {
            var expectedQueryResult = new QueryResultDto<UserDto, UserFilterDto>
            {
                Items = new List<UserDto>()
            };

            var userFacade = CreateUserFacade(expectedQueryResult);

            var actualUsers = await userFacade.GetAllUsersAsync();

            Assert.Equal(expectedQueryResult.Items, actualUsers);
        }

        [Fact]
        public async Task GetAllUsersAsync_TwoExistingUsers_ReturnCorrectResult()
        {
            var expectedQueryResult = new QueryResultDto<UserDto, UserFilterDto>
            {
                Items = new List<UserDto>
                {
                    new UserDto {Id = Guid.NewGuid()},
                    new UserDto {Id = Guid.NewGuid()}
                }
            };

            var userFacade = CreateUserFacade(expectedQueryResult);

            var actualResult = await userFacade.GetAllUsersAsync();

            Assert.Equal(expectedQueryResult.Items, actualResult);
        }

        private static UserFacade CreateUserFacade(QueryResultDto<UserDto, UserFilterDto> result)
        {
            var mockFacadeManager = new FacadeMockManager();
            var uowMock = FacadeMockManager.GetUowMock();
            var mapperMock = FacadeMockManager.GetMapper();
            var repositoryMock = mockFacadeManager.GetRepositoryMock<User>();
            var repositoryLoginMock = mockFacadeManager.GetRepositoryMock<UserLogin>();
            var userQueryObjectMock = mockFacadeManager.GetQueryObjectMock<UserDto, User, UserFilterDto>(result);
            var userLoginQueryObjectMock =
                mockFacadeManager.GetQueryObjectMock<UserLoginDto, UserLogin, UserLoginFilterDto>(null);
            var userService = new UserService(mapperMock, repositoryMock.Object, userQueryObjectMock.Object);
            var userLoginService = new UserLoginService(mapperMock, repositoryLoginMock.Object,
                userLoginQueryObjectMock.Object);

            var userFacade = new UserFacade(uowMock.Object, userService, userLoginService);

            return userFacade;
        }
    }
}
