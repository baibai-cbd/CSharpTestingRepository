using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ThreadSafeRepository.DapperModel;

namespace ThreadSafeRepository.DapperRepository
{
    public class EmployeeRepository
    {
        protected readonly string _connectionString;

        public EmployeeRepository(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public Employee GetEmployeeWithAllData(int id)
        {
            var employeeDictionary = new Dictionary<int, Employee>();
            var projectDictionary = new Dictionary<int, Project>();

            using (var conn = new SqlConnection(_connectionString))
            {
                var results = conn.Query<Employee, Project, ProjectItem, Employee>(GetQueryString(id),
                    (employee, project, item) =>
                    {
                        if (!employeeDictionary.TryGetValue(employee.EmployeeID, out Employee employeeData))
                        {
                            employeeData = employee;
                            employeeDictionary.Add(employeeData.EmployeeID, employeeData);
                        }
                        if (project != null)
                        {
                            if (!projectDictionary.TryGetValue(project.ProjectID, out Project projectData))
                            {
                                projectData = project;
                                projectDictionary.Add(projectData.ProjectID, projectData);
                                employeeData.Projects.Add(projectData);
                            }
                            if (item != null)
                                projectData.Items.Add(item);
                        }
                        return employeeData;
                    },
                    splitOn: "ProjectID,ProjectItemID");
            }
            if (employeeDictionary.Count > 0)
                return employeeDictionary[id];

            return null;
        }

        private string GetQueryString(int id)
        {
            return $@"Select e.EmployeeID, e.Name, e.Guid,
                        p.ProjectID, p.Name, p.AssignedEmployeeID, p.ProjectCost,
	                    pi.ProjectItemID, pi.ItemName, pi.AssociatedProjectID ,pi.ItemDatetime
                        From dbo.Employee e
                        Join dbo.Project p on p.AssignedEmployeeID = e.EmployeeID
                        Join dbo.ProjectItem pi on pi.AssociatedProjectID = p.ProjectID
                        Where e.EmployeeID = {id}";
        }
    }
}
