using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
  public class Test_BasicUnityExamples
  {
    // A Test behaves as an ordinary method
    [Test]
    public void BasicDivisionTest()
    {
      int x = 10;
      int y = 5;
      int correctAnswer = 2;

      Assert.AreEqual(correctAnswer, x / y);
    }
  }
}
