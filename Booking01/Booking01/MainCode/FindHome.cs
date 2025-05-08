using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Booking01.MainCode
{
    public class FindHome
    {
        private User session;
        public static List<Home> houses = new List<Home>();
        
        

        public void ChooseHome(Home home)
        {
            session.LookAt = home;
        }

        public List<Home> Search(int maxPrice, string country, int numOfRooms)
        {
            List<Home> SearchHomes = new List<Home>();

            foreach (Home house in houses)
            {
                if(house.NumOfRooms == numOfRooms && house.Country == country && house.Price < maxPrice)
                {
                    SearchHomes.Add(house);
                }
            }
            return SearchHomes;
        }

        //public bool Login(string email, string password)
        //{
        //    string file = "";
        //    if (File.Exists("Users.json"))
        //    {
        //        file = File.ReadAllText("Users.json");
        //        JObject json = JObject.Parse(file);
        //        JArray peopleArray = (JArray)json["User"];


        //        foreach (JObject item in peopleArray)
        //        {
        //            if (item.ToObject<User>().Email == email && item.ToObject<User>().Password == password)
        //            {
        //                session = item.ToObject<User>();
        //                return true;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        File.WriteAllText("Users.json", "file");
        //    }

        //    return false;
        //}

        //public bool Registration(string email, string password, string fullName, string birthDay, string phoneNumber)
        //{

        //    if (CheckRules(email, password, fullName, birthDay, phoneNumber) > 0)
        //    {
        //        return false;
        //    }

        //    string[] fullname = fullName.Split(' ');
        //    string[] birthday = birthDay.Split('.');

        //    string file = "";
        //    int userCount = 0;
        //    if (File.Exists("Users.json"))
        //    {
        //        file = File.ReadAllText("Users.json");
        //        JObject json = JObject.Parse(file);
        //        JArray peopleArray = (JArray)json["User"];

        //        userCount = peopleArray.Count();

        //        foreach (JObject item in peopleArray)
        //        {
        //            if (item.ToObject<User>().Email == email)
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        File.WriteAllText("Users.json", "file");

        //    }

        //    session = new User(++userCount, fullname[0], email, fullname[1], new DateTime(int.Parse(birthday[0]), int.Parse(birthday[1]), int.Parse(birthday[2])), phoneNumber, false, password);
        //    SaveUser(session);
        //    return true;
        //}

        public int CheckRules(string email, string password, string fullName, string birthDay, string phoneNumber)
        {
            string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isEmail = Regex.IsMatch(email, emailRegex);
            if (!isEmail) {
                return 1;
            }

            if(password.Length < 8)
            {
                return 2;
            }

            if(fullName.Length < 2)
            {
                return 3;
            }

            string[] birthday = birthDay.Split('.');
            try
            {
                int q = int.Parse(birthday[0]);
                int w = int.Parse(birthday[1]);
                int e = int.Parse(birthday[2]);
            }catch(Exception) { return 4; }

            if (int.Parse(birthday[2]) > 2004)
            {
                return 4;
            }

            string phoneRegex = @"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$";
            bool isPhone = Regex.IsMatch(phoneNumber, phoneRegex);
            if(!isPhone)
            {
                return 5;
            }

            return 0;
        }


        public List<Home> HomeFromFile()
        {
            string file = "";
            List<Home> Houses = new List<Home>();

            if (File.Exists("Houses.json"))
            {
                file = File.ReadAllText("Houses.json");
                JObject json = JObject.Parse(file);
                JArray homeArray = (JArray)json["Home"];


                foreach (JObject item in homeArray)
                {
                    Houses.Add(item.ToObject<Home>());
                }
            }
            else
            {
                File.WriteAllText("Houses.json", "file");
            }
            return Houses;
        }

        public void HomeToFile(List<Home> homes)
        {
            JArray jArray = new JArray();
            if (File.Exists("Houses.json"))
            {
                foreach (Home item in homes)
                {
                    jArray.Add(JObject.FromObject(item));
                }
            }
            else
            {
                File.WriteAllText("Houses.json", "file");
            }
            File.WriteAllText("Houses.json", jArray.ToString());
        }





        public void SaveUser(User user)
        {
            JObject userObject = JObject.FromObject(user); 

            string json = File.ReadAllText("Users.json");
            JArray existingArray = JArray.Parse(json);

            existingArray.Add(userObject);

            string updatedJson = existingArray.ToString();
            File.WriteAllText("Users.json", updatedJson);
        }

        public static void UpdateUserInfo(User user)
        {
            string json = File.ReadAllText("Users.json");
            JArray existingObjects = JArray.Parse(json);

            for (int i = 0; i < existingObjects.Count; i++)
            {
                if (existingObjects[i].ToObject<User>().Email == user.Email)
                {
                    existingObjects.RemoveAt(i);
                    existingObjects.Add(JObject.FromObject(user));
                    break; 
                }
            }
            File.WriteAllText("Users.json", existingObjects.ToString());
        }

        public bool Login(string email, string password)
        {
            if (File.Exists("Users.json")) 
            {
                string file = File.ReadAllText("Users.json");
                JObject json = JObject.Parse(file);
                JArray peopleArray = (JArray)json["Users"]; 

                foreach (JObject item in peopleArray)
                {
                    if (item.ToObject<User>().Email == email && item.ToObject<User>().Password == password)
                    {
                        session = item.ToObject<User>();
                        return true;
                    }
                }
            }
            else
            {
                File.WriteAllText("Users.json", "[]"); 
            }

            return false;
        }

        public bool Registration(string email, string password, string fullName, string birthDay, string phoneNumber)
        {
            if (CheckRules(email, password, fullName, birthDay, phoneNumber) > 0)
            {
                return false;
            }

            string[] fullname = fullName.Split(' ');
            string[] birthday = birthDay.Split('.');

            int userCount = 0;
            JArray peopleArray = new JArray();

            if (File.Exists("Users.json"))
            {
                string file = File.ReadAllText("Users.json");
                JArray existingArray = JArray.Parse(file);
                //JObject json = JObject.Parse(file);
                //peopleArray = (JArray)json["Users"];

                //userCount = peopleArray.Count();
                userCount = existingArray.Count();

                foreach (JObject item in existingArray)
                {
                    if (item.ToObject<User>().Email == email)
                    {
                        return false;
                    }
                }
            }
            else
            {
                File.WriteAllText("Users.json", "[]");
            }

            session = new User(++userCount, fullname[0], email, fullname[1], new DateTime(int.Parse(birthday[0]), int.Parse(birthday[1]), int.Parse(birthday[2])), phoneNumber, false, password);

            peopleArray.Add(JObject.FromObject(session)); 

            SaveUser(session);

            return true;
        }

        //public void SaveUser(User user)
        //{
        //    JArray jsonArray = new JArray();
        //    jsonArray.Add(JObject.FromObject(user));

        //    string json = File.ReadAllText("Users.json");
        //    JArray existingArray = JArray.Parse(json);

        //    existingArray.Add(jsonArray);

        //    string updatedJson = existingArray.ToString();
        //    File.WriteAllText("Users.json", updatedJson);
        //}


        //public static void UpdateUserInfo(User user)
        //{

        //    string json = File.ReadAllText("Users.json");
        //    List<JObject> existingObjects = JsonConvert.DeserializeObject<List<JObject>>(json);


        //    for (int i = 0; i < existingObjects.Count; i++)
        //    {
        //        if (existingObjects[i].ToObject<User>().Email == user.Email)
        //        {
        //            existingObjects.RemoveAt(i);
        //            existingObjects.Add(JObject.FromObject(user));
        //        }
        //    }
        //    File.WriteAllText("Users.json", existingObjects.ToString());
        //}

        public static void UpdateHomeInfo(Home home)
        {
            if (File.Exists("Houses.json"))
            {
                string json = File.ReadAllText("Houses.json");
                JArray existingArray = JArray.Parse(json);

                for (int i = 0; i < existingArray.Count; i++)
                {
                    if (existingArray[i].ToObject<Home>().Id == home.Id)
                    {
                        existingArray.RemoveAt(i);
                        existingArray.Add(new JArray(JObject.FromObject(home)));
                    }
                }
                File.WriteAllText("Houses.json", existingArray.ToString());
            }
            else
            {
                JArray existingArray = new JArray
                {
                    new JArray(JObject.FromObject(home))
                };
                File.WriteAllText("Houses.json", existingArray.ToString());
            }
            
        }

        public static void DelHomeFromFile(Home home)
        {
            if (File.Exists("Houses.json"))
            {
                string json = File.ReadAllText("Houses.json");
                JArray existingArray = JArray.Parse(json);

                for (int i = 0; i < existingArray.Count; i++)
                {
                    if (existingArray[i].ToObject<Home>().Id == home.Id)
                    {
                        existingArray.RemoveAt(i);
                    }
                }
                File.WriteAllText("Houses.json", existingArray.ToString());
            }

        }

        public bool AddHome(ref User owner, HousesType type, string description, float price, string country, int numOfRooms, int numOfFloors, int NumOfBedrooms, string address, Image image, int Floor)
        {
            
            if(CheckRules(description, price, country, numOfRooms, numOfFloors, NumOfBedrooms, address) > 0)
            {
                return false;
            }

            if(type == HousesType.HOUSE)
            {
                houses.Add(new House(houses.Count + 1, description, price, country, numOfRooms, numOfFloors, NumOfBedrooms, address, image, session));
            }
            else if(type == HousesType.APARTMENT)
            {
                houses.Add(new Apartment(houses.Count + 1, description, price, country, numOfRooms, Floor, NumOfBedrooms, address, image, session));
            
            }
            owner.RentOwn.Add(houses.Last());
            UpdateHomeInfo(houses.Last());
            return true;
        }
        public bool AddHome(ref User owner, HousesType type, string description, float price, string country, int numOfRooms, int numOfFloors, int NumOfBedrooms, string address,  int Floor)
        {
            if (CheckRules(description, price, country, numOfRooms, numOfFloors, NumOfBedrooms, address) > 0)
            {
                return false;
            }

            if (type == HousesType.HOUSE)
            {
                houses.Add(new House(houses.Count + 1, description, price, country, numOfRooms, numOfFloors, NumOfBedrooms, address, session));
            }
            else if (type == HousesType.APARTMENT)
            {
                houses.Add(new Apartment(houses.Count + 1, description, price, country, numOfRooms, Floor, NumOfBedrooms, address, session));

            }

            owner.RentOwn.Add(houses.Last());
            UpdateHomeInfo(houses.Last());
            return true;
        }

        public int CheckRules(string description, float price, string country, int numOfRooms, int numOfFloors, int NumOfBedrooms, string address)
        {
            if(description.Length < 20)
            {
                return 1;
            }

            if(price < 0)
            {
                return 2;
            }

            if(country.Length < 2){
                return 3;
            }

            if(numOfRooms < 1)
            {
                return 4;
            }

            if(numOfFloors < 1)
            {
                return 5;
            }

            if(NumOfBedrooms < 0)
            {
                return 6;
            }

            if(address.Length < 10)
            {
                return 7;
            }

            return 0;
            
        }

    }
}
