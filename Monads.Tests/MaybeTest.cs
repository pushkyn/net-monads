using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Monads.Tests
{
    [TestClass]
    public class MaybeTest
    {
        private const string TestValue = "It's ok";
        private const string TestKey = "key";

        [TestMethod]
        public void CanClassReturnValueWith()
        {
            var sc = new SomeClass
                         {
                             SomeString = TestValue,
                             SomeValue = 5
                         };

            Assert.AreEqual(sc.ToMaybe().With(x => x.SomeString).Value, TestValue);
        }

        [TestMethod]
        public void CanClassReturnNullWhenClassIsNull()
        {
            SomeClass sc = null;

            Assert.IsNull(sc.ToMaybe().With(x => x.SomeString).Value);
        }

        [TestMethod]
        public void CanClassReturnValueReturn()
        {
            var sc = new SomeClass
            {
                SomeString = TestValue,
                SomeValue = 5
            };

            Assert.AreEqual(sc.ToMaybe().Return(x => x.SomeString, "Test"), TestValue);
        }

        [TestMethod]
        public void CanClassReturnValueWhenClassIsNullReturn()
        {
            var sc = new SomeClass
            {
                SomeString = null,
                SomeValue = 5
            };

            Assert.AreEqual(sc.ToMaybe().With(x => x.SomeString).Return(x => x, TestKey), TestKey);
        }

        [TestMethod]
        public void CanClassSelect()
        {
            var result = from a in "Hello World!".ToMaybe()
                         from c in (new DateTime(2013, 2, 3)).ToMaybe()
                         select a + " " + c.Value.ToShortDateString();

            Assert.AreEqual(result.Value, "Hello World! 03.02.2013");
        }


        public class SomeClass
        {
            public string SomeString { get; set; }
            public int SomeValue { get; set; }
            public Dictionary<string, string> SomeDictionary { get; set; }
        }
    }
}
