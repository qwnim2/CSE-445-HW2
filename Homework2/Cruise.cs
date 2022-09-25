using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Homework2
{
    public class Cruise
    {
        static Random rng = new Random(); // To generate random numbers
        public static event priceCutEvent priceCut; // Link event to delegate
        // Initialize price cut counts to 0. max 20.
        private static Int32 priceCutCounter = 0;
        private static Int32 MAX_PRICE_CUT = 20;
        // Initialize ticket price
        private static Int32 ticketPrice = 200;
        public int getPrice()
        {
            return ticketPrice;
        }
        public static void changePrice(Int32 price)
        {
            if (price < ticketPrice) // a price cut
            {
                if (priceCut != null) // there is at least a subscriber 
                {
                    priceCut(price); // emit event to subscribers
                }
                ticketPrice = price;
                priceCutCounter += 1; // price cut count + 1
            }
            else
            {
                ticketPrice = price;
            }
        }

        private Int32 PricingModel()
        {
            Int32 p = rng.Next(40, 200); // randomly generate the price between 40, 200
            return p;
        }
        public void cruiseFunc()
        {
            while (priceCutCounter < MAX_PRICE_CUT) // cruise thread still alive
            {
                Thread.Sleep(500);
                Int32 p = PricingModel();           // get the new price
                Console.WriteLine("New Prcie is {0} by Cruise", p);
                Cruise.changePrice(p);
            }
            myApplication.cruiseThreadAlive = false;
        }
        public void checkOrder() // event handler 
        {
            OrderClass OrderObject = myApplication.mcBuffer.getOneCell();   // get written cell from buffer
            Thread thread = new Thread(()=>OrderProcessing.orderProcessingFunc(OrderObject, getPrice())); // create a new thread to process
            thread.Start();
        }

    }
}
