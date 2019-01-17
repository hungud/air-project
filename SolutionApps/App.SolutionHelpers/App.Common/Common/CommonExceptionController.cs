using System;

namespace App.Common.Application
{
    public static class CommonExceptionController
    {
        public static string ExceptionHandler(Exception exception)
        {
            string message=string.Empty;
            //if (exception is System.Data.OracleClient.OracleException)
            //{
            //    message = "0###Error during database opertaions---" + exception.Message;
            //}
            //else if (exception is System.InvalidCastException)
            if (exception is System.InvalidCastException)
            {
                message = "0###Invalid cast operation---" + exception.Message;
            }
            else if (exception is System.ArgumentException)
            {
                message = "0###Arguement Exception---" + exception.Message;
            }
            else if (exception is System.ArgumentNullException)
            {
                message = "0###Null arguements being passed---" + exception.Message;
            }
            else if (exception is System.ArgumentOutOfRangeException)
            {
                message = "0###Arguements out of range---" + exception.Message;
            }
            else if (exception is System.ArithmeticException)
            {
                message = "0###Error during arithmentic operation---" + exception.Message;
            }
            else if (exception is System.DivideByZeroException)
            {
                message = "0###Divide by zero---" + exception.Message;
            }
            else if (exception is System.DllNotFoundException)
            {
                message = "0###Requested dll can not be found---" + exception.Message;
            }
            else if (exception is System.FormatException)
            {
                message = "0###Invalid Format---" + exception.Message;
            }
            else if (exception is System.IndexOutOfRangeException)
            {
                message = "0###Index out of range---" + exception.Message;
            }
            else if (exception is System.OutOfMemoryException)
            {
                message = "0###Out of memory---" + exception.Message;
            }
            else if (exception is System.TimeoutException)
            {
                message = "0###Invalid cast operation---" + exception.Message;
            }
            else if (exception is System.UnauthorizedAccessException)
            {
                message = "0###Unauthorized access---" + exception.Message;
            }
            else if (exception is System.StackOverflowException)
            {
                message = "0###Stack overflow---" + exception.Message;
            }
            else 
            {
                message = "0###" + exception.Message;
            }
         return message;   
        }
       
    }
}
