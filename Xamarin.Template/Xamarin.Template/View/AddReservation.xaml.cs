using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Entity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Repository;
using Xamarin.Template.ViewModels;

namespace Xamarin.Template.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddReservation : ContentPage
    {
        int idSpace = 0;
        BookingRepository bookingRepository;
        public AddReservation(int _idSpace)
        {
            InitializeComponent();
            idSpace = _idSpace;
            BindingContext = new ReservationViewModel();
            bookingRepository = new BookingRepository();


        }

        private async void Valider_Clicked(object sender, System.EventArgs e)
        {
            Booking b = new Booking
            {
                Date = ((ReservationViewModel)this.BindingContext).Date.Date,
                StartHour = ((ReservationViewModel)this.BindingContext).StartHour.Hours,
                EndHour = ((ReservationViewModel)this.BindingContext).EndHour.Hours,
                SpaceId = idSpace,
                UserId = 2,
                NbAttendees = ((ReservationViewModel)this.BindingContext).NbAttendees,             

            };

            if (await bookingRepository.CreateBooking(b))
            {
                await DisplayAlert("Création de l'espace", "L'espace a bien été créé", "Ok");

                await Navigation.PushAsync(new Reservation(idSpace));
            }
            else
            {
                await DisplayAlert("Création de l'espace", "Échec de la création de la réservation", "Ok");
            }
           ;

            
        }
    }
}