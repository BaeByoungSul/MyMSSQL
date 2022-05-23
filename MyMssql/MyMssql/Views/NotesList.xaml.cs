using MyMssql.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMssql.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesList : ContentPage
    {
        NoteService noteService = new NoteService("AZUREDB");
//        NoteService noteService = new NoteService("COMPANY_BBSPC");
        public NotesList()
        {
            InitializeComponent();

           // NoteService note = new NoteService();
            //var t = Task.Run(() => note.GetAllNotes());
            //t.Wait();

            //MyNotesAll notesAll = Task.Run( () => note.GetAllNotes()  );  

            //BindingContext = t.Result;

            btnAdd.Clicked += BtnAdd_Clicked;
            lstv1.ItemSelected += Lstv1_ItemSelected;
            //lstv1.SelectionChanged += Lstv1_SelectionChanged;
        }

        private async void Lstv1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Note note = (Note)e.CurrentSelection.FirstOrDefault();

                await Navigation.PushAsync(new NoteMgt() { NoteID = note.NoteID.ToString() });

            }
        }

        private async void Lstv1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null) return;
            Note note = (Note)e.SelectedItem;
            await Navigation.PushAsync(new NoteMgt() { NoteID = note.NoteID.ToString() });
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NoteMgt());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                BindingContext = await noteService.GetAllNotes();
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.ToString(), "Confirm");
               
            }
            
        }
    }
}