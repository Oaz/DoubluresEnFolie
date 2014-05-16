using System;
using NUnit.Framework;

namespace DoubluresEnFolie
{
  [TestFixture]
  public class Scene1
  {
    [Test]
    public void VerifieTemperatureMaximale ()
    {
      Assert.That (Thermometre.CalculeTemperatureMaximale (), Is.EqualTo (22.Degres ()));
    }
    
    class Thermometre
    {
      public static Temperature CalculeTemperatureMaximale ()
      {
        return 22.Degres ();
      }
    }
  }
}

