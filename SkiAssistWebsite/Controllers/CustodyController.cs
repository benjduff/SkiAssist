using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SkiAssistWebsite.Helper;
using SkiAssistWebsite.ViewModels;

namespace SkiAssistWebsite.Controllers
{
    public class CustodyController : Controller
    {
        // GET: Custody
        public ActionResult NewCustodyType()
        {
            var custodyTypes = HttpHelper.GetList<string>("api/custodytypes");
            var custodyTypeVm = new CustodyTypeViewModel()
            {
                custodyTypeList = custodyTypes.Result.ToList()
            };

            return View(custodyTypeVm);
        }

        public ActionResult PostCustodyType(CustodyTypeViewModel custodyTypeVm)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCustodyType", custodyTypeVm);
            }
            var custodyEntry = new { custodyType1 = custodyTypeVm.custodyType1 };
            var custodyAsJson = JsonConvert.SerializeObject(custodyEntry);
            var response = HttpHelper.Post(custodyAsJson, "api/custodytypes");
            Thread.Sleep(1000);

            return RedirectToAction("NewCustodyType", "Custody");
        }

        public ActionResult DeleteCustodyType(string custodyType)
        {
            //Set vars
            //Serialize as Json
            var custodyAsJson = JsonConvert.SerializeObject(custodyType);
            var response = HttpHelper.
            //Call HttpHelper.Delete (Needs to be created)
            //Thread.Sleep(1000);

            return RedirectToAction("NewCustodyType", "Custody");
        }
    }
}