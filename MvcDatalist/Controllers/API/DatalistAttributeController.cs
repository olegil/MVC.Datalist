﻿using MvcDatalist.Models;
using System.Web.Mvc;

namespace MvcDatalist.Controllers.API
{
    public class DatalistAttributeController : Controller
    {
        #region Properties

        [HttpGet]
        public ActionResult Type()
        {
            return View(new UserModel());
        }

        #endregion
    }
}
