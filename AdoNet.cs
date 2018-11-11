using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_biz_Runtime
{
    class AdoNet
    {

        static void Main(string[] args)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("data source=.; database=NareshReddy; integrated security=SSPI");

                SqlCommand cm = new SqlCommand("create table Industry(IndustryId int unique,IndustriesData varchar(1000))", con);
                con.Open();
                cm.ExecuteNonQuery();
                Console.WriteLine("Indrustry Table created Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            finally
            {
                con.Close();
            }
        }
    }
}

