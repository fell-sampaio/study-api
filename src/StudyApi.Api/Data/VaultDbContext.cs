using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudyApi.Api.Data;

public class VaultDbContext(DbContextOptions<VaultDbContext> options) : IdentityDbContext(options)
{

}
