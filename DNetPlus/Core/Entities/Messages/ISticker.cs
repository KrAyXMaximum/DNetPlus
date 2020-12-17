namespace Discord.API
{
    /// <summary>
    /// Discord sticker object.
    /// </summary>
    public interface ISticker
    {
        /// <summary>
        /// Id of the sticker.
        /// </summary>
        ulong Id { get; }
        /// <summary>
        /// Id of the pack this sticker is in.
        /// </summary>
        ulong PackId { get; }
        /// <summary>
        /// Name of the sticker.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Description of the sticker.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Extra meta data and unicode for sticker.
        /// </summary>
        string Tag { get;  }
        /// <summary>
        /// Id of the asset image for certain stickers.
        /// </summary>
        string Asset { get;  }
        /// <summary>
        /// Id of the preview image for certain stickers.
        /// </summary>
        string PreviewAsset { get; }
        /// <summary>
        /// The type of sticker Discord uses.
        /// </summary>
        StickerType Type { get; }
    }
}
