using DCR.ViewModel.ViewModel;
using DCRHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace DCRConsumeWebApi.Controllers
{
    public class HomeController : Controller
    {
        ApiCall apiCall = new ApiCall();



        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Dashboard(MenuListViewModel model)
        {
            //string data = JsonConvert.SerializeObject(model);

            //string responseContent = await apiCall.consumeapi(data, "/MenuList/GetMenuLists");

            //return Json(responseContent);


            string data = JsonConvert.SerializeObject(model);

            // Check if the data is already stored in the session
            if (HttpContext.Session.GetString("MenuListData") != null)
            {
                // If data is found in the session, use that data
                string storedData = HttpContext.Session.GetString("MenuListData");
                return Json(storedData);
            }

            // If data is not found in the session, make an API call and save the response in the session
            string responseContent = await apiCall.consumeapi(data,"/MenuList/GetMenuLists");

            // Save the data in the session
            HttpContext.Session.SetString("MenuListData", responseContent);

            return Json(responseContent);


        }


       




    }
}