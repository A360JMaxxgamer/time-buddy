using Fluxor;
using Microsoft.EntityFrameworkCore;
using TimeBuddy.Core.Contexts;

namespace TimeBuddy.Core.Store.ProjectUseCase;

public class Effects
{
    private readonly TimeBuddyContext _timeBuddyContext;

    public Effects(TimeBuddyContext timeBuddyContext)
    {
        _timeBuddyContext = timeBuddyContext;
    }

    [EffectMethod]
    public async Task HandleLoadProjectAction(LoadProjectAction action, IDispatcher dispatcher)
    {
        try
        {
            dispatcher.Dispatch(new SetIsloadingAction(true));
            var project = await _timeBuddyContext.Projects.FirstOrDefaultAsync(p => p.Id == action.ProjectId);
            if (project is not null)
            {
                dispatcher.Dispatch(new SetProjectAction(project));
            }
        }
        catch (Exception e)
        {
            // Todo add loggin
        }
        finally
        {
            dispatcher.Dispatch(new SetIsloadingAction(false));
        }
    }
}