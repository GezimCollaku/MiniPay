using Microsoft.EntityFrameworkCore;
using MiniPay.API.Domain;

namespace MiniPay.API.Data;

public class MiniPayDbContext : DbContext
{
    public MiniPayDbContext(DbContextOptions<MiniPayDbContext> options)
        : base(options)
    {
    }

    public DbSet<PaymentProvider> PaymentProviders { get; set; }
}