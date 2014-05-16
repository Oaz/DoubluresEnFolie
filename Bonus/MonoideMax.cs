using System;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace DoubluresEnFolie.Bonus
{
  public struct MonoideMax<T>
    where T:struct
  {
    public static MonoideMax<T> Inconnu {
      get {
        return new MonoideMax<T> ();
      }
    }
    
    public static implicit operator MonoideMax<T> (T t)
    {
      return new MonoideMax<T> { t_ = t };
    }
    
    public static MonoideMax<T> operator * (MonoideMax<T> mm1, MonoideMax<T> mm2)
    {
      if (!mm1.t_.HasValue)
        return mm2;
      if (!mm2.t_.HasValue)
        return mm1;
      if (Comparer<T>.Default.Compare (mm1.t_.Value, mm2.t_.Value) < 0)
        return mm2;
      return mm1;
    }
    
    public T Resultat {
      get {
        return t_.Value;
      }
    }
    
    public override string ToString ()
    {
      if (t_.HasValue)
        return t_.ToString ();
      else
        return "Inconnu";
    }
    
    private T? t_;
  }
  
  [TestFixture]
  public class MonoideMaxTests
  {
    [Test, TestCaseSource("CasMonoideMaxInt")]
    public void MonoideMaxInt (MonoideMax<int> a, MonoideMax<int> b, MonoideMax<int> max)
    {
      Assert.That (a * b, Is.EqualTo (max));
    }  
    
    static object[] CasMonoideMaxInt =
    {
      new MonoideMax<int>[] { MonoideMax<int>.Inconnu, MonoideMax<int>.Inconnu, MonoideMax<int>.Inconnu },
      new MonoideMax<int>[] { MonoideMax<int>.Inconnu, 3, 3 },
      new MonoideMax<int>[] { 3, MonoideMax<int>.Inconnu, 3 },
      new MonoideMax<int>[] { MonoideMax<int>.Inconnu, 0, 0 },
      new MonoideMax<int>[] { 0, MonoideMax<int>.Inconnu, 0 },
      new MonoideMax<int>[] { MonoideMax<int>.Inconnu, -2, -2 },
      new MonoideMax<int>[] { -2, MonoideMax<int>.Inconnu, -2 },
      new MonoideMax<int>[] { -8, 3, 3 },
      new MonoideMax<int>[] { 3, -8, 3 },
      new MonoideMax<int>[] { 8, 3, 8 },
      new MonoideMax<int>[] { 3, 8, 8 }
    };
        
    [Test, TestCaseSource("CasMonoideTemperatureMaximale")]
    public MonoideMax<Temperature> MonoideTemperatureMaximale (
      MonoideMax<Temperature> tm1,
      MonoideMax<Temperature> tm2
    )
    {
      return tm1 * tm2;
    }  
    
    public static IEnumerable CasMonoideTemperatureMaximale {
      get {
        var inconnu = MonoideMax<Temperature>.Inconnu;
        MonoideMax<Temperature> max18 = 18.Degres ();
        MonoideMax<Temperature> max10 = 10.Degres ();
        yield return new TestCaseData (inconnu, inconnu).Returns (inconnu);
        yield return new TestCaseData (inconnu, max18).Returns (max18);
        yield return new TestCaseData (max18, inconnu).Returns (max18);
        yield return new TestCaseData (max10, max18).Returns (max18);
        yield return new TestCaseData (max18, max10).Returns (max18);
      }
    }
  }
}

