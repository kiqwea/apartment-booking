using Booking01.MainCode;
using Newtonsoft.Json;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;


namespace TestProject1
{
    public class Tests
    {
        FindHome findHome;
        User _user;
        Apartment home;
        DateTime time;


        [SetUp]
        public void Setup()
        {
            findHome = new FindHome();
            _user = new User(1, "John", "test@example.com", "Doe", new DateTime(1990, 1, 1), "1234567890", false, "password");
            home = new Apartment(1, "qwe", 10000, "Ukraine", 2, 1, 1, "qq", _user);
            FindHome.houses.Add(home);
            time = DateTime.Today;

        }


        [Test]
        public void TestLoginValidCredentials()
        {
            var userJson = "{\"Users\": [{\"Id\": 1,\"FirstName\": \"Test\",\"Email\": \"test@example.com\",\"Password\": \"test123\",\"LastName\": \"User\",\"BirthDay\": \"1990-01-01T00:00:00\",\"PhoneNumber\": \"1234567890\",\"RentOwn\": [],\"Renting\": [],\"LookAt\": null,\"IsAdmin\": false}]}";
            System.IO.File.WriteAllText("Users.json", userJson);
            var user = new User();

            var result = findHome.Login("test@example.com", "test123");

            Assert.IsTrue(result);
        }

        [Test]
        public void TestLoginInvalidCredentials()
        {
            var userJson = "{\"Users\": [{\"Id\": 1,\"FirstName\": \"Test\",\"Email\": \"test@example.com\",\"Password\": \"test123\",\"LastName\": \"User\",\"BirthDay\": \"1990-01-01T00:00:00\",\"PhoneNumber\": \"1234567890\",\"RentOwn\": [],\"Renting\": [],\"LookAt\": null,\"IsAdmin\": false}]}";
            System.IO.File.WriteAllText("Users.json", userJson);
            var user = new User();

            var result = findHome.Login("test@example.com", "invalidpassword");

            Assert.IsFalse(result);
        }


        [Test]
        public void TestRegistrationValid()
        {
            System.IO.File.WriteAllText("Users.json", "[]");
            var email = "test1@example.com";
            var password = "test123456";
            var fullName = "Test User";
            var birthDay = "1990.01.01";
            var phoneNumber = "1234567890";

            var result = findHome.Registration(email, password, fullName, birthDay, phoneNumber);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestRegistrationInvalid()
        {
           
            System.IO.File.WriteAllText("Users.json", "[]");
            var email1 = "test@example.com";
            var password1 = "test123456";
            var fullName1 = "Test User";
            var birthDay1 = "1990.01.01";
            var phoneNumber1 = "1234567890";

            // Act
            findHome.Registration(email1, password1, fullName1, birthDay1, phoneNumber1);


            var email = "test@example.com";
            var password = "test123456";
            var fullName = "Test User";
            var birthDay = "1990.01.01";
            var phoneNumber = "1234567890";

            var result = findHome.Registration(email, password, fullName, birthDay, phoneNumber);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestRegistrationWithInvalidPassword()
        {
            var user = new User();
            user.Email = "test@example.com";
            user.Password = "short";
            var fullName = "John Smith";
            var birthDay = "01.01.2000";
            var phoneNumber = "(123) 456-7890";

            var result = findHome.Registration(user.Email, user.Password, fullName, birthDay, phoneNumber);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestRegistrationWithInvalidFullName()
        {
            var user = new User();
            user.Email = "test@example.com";
            user.Password = "password123";
            var fullName = "J";
            var birthDay = "01.01.2000";
            var phoneNumber = "(123) 456-7890";

            var result = findHome.Registration(user.Email, user.Password, fullName, birthDay, phoneNumber);

            Assert.IsFalse(result);
        }

        [Test]
        public void Book_ShouldReturnZero_WhenHomeIsAvailableAndDateIsInTheFuture()
        {

            int result = _user.Book(home, _user, time.AddDays(1));

            Assert.AreEqual(0, result);
            Assert.IsFalse(home.IsFree);
            Assert.IsTrue(_user.Renting.Contains(home));
        }

        [Test]
        public void Book_ShouldReturnTwo_WhenDateIsInThePast()
        {
            int result = _user.Book(home, _user, time.AddDays(-1));

            Assert.AreEqual(2, result);
            Assert.IsTrue(home.IsFree);
            Assert.IsFalse(_user.Renting.Contains(home));
        }

        [Test]
        public void AddHouse_ValidInput_ReturnsTrue()
        {

            HousesType type = HousesType.HOUSE;
            string description = "Beautiful house with a view";
            float price = 500000;
            string country = "USA";
            int numOfRooms = 4;
            int numOfFloors = 2;
            int numOfBedrooms = 3;
            string address = "123 Main St";

            int floor = 0;

            bool result = findHome.AddHome(ref _user, type, description, price, country, numOfRooms, numOfFloors, numOfBedrooms, address, floor);

            
            Assert.IsTrue(result);
        }

        [Test]
        public void AddApartment_InvalidCountry_ReturnsFalse()
        {

            HousesType type = HousesType.APARTMENT;
            string description = "Spacious apartment in the city center";
            float price = 100000;
            string country = "U";
            int numOfRooms = 2;
            int floor = 5;
            int numOfBedrooms = 1;
            string address = "456 Elm St";

            bool result = findHome.AddHome(ref _user, type, description, price, country, numOfRooms, 0, numOfBedrooms, address, floor);


            Assert.IsFalse(result);
        }
    }
}