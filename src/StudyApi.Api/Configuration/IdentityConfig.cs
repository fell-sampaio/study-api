﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using StudyApi.Api.Data;

namespace StudyApi.Api.Configuration;

public static class IdentityConfig
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<VaultDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<VaultDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
