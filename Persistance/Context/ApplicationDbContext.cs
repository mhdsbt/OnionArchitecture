﻿using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Persistence.Context
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
		{
		}

		public DbSet<Product> Products { get; set; }

        public DbSet<AutoCallRequest> AutoCallRequest { get; set; }

        public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}
	}
}