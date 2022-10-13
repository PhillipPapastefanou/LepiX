//================================================================
//
// Phillip Papastefanou, Lorenz Fahse; 01-10-2022
// Federal Agency for Nature Conservation (BfN), Germany 
//
//================================================================
using System;
class ParamDataNotFoundException : ApplicationException
{
    string errorMessage;

    public ParamDataNotFoundException(string paramName)
    {
        errorMessage = "Parameter data'" + paramName + "' not found in file";
    }

    public override string ToString()
    {
        return Message;
    }

    public override string Message
    {
        get { return errorMessage; }
    }
}
//------------------------------------------------------------
class ParamTypeNotFoundException : ApplicationException
{
    string errorMessage;

    public ParamTypeNotFoundException(string dataType)
    {
        errorMessage = "Parameter type'" + dataType + "' not found in file.";
    }

    public override string ToString()
    {
        return Message;
    }

    public override string Message
    {
        get { return errorMessage; }
    }
}
//------------------------------------------------------------
class DayDegreesTooLessException : ApplicationException
{
    string errorMessage;

    public DayDegreesTooLessException(int sum)
    {
        errorMessage = "Overall daydegrees: " + sum.ToString() + ". Less than DayDegrees";
    }

    public override string ToString()
    {
        return Message;
    }

    public override string Message
    {
        get { return errorMessage; }
    }
}
//------------------------------------------------------------
class RelativePollenAmountTooLessException : ApplicationException
{
    string errorMessage;

    public RelativePollenAmountTooLessException(double sum)
    {
        errorMessage = "Overall Sum of the pollen amount: " + sum.ToString() + ". Should be 1.0 (100%).";
    }

    public override string ToString()
    {
        return Message;
    }

    public override string Message
    {
        get { return errorMessage; }
    }
}
//------------------------------------------------------------
class InvalidDateExecption : ApplicationException
{
    string errorMessage;

    public InvalidDateExecption(int day, int month)
    {
        errorMessage = String.Format("Invalid Date, Day:{0} Month:{1}.", day, month);
    }

    public override string ToString()
    {
        return Message;
    }

    public override string Message
    {
        get { return errorMessage; }
    }
}
//------------------------------------------------------------
//================================================================
