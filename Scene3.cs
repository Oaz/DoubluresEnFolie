using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DoubluresEnFolie
{
  [TestFixture]
  public class Scene3
  {
    [Test]
    public void VerifieTemperatureMaximale ()
    {
      var capteur = Doublures.Stub (18.Degres ());
      var ecran = new Doublures.Spy<string> ();
      Thermometre.CalculeTemperatureMaximale (capteur, ecran.Envoie);
      Assert.That (ecran.Valeur, Is.EqualTo ("18 degrés"));
    }
    
    class Thermometre
    { 
      public static void CalculeTemperatureMaximale (IEnumerable<Temperature> capteur, Action<string> afficheSurEcran)
      {
        var valeur = capteur.First ();
        afficheSurEcran (valeur.ToString ());
      }
    }
  }
}

