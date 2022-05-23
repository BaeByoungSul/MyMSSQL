using System;
using System.Collections.Generic;
using System.Text;

namespace MyMssql.Model
{
    public class Note
    {
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string NoteText { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
