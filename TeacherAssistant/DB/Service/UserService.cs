using System.Data.SqlServerCe;

namespace TeacherAssistant.DB.Service
{
    public class UserService
    {
        public LocalDB DB { get; set; }

        /// <summary>
        ///   Let's login boys!
        /// </summary>
        /// <param name="usr">obvious</param>
        /// <param name="pass">obvious</param>
        /// <returns>true/false : logged in</returns>
        public bool Login(string usr, string pass)
        {
            return DB.ExecuteReturnBoolean(
                "SELECT ID FROM tblUser WHERE Name = N'{0}' AND Pass = N'{1}';", 
                    new string[]{usr, pass});
        }
    }
}