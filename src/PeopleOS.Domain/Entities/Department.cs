using PeopleOS.Domain.Common;
namespace PeopleOS.Domain.Entities
{
    public class Department: AuditableEntity
    {
        public Guid OrganizationId { get; private set; } //foreign key to the organization

        public string Name { get; private set;} = null!; 

        public string? Description { get; private set; }

        private Department()
        {
            // Required by EF Core. Because it needs a parameterless constructor but the private is needed so that no one can create without using the factory method. 
        }

        public Department(Guid organizationId, string name, string? description = null)
        {
            if(string.IsNullOrWhiteSpace(name))
            {  
                throw new ArgumentException("Department name cannot be null or empty.", nameof(name));
            }

            if(organizationId == Guid.Empty)
            {
                throw new ArgumentException("Organization ID cannot be empty.", nameof(organizationId));
            }
            
            OrganizationId = organizationId;
            Name = name.Trim();
            Description = description?.Trim();
        }

    }
}