﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;

namespace Datalist
{
    public abstract class GenericDatalist<T> : AbstractDatalist where T : class
    {
        protected IEnumerable<PropertyInfo> AttributedProperties
        {
            get
            {
                return typeof(T)
                    .GetProperties()
                    .Where(property => property.GetCustomAttribute<DatalistColumnAttribute>(false) != null)
                    .OrderBy(property => property.GetCustomAttribute<DatalistColumnAttribute>(false).Position);
            }
        }

        protected GenericDatalist()
        {
            foreach (PropertyInfo property in AttributedProperties)
                Columns.Add(GetColumnKey(property), GetColumnHeader(property), GetColumnCssClass(property));
        }
        protected virtual String GetColumnKey(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            DatalistColumnAttribute column = property.GetCustomAttribute<DatalistColumnAttribute>(false);
            if (column != null && column.Relation != null)
                return property.Name + "." + GetColumnKey(GetRelationProperty(property, column.Relation));

            return property.Name;
        }
        protected virtual String GetColumnHeader(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            DatalistColumnAttribute column = property.GetCustomAttribute<DatalistColumnAttribute>(false);
            if (column != null && column.Relation != null)
                return GetColumnHeader(GetRelationProperty(property, column.Relation));

            DisplayAttribute header = property.GetCustomAttribute<DisplayAttribute>(false);
            if (header != null) return header.GetName();
            return property.Name;
        }
        protected virtual String GetColumnCssClass(PropertyInfo property)
        {
            return String.Empty;
        }
        private PropertyInfo GetRelationProperty(PropertyInfo property, String relation)
        {
            PropertyInfo relationProperty = property.PropertyType.GetProperty(relation);
            if (relationProperty != null)
                return relationProperty;

            throw new DatalistException(String.Format("{0}.{1} does not have property named \"{2}\".",
                property.DeclaringType.Name,
                property.Name,
                relation));
        }

        public override DatalistData GetData()
        {
            IQueryable<T> models = GetModels();
            models = Filter(models);
            models = Sort(models);

            return FormDatalistData(models);
        }
        protected abstract IQueryable<T> GetModels();

        private IQueryable<T> Filter(IQueryable<T> models)
        {
            if (CurrentFilter.Id != null)
                return FilterById(models);

            if (CurrentFilter.AdditionalFilters.Count > 0)
                models = FilterByAdditionalFilters(models);

            return FilterBySearchTerm(models);
        }
        protected virtual IQueryable<T> FilterById(IQueryable<T> models)
        {
            PropertyInfo idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new DatalistException(String.Format("Type {0} does not have property named \"Id\".", typeof(T).Name));

            if (idProperty.PropertyType == typeof(String))
                return models.Where("Id = \"" + CurrentFilter.Id + "\"");

            Decimal temp;
            if (IsNumeric(idProperty.PropertyType) && Decimal.TryParse(CurrentFilter.Id, out temp))
                return models.Where("Id = " + CurrentFilter.Id);

            throw new DatalistException(String.Format("{0}.Id can not be filtered by \"{1}\", because of unconvertable types.", typeof(T).Name, CurrentFilter.Id));
        }
        protected virtual IQueryable<T> FilterByAdditionalFilters(IQueryable<T> models)
        {
            List<String> queries = new List<String>();
            foreach (KeyValuePair<String, Object> filter in CurrentFilter.AdditionalFilters.Where(item => item.Value != null))
                queries.Add(FormEqualsQuery(GetType(filter.Key), filter.Key, filter.Value));

            if (queries.Count == 0) return models;
            return models.Where(String.Join(" && ", queries));
        }
        protected virtual IQueryable<T> FilterBySearchTerm(IQueryable<T> models)
        {
            if (CurrentFilter.SearchTerm == null) return models;

            String term = CurrentFilter.SearchTerm.ToLower();
            List<String> queries = new List<String>();

            foreach (String propertyName in Columns.Keys)
                if (GetType(propertyName) == typeof(String))
                    queries.Add(FormContainsQuery(propertyName, term));

            if (queries.Count == 0) return models;
            return models.Where(String.Join(" || ", queries));
        }
        protected virtual IQueryable<T> Sort(IQueryable<T> models)
        {
            String sortColumn = CurrentFilter.SortColumn ?? DefaultSortColumn;
            if (sortColumn != null)
                if (Columns.Keys.Contains(sortColumn))
                    return models.OrderBy(String.Format("{0} {1}", sortColumn, CurrentFilter.SortOrder));
                else
                    throw new DatalistException(String.Format("Datalist does not contain sort column named \"{0}\"", sortColumn));

            if (Columns.Any())
                return models.OrderBy(String.Format("{0} {1}", Columns.First().Key, CurrentFilter.SortOrder));

            throw new DatalistException("Datalist columns can not be empty.");
        }

