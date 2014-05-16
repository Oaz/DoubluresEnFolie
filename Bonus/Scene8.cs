using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;

namespace DoubluresEnFolie.Bonus
{
  [TestFixture]
  public class Scene8
  {
    IEnumerable<Temperature> capteur_;
    Doublures.Spy<string> ecran_;
    
    [SetUp]
    public void Init ()
    {
      capteur_ = Doublures.Stub (18.Degres (), 25.Degres (), 10.Degres ());
      ecran_ = new Doublures.Spy<string> ();
    }
    
    [Test, TestCaseSource("CasTemperatureMaximale")]
    public void VerifieTemperatureMaximale (int nombreDeLectures, string affichageAttendu)
    {
      var chronometre = Doublures.Fake<TimeSpan> (
        nombreDeLectures,
        () => {
        Assert.That (ecran_.Valeur, Is.EqualTo (affichageAttendu)); }
      );
      Thermometre.CalculeTemperatureMaximale (capteur_, ecran_.Envoie, chronometre);
    }
    
    static object[] CasTemperatureMaximale =
    {
      new object[] { 1, "18 degrés" },
      new object[] { 2, "25 degrés" },
      new object[] { 3, "25 degrés" }
    };
    
    public class Thermometre
    {
      public static void CalculeTemperatureMaximale (
        IEnumerable<Temperature> capteur,
        Action<string> afficheSurEcran,
        Action<TimeSpan> attends
      )
      {
        var maximumObservé = MonoideMax<Temperature>.Inconnu;
        foreach (var temperature in capteur) {
          maximumObservé = maximumObservé * temperature;
          afficheSurEcran (maximumObservé.ToString ());
          attends (10.Secondes ());
        }
      }
    }  
    
  
  }
}

