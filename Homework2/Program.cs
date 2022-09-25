using System;
using System.Threading;

namespace Homework2
{
    // Define three delegates for Events. PriceCut, PlaceOrder, OrderFinished
    public delegate void priceCutEvent(Int32 pr);
    public delegate void placeOrderEvent();
    public delegate void orderFinishedEvent(Int32 senderID, Int32 ticketsNums, double unitPrice, double totalPrice, double TAX);
    
    public class myApplication
    {
        private static Int32 N_AGENT = 5;   // 5 ticketagents
        private static Int32 BUFFER_SIZE = 2; // MultiCellBufer has 2 cells
        public static Boolean cruiseThreadAlive = true; // Use this to know whether the cruise is alive
        public static MultiCellBuffer mcBuffer;
        static void Main(string[] args)
        {
            // initialize objects
            Cruise ticket = new Cruise();
            Thread cruise = new Thread(new ThreadStart(ticket.cruiseFunc));
            mcBuffer = new MultiCellBuffer(BUFFER_SIZE);
            TicketAgent ticketstore = new TicketAgent();

            cruise.Start(); // start the cruise thread
            
            // bind the event to event handler
            Cruise.priceCut += new priceCutEvent(ticketstore.ticketOnSale);
            TicketAgent.placeOrder += new placeOrderEvent(ticket.checkOrder);
            OrderProcessing.orderFinished += new orderFinishedEvent(ticketstore.getConfirmed);

            Thread[] TicketAgents = new Thread[N_AGENT];
            for(Int32 i = 0; i< N_AGENT; i++)        // N=5 here
            {   // Start N TicketAgent threads
                TicketAgents[i] = new Thread(new ThreadStart(ticketstore.TicketAgentFunc));
                TicketAgents[i].Name = (i + 1).ToString();
                Console.WriteLine(i);
                TicketAgents[i].Start();
            }
        }
    }
}
