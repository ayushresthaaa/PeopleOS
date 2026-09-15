using PeopleOS.Domain.Common;
using PeopleOS.Domain.Enums;
namespace PeopleOS.Domain.ValueObjects
{
    public class AttendancePolicy : ValueObject
    {
        public TimeOnly WorkStartTime { get; private set; }
        public TimeOnly WorkEndTime { get; private set; }
        public TimeSpan GracePeriod { get; private set; }//this will be used to calculate the late time of the employee
        private AttendancePolicy() { }

        public AttendancePolicy(TimeOnly workStartTime, TimeOnly workEndTime, TimeSpan gracePeriod)

        {
             if (workEndTime <= workStartTime)
            {
                throw new ArgumentException(
                    "Work end time must be later than work start time.",
                    nameof(workEndTime));
            }

            if (gracePeriod < TimeSpan.Zero)
            {
                throw new ArgumentException(
                    "Grace period cannot be negative.",
                    nameof(gracePeriod));
            }

            if (gracePeriod >= (workEndTime - workStartTime))
            {
                throw new ArgumentException(
                    "Grace period must be shorter than the working period.",
                    nameof(gracePeriod));
            }

            WorkStartTime = workStartTime;
            WorkEndTime = workEndTime;
            GracePeriod = gracePeriod;
        }

        public AttendanceStatus DetermineStatus(DateTimeOffset checkInTime)
        {
            var checkIn = TimeOnly.FromDateTime(checkInTime.DateTime); //here timeonly is parsing time only from date time, after getting checkintime. 

            var lateThreshold = WorkStartTime.Add(GracePeriod);

            return checkIn > lateThreshold ? AttendanceStatus.Late : AttendanceStatus.Present;


        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return WorkStartTime;
            yield return WorkEndTime;
            yield return GracePeriod;
        }
    }
}