using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Text;

namespace WideraZaliczenieSqlWidok.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Calculate();
        }

        private IActionResult Calculate()
        {
            try 
            { 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "develop_server"; 
                builder.UserID = "sa";            
                builder.Password = "yourStrong(!)Password";     
                builder.InitialCatalog = "master";
         
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    
                    connection.Open();       
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM view_obliczenia;");
                    String sql = sb.ToString();

                    var list = new List<Tuple<decimal, decimal, string>>();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Tuple<decimal, decimal, string>(reader.GetDecimal(0), reader.GetDecimal(1), reader.GetString(2)));
                            }
                        }

                        return Ok(GetResult(list));
                    }                    
                }
            }
            catch (SqlException e)
            {
                return BadRequest(e.Message);
            }
        }

        private List<string> GetResult(List<Tuple<decimal, decimal, string>> values)
        {
            var result = new List<string>();

            values.ForEach(x => {
                if(x.Item3 == "dodaj")
                    result.Add(Dodaj(x.Item1, x.Item2));
               if(x.Item3 == "podziel")
                    result.Add(Podziel(x.Item1, x.Item2));
               if(x.Item3 == "odejmij")
                    result.Add(Odejmij(x.Item1, x.Item2));
               if(x.Item3 == "pomnoz")
                    result.Add(Pomnoz(x.Item1, x.Item2));
            });    

            return result;    
        }

        private string Dodaj(decimal value1, decimal value2)
        {
            return value1 + " + " + value2 + " = " + (value1 + value2).ToString();
        }

        private string Podziel(decimal value1, decimal value2)
        {
            return value1 + " / " + value2 + " = " + (value1 / value2).ToString();
        }

        private string Odejmij(decimal value1, decimal value2)
        {
            return value1 + " - " + value2 + " = " + (value1 - value2).ToString();
        }
        private string Pomnoz(decimal value1, decimal value2)
        {
            return value1 + " * " + value2 + " = " + (value1 * value2).ToString();
        }   
    }
}
