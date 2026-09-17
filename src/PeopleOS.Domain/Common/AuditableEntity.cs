namespace PeopleOS.Domain.Common
{
    /// <summary>
    /// Represents an entity that can be audited for creation and modification.
    /// </summary>
    public abstract class AuditableEntity : BaseEntity
    {         public DateTimeOffset CreatedAt { get; protected set; } //offset means it is aware of the timezone

        public DateTimeOffset? UpdatedAt { get; protected set; } //nullable because it may not be updated yet

        protected AuditableEntity()
        {
            CreatedAt = DateTimeOffset.UtcNow; // Set the creation time to the current UTC time
        } //utcnow means it is not affected by the timezone of the server, it is always the same time regardless of where the server is located. It is based on the Coordinated Universal Time (UTC) standard, which is the primary time standard by which the world regulates clocks and time. It does not observe daylight saving time, so it remains constant throughout the year. This makes it a reliable reference for timekeeping across different regions and time zones.

    }
}