using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Formatting = Newtonsoft.Json.Formatting;

namespace ServerCode
{
    class DictionaryExample
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("1", "Akshay");
            dic.Add("2", "nitya");
            dic.Add("3", "Raghvan");
            dic.Add("4", "Milind");
            dic.Add("5", null);
            dic.Add("6", "Mahesh");
            dic.Add("10", "lokesh");
            dic.Add("7", "dipika");
            dic.Add("8", "main");
            dic.Add("9", "naresh");
            Console.WriteLine();
            string json = JsonConvert.SerializeObject(dic, Formatting.Indented);
            Console.WriteLine(json);
            File.WriteAllText(@"F:\Exam 10Th\Exam biz Runtime\Exam biz Runtime\JsonDic.json", json);


            Dictionary<string, string> htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            IEnumerable<String> key1 = htmlAttributes.Keys;
            foreach (string keyeys in key1)
            {
                if(dic[keyeys] != null)
                Console.WriteLine(keyeys + " : " + dic[keyeys]);
            }
            Console.WriteLine("Enter which Operation do u want Perform \n  1.Delete 2.update 4.Exit");
            int s1 = Convert.ToInt32(Console.ReadLine());
            if (s1 == 1)
            {
                try
                {
                    Console.WriteLine("Enter Key to delete");
                    string data = Console.ReadLine();
                    dic.Remove(data);
                    //htmlAttributes.Remove(data);
                    htmlAttributes = dic;
                    IEnumerable<String> key2 = htmlAttributes.Keys;
                    foreach (string keyeys in key2)
                    {
                        if (dic[keyeys] != null)
                            Console.WriteLine(keyeys + " : " + dic[keyeys]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else if (s1 == 2)
            {

                Console.WriteLine("enter name");
                string chitti = Console.ReadLine();
                dic["5"] = chitti;
                IEnumerable<String> key2 = htmlAttributes.Keys;
                foreach (string keyeys in key2)
                {
                    Console.WriteLine(keyeys + " : " + dic[keyeys]);
                }
            }
            else if (s1 == 3)
            {
                return;
            }
        }
    }
}