        protected virtual DatalistData FormDatalistData(IQueryable<T> models)
        {
            DatalistData data = new DatalistData();
            data.FilteredRecords = models.Count();
            data.Columns = Columns;

            IQueryable<T> pagedModels = models
                .Skip(CurrentFilter.Page * CurrentFilter.RecordsPerPage)
                .Take(CurrentFilter.RecordsPerPage);

            foreach (T model in pagedModels)
            {
                Dictionary<String, String> row = new Dictionary<String, String>();
                AddId(row, model);
                AddAutocomplete(row, model);
                AddColumns(row, model);
                AddAdditionalData(row, model);

                data.Rows.Add(row);
            }

            return data;
        }
        protected virtual void AddId(Dictionary<String, String> row, T model)
        {
            row.Add(IdKey, GetValue(model, "Id"));
        }
        protected virtual void AddAutocomplete(Dictionary<String, String> row, T model)
        {
            if (!Columns.Any())
                throw new DatalistException("Datalist columns can not be empty.");

            row.Add(AcKey, GetValue(model, Columns.Keys.First()));
        }
        protected virtual void AddColumns(Dictionary<String, String> row, T model)
        {
            if (!Columns.Any())
                throw new DatalistException("Datalist columns can not be empty.");

            foreach (String column in Columns.Keys)
                AddColumn(row, column, model);
        }
        protected virtual void AddAdditionalData(Dictionary<String, String> row, T model)
        {
        }

        private String FormContainsQuery(String propertyName, String term)
        {
            return String.Format(@"({0} && {1}.ToLower().Contains(""{2}""))", FormNotNullQuery(propertyName), propertyName, term);
        }
        private String FormEqualsQuery(Type type, String propertyName, Object term)
        {
            if (type == typeof(String))
                return String.Format(@"({0} && {1} == ""{2}"")", FormNotNullQuery(propertyName), propertyName, term);

            Decimal number;
            if (IsNumeric(type) && Decimal.TryParse(term.ToString(), out number))
                return String.Format("({0} && {1} == {2})", FormNotNullQuery(propertyName), propertyName, number.ToString().Replace(',', '.'));

            throw new DatalistException(String.Format("{0} type is not supported in dynamic filtering.", type.Name));
        }
        private String FormNotNullQuery(String propertyName)
        {
            List<String> queries = new List<String>();
            String[] properties = propertyName.Split('.');

            for (Int32 i = 0; i < properties.Length; ++i)
                queries.Add(String.Join(".", properties.Take(i + 1)) + " != null");

            return String.Join(" && ", queries);
        }

        private void AddColumn(Dictionary<String, String> row, String column, T model)
        {
            row.Add(column, GetValue(model, column));
        }
        private String GetValue(Object model, String fullPropertyName)
        {
            if (model == null) return String.Empty;

            Type type = model.GetType();
            String[] properties = fullPropertyName.Split('.');
            PropertyInfo property = type.GetProperty(properties[0]);
            if (property == null)
                throw new DatalistException(String.Format("Type {0} does not have property named {1}.", type.Name, properties[0]));

            if (properties.Length > 1)
                return GetValue(property.GetValue(model), String.Join(".", properties.Skip(1)));

            Object value = property.GetValue(model) ?? String.Empty;
            DatalistColumnAttribute datalistColumn = property.GetCustomAttribute<DatalistColumnAttribute>(false);
            if (datalistColumn != null && datalistColumn.Format != null)
                value = String.Format(datalistColumn.Format, value);

            return value.ToString();
        }
        private Type GetType(String fullPropertyName)
        {
            Type type = typeof(T);
            String[] properties = fullPropertyName.Split('.');
            foreach (String propertyName in properties)
            {
                PropertyInfo property = type.GetProperty(propertyName);
                if (property == null)
                    throw new DatalistException(String.Format("Type {0} does not have property named {1}.",
                        type.Name, propertyName));

                type = property.PropertyType;
            }

            return type;
        }
        private Boolean IsNumeric(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type.IsEnum) return false;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
                default:
                    return false;
            }
        }
    }
}
