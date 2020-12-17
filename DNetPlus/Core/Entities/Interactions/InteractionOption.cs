namespace Discord
{
    public class InteractionOption
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public InteractionOptionType Type { get; set; }
        public bool Required { get; set; }
        public bool Default { get; set; }
        public InteractionOption[] Options { get; set; }

        public InteractionChoice[] Choices { get; set; }
    }
}
