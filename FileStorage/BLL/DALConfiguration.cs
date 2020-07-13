using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DAL.DbContexts;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BLL
{
    public class DALConfiguration
    {
        public static void InjectDependencies(IServiceCollection services, string connection)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddScoped<IRepository<FileModel>, FileRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            //services.AddScoped<IRepository<Supplier>, SupplierRepository>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();


        }
    }
}
