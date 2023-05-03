using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyController.Models
{
    internal class DomainCounter
    {
        public const int InitialCountOfDomainElements = 1;
        public int CompanyCount { get; set; }
        public int DepartmentCount { get; set; }
        public int UsernameCount { get; set; }
        public int ProjectCount { get; set; }

        public DomainCounter()
        {
            CompanyCount = InitialCountOfDomainElements;
            DepartmentCount = InitialCountOfDomainElements;
            UsernameCount = InitialCountOfDomainElements;
            ProjectCount = InitialCountOfDomainElements;
        }
    }
}
