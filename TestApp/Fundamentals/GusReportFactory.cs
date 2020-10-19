using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Fundamentals.Gus
{

    public class ReportTest
    {
        public void Test()
        {
            // P, LP, LF -> Osobowość prawna
            // F -> Działalność fizyczna
            // nieznany -> NotSupportedException

            string type = "P";

            Report report = ReportFactory.Create(type);

            report.Name = "Abc";
        }
    }

    public class ReportFactory
    {
        public static Report Create(string type)
        {
            switch (type)
            {
                case "P":
                case "LP":
                case "LF":
                    return new LegalPersonality();
                case "F":
                    return new SoleTraderReport();
                default:
                    throw new NotSupportedException(type);
            }
        }
    }

    public abstract class Report
    {
        public string Name { get; set; }
    }

    // Działalność fizyczna
    public class SoleTraderReport : Report
    {

    }

    // Osobowość prawna
    public class LegalPersonality : Report
    {

    }

    //public class ReportFactory
    //{
    //    public static Report Create(string type)
    //    {
    //        // P, LP, LF -> Osobowość prawna
    //        // F -> Działalność fizyczna
    //        // nieznany -> NotSupportedException

    //        throw new NotImplementedException();
    //    }
    //}

 
}
