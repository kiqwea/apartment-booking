using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Booking01.MainCode
{
    public class Apartment : Home
    {
        public List<Image> Images;
        public int Floor { get; set; }
        


        public Apartment(int id, string description, float price, string country, int numOfRooms, int floor, int numOfBedrooms, string address, Image image, User owner)
        {
            Id = id;
            Description = description;
            Price = price;
            Country = country;
            NumOfRooms = numOfRooms;
            NumOfBedrooms = numOfBedrooms;
            Address = address;
            Images.Add(image);
            Owner = owner;
            Floor = floor;
            IsFree = true;

        }

        public Apartment(int id, string description, float price, string country, int numOfRooms, int Floor, int numOfBedrooms, string address, User owner)
        {
            Id = id;
            Description = description;
            Price = price;
            Country = country;
            NumOfRooms = numOfRooms;
            NumOfBedrooms = numOfBedrooms;
            Address = address;
            Owner = owner;
            IsFree = true;
        }
    }
}
