using System;
using System.Threading;

namespace Homework2
{   
    public delegate void priceCutEvent(Int32 pr); // Define a delegate
    public class ChickenFarm
    {
        static Random rng = new Random(); // To generate random numbers
        public static event priceCutEvent priceCut; // Link event to delegate
        private static Int32 chickenPrice = 10;
        public int getPrice()
        {
            return chickenPrice;
        }
        public static void changePrice(Int32 price)
        {
            if (price < chickenPrice) // a price cut
            {
                if (priceCut != null) // there is at least a subscriber 
                {
                    priceCut(price); // emit event to subscribers
                }
                chickenPrice = price;
            }
        }

        public void farmerFunc()
        {
            for (Int32 i = 0; i < 50; i++)
            {
                Thread.Sleep(500);
                // Take the order from the queue of the orders;
                // Decide the price based on the orders;
                Int32 p = rng.Next(5, 10);
                Console.WriteLine("New Prcie is {0}", p);
                ChickenFarm.changePrice(p);
            }
        } 
    }
    public class Retailer
    {
        public void retailerFunc()
        {
            ChickenFarm chicken = new ChickenFarm();
            for (Int32 i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Int32 p = chicken.getPrice();
                Console.WriteLine("Store {0} has everyday low price: ${1} each", Thread.CurrentThread.Name, p); // Thread.CurrentThread.Name print thread name
            }
        }

        public void chickenOnSale(Int32 p) // Event handler
        {   // order chickens from chicken farm - send order into queue
            Console.WriteLine("Store {0} chickens are on sale: as low as ${1} each", Thread.CurrentThread.Name, p); 
            // Thread.CurrentThread.Name cannot print a name
        }
    }

    public class myApplication
    {
        static void Main(string[] args)
        {
            ChickenFarm chicken = new ChickenFarm();
            Thread farmer = new Thread(new ThreadStart(chicken.farmerFunc));
            farmer.Start(); // startone farmer thread
            Retailer chickenstore = new Retailer();
            ChickenFarm.priceCut += new priceCutEvent(chickenstore.chickenOnSale);
            Thread[] retailers = new Thread[3];
            for(int i = 0; i<3; i++)        // N=3 here
            {   // Start N retailer threads
                retailers[i] = new Thread(new ThreadStart(chickenstore.retailerFunc));
                retailers[i].Start();
            }
        }
    }
        
            
    

    
}
