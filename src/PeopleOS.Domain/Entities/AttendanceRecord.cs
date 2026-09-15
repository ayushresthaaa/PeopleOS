using PeopleOS.Domain.Common;
using PeopleOS.Domain.Enums;
using PeopleOS.Domain.ValueObjects;

namespace PeopleOS.Domain.Entities;

public class AttendanceRecord : AuditableEntity
{
    public Guid EmployeeId { get; private set; }

    public DateOnly Date { get; private set; }

    public DateTimeOffset? CheckInTime { get; private set; }

    public DateTimeOffset? CheckOutTime { get; private set; }

    public AttendanceStatus Status { get; private set; }

    private AttendanceRecord()
    {
        // Required by EF Core.
    }

    public AttendanceRecord(
        Guid employeeId,
        DateOnly date)
    {
        if (employeeId == Guid.Empty)
        {
            throw new ArgumentException(
                "EmployeeId cannot be empty.",
                nameof(employeeId));
        }

        if (date > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new ArgumentException(
                "Attendance date cannot be in the future.",
                nameof(date));
        }

        EmployeeId = employeeId;
        Date = date;
        Status = AttendanceStatus.Absent;
    }

    public void CheckIn(
        DateTimeOffset checkInTime,
        AttendancePolicy policy)
    {
        ArgumentNullException.ThrowIfNull(policy);

        if (CheckInTime.HasValue)
        {
            throw new InvalidOperationException(
                "Employee has already checked in.");
        }

        if (CheckOutTime.HasValue)
        {
            throw new InvalidOperationException(
                "Employee has already checked out.");
        }

        if (DateOnly.FromDateTime(checkInTime.DateTime) != Date)
        {
            throw new ArgumentException(
                "Check-in time must belong to the attendance date.",
                nameof(checkInTime));
        }

        CheckInTime = checkInTime;
        Status = policy.DetermineStatus(checkInTime);
    }

    public void CheckOut(DateTimeOffset checkOutTime)
    {
        if (!CheckInTime.HasValue)
        {
            throw new InvalidOperationException(
                "Employee must check in before checking out.");
        }

        if (CheckOutTime.HasValue)
        {
            throw new InvalidOperationException(
                "Employee has already checked out.");
        }

        if (checkOutTime < CheckInTime.Value)
        {
            throw new ArgumentException(
                "Check-out time cannot be earlier than check-in time.",
                nameof(checkOutTime));
        }

        if (DateOnly.FromDateTime(checkOutTime.DateTime) != Date)
        {
            throw new ArgumentException(
                "Check-out time must belong to the attendance date.",
                nameof(checkOutTime));
        }

        CheckOutTime = checkOutTime;
    }

    public void MarkAbsent()
    {
        if (CheckInTime.HasValue || CheckOutTime.HasValue)
        {
            throw new InvalidOperationException(
                "An employee who has attendance times cannot be marked absent.");
        }

        Status = AttendanceStatus.Absent;
    }

    public void MarkOnLeave()
    {
        if (CheckInTime.HasValue || CheckOutTime.HasValue)
        {
            throw new InvalidOperationException(
                "An employee with attendance times cannot be marked on leave.");
        }

        Status = AttendanceStatus.OnLeave;
    }

    public void MarkHalfDay()
    {
        if (!CheckInTime.HasValue)
        {
            throw new InvalidOperationException(
                "An employee must have a check-in time to be marked half-day.");
        }

        Status = AttendanceStatus.HalfDay;
    }
}