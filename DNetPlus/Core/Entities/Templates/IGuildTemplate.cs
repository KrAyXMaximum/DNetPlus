using System;

namespace Discord
{
    /// <summary>
    /// A guild template option with optional snapshot data.
    /// </summary>
    public interface IGuildTemplate
    {
        /// <summary> The code/id of the template. </summary>
        public string Code { get; }
        /// <summary> Name of the template. </summary>
        public string Name { get; }
        /// <summary> Description of the template. </summary>
        public string Description { get; }
        /// <summary> How many times the template has been used by others. </summary>
        public int UsageCount { get; }
        /// <summary> Id of the owner account that created the template. </summary>
        public ulong CreatorId { get; }
        /// <summary> Basic info of the template owner that created it. </summary>
        public IUser Creator { get; }
        /// <summary> Date of when the template was created. </summary>
        public DateTimeOffset CreatedAt { get; }
        /// <summary> Date of when the template was last updated. </summary>
        public DateTimeOffset UpdatedAt { get; }
        /// <summary> Id of the guild that matches the template/snapshot. </summary>
        public ulong SourceGuildId { get; }

        /// <summary> Optional data for a snapshot of the guild including properties, roles and channels. </summary>
        public Optional<Rest.RestGuildSnapshot> Snapshot { get; }
    }
}
