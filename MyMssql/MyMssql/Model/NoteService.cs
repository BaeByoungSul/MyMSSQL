using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace MyMssql.Model
{


    public class NoteService
    {
        public string  DBConnectName { get; set; }
        public NoteService(string dBConnectName)
        {
            DBConnectName = dBConnectName;
        }
    
        /// <summary>
        /// List Up All Notes
        /// </summary>
        /// <returns></returns>
        public Task<MyNotesAll> GetAllNotes( )
        {
            try
            {
                SqlCommand dbcmd = new SqlCommand();
                dbcmd.CommandType = System.Data.CommandType.StoredProcedure;
                dbcmd.CommandText = "[USP_NOTES_SEL]";

                DBClient _cli = new DBClient(DBConnectName);
                DataSet ds = _cli.GetDataSet(dbcmd);
                MyNotesAll notesAll = new MyNotesAll();
                notesAll.Notes = MyStatic.ConvertDataTable<Note>(ds.Tables[0]);
                
                return Task.Run(() => notesAll);
                //return notesAll;
            }
            catch (Exception ex)
            {
                
                throw ex;

            }
        }

        public  Task<Note> GetMyNoteAsync(int noteId)
        {
            try
            {
                SqlCommand dbcmd = new SqlCommand();
                dbcmd.CommandType = System.Data.CommandType.StoredProcedure;
                dbcmd.CommandText = "[USP_NOTES_SEL]";

                dbcmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@NoteID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                }).Value = noteId;
               

                DBClient _cli = new DBClient(DBConnectName);
                DataSet ds = _cli.GetDataSet(dbcmd);
                Note note1 = new Note();
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    note1 = new Note()
                    {
                        NoteID = Convert.ToInt32( ds.Tables[0].Rows[0]["NoteID"]),
                        Title = ds.Tables[0].Rows[0]["Title"].ToString(),
                        NoteText = ds.Tables[0].Rows[0]["NoteText"].ToString(),
                        LastUpdate =Convert.ToDateTime( ds.Tables[0].Rows[0]["LastUpdate"])

                    };

                    return Task.Run(() => note1);
                }
                //return Task.Run(() => note1);
                return Task.Run(() => { return note1; });

                //ObservableCollection<Note> notes = new ObservableCollection<Note>();
                //notes = MyStatic.ConvertDataTable<Note>(ds.Tables[0]);
                //return note;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public Task<int> SaveMyNoteAsync(Note note)
        {
            try
            {
                SqlCommand dbcmd = new SqlCommand();
                dbcmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (note.NoteID != 0)
                {
                    dbcmd.CommandText = "[USP_NOTES_UPD]";
                    dbcmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@NoteID",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    }).Value = note.NoteID;
                    dbcmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@Title",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    }).Value = note.Title;
                    dbcmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@NoteText",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    }).Value = note.NoteText;

                }
                else
                {
                    dbcmd.CommandText = "[USP_NOTES_ADD]";
                    dbcmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@Title",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    }).Value = note.Title;
                    dbcmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@NoteText",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    }).Value = note.NoteText;

                }

                DBClient _cli = new DBClient(DBConnectName);
                //_cli.ExecDBCommand(dbcmd);

                return Task.Run(()=>_cli.ExecDBCommand(dbcmd));
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        public Task<int> DeleteMyNoteAsync(Note note)
        {
            try
            {
                SqlCommand dbcmd = new SqlCommand();
                dbcmd.CommandType = System.Data.CommandType.StoredProcedure;

                dbcmd.CommandText = "[USP_NOTES_DEL]";
                dbcmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@NoteID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                }).Value = note.NoteID;
                    


                DBClient _cli = new DBClient(DBConnectName);
                

                return Task.Run(() => _cli.ExecDBCommand(dbcmd));
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }

    /// <summary>
    /// NoteList Page BindingContext
    /// </summary>
    public class MyNotesAll
    {
        private ObservableCollection<Note> notes;
        public ObservableCollection<Note> Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                HeaderValue = "오늘도 열심히 정리해주세요";
                FooterValue = "You have " + Notes.Count + " notes";
            }

        }
        public string HeaderValue { get; private set; }
        public string FooterValue { get; private set; }

    }

}
