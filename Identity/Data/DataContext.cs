using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
	public class DataContext : IdentityDbContext
    {
		public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
		{
		}
	}
}

