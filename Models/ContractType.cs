using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeService.Models
{
    public class ContractType
    {
        public ContractType()
        {
            Contract = new HashSet<Contract>();
        }
        public int ContractTypeId { get; set; }
        public string ContractTypeTlEn { get; set; }
        public string ContractTypeTlAr{ get; set;}
        public virtual ICollection<Contract> Contract { get; set; }

    }
}
