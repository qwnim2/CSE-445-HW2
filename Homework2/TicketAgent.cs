using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Homework2
{
    public class TicketAgent
    {
        public static event placeOrderEvent placeOrder; // Link event to delegate

        static Random cardRng = new Random(); // To generate random card numbers
        static Random quanRng = new Random(); // To generate random amount numbers
        static Random sleepRng = new Random(); // To generate random sleeping time
        static Random buyRng = new Random(); // To generate random buying possibility
        private static int threshold = 50; // the threshold to determine whether place order
        public void TicketAgentFunc()
        {
            Cruise ticket = new Cruise();
            while(myApplication.cruiseThreadAlive)
            {
                Thread.Sleep(sleepRng.Next(1000,2000)); // Make sure the ticketagent will at least place order every 1~2 secs
                buy();
            }
        }

        public void ticketOnSale(int p) // Event handler
        {   
            if (myApplication.cruiseThreadAlive) // Make sure the cruise still alive, otherwirse, stop
            {
                int possibility = buyRng.Next(1, 100); // randomly decide the possibility 1~100
                Console.WriteLine("Ticket is on sale !!!");
                if (possibility > threshold) { buy(); }  // if pos > 50, then buy, which is 50% buying
            }
        }
        public void buy()
        {
            int senderID = Convert.ToInt32(Thread.CurrentThread.Name); // Thread ID
            int cardNo = cardRng.Next(5000, 7000); // credit card number
            int quantity = quanRng.Next(5, 20); // nums of ordered tickets
            bool avail = false; // if cell available 
            bool empty = false; // if cell empty
            OrderClass OrderObject = new OrderClass(senderID, cardNo, quantity,avail, empty);
 
            Console.WriteLine("Agent{0} is tryig to create order...", senderID);     
            myApplication.mcBuffer.setOneCell(OrderObject); // set one order in buffer
            placeOrder(); // call the cruise to check the order (event)
        }
        public void getConfirmed(int senderID, int ticketsNums, double unitPrice, double totalPrice, double TAX) // Event handler
        {   // get the comfirmation from the orderprocessing
            Console.WriteLine("Thanks for you order, {0}. Total price: {1}. Number of tickets: {2}." +
                "Unit Price: {3}. Tax: {4}", senderID, totalPrice, ticketsNums, unitPrice, TAX);
        }
    }
}
