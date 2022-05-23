using MyMssql.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMssql.Views
{
    [QueryProperty(nameof(NoteID), nameof(NoteID))]
    public partial class NoteMgt : ContentPage
    {
        NoteService noteService = new NoteService("AZUREDB");
        //NoteService noteService = new NoteService("COMPANY_BBSPC");
        public string NoteID
        {
            set
            {
                LoadNote(value);
            }
        }
        public NoteMgt()
        {
            InitializeComponent();
            BindingContext = new Note();

            btnSave.Clicked += BtnSave_Clicked;
            btnDelete.Clicked += BtnDelete_Clicked;
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Question?", "Are you sure to delete ?", "Yes", "No");
            //Debug.WriteLine("Answer: " + answer);

            if (answer == false)
            {
                return;
            }
            var note = (Note)BindingContext;
            note.LastUpdate = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(note.Title))
            {
                await noteService.DeleteMyNoteAsync(note);
            }

            // Navigate backwards
            await Navigation.PopAsync();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.LastUpdate = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(note.Title))
            {
                await noteService.SaveMyNoteAsync(note);
            }

            // Navigate backwards
            await Navigation.PopAsync();


        }
        async void LoadNote(string noteId)
        {
            try
            {

                int noteID = Convert.ToInt32(noteId);

                Note myNote = await noteService.GetMyNoteAsync(noteID);
                //notehash = myNote.NoteText.GetHashCode();

                BindingContext = myNote;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "Ok");
                Console.WriteLine("Failed to load note.");
            }
        }
    }
}