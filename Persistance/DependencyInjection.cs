using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.MessageConsumer;

public static class DependencyInjection
{
	public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionstring = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<ApplicationDbContext>(options =>
				options.UseMySql(connectionstring,
						ServerVersion.AutoDetect(connectionstring),
						b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
		
		services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        services.AddScoped<IMessagePublisher, CapMessagePublisher>();
		services.AddScoped<IMessageConsumer, CapMessageConsumer>();
    }
}