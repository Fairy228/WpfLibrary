using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfLibrary.Logic
{
    class Book
    {
        public Book()
        {

        }

        public int InventaryNumber
        {
            get; set;
        }

        public string Section
        {
            get; set;
        }

        public string SectionNumber
        {
            get; set;
        }

        public string Author
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string AdditionalInformation
        {
            get; set;
        }

        public string HavkinaNumber
        {
            get; set;
        }

        public string Annotation
        {
            get; set;
        }

        public string ReaderId
        {
            get; set;
        }

        public DateTime DateOfIssue
        {
            get; set;
        }
    }
}
