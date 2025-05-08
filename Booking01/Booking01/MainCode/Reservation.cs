using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking01.MainCode
{
    public class Reservation : IReservation
    {
        public int Book(Home home, User user, DateTime date)
        {
            if (LookIsFree(home) == false)
            {
                return 1;
            }
            if(date < DateTime.Now)
            {
                return 2;
            }

            user.Renting.Add(home);
            home.IsFree = false;
            FindHome.UpdateUserInfo(user);
            FindHome.UpdateHomeInfo(home);
            return 0;
        }

        public bool DelHouse(Home home, User user)
        {
            if(user.IsAdmin == true && home.IsFree == true)
            {
                home.Owner.RentOwn.Remove(home);
                FindHome.houses.Remove(home);
                FindHome.UpdateUserInfo(user);
                FindHome.DelHomeFromFile(home);
                return true;
            }
            return false;
            
        }

        public bool LookIsFree(Home home)
        {
            //foreach (Home item in FindHome.houses)
            //{
            //    if(item == home)
            //    {
            //        return item.IsFree;
            //    }
            //}
            //return false;
            return home.IsFree;
        }

        public void RentOut(Home home, User user) 
        { 
            //user.RentOwn.Add(home);
            //FindHome.houses.Add(home);
        }
    }
}
