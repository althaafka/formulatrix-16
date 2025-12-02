using System;
namespace Prime.Services;

public class PrimeService
{
    public bool IsPrime(int candidate)
    {
        if(candidate < 2) return false;

        if(candidate % 2 == 0) return false;

        int sqrt = (int)Math.Sqrt(candidate);
        for(int i=3; i<sqrt; i++)
        {
            if(candidate % i == 0) return false;
        }
        return true;
    }
    
}
