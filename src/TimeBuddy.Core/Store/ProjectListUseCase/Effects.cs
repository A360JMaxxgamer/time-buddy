using Fluxor;
using Microsoft.EntityFrameworkCore;
using TimeBuddy.Core.Contexts;

namespace TimeBuddy.Core.Store.ProjectListUseCase;

public class Effects
{
    private readonly TimeBuddyContext _timeBuddyContext;

    public Effects(TimeBuddyContext timeBuddyContext)
    {
        _timeBuddyContext = timeBuddyContext;
    }

    [EffectMethod]
    public async Task HandleFetchProjectsAsync(FetchProjectsAction _, IDispatcher dispatcher)
    {
        try
        {
            dispatcher.Dispatch(new SetIsLoadingAction(true));
            var projects = await _timeBuddyContext.Projects
                .AsNoTracking()
                .ToListAsync();
            dispatcher.Dispatch(new SetProjectsAction(projects));
        }
        catch (Exception e)
        {
            // Todo add logging
        }
        finally
        {
            dispatcher.Dispatch(new SetIsLoadingAction(false));
        }
    }
}