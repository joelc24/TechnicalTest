using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.Json.Serialization;

namespace TechnicalTest.Controllers
{
    [Route("api")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        ///  Endpoint that receives an array of values and removes duplicates
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns>
        /// The return is a new array with unique values.
        /// </returns>
        [HttpPost]
        [Route("remove-duplicates")]
        public dynamic Remove([FromBody] Object jsonData)
        {
            Repeated data = JsonConvert.DeserializeObject<Repeated>(jsonData.ToString());
            List<string> uniques = new List<string>();

            foreach (string item in data.repeatedValues)
            {
                bool check = Array.Exists(uniques.ToArray<string>(), element => element == item);

                if (!check)
                {
                    uniques.Add(item);
                }
            }

            return new { uniques };
        }


        /// <summary>
        ///  Searches for the days of the week on which you appear and returns
        ///  an object with the key of your name that will contain an array whose 
        ///  values will be the days of the week on which you appear.
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns>
        /// {
        ///     "ivan": ["monday", "thursday","friday"],
        ///     "alfredo": ["monday", "wednesday"],
        ///     "dario": ["monday","tuesday"]
        /// }
        /// </returns>
        [HttpPost]
        [Route("organize")]
        public dynamic Organize([FromBody] Object jsonData)
        {
            Week week = JsonConvert.DeserializeObject<Week>(jsonData.ToString());
         
            List<string> ivan = new List<string>();
            List<string> alfredo = new List<string>();
            List<string> dario = new List<string>();


            return new { 
                Ivan = GetListPerson(week, ivan, "Ivan"),
                Alfredo = GetListPerson(week, alfredo, "Alfredo"),
                Dario = GetListPerson(week, dario, "Dario")
            };
        }
        /// <summary>
        /// The function from which we are born is used to search and return the
        /// list with the days of the week in which they appear.
        /// </summary>
        /// <param name="week"></param>
        /// <param name="people"></param>
        /// <param name="name"></param>
        /// <returns>
        /// </returns>
        public static List<string> GetListPerson(Week week, List<string> people, string name)
        {

            List<bool> contains = new List<bool>();
            contains.Add(week.monday.Contains(name));
            contains.Add(week.thursday.Contains(name));
            contains.Add(week.wednesday.Contains(name));
            contains.Add(week.tuesday.Contains(name));
            contains.Add(week.friday.Contains(name));

            
            List<string> weekList = new List<string>()
            {
                "monday",
                "thursday",
                "wednesday",
                "tuesday",
                "friday"
            };

            for (int i = 0; i < contains.ToArray().Length; i++)
            {
                if (contains.ToArray()[i])
                {
                    people.Add(weekList.ToArray()[i]);
                }
            }

            

            return people;
        }

    }

    public class Week
    {
        public List<string> monday { get; set; }
        public List<string> thursday { get; set; }
        public List<string> wednesday { get; set; }
        public List<string> tuesday { get; set; }
        public List<string> friday { get; set; }
    }


    public class Repeated
    {
        public List<string> repeatedValues { get; set; }
    }



}
