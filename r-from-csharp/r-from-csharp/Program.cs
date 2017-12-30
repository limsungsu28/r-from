using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Imports needed for R
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using Microsoft.R.Host.Client;

namespace CallR
{
    class Program
    {

        static void Main(string[] args)
        {
            // Init R session
            IRHostSession session = RHostSession.Create("Test");
            Task sessionStartTask = session.StartHostAsync(new RHostSessionCallback());
            sessionStartTask.Wait();

            // Simple output from console
            Console.WriteLine("Arbitrary R code:");
            var result = session.ExecuteAndOutputAsync("Sys.info()");
            result.Wait();
            Console.WriteLine(result.Result.Output);

            // Create R DataFrame
            List<string> colNames = new List<string>(new string[] { "c1", "c2" });
            List<string> rowNames = new List<string>(new string[] { "1", "2", "3", "10" });

            var xx = new object[] { new object[] { 1, 3, 43, 54 }, new object[] { "a", "c", "a", "d" } };
            var list = new List<IReadOnlyCollection<object>>();
            foreach (object o in xx)
            {
                list.Add(o as object[]);
            }

            DataFrame df = new DataFrame(rowNames, colNames, list.AsReadOnly());
            var dtc = session.CreateDataFrameAsync("data", df);
            dtc.Wait();

            // Print data frame in R session
            result = session.ExecuteAndOutputAsync("print(data)");
            result.Wait();
            Console.WriteLine("\nR data frame:");
            Console.WriteLine(result.Result.Output);

            var resultList = session.GetListAsync("list(mean(data$c1), 111)");
            Console.WriteLine("\nList elements returned from R to C#:");
            Console.WriteLine(Convert.ToDouble(resultList.Result[0]) * 100);
            Console.WriteLine(resultList.Result[1]);

            session.StopHostAsync();
            Console.ReadLine();

        }
    }
}
