using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Homework2
{
    public class OrderClass
    {
        private int senderID; // Thread ID
        private int cardNo; // credit card number
        private int quantity; // nums of ordered tickets
        private bool avail = true; // if available
        private bool empty = true; // if empty
        public OrderClass(int senderID, int cardNo, int quantity, bool avail, bool empty)
        {
            // constructor to initialize info of the order
            this.senderID = senderID;
            this.cardNo = cardNo;
            this.quantity = quantity;
            this.avail = avail;
            this.empty = empty;
        }

        public Int32 getSenderID()
        {
            return senderID;
        }

        public Int32 getCardNo()
        {
            return cardNo;
        }
        public Int32 getQuantity()
        {
            return quantity;
        }
        public bool getAvail()
        {
            return avail;
        }
        public bool getEmpty()
        {
            return empty;
        }
        public void setAvail (bool value)
        {
            avail = value;
        }
        public void setEmpty(bool value)
        {
            empty = value;
        }
    }
}
