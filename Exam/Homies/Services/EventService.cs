namespace Homies.Services;

using System.Collections.Generic;
using System.Security.Claims;
using Homies.Contracts;
using Homies.Data;
using Homies.Data.Entities;
using Homies.Models;
using Microsoft.EntityFrameworkCore;

public class EventService : IEventService
{
    private readonly HomiesDbContext context;
    private readonly IHttpContextAccessor httpContextAccessor;

    public EventService(HomiesDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        this.context = context;
        this.httpContextAccessor = httpContextAccessor;
    }
    public async Task AddEventAsync(AddEventViewModel model, string userid)
    {
        userid = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var eventToAdd = new Event() 
        {
            Name = model.Name,
            Description = model.Description,
            CreatedOn = DateTime.UtcNow,
            Start = model.Start,
            End = model.End,
            TypeId = model.TypeId,
            OrganiserId = userid
        };

        await context.AddAsync(eventToAdd);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync()
    {
        var model = await context.Events
            .AsNoTracking()
            .Select(e => new AllEventsViewModel() 
            {
                Id = e.Id,
                Name = e.Name,
                Organiser = e.Organiser.UserName,
                Start = e.Start,
                Type = e.Type.Name
            })
            .ToListAsync();

        return model;
    }

    public async Task<IEnumerable<AllTypesViesModel>> GetAllTypessAsync()
    {
        var model = await context.Types
            .AsNoTracking()
            .Select(c => new AllTypesViesModel() 
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();

        return model;
    }

    public async Task<IEnumerable<AllEventsViewModel>> GetMyEventsAsync(string userId)
    {
        var userEvents = await context.EventsParticipants
            .AsNoTracking()
            .Include(ep => ep.Event.Type)
            .Include(ep => ep.Event.Organiser)
            .Select(b =>b.Event)
            .Where(x => x.EventsParticipants.Any(x => x.HelperId == userId))
            .ToListAsync();

        var models = userEvents
            .Select(x => new AllEventsViewModel()
            {
                Name = x.Name,
                Start = x.Start,
                Type = x.Type.Name,
                Organiser = x.Organiser.UserName,
                Id = x.Id
            });

        return models;
    }

    public async Task<AddEventViewModel> GetSingleEvent(int id)
    {
        var entity = await context.Events.FirstOrDefaultAsync(x => x.Id == id);

        if (entity != null)
        {
            AddEventViewModel model = new()
            {
                Name = entity.Name,
                Description = entity.Description,
                OrganiserId = entity.OrganiserId,
                CreatedOn = entity.CreatedOn,
                Start = entity.Start,
                End = entity.End,
                Types = await GetAllTypessAsync()

            };
            return model;
        }
        return null;
    }

    public async Task JoinAnEvent(int id, string userid)
    {
        var eventToJoin = await context.Events
            .FirstOrDefaultAsync(x => x.Id == id);

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id ==  userid);

        if (user != null && eventToJoin != null ) 
        {
            var eventUser = new EventParticipant()
            {
                Event = eventToJoin,
                Helper = user
            };

            await context.EventsParticipants.AddAsync(eventUser);
            await context.SaveChangesAsync();
        }

    }

    public async Task LeaveAnEvent(int id, string userId)
    {
        var eventToLeave = await context.Events
            .FirstOrDefaultAsync(x => x.Id == id);

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user != null && eventToLeave != null)
        {
            var eventUser = await context.EventsParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == eventToLeave.Id && ep.HelperId == user.Id);

            if (eventUser != null)
            {
                context.EventsParticipants.Remove(eventUser);
                await context.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateContactAsync(int Id, AddEventViewModel model)
    {
        var entity = await context.Events
            .Include(e => e.OrganiserId)
            .Include(e => e.Organiser)
            .FirstOrDefaultAsync(x => x.Id == Id);

        if (entity != null)
        {
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Start =  model.Start;
            entity.End = model.End;
            entity.CreatedOn = model.CreatedOn;
            entity.TypeId = model.TypeId;
        }

        await context.SaveChangesAsync();
    }

}
