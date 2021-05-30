using System;
using System.Collections.Generic;
using System.Text;

namespace Discord
{
    public class InteractionRow
    {
        public InteractionButton[] Components { get; set; }
    }
    public class InteractionButton
    {
        public ComponentButtonType? Style { get; set; }
        public string Label { get; set; }
        public Emoji Emoji { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public bool Disabled { get; set; }
    }
    public enum ComponentType
    {
        ActionRow = 1,
        Button = 2
    }
    public enum ComponentButtonType
    {
        Primary = 1,
        Secondary = 2,
        Success = 3,
        Danger = 4,
        Link = 5
    }
}
