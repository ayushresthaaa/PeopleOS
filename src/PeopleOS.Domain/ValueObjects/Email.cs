using System.Net.Mail;
using PeopleOS.Domain.Common; 
namespace PeopleOS.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public string Value {get; private set; } = null!; 
        private Email()
        {
            
        }

        public Email(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email cannot be empty", nameof(value)); 
            }

            var normalizedValue = value.Trim(); 

            try
            {
                var mailAddress = new MailAddress(normalizedValue); 
                if (mailAddress.Address != normalizedValue)
                {
                    throw new ArgumentException("Invalid email address", nameof(value)); 
                }
            } catch(FormatException)
            {
                throw new ArgumentException("Invalid email address", nameof(value)); 
            }

            Value = normalizedValue; 
        }

        //overriding abstract method 
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value; 
        }

        public override string ToString()
        {
            return Value; 
        }


    }
}