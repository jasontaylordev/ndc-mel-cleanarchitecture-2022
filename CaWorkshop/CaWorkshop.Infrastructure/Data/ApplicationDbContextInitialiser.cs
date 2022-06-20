using CaWorkshop.Domain.Entities;
using CaWorkshop.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CaWorkshop.Infrastructure.Data;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationDbContextInitialiser(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public void Initialise()
    {
        // A good strategy for early development.
        //_context.Database.EnsureDeleted();
        //_context.Database.EnsureCreated();

        if (_context.Database.IsSqlServer())
        {
            _context.Database.Migrate();
        }
        else
        {
            _context.Database.EnsureCreated();
        }
    }

    public async Task Seed()
    {
        await SeedData();
        await SeedUsers();
    }

    private async Task SeedData()
    {
        if (_context.TodoLists.Any())
        {
            return;
        }

        var list = new TodoList
        {
            Title = "Todo List",
            Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
        };

        _context.TodoLists.Add(list);
        await _context.SaveChangesAsync();
    }

    private async Task SeedUsers()
    {
        var user = new ApplicationUser
        {
            UserName = "user@localhost",
            Email = "user@localhost",
            EmailConfirmed = true
        };

        if (_userManager.Users.All(u => u.UserName != user.UserName))
        {
            await _userManager.CreateAsync(user, "Password123!");
        }
    }
}
