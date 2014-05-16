using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DoubluresEnFolie
{
  [TestFixture]
  public class Scene4
  {
    IEnumerable<Temperature> capteur_;
    Doublures.Spy<string> ecran_;
    
    [SetUp]
    public void Init ()
    {
      capteur_ = Doublures.Stub (18.Degres (), 25.Degres ());
      ecran_ = new Doublures.Spy<string> ();
    }
    
    [Test, TestCaseSource("CasTemperatureMaximale")]
    public void VerifieTemperatureMaximale (int nombreDeLectures, string affichageAttendu)
    {
      Thermometre.CalculeTemperatureMaximale (capteur_, ecran_.Envoie, nombreDeLectures);
      Assert.That (ecran_.Valeur, Is.EqualTo (affichageAttendu));
    }
    
    static object[] CasTemperatureMaximale =
    {
      new object[] { 1, "18 degrés" },
      new object[] { 2, "25 degrés" }
    };
    
    class Thermometre
    {
      public static void CalculeTemperatureMaximale (
        IEnumerable<Temperature> capteur,
        Action<string> afficheSurEcran,
        int nombreDeLectures
      )
      {
        foreach (var temperature in capteur.Take(nombreDeLectures)) {
          afficheSurEcran (temperature.ToString ());
        }
      }
    }
  }
}

