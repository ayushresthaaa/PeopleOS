namespace PeopleOS.Domain.Common
{   
    public abstract class BaseEntity
    {
        /// <summary>
        /// The unique identifier for the entity.
        /// </summary>
        public Guid Id { get; protected set; } 

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}