using System.ComponentModel.DataAnnotations;
using PeopleOS.Domain.Common; 
using PeopleOS.Domain.ValueObjects;
using PeopleOS.Domain.Enums; 

namespace PeopleOS.Domain.Entities
{
    public class Employee : AuditableEntity
    {
        public Guid OrganizationId { get; private set; }
        public Guid DepartmentId { get; private set; }
        public Guid JobPositionId { get; private set; }

        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;

        public Email Email { get; private set; } = null!;

        public DateOnly? DateOfBirth { get; private set; }
        public DateOnly HireDate { get; private set; }


        public EmployeeStatus Status { get; private set; }

        private Employee() { }

        public Employee(
        Guid organizationId,
        Guid departmentId,
        Guid jobPositionId,
        string firstName,
        string lastName,
        Email email,
        DateOnly hireDate,
        DateOnly? dateOfBirth = null,
        EmployeeStatus status = EmployeeStatus.Active)
        {
            if (organizationId == Guid.Empty)
                throw new ArgumentException("OrganizationId cannot be empty.", nameof(organizationId));

            if (departmentId == Guid.Empty)
                throw new ArgumentException("DepartmentId cannot be empty.", nameof(departmentId));

            if (jobPositionId == Guid.Empty)
                throw new ArgumentException("JobPositionId cannot be empty.", nameof(jobPositionId));

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

            OrganizationId = organizationId;
            DepartmentId = departmentId;
            JobPositionId = jobPositionId;
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email ?? throw new ArgumentNullException(nameof(email));
            HireDate = hireDate;
            DateOfBirth = dateOfBirth;
            Status = status;
        }
    }
}