using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_biz_Runtime
{
    class InsertValues
    {
        static void Main(string[] args)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("data source=.; database=NareshReddy; integrated security=SSPI");

                SqlCommand cm = new SqlCommand("insert into Industry(IndustryId,IndustriesData)values('91','Industry:SoftWare,CompanyName:bizRuntime,HordWare:CelleconIndia')", con);
                SqlCommand cm1 = new SqlCommand("insert into Industry(IndustryId,IndustriesData)values('92','Industry:SoftWare,CompanyName:wipro,HordWare:IndustrialsCompanyUSA')", con);
                SqlCommand cm2 = new SqlCommand("insert into Industry(IndustryId,IndustriesData)values('93','Industry:SoftWare,CompanyName:tcs,HordWare:IndustrialsBankack')", con);
                SqlCommand cm3 = new SqlCommand("insert into Industry(IndustryId,IndustriesData)values('94','Industry:SoftWare,CompanyName:hcl,HordWare:IndustrialsCenada')", con);

                con.Open();

                cm.ExecuteNonQuery();
                cm1.ExecuteNonQuery();
                cm2.ExecuteNonQuery();
                cm3.ExecuteNonQuery();
               

                Console.WriteLine("Record Inserted Successfully");
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