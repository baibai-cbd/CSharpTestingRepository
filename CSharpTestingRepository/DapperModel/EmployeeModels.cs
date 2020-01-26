using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeRepository.DapperModel
{
    class EmployeeModels
    {
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }

        public ICollection<Project> Projects { get; set; }

        public Employee()
        {
            Projects = new List<Project>();
        }
    }

    public class Project
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public int AssignedEmployeeID { get; set; }
        public int ProjectCost { get; set; }

        public ICollection<ProjectItem> Items { get; set; }

        public Project()
        {
            Items = new List<ProjectItem>();
        }
    }

    public class ProjectItem
    {
        public int ProjectItemID { get; set; }
        public string ItemName { get; set; }
        public int AssociatedProjectID { get; set; }
        public DateTime ItemDatetime { get; set; }
    }
}
