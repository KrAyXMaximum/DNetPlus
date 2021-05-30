using System;
using System.Collections.Generic;
using System.Text;

namespace Discord
{
    public class InteractionComponent
    {
        public ComponentType Type { get; set; }
        public ComponentButtonType? Style { get; set; }
        public string Label { get; set; }
        public Emoji Emoji { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public bool Disabled { get; set; }
        public InteractionComponent[] Components { get; set; }
    }
}
