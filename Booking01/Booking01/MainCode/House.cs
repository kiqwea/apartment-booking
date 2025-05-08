using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Booking01.MainCode
{
    public class House : Home
    {
        public List<Image> Images = new List<Image>();

        public int NumOfFloors { get; set; }


        public House(int id, string description, float price, string country, int numOfRooms, int numOfFloors, int numOfBedrooms, string address, Image image, User user)
        {
            Id = id;
            Description = description;
            Price = price;
            Country = country;
            NumOfRooms = numOfRooms;
            NumOfFloors = numOfFloors;
            NumOfBedrooms = numOfBedrooms;
            Address = address;
            Images.Add(image); 
            Owner = user;
            IsFree = true;

        }
        public House(int id, string description, float price, string country, int numOfRooms, int numOfFloors, int numOfBedrooms, string address, User user)
        {
            Id = id;
            Description = description;
            Price = price;
            Country = country;
            NumOfRooms = numOfRooms;
            NumOfFloors = numOfFloors;
            NumOfBedrooms = numOfBedrooms;
            Address = address;
            Owner = user;
            IsFree = true;

        }
    }
}
