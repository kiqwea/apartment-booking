using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking01.MainCode
{
    interface IReservation
    {
        int Book(Home home, User user, DateTime date);
        bool LookIsFree(Home home);
        void RentOut(Home home, User user);
        bool DelHouse(Home home, User user);
    }
}
