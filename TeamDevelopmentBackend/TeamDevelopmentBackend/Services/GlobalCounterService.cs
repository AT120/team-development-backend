using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Services.Interfaces;

namespace TeamDevelopmentBackend.Services;

public class GlobalCounterService : IGlobalCounter<ulong>
{
    private ulong counter;
    private readonly IServiceScopeFactory _scopeFactory;
    
    public GlobalCounterService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        
        using(var scope = _scopeFactory.CreateScope())
        {
            var _dbcontext = scope.ServiceProvider.GetRequiredService<DefaultDBContext>();
            counter = _dbcontext.Counter.FirstOrDefault()?.Last ?? 0UL;
        }
    }

    ~GlobalCounterService()
    {
        using(var scope = _scopeFactory.CreateScope())
        {
            var _dbcontext = scope.ServiceProvider.GetRequiredService<DefaultDBContext>();
            var storage = _dbcontext.Counter.FirstOrDefault();
            if (storage is null)
                _dbcontext.Counter.Add(new CounterStorageDbModel { Last = counter });
            else
                storage.Last = counter;

            _dbcontext.SaveChanges();
        }
    }

    public ulong Next() => ++counter;

}