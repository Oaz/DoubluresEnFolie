using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DoubluresEnFolie
{
  public class Doublures
  {
    public static IEnumerable<T> Stub<T> (params T[] valeurs)
    {
      return valeurs;
    }    
    
    public class Spy<T>
    {
      public T Valeur { get; private set; }
      public void Envoie (T valeur)
      {
        Valeur = valeur;
      }
    }    

    public static Action<T> Fake<T> (int iterations, Action atTheEnd)
    {
      return t =>
      {
        iterations--;
        if (iterations > 0)
          return;
        atTheEnd ();
        Assert.Pass ();
      };
    }
  }
}

