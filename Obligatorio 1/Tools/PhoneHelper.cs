using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;

namespace Tools
{
    public static class PhoneHelper
    {
        public static string GetPhoneWithCorrectFormat(string phone)
        {
            bool error = true;
            if (string.IsNullOrEmpty(phone))
                throw new Exception("El teléfono no puede estar vacío");
            var newNumber = "005989";
            var numArr = phone.ToCharArray();
            if (numArr.Length == 9)
            {//09x
                if (numArr[0].Equals('0') && numArr[1].Equals('9'))
                {
                    for (int i = 2; i < numArr.Length; i++)
                    {
                        newNumber += numArr[i];
                    }
                    error = false;
                }
            }
            else if (numArr.Length == 13)
            {//00598
                if (numArr[0].Equals('0') && numArr[1].Equals('0') && numArr[2].Equals('5') && numArr[3].Equals('9') && numArr[4].Equals('8') && numArr[5].Equals('9'))
                {
                    for (int i = 6; i < numArr.Length; i++)
                    {
                        newNumber += numArr[i];
                    }
                    error = false;
                }
            }
            else if (numArr.Length == 12)
            {//+598
                if (numArr[0].Equals('+') && numArr[1].Equals('5') && numArr[2].Equals('9') && numArr[3].Equals('8') && numArr[4].Equals('9'))
                {
                    for (int i = 5; i < numArr.Length; i++)
                    {
                        newNumber += numArr[i];
                    }
                    error = false;
                }
            }
            if (error == true)
            {
                throw new WrongNumberFormatException ("El teléfono ingresado es incorrecto");
            }
            return newNumber;

        }
    }
}
