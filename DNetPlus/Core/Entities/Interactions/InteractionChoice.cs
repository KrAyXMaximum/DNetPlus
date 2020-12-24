namespace Discord
{
    public class InteractionChoice
    {
        public InteractionChoice[] Choices { get; internal set; }
        public string Name { get; internal set; }
        public string Value { get; internal set; }
    }
}
