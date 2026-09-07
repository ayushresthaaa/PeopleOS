namespace PeopleOS.Domain.Common
{
    /// <summary>
    /// Represents an entity that can be audited for creation and modification.
    /// </summary>
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTimeOffset CreatedAt { get; protected set; } //offset means it is aware of the timezone

        public DateTimeOffset? UpdatedAt { get; protected set; } //nullable because it may not be updated yet

        protected AuditableEntity()
        {
            CreatedAt = DateTimeOffset.UtcNow; // Set the creation time to the current UTC time
        }

    }
}