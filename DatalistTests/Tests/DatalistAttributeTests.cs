﻿using Datalist;
using NUnit.Framework;
using System;

namespace DatalistTests.Tests
{
    [TestFixture]
    public class DatalistAttributeTests
    {
        #region Constructor: DatalistAttribute(Type type)

        [Test]
        public void DatalistAttribute_NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new DatalistAttribute(null));
        }

        [Test]
        public void DatalistAttribute_UnassignableTypeThrows()
        {
            Assert.Throws<ArgumentException>(() => new DatalistAttribute(typeof(Object)));
        }

        [Test]
        public void DatalistAttribute_SetsType()
        {
            Type expected = typeof(AbstractDatalist);
            Assert.AreEqual(expected, new DatalistAttribute(expected).Type);
        }

        #endregion
    }
}
