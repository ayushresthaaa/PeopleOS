using PeopleOS.Domain.Common; 
using PeopleOS.Domain.Enums;
namespace PeopleOS.Domain.Entities
{
    public class AttendanceRecord : AuditableEntity
    {
        public Guid EmployeeId {get; private set; }
        public DateOnly Date {get; private set; }
        public DateTimeOffset? CheckInTime {get; private set; }
        public DateTimeOffset? CheckOutTime {get; private set; }

        public AttendanceStatus Status {get; private set; }

        private AttendanceRecord()
        {
            //for ef core
        }

        public AttendanceRecord(Guid employeeId, DateOnly date, AttendanceStatus status, DateTimeOffset? checkInTime = null, DateTimeOffset? checkOutTime = null)
        {
            //Checkintime and checkout time are optional because the employee may not have checked in or out yet.

            if (employeeId == Guid.Empty)
            {
                throw new ArgumentException("EmployeeId cannot be empty.", nameof(employeeId));
            }

            if(date > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new ArgumentException("Attendance date cannot be in the future.", nameof(date));
            }

            if(status == AttendanceStatus.Present && checkInTime == null)
            {
                throw new ArgumentException("Check-in time must be provided for present status.", nameof(checkInTime));
            }


            if (status == AttendanceStatus.Absent && (checkInTime != null || checkOutTime != null))
            {
                throw new ArgumentException("Check-in and check-out times must be null for absent status.");
            }

            if (checkInTime != null && checkOutTime != null && checkInTime > checkOutTime)
            {
                throw new ArgumentException("Check-in time cannot be later than check-out time.");
            }

            if(status == AttendanceStatus.OnLeave && (checkInTime != null || checkOutTime != null))
            {
                throw new ArgumentException("Check-in and check-out times must be null for on leave status.");
            }
            
            EmployeeId = employeeId;
            Date = date;
            Status = status;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;

        }
    }
}