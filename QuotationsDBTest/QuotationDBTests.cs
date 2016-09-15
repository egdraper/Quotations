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
            string connectionString = ConfigurationManager.ConnectionStrings["TestDBConnectionStringA"].ConnectionString;
            using (QuotationIndexDB db = new QuotationIndexDB(connectionString))
            {
                List<Quotes> list = db.GetAllQuotes();
                Assert.IsTrue(list.Count > 0);
                foreach (Quotes q in list)
                {
                    Assert.IsFalse(string.IsNullOrEmpty(q.AuthorsFirstName));
                    Console.WriteLine("{0} {1}", q.AuthorsFirstName, q.AuthorsLastName);
                }
            }
        }

        [TestMethod]
        public void ReadSingleQuote()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDBConnectionStringA"].ConnectionString;
            using (QuotationIndexDB db = new QuotationIndexDB(connectionString))
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
                Tags = "Cool, Amazing, Best Class Ever",
                Type = ReferenceType.Artical,
                Url = "https://www.thebestwebsite.com/thecool/artical/isHere.html",
            };
        }

        [TestMethod]
        public void InsertNewQuote()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDBConnectionStringA"].ConnectionString;
            using (QuotationIndexDB db = new QuotationIndexDB(connectionString))
            {

                Quotes q = new Quotes()
                {
                    Id = -1,
                    AuthorsFirstName = "Farmer",
                    AuthorsLastName = "Joe",
                    PublicationsName = "How to Farm",
                    PublishedDate = DateTime.Now,
                    Quote = "If there was a Cow for every farmer, then the world would be out of cows",
                    Tags = "Cow, Farmer, other",
                    Type = ReferenceType.Book,
                    Url = "www.wearefarmersdadadadadadada.com"

                };

                db.InsertNewQuote(q);
                Assert.IsTrue(q.Id > 0);
            }
        }

        [TestMethod]
        public void UpdateQuote()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDBConnectionStringA"].ConnectionString;
            using (QuotationIndexDB db = new QuotationIndexDB(connectionString))
            {
                Quotes q = new Quotes()
                {
                    Id = -1,
                    AuthorsFirstName = "Farmer",
                    AuthorsLastName = "Joe",
                    PublicationsName = "How to Farm",
                    PublishedDate = DateTime.Now,
                    Quote = "If there was a Cow for every farmer, then the world would be out of cows",
                    Tags = "Cow, Farmer, other",
                    Type = ReferenceType.Book,
                    Url = "www.wearefarmersdadadadadadada.com"

                };

                db.InsertNewQuote(q);
                Assert.IsTrue(q.Id > 0);

                q.AuthorsFirstName = "Farmer1";
                q.AuthorsLastName = "Joe1";
                q.PublicationsName = "How to Farm1";
                q.PublishedDate = DateTime.Now;
                q.Quote = "If there was a Cow for every farmer, then the world would be out of cows1";
                q.Tags = "Cow, Farmer, other1";
                q.Type = ReferenceType.Book;
                q.Url = "www.wearefarmersdadadadadadada.com1";

                db.UpdateQuote(q);
                Quotes q2 = db.GetSingleQuote(q.Id);
                Assert.AreEqual(q.AuthorsFirstName, q2.AuthorsFirstName);
                Assert.AreEqual(q.AuthorsLastName, q2.AuthorsLastName);
                Assert.AreEqual(q.PublicationsName, q2.PublicationsName);
                Assert.AreEqual(q.Quote, q2.Quote);
                Assert.AreEqual(q.Tags, q2.Tags);
                Assert.AreEqual(q.Type, q2.Type);
                Assert.AreEqual(q.Url, q2.Url);
            }


        }

        [TestMethod]
        public void DeleteQuote()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDBConnectionStringA"].ConnectionString;
            using (QuotationIndexDB db = new QuotationIndexDB(connectionString))
            {
                Quotes q = new Quotes()
                {
                    Id = -1,
                    AuthorsFirstName = "Farmer",
                    AuthorsLastName = "Joe",
                    PublicationsName = "How to Farm",
                    PublishedDate = DateTime.Now,
                    Quote = "If there was a Cow for every farmer, then the world would be out of cows",
                    Tags = "Cow, Farmer, other",
                    Type = ReferenceType.Book,
                    Url = "www.wearefarmersdadadadadadada.com"

                };

                db.InsertNewQuote(q);
                Assert.IsTrue(q.Id > 0);

                db.DeleteQuote(q);
                Quotes q2 = db.GetSingleQuote(q.Id);
                Assert.IsTrue(q2 == null);
            }
        }
    }
}
