﻿using Datalist.Tests.Objects.Models;
using Datalist.Tests.Objects.Stubs;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.IO;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Datalist.Tests.Unit
{
    [TestFixture]
    public class DatalistExtensionsTests
    {
        private HtmlHelper<TestModel> html;
        private TestDatalistStub datalist;
        private TestModel testModel;

        [SetUp]
        public virtual void SetUp()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest(null, "http://localhost:7013/", null),
                new HttpResponse(new StringWriter()));

            datalist = new TestDatalistStub();
            html = MockHtmlHelper();
        }

        [TearDown]
        public virtual void TearDown()
        {
            HttpContext.Current = null;
        }

        #region Extension method: AutoComplete<TModel>(this HtmlHelper<TModel> html, String name, Object value, AbstractDatalist model, Object htmlAttributes = null)

        [Test]
        public void AutoComplete_CreatesAutocompleteAndHiddenInput()
        {
            CreatesAutocompleteAndHiddenInput("TestId", html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsIdAttribute()
        {
            AddsIdAttribute("TestId", html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsDatalistClasses()
        {
            AddsDatalistClassesForDatalistInput(html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsSpecifiedClass()
        {
            AddsSpecifiedClass("TestClass", html.AutoComplete("TestId", String.Empty, datalist, new { @class = "TestClass" }));
        }

        [Test]
        public void AutoComplete_AddsHiddenInputAttribute()
        {
            AddsHiddenInputAttribute("TestId", html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsFiltersAttribute()
        {
            datalist.AdditionalFilters.Add("Test1");
            datalist.AdditionalFilters.Add("Test2");

            AddsFiltersAttribute(html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsRecordsPerPageAttribute()
        {
            AddsRecordsPerPageAttribute(html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsSortColumnAttribute()
        {
            AddsSortColumnAttribute(html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsSortOrderAttribute()
        {
            AddsSortOrderAttribute(html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsDialogTitleAttribute()
        {
            AddsDialogTitleAttribute(html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsUrlAttribute()
        {
            AddsUrlAttribute(html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsTermAttribute()
        {
            AddsTermAttribute(html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsPageAttribute()
        {
            AddsPageAttribute(html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsIdForHiddenInput()
        {
            AddsIdForHiddenInput("TestId", html.AutoComplete("TestId", String.Empty, datalist));
        }

        [Test]
        public void AutoComplete_AddsValueForHiddenInput()
        {
            AddsValueForHiddenInput("TestValue", html.AutoComplete("TestId", "TestValue", datalist));
        }

        [Test]
        public void AutoComplete_AddsDatalistClassesForHiddenInput()
        {
            AddsDatalistClassesForHiddenInput(html.AutoComplete("Id", String.Empty, datalist));
        }

        #endregion

        #region Extension method: AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes = null)

        [Test]
        public void AutoCompleteFor_WithoutModel_MissingAttributeThrows()
        {
            Assert.Throws<DatalistException>(() =>
                CreatesAutocompleteAndHiddenInputFromExpression(
                    model => model.Id,
                    html.AutoCompleteFor(model => model.Id)));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_CreatesAutocompleteAndHiddenInputFromExpression()
        {
            CreatesAutocompleteAndHiddenInputFromExpression(model => model.ParentId, html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsIdAttributeFromExpression()
        {
            AddsIdAttributeFromExpression(model => model.ParentId, html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsDatalistClasses()
        {
            AddsDatalistClassesForDatalistInput(html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsSpecifiedClass()
        {
            AddsSpecifiedClass("TestClass", html.AutoCompleteFor(model => model.ParentId, new { @class = "TestClass" }));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsHiddenInputAttributeFromExpression()
        {
            AddsHiddenInputAttributeFromExpression(model => model.ParentId, html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsFiltersAttribute()
        {
            AddsFiltersAttribute(html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsRecordsPerPageAttribute()
        {
            AddsRecordsPerPageAttribute(html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsSortColumnAttribute()
        {
            AddsSortColumnAttribute(html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsSortOrderAttribute()
        {
            AddsSortOrderAttribute(html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsDialogTitleAttribute()
        {
            AddsDialogTitleAttribute(html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsUrlAttribute()
        {
            AddsUrlAttribute(html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsTermAttribute()
        {
            AddsTermAttribute(html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsPageAttribute()
        {
            AddsPageAttribute(html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsIdForHiddenInputFromExpression()
        {
            AddsIdForHiddenInputFromExpression(model => model.ParentId, html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsValueForHiddenInput()
        {
            testModel.ParentId = "TestValue";

            AddsValueForHiddenInput(testModel.ParentId, html.AutoCompleteFor(model => model.ParentId));
        }

        [Test]
        public void AutoCompleteFor_WithoutModel_AddsDatalistClassesForHiddenInput()
        {
            AddsDatalistClassesForHiddenInput(html.AutoCompleteFor(model => model.ParentId));
        }

        #endregion

        #region Extension method: AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, AbstractDatalist model, Object htmlAttributes = null)

        [Test]
        public void AutoCompleteFor_CreatesAutocompleteAndHiddenInputFromExpression()
        {
            Expression<Func<TestModel, String>> expression = model => model.FirstRelationModel.Value;

            CreatesAutocompleteAndHiddenInputFromExpression(expression, html.AutoCompleteFor(expression, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsIdAttributeFromExpression()
        {
            Expression<Func<TestModel, String>> expression = model => model.FirstRelationModel.Value;

            AddsIdAttributeFromExpression(expression, html.AutoCompleteFor(expression, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsDatalistClasses()
        {
            AddsDatalistClassesForDatalistInput(html.AutoCompleteFor(model => model.Id, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsSpecifiedClass()
        {
            AddsSpecifiedClass("TestClass", html.AutoCompleteFor(model => model.Id, datalist, new { @class = "TestClass" }));
        }

        [Test]
        public void AutoCompleteFor_AddsHiddenInputAttributeFromExpression()
        {
            Expression<Func<TestModel, String>> expression = model => model.FirstRelationModel.Value;

            AddsHiddenInputAttributeFromExpression(expression, html.AutoCompleteFor(expression, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsFiltersAttribute()
        {
            datalist.AdditionalFilters.Add("Test1");
            datalist.AdditionalFilters.Add("Test2");

            AddsFiltersAttribute(html.AutoCompleteFor(model => model.Id, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsRecordsPerPageAttribute()
        {
            AddsRecordsPerPageAttribute(html.AutoCompleteFor(model => model.Id, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsSortColumnAttribute()
        {
            AddsSortColumnAttribute(html.AutoCompleteFor(model => model.Id, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsSortOrderAttribute()
        {
            AddsSortOrderAttribute(html.AutoCompleteFor(model => model.Id, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsDialogTitleAttribute()
        {
            AddsDialogTitleAttribute(html.AutoCompleteFor(model => model.Id, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsUrlAttribute()
        {
            AddsUrlAttribute(html.AutoCompleteFor(model => model.Id, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsTermAttribute()
        {
            AddsTermAttribute(html.AutoCompleteFor(model => model.Id, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsPageAttribute()
        {
            AddsPageAttribute(html.AutoCompleteFor(model => model.Id, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsIdForHiddenInputFromExpression()
        {
            Expression<Func<TestModel, String>> expression = model => model.FirstRelationModel.Value;

            AddsIdForHiddenInputFromExpression(expression, html.AutoCompleteFor(expression, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsValueForHiddenInput()
        {
            testModel.Id = "TestValue";

            AddsValueForHiddenInput(testModel.Id, html.AutoCompleteFor(model => model.Id, datalist));
        }

        [Test]
        public void AutoCompleteFor_AddsDatalistClassesForHiddenInput()
        {
            AddsDatalistClassesForHiddenInput(html.AutoCompleteFor(model => model.Id, datalist));
        }

        #endregion

        #region Extension method: Datalist<TModel>(this HtmlHelper<TModel> html, String name, Object value, AbstractDatalist model, Object htmlAttributes = null)

        [Test]
        public void Datalist_WrapsAutocompleteInInputGroup()
        {
            WrapsAutocompleteInInputGroup(html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_CreatesAutocompleteAndHiddenInput()
        {
            CreatesAutocompleteAndHiddenInput("TestId", html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsIdAttribute()
        {
            AddsIdAttribute("TestId", html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsDatalistClasses()
        {
            AddsDatalistClassesForDatalistInput(html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsSpecifiedClass()
        {
            AddsSpecifiedClass("TestClass", html.Datalist("TestId", String.Empty, datalist, new { @class = "TestClass" }));
        }

        [Test]
        public void Datalist_AddsHiddenInputAttribute()
        {
            AddsHiddenInputAttribute("TestId", html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsFiltersAttribute()
        {
            datalist.AdditionalFilters.Add("Test1");
            datalist.AdditionalFilters.Add("Test2");

            AddsFiltersAttribute(html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsRecordsPerPageAttribute()
        {
            AddsRecordsPerPageAttribute(html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsSortColumnAttribute()
        {
            AddsSortColumnAttribute(html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsSortOrderAttribute()
        {
            AddsSortOrderAttribute(html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsDialogTitleAttribute()
        {
            AddsDialogTitleAttribute(html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsUrlAttribute()
        {
            AddsUrlAttribute(html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsTermAttribute()
        {
            AddsTermAttribute(html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsPageAttribute()
        {
            AddsPageAttribute(html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsIdForHiddenInput()
        {
            AddsIdForHiddenInput("TestId", html.Datalist("TestId", String.Empty, datalist));
        }

        [Test]
        public void Datalist_AddsValueForHiddenInput()
        {
            AddsValueForHiddenInput("TestValue", html.Datalist("TestId", "TestValue", datalist));
        }

        [Test]
        public void Datalist_AddsDatalistClassesForHiddenInput()
        {
            AddsDatalistClassesForHiddenInput(html.Datalist("Id", String.Empty, datalist));
        }

        [Test]
        public void Datalist_CreatesOpenSpan()
        {
            CreatesOpenSpan(html.Datalist("TestId", String.Empty, datalist));
        }

        #endregion

        #region Extension method: DatalistFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes = null)

        [Test]
        public void DatalistFor_WithoutModel_WrapsAutocompleteInInputGroup()
        {
            WrapsAutocompleteInInputGroup(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_MissingAttributeThrows()
        {
            Assert.Throws<DatalistException>(() =>
                CreatesAutocompleteAndHiddenInputFromExpression(
                    model => model.Id,
                    html.DatalistFor(model => model.Id)));
        }

        [Test]
        public void DatalistFor_WithoutModel_CreatesAutocompleteAndHiddenInputFromExpression()
        {
            CreatesAutocompleteAndHiddenInputFromExpression(model => model.ParentId, html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsIdAttributeFromExpression()
        {
            AddsIdAttributeFromExpression(model => model.ParentId, html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsDatalistClasses()
        {
            AddsDatalistClassesForDatalistInput(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsSpecifiedClass()
        {
            AddsSpecifiedClass("TestClass", html.DatalistFor(model => model.ParentId, new { @class = "TestClass" }));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsHiddenInputAttributeFromExpression()
        {
            AddsHiddenInputAttributeFromExpression(model => model.ParentId, html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsFiltersAttribute()
        {
            AddsFiltersAttribute(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsRecordsPerPageAttribute()
        {
            AddsRecordsPerPageAttribute(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsSortColumnAttribute()
        {
            AddsSortColumnAttribute(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsSortOrderAttribute()
        {
            AddsSortOrderAttribute(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsDialogTitleAttribute()
        {
            AddsDialogTitleAttribute(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsUrlAttribute()
        {
            AddsUrlAttribute(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsTermAttribute()
        {
            AddsTermAttribute(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsPageAttribute()
        {
            AddsPageAttribute(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsIdForHiddenInputFromExpression()
        {
            AddsIdForHiddenInputFromExpression(model => model.ParentId, html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsValueForHiddenInput()
        {
            testModel.ParentId = "TestValue";

            AddsValueForHiddenInput(testModel.ParentId, html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_AddsDatalistClassesForHiddenInput()
        {
            AddsDatalistClassesForHiddenInput(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_WithoutModel_CreatesOpenSpan()
        {
            CreatesOpenSpan(html.DatalistFor(model => model.ParentId));
        }

        #endregion

        #region Extension method: DatalistFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, AbstractDatalist model, Object htmlAttributes = null)

        [Test]
        public void DatalistFor_WrapsAutocompleteInInputGroup()
        {
            WrapsAutocompleteInInputGroup(html.DatalistFor(model => model.ParentId));
        }

        [Test]
        public void DatalistFor_CreatesAutocompleteAndHiddenInputFromExpression()
        {
            Expression<Func<TestModel, String>> expression = model => model.FirstRelationModel.Value;

            CreatesAutocompleteAndHiddenInputFromExpression(expression, html.DatalistFor(expression, datalist));
        }

        [Test]
        public void DatalistFor_AddsIdAttributeFromExpression()
        {
            Expression<Func<TestModel, String>> expression = model => model.FirstRelationModel.Value;

            AddsIdAttributeFromExpression(expression, html.DatalistFor(expression, datalist));
        }

        [Test]
        public void DatalistFor_AddsDatalistClasses()
        {
            AddsDatalistClassesForDatalistInput(html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_AddsSpecifiedClass()
        {
            AddsSpecifiedClass("TestClass", html.DatalistFor(model => model.Id, datalist, new { @class = "TestClass" }));
        }

        [Test]
        public void DatalistFor_AddsHiddenInputAttributeFromExpression()
        {
            Expression<Func<TestModel, String>> expression = model => model.FirstRelationModel.Value;

            AddsHiddenInputAttributeFromExpression(expression, html.DatalistFor(expression, datalist));
        }

        [Test]
        public void DatalistFor_AddsFiltersAttribute()
        {
            datalist.AdditionalFilters.Add("Test1");
            datalist.AdditionalFilters.Add("Test2");

            AddsFiltersAttribute(html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_AddsRecordsPerPageAttribute()
        {
            AddsRecordsPerPageAttribute(html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_AddsSortColumnAttribute()
        {
            AddsSortColumnAttribute(html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_AddsSortOrderAttribute()
        {
            AddsSortOrderAttribute(html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_AddsDialogTitleAttribute()
        {
            AddsDialogTitleAttribute(html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_AddsUrlAttribute()
        {
            AddsUrlAttribute(html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_AddsTermAttribute()
        {
            AddsTermAttribute(html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_AddsPageAttribute()
        {
            AddsPageAttribute(html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_AddsIdForHiddenInputFromExpression()
        {
            Expression<Func<TestModel, String>> expression = model => model.FirstRelationModel.Value;

            AddsIdForHiddenInputFromExpression(expression, html.DatalistFor(expression, datalist));
        }

        [Test]
        public void DatalistFor_AddsValueForHiddenInput()
        {
            testModel.Id = "TestValue";

            AddsValueForHiddenInput(testModel.Id, html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_AddsDatalistClassesForHiddenInput()
        {
            AddsDatalistClassesForHiddenInput(html.DatalistFor(model => model.Id, datalist));
        }

        [Test]
        public void DatalistFor_CreatesOpenSpan()
        {
            CreatesOpenSpan(html.DatalistFor(model => model.ParentId, datalist));
        }

        #endregion

        #region Test helpers

        private HtmlHelper<TestModel> MockHtmlHelper()
        {
            ViewDataDictionary<TestModel> viewData = new ViewDataDictionary<TestModel>();
            viewData.Model = new TestModel();
            testModel = viewData.Model;

            Mock<IViewDataContainer> containerMock = new Mock<IViewDataContainer>();
            containerMock.Setup(c => c.ViewData).Returns(viewData);

            Mock<ViewContext> viewContextMock = new Mock<ViewContext> { CallBase = true };
            viewContextMock.Setup(c => c.ViewData).Returns(viewData);
            viewContextMock.Setup(c => c.HttpContext.Items).Returns(new Hashtable());

            return new HtmlHelper<TestModel>(viewContextMock.Object, containerMock.Object);
        }

        private void CreatesAutocompleteAndHiddenInput(String id, Object actual)
        {
            String pattern = String.Format(@"<input(.*) id=""{0}{1}""(.*) /><input(.*) id=""{0}""(.*) />", id, AbstractDatalist.Prefix);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsIdAttribute(String id, Object actual)
        {
            String pattern = String.Format(@"<input(.*) id=""{0}{1}""(.*) />", id, AbstractDatalist.Prefix);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsDatalistClassesForDatalistInput(Object actual)
        {
            String pattern = @"<input(.*) class=""(.*)(form-control datalist-input|datalist-input form-control)(.*)""(.*) />";
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsSpecifiedClass(String classAttribute, Object actual)
        {
            String pattern = String.Format(@"<input(.*) class=""(.*){0}(.*)""(.*) />", classAttribute);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsFiltersAttribute(Object actual)
        {
            String pattern = String.Format(@"<input(.*) data-datalist-filters=""{0}""(.*) />", String.Join(",", datalist.AdditionalFilters));
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsRecordsPerPageAttribute(Object actual)
        {
            String pattern = String.Format(@"<input(.*) data-datalist-records-per-page=""{0}""(.*) />", datalist.DefaultRecordsPerPage);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsSortColumnAttribute(Object actual)
        {
            String pattern = String.Format(@"<input(.*) data-datalist-sort-column=""{0}""(.*) />", datalist.DefaultSortColumn);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsSortOrderAttribute(Object actual)
        {
            String pattern = String.Format(@"<input(.*) data-datalist-sort-order=""{0}""(.*) />", datalist.DefaultSortOrder);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsDialogTitleAttribute(Object actual)
        {
            String pattern = String.Format(@"<input(.*) data-datalist-dialog-title=""{0}""(.*) />", datalist.DialogTitle);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsHiddenInputAttribute(String id, Object actual)
        {
            String pattern = String.Format(@"<input(.*) data-datalist-hidden-input=""{0}""(.*) />", id);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsUrlAttribute(Object actual)
        {
            String pattern = String.Format(@"<input(.*) data-datalist-url=""{0}""(.*) />", datalist.DatalistUrl);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsTermAttribute(Object actual)
        {
            String pattern = @"<input(.*) data-datalist-term=""""(.*) />";
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsPageAttribute(Object actual)
        {
            String pattern = @"<input(.*) data-datalist-page=""0""(.*) />";
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsIdForHiddenInput(String id, Object actual)
        {
            String pattern = String.Format(@"<input(.*) id=""{0}""(.*) />", id);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsValueForHiddenInput(String value, Object actual)
        {
            String pattern = String.Format(@"/><input(.*) value=""{0}""(.*) />", value);
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void AddsDatalistClassesForHiddenInput(Object actual)
        {
            String pattern = @"<input(.*) class=""datalist-hidden-input""(.*) />";
            StringAssert.IsMatch(pattern, actual.ToString());
        }

        private void CreatesAutocompleteAndHiddenInputFromExpression(Expression<Func<TestModel, String>> expression, Object actual)
        {
            String expressionId = TagBuilder.CreateSanitizedId(ExpressionHelper.GetExpressionText(expression));
            CreatesAutocompleteAndHiddenInput(expressionId, actual);
        }
        private void AddsIdAttributeFromExpression(Expression<Func<TestModel, String>> expression, Object actual)
        {
            String expressionId = TagBuilder.CreateSanitizedId(ExpressionHelper.GetExpressionText(expression));
            AddsIdAttribute(expressionId, actual);
        }
        private void AddsHiddenInputAttributeFromExpression(Expression<Func<TestModel, String>> expression, Object actual)
        {
            String expressionId = TagBuilder.CreateSanitizedId(ExpressionHelper.GetExpressionText(expression));
            AddsHiddenInputAttribute(expressionId, actual);
        }
        private void AddsIdForHiddenInputFromExpression(Expression<Func<TestModel, String>> expression, Object actual)
        {
            String expressionId = TagBuilder.CreateSanitizedId(ExpressionHelper.GetExpressionText(expression));
            AddsIdForHiddenInput(expressionId, actual);
        }

        private void WrapsAutocompleteInInputGroup(Object actual)
        {
            String pattern = @"<div class=""input-group"">(.*)</div>";
            StringAssert.IsMatch(pattern, actual.ToString());
        }
        private void CreatesOpenSpan(Object actual)
        {
            String pattern = @"<span class=""datalist-open-span input-group-addon""><span class=""glyphicon glyphicon-search""></span></span>";
            StringAssert.IsMatch(pattern, actual.ToString());
        }

        #endregion
    }
}
