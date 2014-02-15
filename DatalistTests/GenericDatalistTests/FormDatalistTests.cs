﻿using DatalistTests.TestContext.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatalistTests.GenericDatalistTests
{
    [TestClass]
    public class FormDatalistTests : GenericDatalistTests
    {
        [TestMethod]
        public void FilteredRecordsTest()
        {
            var expected = Datalist.Models.Count;
            var actual = Datalist.BaseFormDatalistData(Datalist.Models.AsQueryable()).FilteredRecords;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ColumnsTest()
        {
            var expected = Datalist.Columns;
            var actual = Datalist.BaseFormDatalistData(Datalist.Models.AsQueryable()).Columns;

            Assert.ReferenceEquals(expected, actual);
        }

        [TestMethod]
        public void RowsTest()
        {
            var expected = Datalist.CurrentFilter.RecordsPerPage;
            var actual = Datalist.BaseFormDatalistData(Datalist.Models.AsQueryable()).Rows.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RowModelsTest()
        {
            Datalist.CurrentFilter.Page = 3;
            Datalist.CurrentFilter.RecordsPerPage = 3;
            var expectedModels = Datalist.Models.Skip(9).Take(3);
            Datalist.BaseFormDatalistData(Datalist.Models.AsQueryable());
            var callCount = Math.Min(Datalist.CurrentFilter.RecordsPerPage, Datalist.Models.Count);

            DatalistMock.Protected().Verify("AddId", Times.Exactly(callCount), ItExpr.IsAny<Dictionary<String, String>>(), ItExpr.Is<TestModel>(match => expectedModels.Contains(match)));
        }

        [TestMethod]
        public void AddIdCalledTest()
        {
            Datalist.BaseFormDatalistData(Datalist.Models.AsQueryable());
            var callCount = Math.Min(Datalist.CurrentFilter.RecordsPerPage, Datalist.Models.Count);
            DatalistMock.Protected().Verify("AddId", Times.Exactly(callCount), ItExpr.IsAny<Dictionary<String, String>>(), ItExpr.IsAny<TestModel>());
        }

        [TestMethod]
        public void AddAutocompleteCalledTest()
        {
            Datalist.BaseFormDatalistData(Datalist.Models.AsQueryable());
            var callCount = Math.Min(Datalist.CurrentFilter.RecordsPerPage, Datalist.Models.Count);
            DatalistMock.Protected().Verify("AddAutocomplete", Times.Exactly(callCount), ItExpr.IsAny<Dictionary<String, String>>(), ItExpr.IsAny<TestModel>());
        }

        [TestMethod]
        public void AddColumnsCalledTest()
        {
            Datalist.BaseFormDatalistData(Datalist.Models.AsQueryable());
            var callCount = Math.Min(Datalist.CurrentFilter.RecordsPerPage, Datalist.Models.Count);
            DatalistMock.Protected().Verify("AddColumns", Times.Exactly(callCount), ItExpr.IsAny<Dictionary<String, String>>(), ItExpr.IsAny<TestModel>());
        }

        [TestMethod]
        public void AddAdditionalDataCalledTest()
        {
            Datalist.BaseFormDatalistData(Datalist.Models.AsQueryable());
            var callCount = Math.Min(Datalist.CurrentFilter.RecordsPerPage, Datalist.Models.Count);
            DatalistMock.Protected().Verify("AddAdditionalData", Times.Exactly(callCount), ItExpr.IsAny<Dictionary<String, String>>(), ItExpr.IsAny<TestModel>());
        }
    }
}