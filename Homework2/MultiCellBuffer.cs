using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Homework2
{
    public class MultiCellBuffer
    {
        private static OrderClass[] bufferCells;
        private int availibility = 2; // only two can read/write cell
        private static Semaphore cruise_sema; // semaphore for cruise
        private static Semaphore agent_sema; // semaphore for agent
        public MultiCellBuffer(Int32 n)
        {
            // constructor to initialize the buffer
            lock (this) // protect the cell from others
            {
                cruise_sema = new Semaphore(n, n);
                agent_sema = new Semaphore(1, 1);
                bufferCells = new OrderClass[n];
                for (int i = 0; i < n; i++)
                {
                    bufferCells[i] = new OrderClass(5, 5, 5, true, true); // initialize all cells
                }
            }

        }

        public void setOneCell(OrderClass order) // agent calls the function to place orders
        {
            agent_sema.WaitOne(); // block the thread until signaled
            lock (this) // protect from others
            {
                while(availibility == 0)
                {
                    Monitor.Wait(this); // wait for the availibility
                }
                for (int i = 0; i < 2; i++)
                {
                    if (bufferCells[i].getAvail() == true) // if the cell can be written
                    {
                        bufferCells[i] = order;
                        availibility--; // minus one availbility until it is processed
                        break;
                    }
                }
                agent_sema.Release(); // release one seat
                Monitor.Pulse(this); // wake another thread up
            }
        }

        public OrderClass getOneCell()
        {
            cruise_sema.WaitOne(); // block the thread until signaled
            OrderClass newOrder = new OrderClass(5, 5, 5, true, true); // initialize the order
            lock(this)
            {
                while (availibility == 2)
                {
                    Monitor.Wait(this);
                }
                for (int i = 0; i < 2; i++)
                {
                    if (bufferCells[i].getAvail() == false && bufferCells[i].getEmpty() == false) // check if they are good to process
                    {
                        newOrder = bufferCells[i]; // assign order to newOrder
                        bufferCells[i].setEmpty(true); // set them as empty to write by others
                        bufferCells[i].setAvail(true); // set them available so won't be processed again
                        availibility++; // the availbility+=1
                        break;
                    }
                }
                cruise_sema.Release(); // release the seat
                Monitor.Pulse(this); // wake another thread
            }
            return newOrder;
        }
    }
}
