namespace PeopleOS.Domain.Common
{
    public abstract class ValueObject
    {
        //this stores the object's equality component as defined/passed
        protected abstract IEnumerable<object?> GetEqualityComponents();

        //this is for the equality check that uses the equality components

        public override bool Equals(object? obj)
        {
            if(obj is not ValueObject other || this.GetType() != other.GetType())
            {
                return false; 
            }

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents()); 
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents().Aggregate(0, (hash, value) => HashCode.Combine(hash, value)); 
        }

        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            return Equals(left, right); 
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !Equals(left, right); 
        }
    }
}