﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Datalist.Tests.Objects.Models
{
    public class TestRelationModel
    {
        public const String DisplayValue = "Value of relation";

        public String Id { get; set; }

        [Display(Name = DisplayValue)]
        public String Value { get; set; }

        public String NoValue { get; set; }
    }
}
