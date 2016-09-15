using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuotationDB;
using System.Collections.Generic;
using System.Configuration;

namespace QuotationsDBTest
{
    [TestClass]
    public class QuotationDBTests
    {
        [TestMethod]
        public void ReadAllQuotes()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDBConnectionString"].ConnectionString;
            using (QuotationIndexDB db = new QuotationIndexDB()) 
            { 
                List <Quotes> list = db.GetAllQuotes();
                Assert.IsTrue(list.Count > 0);
                foreach(Quotes q in list)
                {
                    Assert.IsFalse(string.IsNullOrEmpty(q.AuthorsFirstName));
                    Console.WriteLine("{0} {1}", q.AuthorsFirstName, q.AuthorsLastName);
                }
            }
        }

        [TestMethod]
        public void ReadSingleQuote()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDBConnectionString"].ConnectionString;
            using (QuotationIndexDB db = new QuotationIndexDB())
            {
                Quotes quote = db.GetSingleQuote(1);
                Assert.IsNotNull(quote);
                Assert.IsFalse(string.IsNullOrEmpty(quote.AuthorsFirstName));
                Console.WriteLine("{0} {1}", quote.AuthorsFirstName, quote.AuthorsLastName);
            }
        }

        [TestMethod]
        public void QuotationDBConstructor()
        {
            Quotes quotes = new Quotes()
            {
                AuthorsFirstName = "Tefirst",
                AuthorsLastName = "daLast",
                PublicationsName = "The Mighty C# Program",
                PublishedDate = DateTime.MaxValue,
                Quote = "This class was so amazing, I cried Every day I missed it",
                Tags = new List<string>() { "Cool", "Amazing", "Best Class Ever" },
                Type = ReferenceType.Artical,
                Url = "https://www.thebestwebsite.com/thecool/artical/isHere.html",
            };
        }
    }
}
