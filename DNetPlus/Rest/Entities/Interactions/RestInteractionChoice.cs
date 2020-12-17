using Model = Discord.API.InteractionChoice_Json;

namespace Discord
{
    public class RestInteractionChoice
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        internal static RestInteractionChoice Create(Model model)
        {
            return new RestInteractionChoice
            {
                Name = model.Name,
                Value = model.Value
            };
        }
    }
}
