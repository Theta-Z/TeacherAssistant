using System.IO;
using System.Linq;
using System.Reflection;
using System.Data.SqlServerCe;

namespace TeacherAssistant.DB
{
    /// <summary>
    /// Since this LocalDB only uses sdf, there are no stored procedures. Certain things
    /// may seem a bit primative, including the "security".
    /// </summary>
    public class LocalDB
    {
        private string CS;
        private string[] INVALID = { "'", @"\", "--" };

        public LocalDB()
        {
            var _DS = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6) + "\\DB\\__DB.sdf";
            CS = string.Format("Data Source={0};Max Database Size=256;Max Buffer Size = 1024; Encrypt Database = True; Password=#P@$$WrD!;", _DS);
        }

        /// <summary>
        ///  Create SQL CE Connection using out Connection String
        /// </summary>
        private SqlCeConnection CreateConnection()
        {
            return new SqlCeConnection(CS);
        }

        /// <summary>
        ///  Use this for a command that, if there is output... The answer is true.
        ///  E.g.,
        ///    SELECT 
        ///        NULL
        ///    FROM tblUser
        ///    WHERE [INFO = INPUT];
        ///  ^ If the above has output; the answer is true.
        /// </summary>
        /// <param name="command">Command to run: "SELECT NULL FROM tbl WHERE field={0}"</param>
        /// <param name="inlineParams">Parameters to go with the command.</param>
        /// <returns>True if there is output.</returns>
        public bool ExecuteReturnBoolean(string command, string[] inlineParams)
        {
            if (HasInvalidChars(inlineParams))
                return false;

            var result = false;

            using (var con = CreateConnection())
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = string.Format(command, inlineParams);

                    var s = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
                    if (s.HasRows)
                        result = true;
                }
                con.Close();
            }

            return result;
        }

        /// <summary>
        ///   Are there invalid characters? Lets not duplicate code.
        /// </summary>
        /// <param name="testThese">array of strings to test</param>
        /// <returns></returns>
        public bool HasInvalidChars(string[] testThese)
        {
            var valid = true;

            foreach (var test in testThese)
            {
                valid = (from nope in INVALID
                         where test.Contains(nope)
                         select nope).Count() != 1;

                if (!valid) return true;
            }

            return false;
        }
    }
}

/*
DB**
  Teacher -> [many]Student(s)
  User => TypeOf(Teacher), TypeOf(Student)

interface**

[MAIN]
  [Login]
typeof(login)

  [Teacher]
    [Quiz Area] => Create/Edit
    [Assignment Area] => Create/Edit Homework/Assignments
    [Account Settings]
  [Student] (show previously completed, if marked allowed by teacher)
    [View Available Tests]
    [View Available Assignments]
    [View Available Homework]
    [Practice] (allow retaking of tests/assignments/homework, if teacher allows)
    [Settings] (BGColour, etc)
 */