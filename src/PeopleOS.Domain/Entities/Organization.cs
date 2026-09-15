using PeopleOS.Domain.Common; 

namespace PeopleOS.Domain.Entities
{

    //inherit the property of the Auditable Base Entity 
    public class Organization: AuditableEntity
    {
        public string Name { get; private set;} = null!; 

        public string? Description { get; private set; }

        private Organization()
        {
            // Required by EF Core. Because it needs a parameterless constructor but the private is needed so that no one can create without using the factory method. 
        }

        public Organization(string name, string? description = null)
        {
            if(string.IsNullOrWhiteSpace(name))
            {  
                throw new ArgumentException("Organization name cannot be null or empty.", nameof(name));
            }

            Name = name.Trim();
            Description = description?.Trim();
        }

    }
} 