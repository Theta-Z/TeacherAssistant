using System;

namespace TeacherAssistant.DB.Domain
{
    public class User
    {
        public int ID;
        public string Name;
        public string Pass; //: The default passwords for testing purposes are password
        public Enums.UserType Type;
        public DateTime LockedUntil;
    }
}