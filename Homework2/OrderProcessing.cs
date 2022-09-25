using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Homework2
{
    public class OrderProcessing
    {
        public static event orderFinishedEvent orderFinished; // Link event to delegate
        public static void orderProcessingFunc(OrderClass OrderObject, int unitPrice)
        {
            int senderID = OrderObject.getSenderID();
            int cardNo = OrderObject.getCardNo();
            int ticketsNums = OrderObject.getQuantity();
            double TAX = 0.08;
            double locationCharge = 0; // No location charge in my business
        
            if (checkCreditCard(cardNo)==true) // check if the credit card is valid
            {
                double totalPrice = (ticketsNums * unitPrice * (1 + TAX)) + locationCharge;
                orderFinished(senderID, ticketsNums, unitPrice, totalPrice, TAX); // send the event to agent with comfirmation info
            }
            else
            {
                Console.WriteLine("Invalid Card Number!"); // Invalid Card
            }
        }

        private static bool checkCreditCard(int cardNo)
        {
            if(cardNo>=5000 && cardNo <= 7000)
            {
                return true;
            }
            return false;
        }
    }
}
