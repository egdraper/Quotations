using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationDB
{
    public class QuotationIndexDB : IDisposable
    {
        private SqlConnection _conn;
        public QuotationIndexDB(string connectionString)
        {
            _conn = new SqlConnection();
            _conn.ConnectionString = connectionString;
            _conn.Open();
        }

        public void Dispose()
        {
            _conn.Close();
        }

        public List<Quotes> GetAllQuotes()
        {
            var sql = @"SELECT
                         ID, AuthorsFirstName, AuthorsLastName, PublicationName,
                         Type, PublishedDate, Quote, Url, Tags 
                        FROM dbo.Quotes";

            var quoteList = new List<Quotes>();
            using(SqlCommand cmd  = new SqlCommand(sql, _conn))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Quotes quote = new Quotes()
                        {
                            Id = (int)reader["Id"],
                            AuthorsFirstName = (string)reader["AuthorsFirstName"],
                            AuthorsLastName = (string)reader["AuthorsLastName"],
                            PublicationsName = (string)reader["PublicationsName"],
                            PublishedDate = (DateTime)reader["PublishedDate"],
                            Quote = (string)reader["Quote"],
                            Url = (string)reader["Url"],
                            Tags = (string)reader["Tags"],
                            Type = (ReferenceType)Enum.Parse(typeof(ReferenceType), (string)reader["Type"])
                        };

                        quoteList.Add(quote);
                    }
                }
            }
            return quoteList;
        }

        public Quotes GetSingleQuote(int id)
        {
            var sql = @"SELECT
                         ID, AuthorsFirstName, AuthorsLastName, PublicationName,
                         Type, PublishedDate, Quote, Url, Tags 
                        FROM dbo.Quotes 
                        WHERE Id = @id";

            Quotes quote = null;
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        quote = new Quotes()
                        {
                            Id = (int)reader["Id"],
                            AuthorsFirstName = (string)reader["AuthorsFirstName"],
                            AuthorsLastName = (string)reader["AuthorsLastName"],
                            PublicationsName = (string)reader["PublicationsName"],
                            PublishedDate = (DateTime)reader["PublishedDate"],
                            Quote = (string)reader["Quote"],
                            Url = (string)reader["Url"],
                            Tags = (string)reader["Tags"],
                            Type = (ReferenceType)Enum.Parse(typeof(ReferenceType), (string)reader["Type"])
                        };
                    }
                }
            }
            return quote;
        }
    }
}
