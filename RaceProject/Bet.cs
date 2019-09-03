using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceProject
{
    public class Bet
    {
        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        // coding for dog number//
        private int _dogNumber;

        public int DogNumber
        {
            get { return _dogNumber; }
            set { _dogNumber = value; }
        }

        private Guy _bettor;

        public Guy Bettor
        {
            get { return _bettor; }
            set { _bettor = value; }
        }

        public string GetDescription()
        {
            if (this._amount == 0) // mean initially user doesnot have any bucks or bet placed
                return this._bettor.Name + " hasn't placed any bet";
            else // else return what he placed and on what dog
                return this._bettor.Name + " placed $ " + this._bettor.MyBet._amount.ToString() + " on dog # " + this._bettor.MyBet.DogNumber.ToString();
        }
        // winning coding of dogs//

        public int Payout(int winningDogNo)
        {
            if (this._bettor.MyBet.DogNumber == winningDogNo)
                return this._amount;
            else
                return -this._amount;
        }
    }
}
