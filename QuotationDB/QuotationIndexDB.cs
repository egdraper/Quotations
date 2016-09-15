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
                         ID, AuthorsFirstName, AuthorsLastName, PublicationsName,
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
                         ID, AuthorsFirstName, AuthorsLastName, PublicationsName,
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

        public void InsertNewQuote(Quotes quote)
        {
            var sql = @"INSERT INTO dbo.Quotes
                         (AuthorsFirstName, AuthorsLastName, PublicationsName,
                         Type, PublishedDate, Quote, Url, Tags)
                        VALUES ( @authorsFirstName, @authorsLastName, @publicationsName,
                         @type, @publishedDate, @quote, @url, @tags)

                       SELECT Id = Scope_Identity()";

            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@authorsFirstName", quote.AuthorsFirstName);
                cmd.Parameters.AddWithValue("@authorsLastName", quote.AuthorsLastName);
                cmd.Parameters.AddWithValue("@publicationsName", quote.PublicationsName);
                cmd.Parameters.AddWithValue("@type", quote.Type.ToString());
                cmd.Parameters.AddWithValue("@publishedDate", quote.PublishedDate);
                cmd.Parameters.AddWithValue("@quote", quote.Quote);
                cmd.Parameters.AddWithValue("@url", quote.Url);
                cmd.Parameters.AddWithValue("@tags", quote.Tags);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        quote.Id = Convert.ToInt32(reader["Id"]);
                    }
                }
            }
        }

        public void UpdateQuote(Quotes quote)
        {
            var sql = @"UPDATE dbo.Quotes
                         Set AuthorsFirstName = @authorsFirstName,
                             AuthorsLastName = @authorsLastName,
                             PublicationsName = @publicationsName,
                             Type = @type, 
                             PublishedDate = @publishedDate, 
                             Quote = @quote, 
                             Url = @url, 
                             Tags = @tags
                        WHERE Id = @id";

            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@authorsFirstName", quote.AuthorsFirstName);
                cmd.Parameters.AddWithValue("@authorsLastName", quote.AuthorsLastName);
                cmd.Parameters.AddWithValue("@publicationsName", quote.PublicationsName);
                cmd.Parameters.AddWithValue("@type", quote.Type.ToString());
                cmd.Parameters.AddWithValue("@publishedDate", quote.PublishedDate);
                cmd.Parameters.AddWithValue("@quote", quote.Quote);
                cmd.Parameters.AddWithValue("@url", quote.Url);
                cmd.Parameters.AddWithValue("@tags", quote.Tags);
                cmd.Parameters.AddWithValue("@id", quote.Id);

                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount < 1)
                {
                    throw new Exception("No Rows have been updated in the method UpdateQuotes");
                }
            }
        }

        public void DeleteQuote(Quotes quote)
        {
            var sql = @"DELETE FROM dbo.Quotes WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id", quote.Id);
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount < 1)
                {
                    throw new Exception("No Rows have been updated in the method UpdateQuotes");
                }
            }
        }
    }
}
