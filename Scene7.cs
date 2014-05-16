using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace DoubluresEnFolie
{
  [TestFixture]
  public class Scene7
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
    
    class Thermometre
    {
      public static void CalculeTemperatureMaximale (
        IEnumerable<Temperature> capteur,
        Action<string> afficheSurEcran,
        Action<TimeSpan> attends
      )
      {
        Temperature? maximumObservé = null;
        foreach (var temperature in capteur) {
          if (maximumObservé.HasValue) {
            if (temperature.Valeur > maximumObservé.Value.Valeur)
              maximumObservé = temperature;
          } else {
            maximumObservé = temperature;
          }
          afficheSurEcran (maximumObservé.ToString ());
          attends (10.Secondes ());
        }
      }
    }
  }
}

