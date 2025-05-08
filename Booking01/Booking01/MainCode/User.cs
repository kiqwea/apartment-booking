using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking01.MainCode
{
    public class User : Reservation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public List<Home> RentOwn { get; set; }
        public List<Home> Renting { get; set; }
        public Home LookAt { get; set; }
        public bool IsAdmin { get; set; }

        public User(int id, string firstName, string email, string lastName, DateTime birthDay, string phoneNumber, bool isAdmin, string password)
        {
            Id = id;
            FirstName = firstName;
            Email = email;
            LastName = lastName;
            BirthDay = birthDay;
            PhoneNumber = phoneNumber;
            IsAdmin = isAdmin;
            Password = password;
            Renting = new List<Home>();
            RentOwn = new List<Home>();
        }
        public User() { }
    }
}
