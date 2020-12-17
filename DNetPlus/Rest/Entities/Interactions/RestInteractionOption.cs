using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Model = Discord.API.InteractionOption_Json;

namespace Discord
{
    public class RestInteractionOption
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public InteractionOptionType Type { get; private set; }
        public bool Required { get; private set; }
        public bool Default { get; private set; }
        public RestInteractionOption[] Options { get; private set; }

        public RestInteractionChoice[] Choices { get; private set; }

        internal static RestInteractionOption Create(Model model)
        {
            return new RestInteractionOption
            {
                Name = model.Name,
                Description = model.Description,
                Type = model.Type,
                Required = model.Required,
                Default = model.Default,
                Choices = model.Choices == null ? new RestInteractionChoice[0] : model.Choices.Select(x => RestInteractionChoice.Create(x)).ToArray(),
                Options = model.Options == null ? new RestInteractionOption[0] : model.Options.Select(x => RestInteractionOption.Create(x)).ToArray(),
            };
        }
    }
}
