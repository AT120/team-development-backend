using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Services.Interfaces;

namespace TeamDevelopmentBackend.Services;

public class GlobalCounterService : IGlobalCounter<ulong>
{
    private readonly DefaultDBContext _dbcontext;
    
    public GlobalCounterService(DefaultDBContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public ulong Next()
    {
        var counter = _dbcontext.Counter.FirstOrDefault();
        if (counter is null)
        {
            counter = new CounterStorageDbModel { Id = 0, Last = 0 };
            _dbcontext.Counter.Add(counter);
        }
        
        counter.Last++;
        _dbcontext.SaveChanges();
        return counter.Last;
    }

}