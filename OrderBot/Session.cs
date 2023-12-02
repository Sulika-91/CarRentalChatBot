using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            INTRESTED, PREFER, RESERVATION,RENTED,STATUS,OUTPUT
        }

        private State nCur = State.INTRESTED;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            //this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.INTRESTED:
                    aMessages.Add("Welcome to car rental.");
                    aMessages.Add("What type of car are you interested in? \n1: Sedan \n 2: SUV");
                    this.nCur = State.PREFER;
                    break;
                case State.PREFER:
                    this.oOrder.Intrested = sInMessage;
                    aMessages.Add("Which transmission type do you prefer? \n1: Automatic\n2: Manual");
                    this.nCur = State.RESERVATION;
                    break;

                case State.RESERVATION:
                    this.oOrder.Prefer = sInMessage;
                    aMessages.Add("Can I modify or cancel my reservation? \n1: Yes, you can modify or cancel within a certain timeframe.\n 2: Sorry, once booked, modifications or cancellations may not be allowed.");
                    this.nCur = State.RENTED;
                    break;
                case State.RENTED:
                    this.oOrder.Reservation = sInMessage;
                    aMessages.Add("How can I rate the car I rented? \n 1: You can rate the car on our website or app.\n 2: Ratings can be given through a follow-up email.");
                    this.nCur = State.STATUS;
                    break;
                case State.STATUS:
                    this.oOrder.Rented = sInMessage;
                    aMessages.Add("Are you want to cancel the reservation? \n 1: yes \n 2: no");
                    this.nCur = State.OUTPUT;
                    break;
                case State.OUTPUT:
                    this.oOrder.Status = sInMessage;
                    this.oOrder.Save();
                    string msg = "Your Reservation is Completed.";
                    if(this.oOrder.Status == "1")
                    {
                        msg = "Your Reservation is Cancled..";
                    }

                    aMessages.Add(msg);
                    break;



                    //case State.SIZE:
                    //    //this.oOrder.Size = sInMessage;
                    //    //this.oOrder.Save();
                    //    aMessages.Add("What protein would you like on this  " + this.oOrder.Size + " Shawarama?");
                    //    this.nCur = State.PROTEIN;
                    //    break;
                    //case State.PROTEIN:
                    //    string sProtein = sInMessage;
                    //    aMessages.Add("What toppings would you like on this (1. pickles 2. Tzaki) " + this.oOrder.Size + " " + sProtein + " Shawarama?");
                    //    break;



            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}
