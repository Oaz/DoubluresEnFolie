using System;

namespace DoubluresEnFolie
{
  public static class Unites
  {
    public static Temperature Degres (this int valeur)
    {
      return new Temperature (valeur);
    }
    
    public static TimeSpan Secondes (this int valeur)
    {
      return new TimeSpan (0, 0, valeur);
    }
  }
  
  public struct Temperature : IComparable
  {
    public Temperature (decimal valeur)
    {
      Valeur = valeur;
    }
    
    public readonly decimal Valeur;
    
    public override string ToString ()
    {
      return string.Format ("{0} degrés", Valeur);
    }

    public int CompareTo (object obj)
    {
      return Valeur.CompareTo (((Temperature)obj).Valeur);
    }
  }
}

