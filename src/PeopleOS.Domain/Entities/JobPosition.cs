using PeopleOS.Domain.Common; 
using PeopleOS.Domain.Enums; 
namespace PeopleOS.Domain.Entities
{
    public class JobPosition : AuditableEntity
    {
        public Guid DepartmentId {get; private set; }

        public string Title {get; private set; } = null!; 

        public string? Description {get; private set;}

        public JobPositionStatus Status {get; private set;} 

        private JobPosition()
        {
            //for ef core
        }

        public JobPosition(Guid departmentId, string title, JobPositionStatus status = JobPositionStatus.Active, string? description = null)
        {
            if(departmentId ==Guid.Empty)
            {
                throw new ArgumentException("DepartmentId cannot be left", nameof(departmentId));
                
            }
            if(string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title of the Job position cannot be left empty", nameof(title)); 
            }

            DepartmentId = departmentId; 
            Title = title.Trim(); 
            Description = description; 
            Status = status; 

        }
    }
}