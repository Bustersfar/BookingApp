
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
public class Medarbejder
{
    public string Id { get; }
    public string Navn { get; }

    public Medarbejder(string id, string navn)
    {
        Id = id;
        Navn = navn;
    }
}

public class Mødelokale
{
    public string Id { get; }

    public Mødelokale(string id)
    {
        Id = id;
    }
}
public class Booking
{
    public Medarbejder Medarbejder { get; }
    public Mødelokale Mødelokale { get; }
    public DateTime StartTidspunkt { get; }
    public DateTime SlutTidspunkt { get; }

    public Booking(Medarbejder medarbejder,
                   Mødelokale mødelokale,
                   DateTime startTidspunkt,
                   DateTime slutTidspunkt)
    {
        Medarbejder = medarbejder;
        Mødelokale = mødelokale;
        StartTidspunkt = startTidspunkt;
        SlutTidspunkt = slutTidspunkt;
    }
}

// Hovedprogrammet i BookingApp
public class BookingApp
{
    public static void Main()
    {
           
        // Lav 3 lister der kan indeholde objekter af typen Medarbejder, Mødelokale og Booking

            List<Medarbejder> medarbejderListe = new List<Medarbejder>();
            List<Mødelokale> mødelokaleListe = new List<Mødelokale>();
            List<Booking> bookingList = new List<Booking>();

        // Put nogle medarbejdere i medarbejderlisten
        
            medarbejderListe.Add(new Medarbejder("1", "Alice"));
            medarbejderListe.Add(new Medarbejder("2", "Bob"));
            medarbejderListe.Add(new Medarbejder("3", "Charlie"));
            medarbejderListe.Add(new Medarbejder("4", "David"));
            medarbejderListe.Add(new Medarbejder("5", "Eve"));

        // Put de 3 mødelokaler i mødelokale listen
        
            mødelokaleListe.Add(new Mødelokale("1"));
            mødelokaleListe.Add(new Mødelokale("2"));
            mødelokaleListe.Add(new Mødelokale("3"));
            
        // Put nogle bookinger i bookinglisten

            DateTime startTid = new DateTime(2026, 3, 1, 10, 00, 0, DateTimeKind.Local);
            DateTime slutTid = new DateTime(2026, 3, 1, 12, 00, 0, DateTimeKind.Local);
            bookingList.Add(new Booking(medarbejderListe[0], mødelokaleListe[0], startTid, slutTid)); // Mødelokale 1, medarbejder Alice, 1. marts 2026 kl. 10-12
        
        // Udskriv listen af bookinger i formatet "Mødelokale: {0}, Periode: {1}, Dato: {2}, Medarbejder {3}"
            foreach (var booking in bookingList)
            {
                Console.WriteLine("Mødelokale: {0}, er booket af {1}, fra {2}, til {3}", booking.Mødelokale.Id, booking.Medarbejder.Navn, booking.startTidspunkt, booking.slutTidspunkt);
            }
        /* Find index for booking med dato 22/02/2026, periode 1 og mødelokale 3
        date = new DateOnly(2026, 02, 22);
        int index = bookingList.FindIndex(booking => booking.getMeetingRoomId == "3" && booking.getPeriod == "1" && booking.getDate == date);
        Console.WriteLine(index); // skal udskrive 2, da det er den tredje booking i listen (index starter ved 0)

        // Fjern den booking, der blev fundet i forrige trin
        bookingList.RemoveAt(index);
        Console.WriteLine("\nHar fjernet booking nr. " + (index+1) + "\n");
        */
    }
}