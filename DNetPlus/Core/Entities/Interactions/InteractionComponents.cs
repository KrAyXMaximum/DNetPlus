using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Discord
{
    public class InteractionRow
    {
        public InteractionButton[] Buttons { get; set; }
        public InteractionDropdown Dropdown { get; set; }
    }
    public class InteractionDropdown
    {
        public string Placeholder { get; set; }
        public int MaxValues { get; set; } = 1;
        public int MinValues { get; set; } = 1;
        public InteractionOption[] Options { get; set; }
    }
    public class InteractionOption
    {
        public InteractionOption(string label, string id, string description, IEmote emoji = null)
        {
            Label = label;
            Value = id;
            Description = description;
            Emoji = emoji;
        }
        public string Label { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public IEmote Emoji { get; set; }
        public bool Default { get; set; }
    }
    public class InteractionButton
    {
        public InteractionButton(ComponentButtonType style, string label, string id, IEmote emoji = null)
        {
            Style = style;
            Label = label;
            Url = style == ComponentButtonType.Link ? id : "";
            Id = style == ComponentButtonType.Link ? "" : id;
            Emoji = emoji;
        }

        public ComponentButtonType Style { get; set; }
        public string Label { get; set; }
        public IEmote Emoji { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool Disabled { get; set; }
        public bool Default { get; set; }
    }
    public enum ComponentType
    {
        ActionRow = 1,
        Button = 2,
        Dropdown = 3
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
