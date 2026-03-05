using System.ComponentModel.Design;
using System.Globalization;

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
    public DateOnly Dato { get; }
    public TimeOnly SlutTidspunkt { get; }

    public TimeOnly StartTidspunkt { get; }

    public Booking(Medarbejder medarbejder,
                   Mødelokale mødelokale,
                   DateOnly dato,
                   TimeOnly startTidspunkt,
                   TimeOnly slutTidspunkt)
    {
        Medarbejder = medarbejder;
        Mødelokale = mødelokale;
        Dato = dato;
        StartTidspunkt = startTidspunkt;
        SlutTidspunkt = slutTidspunkt;
    }
}
public class Kalender
{
    private List<Booking> _bookingListe;
    public Kalender(List<Booking> bookingListe)
    {
        _bookingListe = bookingListe;
    }
    public void OpretBooking(Medarbejder medarbejder, Mødelokale mødelokale, DateOnly dato, TimeOnly startTidspunkt, TimeOnly slutTidspunkt)
    {
        Booking nyBooking = new Booking(medarbejder, mødelokale, dato, startTidspunkt, slutTidspunkt);
        _bookingListe.Add(nyBooking);
    }
    public void SletBooking(Booking booking)
    {
        _bookingListe.Remove(booking);
    }
    public void FlytBooking(Booking booking, DateOnly nyDato, TimeOnly nyStartTidspunkt, TimeOnly nySlutTidspunkt)
    {
        SletBooking(booking);
        OpretBooking(booking.Medarbejder, booking.Mødelokale, nyDato, nyStartTidspunkt, nySlutTidspunkt);
    }
    public void VisBookinger(DateOnly dato)
    {
        Console.WriteLine("Bookinger for " + dato + ":\n");

        int count = _bookingListe.Count(b => b.Dato == dato);
        if (count > 0)
        {
            foreach (var booking in _bookingListe)
            {
                if (booking.Dato == dato)
                {
                    Console.WriteLine("Mødelokale {0} er booket af {1}, fra {2}, til {3}", booking.Mødelokale.Id, booking.Medarbejder.Navn, booking.StartTidspunkt, booking.SlutTidspunkt);
                }

            }
        }
        else
        {
            Console.WriteLine("\nDer er ingen bookinger for denne dato");
        }
    }
}

// Hovedprogrammet i BookingApp
public class BookingApp
{
    
    
    public static void Main()
    {
        List<Medarbejder> medarbejderListe = new List<Medarbejder>();
        List<Mødelokale> mødelokaleListe = new List<Mødelokale>(); // Burde nok være array, da der ikke kommer flere mødelokaler til.
        List<Booking> bookingListe = new List<Booking>();
        Kalender kalender = new Kalender(bookingListe); // Opret en kalender, som indeholder en tom liste af bookinger

        // Put nogle medarbejdere i medarbejderlisten

        medarbejderListe.Add(new Medarbejder("1", "Sofie Møller"));
        medarbejderListe.Add(new Medarbejder("2", "Amir Rahimi"));
        medarbejderListe.Add(new Medarbejder("3", "Jonas Tved"));
        medarbejderListe.Add(new Medarbejder("4", "Louise Falk"));
        medarbejderListe.Add(new Medarbejder("5", "Mette Ates"));
        medarbejderListe.Add(new Medarbejder("6", "Henrik Krøll"));

        // Put de 3 mødelokaler i mødelokale listen

        mødelokaleListe.Add(new Mødelokale("1"));
        mødelokaleListe.Add(new Mødelokale("2"));
        mødelokaleListe.Add(new Mødelokale("3"));

        bool erIgang = true;
        while (erIgang)
        {
            Console.Clear();
            Console.WriteLine("--- HOVEDMENU ---\n");
            Console.WriteLine("1) Vis bookinger");
            Console.WriteLine("2) Opret booking");
            Console.WriteLine("3) Slet booking");
            Console.WriteLine("4) Ændre booking");
            Console.WriteLine("5) Afslut program");
            Console.Write("\nVælg en mulighed: ");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    string header = "Vis booking";
                    Header(header);

                    string promptVisDato = "Indtast dato der skal vises bookinger for, i formatet ddMMyyyy, og tryk enter";
                    DateOnly visDatoInput = HentDato(promptVisDato);

                    Header(header);

                    kalender.VisBookinger(visDatoInput);

                    Vent();
                    break;
                
                case "2":
                    string header2 = "Opret booking";
                    Header(header2);

                    Console.WriteLine("Indtast dit Id-nummer og tryk enter\n");
                    string idInput = Console.ReadLine();
                    Medarbejder medarbejder = medarbejderListe.FirstOrDefault(m => m.Id == idInput);
                    
                    Console.WriteLine("\nIndtast ønsket mødelokale (1, 2 eller 3) og tryk enter:\n");
                    string mødelokaleInput = Console.ReadLine();
                    Mødelokale mødelokale = mødelokaleListe.FirstOrDefault(m => m.Id == mødelokaleInput);
                    
                    string promptBookingDato = "\nIndtast dato for bookingen i formatet ddMMyyyy og tryk enter";
                    DateOnly bookingDato = HentDato(promptBookingDato);
                    
                    Console.WriteLine("\nIndtast starttidspunkt i formatet TTmm og tryk enter\n");
                    string startTidspunktInput = Console.ReadLine();
                    TimeOnly startTidspunkt = TimeOnly.ParseExact(startTidspunktInput, "HHmm", CultureInfo.InvariantCulture);
                    
                    Console.WriteLine("\nIndtast sluttidspunkt i formatet TTmm og tryk enter\n");
                    string slutTidspunktInput = Console.ReadLine();
                    TimeOnly slutTidspunkt = TimeOnly.ParseExact(slutTidspunktInput, "HHmm", CultureInfo.InvariantCulture);
                    
                    kalender.OpretBooking(medarbejder, mødelokale, bookingDato, startTidspunkt, slutTidspunkt);

                    string header3 = "Booking oprettet";
                    Header(header3);

                    Console.WriteLine(medarbejder.Navn + 
                        ", du har booket mødelokale " + mødelokale.Id + 
                        " d. " + bookingDato + 
                        " fra " + startTidspunkt + 
                        " til " + slutTidspunkt);
                    
                    Vent();
                    break;

                case "3":
                    // kalender.SletBooking();
                    break;

                case "4":
                    // kalender.FlytBooking();
                    break;

                case "5":
                    erIgang = false;
                    Console.WriteLine("Programmet afsluttes...");
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg, prøv igen.");
                    break;
            }
        }
    }
    public static DateOnly HentDato(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt + "\n");
            string datoString = Console.ReadLine();
            if (DateOnly.TryParseExact(datoString, "ddMMyyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateOnly dato))
            {
                return dato;
            }
            else
            {
                Console.WriteLine("Ugyldigt datoformat, prøv igen");
            }
        }
    }
    public static void Vent()
    { 
        Console.WriteLine("\nTryk enter for at returnere til hovedmenu");
        Console.ReadLine();
    }

    public static void Header(string header)
    {
        Console.Clear();
        Console.WriteLine(header);
        Console.WriteLine(new string('-', header.Length) + "\n");
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