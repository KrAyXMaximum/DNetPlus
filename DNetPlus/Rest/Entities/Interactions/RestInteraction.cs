using Discord.Rest;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Model = Discord.API.Interaction_Json;

namespace Discord
{
    public class RestInteraction
    {
        public ulong Id { get; private set; }
        public ulong ApplicationId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public RestInteractionOption[] Options { get; private set; }

        internal static RestInteraction Create(Model model)
        {
            RestInteraction entity = new RestInteraction
            {
                Id = model.Id,
                Description = model.Description,
                ApplicationId = model.ApplicationId,
                Name = model.Name,
                Options = model.Options == null ? new RestInteractionOption[0] : model.Options.Select(x => RestInteractionOption.Create(x)).ToArray(),
            };
            return entity;
        }
    }
}
