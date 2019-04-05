using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WebAppAlexey.BLL.Interfaces;
using WebAppAlexey.BLL.ViewModels;

namespace WebAppAlexey.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer customerService;


        public CustomerController(ICustomer customerService)
        {
            this.customerService = customerService;
        }

        [AllowAnonymous]
        [HttpPost, Route("auth/Registration")]
        public IActionResult Registration([FromBody]UserFunctionalyViewModel registrationViewModelPl)
        {
            ResultViewModel resultViewModel = customerService.Registration(registrationViewModelPl);
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.Information);
            }
            return new BadRequestObjectResult("Unknown error");
        }

        [AllowAnonymous]
        [HttpPost, Route("auth/Login")]
        public IActionResult Login([FromBody]UserFunctionalyViewModel user)
        {
            ResultViewModel resultViewModel = customerService.Login(user);
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.Information);
            }
            return new BadRequestObjectResult("Unknown error");

        }

        [Authorize(Roles = "Customer")]
        [HttpPost, Route("customer/AddCarrier")]
        public IActionResult LinkCarrier([FromBody]CarrierViewModel model)
        {
            ResultViewModel resultViewModel = customerService.AddCarrier(User.FindFirstValue(ClaimTypes.Email), model);
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.Information);
            }
            return new BadRequestObjectResult("Unknown error");
        }

        [Authorize(Roles = "Customer")]
        [HttpPost, Route("customer/DeleteCarrier")]
        public IActionResult DenyCarrier([FromBody]CarrierViewModel model)
        {
            ResultViewModel resultViewModel = customerService.DeleteCarrier(User.FindFirstValue(ClaimTypes.Email), model);
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.Information);
            }
            return new BadRequestObjectResult("Unknown error");

        }

        [Authorize(Roles = "Customer")]
        [HttpPost, Route("customer/AddAddress")]
        public IActionResult CreateAddress([FromBody]AdressViewModel model)
        {
            ResultViewModel resultViewModel = customerService.AddAddress(User.FindFirstValue(ClaimTypes.Email), model);
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.Information);
            }
            return new BadRequestObjectResult("Unknown error");
        }

        [Authorize(Roles = "Customer")]
        [HttpPost, Route("customer/EditAddress")]
        public IActionResult EditAddress([FromBody]AdressViewModel model)
        {
            ResultViewModel resultViewModel = customerService.EditAddress(User.FindFirstValue(ClaimTypes.Email), model);
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.Information);
            }
            return new BadRequestObjectResult("Unknown error");
        }

        [Authorize(Roles = "Customer")]
        [HttpPost, Route("customer/DeleteAddress")]
        public IActionResult DeleteAddress([FromBody]AdressViewModel model)
        {
            ResultViewModel resultViewModel = customerService.DeleteAddress(User.FindFirstValue(ClaimTypes.Email), model);
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.Information);
            }
            return new BadRequestObjectResult("Unknown error");
        }

        [Authorize(Roles = "Customer")]
        [HttpGet, Route("customer/MyAddressBook")]
        public IActionResult MyAddresses()
        {
            ResultViewModel resultViewModel = customerService.GetMyAddressBook(User.FindFirstValue(ClaimTypes.Email));
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.DataSet);
            }
            return new BadRequestObjectResult("Unknown error");
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet, Route("customer/GetAvailableCarriers")]
        [Produces(typeof(string))]
        public IActionResult GetAllCarr()
        {
            ResultViewModel resultViewModel = customerService.GetAvaliableCarriers(User.FindFirstValue(ClaimTypes.Email));
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.DataSet);
            }
            return new BadRequestObjectResult("Unknown error");
        }

        [Authorize(Roles = "Customer")]
        [HttpGet, Route("customer/GetMyCarriers")]
        [Produces(typeof(string))]
        public IActionResult GetMyCarr()
        {
            ResultViewModel resultViewModel = customerService.GetMyCarriers(User.FindFirstValue(ClaimTypes.Email));
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.DataSet);
            }
            return new BadRequestObjectResult("Unknown error");
        }

        [Authorize]
        [HttpGet, Route("customer/Selectplan")]
        public IActionResult ChangeMyPlan()
        {
            ResultViewModel resultViewModel = customerService.SelectPlan(User.FindFirstValue(ClaimTypes.Email));
            switch (resultViewModel.Flag)
            {
                case (int)HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(resultViewModel.Information);
                case (int)HttpStatusCode.OK:
                    return new OkObjectResult(resultViewModel.Information);
            }
            return new BadRequestObjectResult("Unknown error");
        }

        [HttpGet, Route("customer/TestM")]
        public IActionResult MyTestsM()
        {
            ResultViewModel resultViewModel = customerService.TestM();
            return new OkObjectResult(resultViewModel.DataSet);
            //return new OkObjectResult(User.FindFirstValue(ClaimTypes.Email));
        }

    }
}