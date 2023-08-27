using PersonalProfile.Model;
using PersonalProfile.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Interface
{
    public interface IEmployee
    {
        Task<WebResponse> Register(EmployeeRequest employeeRequest);
    }
}
