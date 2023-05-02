using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Systems_Project_Spring_2023.Models
{
    public class AccountRoles
    {
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public List<IdentityUserRole<string>> Roles { get; set; }
    }
}
