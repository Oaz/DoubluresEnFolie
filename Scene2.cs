using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DoubluresEnFolie
{
  [TestFixture]
  public class Scene2
  {
    [Test]
    public void VerifieTemperatureMaximale ()
    {
      var capteur = Doublures.Stub (18.Degres ());
      Assert.That (Thermometre.CalculeTemperatureMaximale (capteur), Is.EqualTo (18.Degres ()));
    }
    
    class Thermometre
    {
      public static Temperature CalculeTemperatureMaximale (IEnumerable<Temperature> capteur)
      {
        return capteur.First ();
      }
    }
  }
}

