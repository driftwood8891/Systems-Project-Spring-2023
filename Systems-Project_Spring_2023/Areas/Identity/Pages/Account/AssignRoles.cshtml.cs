using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Systems_Project_Spring_2023.Areas
{
    [Authorize(Roles="Admin")]
    public class AssignRolesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SelectList Accounts { get; set; }
        public SelectList Roles { get; set; }
        public List<(string accountName, string roleName)> AccountRoles { get; set; }



        [BindProperty]
        public string AccountId { get; set; }

        [BindProperty]
        public string RoleId { get; set; }

        public AssignRolesModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            var accounts = await _userManager.Users.ToListAsync();
            Accounts = new SelectList(accounts, "Id", "UserName");

            var roles = await _roleManager.Roles.ToListAsync();
            Roles = new SelectList(roles, "Id", "Name");


            AccountRoles = new List<(string accountName, string roleName)>();
            foreach (var account in accounts)
            {
                var accountRoles = await _userManager.GetRolesAsync(account);
                foreach (var roleName in accountRoles)
                {
                    AccountRoles.Add((account.UserName, roleName));
                }
            }
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(AccountId);
            var oldRole = await _userManager.GetRolesAsync(user);
            if (oldRole.Any())
            {
                await _userManager.RemoveFromRoleAsync(user, oldRole.First());
            }
            var role = await _roleManager.FindByIdAsync(RoleId);

            if (user == null || role == null)
            {
                return NotFound();
            }

            await _userManager.AddToRoleAsync(user, role.Name);

            TempData["Message"] = $"Role '{role.Name}' assigned to account '{user.UserName}'";
            return RedirectToPage();
        }

    }
}

