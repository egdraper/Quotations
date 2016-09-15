using System;
using System.Collections.Generic;

namespace QuotationDB
{
    public enum ReferenceType
    {
        Book,
        Artical,
        WebPost,
        PersonalQuote
    }
    public class Quotes
    {
        public int Id;
        public string AuthorsFirstName;
        public string AuthorsLastName;
        public string PublicationsName;
        public ReferenceType Type;
        public DateTime PublishedDate;
        public string Quote;
        public string Url;
        public string Tags;
    }
}
