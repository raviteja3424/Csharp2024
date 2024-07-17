using System;

class Passenger
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class TicketBookingException : Exception
{
    public TicketBookingException(string message) : base(message)
    {
    }
}

class TrainTicketBooking
{
    public void TicketBooking(int no_of_tickets)
    {
        if (no_of_tickets > 2)
        {
            throw new TicketBookingException("Cannot book more than 2 tickets at a time.");
        }
        else
        {
            Console.WriteLine("Ticket Booked Successfully.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TrainTicketBooking ticketBooking = new TrainTicketBooking();

        Console.Write("Enter passenger name: ");
        string name = Console.ReadLine();

        Console.Write("Enter passenger age: ");
        int age = int.Parse(Console.ReadLine());

        Passenger passenger = new Passenger
        {
            Name = name,
            Age = age
        };

        Console.Write("Enter number of tickets to book: ");
        int no_of_tickets = int.Parse(Console.ReadLine());

        try
        {
            ticketBooking.TicketBooking(no_of_tickets);
        }
        catch (TicketBookingException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadKey();
    }
}
