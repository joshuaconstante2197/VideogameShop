using System;
using System.Collections.Generic;
using System.Text;

namespace VideogameShop.Library.Services
{
    public class CreditCardValidationService
    {
        public bool Validate(string card)
        {
            var dCard = long.Parse(card);
            int remainder0;
            int remainder1;
            int total;
            int sum1 = 0;
            int sum2 = 0;
            int nextN;

            string type = checkCard(dCard);
            //loop through all the digits
            for (int i = 1; dCard > 0; i++)
            {
                remainder0 = getDigits(dCard);
                //if digit looped is a pair number then
                if (i % 2 == 0)
                {
                    //if digit looped times two is higher than 9 when multiplied but two then separate it an loop through it to make it single digit
                    if ((remainder0 * 2) > 9)
                    {
                        nextN = remainder0 * 2;
                        for (int j = 0; j < 2; j++)
                        {
                            remainder1 = getDigits(nextN);
                            sum1 += remainder1;
                            nextN = nextN / 10;
                        }
                    }
                    //if not just add it to total of pairs
                    else
                    {
                        sum1 += remainder0 * 2;
                    }
                }
                //add to the total of unpairs
                else if (i % 2 != 0)
                {
                    sum2 += remainder0;
                }
                //drop the last digit that was already evaluated
                dCard = dCard / 10;
            }
            total = sum1 + sum2;
            //if last digit of total sum ends on 0 then print type
            if (total % 10 == 0)
            {
                return true;
            }
            //if not print invalid
            else
            {
                return false;
            }
        }

        //function to get the last digit of number passed
        private int getDigits(long d)
        {
            int r = 0;
            r = ((int)(d % 10));
            return r;
        }

        //function to check for type of card
        private string checkCard(long t)
        {

            int count = 0;
            //loop trough the card number until only have two digits
            for (int i = 0; t >= 99; i++)
            {
                t = t / 10;
                count = i;
            }
            string typeOfCard;

            if ((t == 34 || t == 37) && (count == 12))
            {
                typeOfCard = "Amex";
                return typeOfCard;
            }
            else if ((t == 51 || t == 52 || t == 53 || t == 54 || t == 55) && count == 13)
            {
                typeOfCard = "MASTERCARD";
                return typeOfCard;
            }
            //checking if only the firts digit is a four
            else if (((t / 10) == 4 && (count == 10)) || ((t / 10) == 4 && (count == 13)))
            {
                typeOfCard = "VISA";
                return typeOfCard;
            }
            else
            {
                typeOfCard = "INVALID";
                return typeOfCard;
            }

        }

    }
    
}

