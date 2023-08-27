using Microsoft.AspNetCore.Identity;
using PersonalProfile.DataContext;
using PersonalProfile.Exceptions;
using PersonalProfile.Interface;
using PersonalProfile.Model;
using PersonalProfile.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<WebResponse> Register(EmployeeRequest employeeRequest)
        {
            WebResponse webResponse = new WebResponse();
            var data = new EmployeeResponse();

            var user = new ApplicationUser()
            {
                Name = employeeRequest.Name,
                Email = employeeRequest.Email,
                UserName = employeeRequest.UserName
            };

            if (!employeeRequest.Password.Equals(employeeRequest.ConfirmPassword))
            {
                throw new InvalidException("Password and Confirm Password do not match.");
            }

            var existEmail = await ValidateEmail(employeeRequest.Email);
            if (existEmail != null)
            {
                throw new InvalidException("Email already exist");
            }

            var existUsername = await ValidateUserName(employeeRequest.UserName);
            if (existUsername != null)
            {
                throw new InvalidException("Username already exist");
            }

            var result = await _userManager.CreateAsync(user, employeeRequest.Password);

            if (result.Succeeded)
            {
                data.Name = employeeRequest.Name;
                data.Username = employeeRequest.Email;
                data.Email = employeeRequest.Email;
                webResponse.status = true;
                webResponse.message = "Register Successfully";
                webResponse.data = data;
            }
            else
            {
                throw new BadRequestException(String.Join(", ", result.Errors.Select(x => x.Description)));
            }

            return webResponse;
        }

        public async Task<ApplicationUser> ValidateEmail(string email)
        {
            var user = new ApplicationUser();
            try
            {
                user = await _userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"something went wrong : {ex.Message}");
            }

            return user;
        }

        public async Task<ApplicationUser> ValidateUserName(string username)
        {
            var user = new ApplicationUser();
            try
            {
                user = await _userManager.FindByNameAsync(username);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"something went wrong : {ex.Message}");
            }

            return user;
        }
    }
}
