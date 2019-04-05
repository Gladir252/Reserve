using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAppAlexey.BLL.Interfaces;
using WebAppAlexey.BLL.ViewModels;

namespace WebAppAlexey.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin adminService;

        public AdminController(IAdmin adminService)
        {
            this.adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("CreateCarrier")]
        public IActionResult CreateCarr([FromBody]CarrierViewModel carrierViewModel)
        {
            ResultViewModel resultViewModel = adminService.CreateCarrier(carrierViewModel);
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

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("EditCarrier")]
        public IActionResult EditCarr([FromBody]CarrierViewModel carrierViewModel)
        {
            ResultViewModel resultViewModel = adminService.EditCarrier(carrierViewModel);
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

        [Authorize(Roles = "Admin")]
        [HttpGet, Route("GetAllCarriers")]
        public IActionResult GetAllCarr()
        {
            ResultViewModel resultViewModel = adminService.GetListCarrier();
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

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("CreateUser")]
        public IActionResult CreateUs([FromBody]UserFunctionalyViewModel userFunctionalyViewModel)
        {
            ResultViewModel resultViewModel = adminService.CreateUser(userFunctionalyViewModel);
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

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("GetThisUsers")]
        public IActionResult GetThisUsers(CarrierViewModel carrierViewModel)
        {
            ResultViewModel resultViewModel = adminService.GetCarriersUserList(carrierViewModel);
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

        [Authorize(Roles = "Admin")]
        [HttpGet, Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            ResultViewModel resultViewModel = adminService.GetAddedUsersList();
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

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("EditUser")]
        public IActionResult EditUsr(UserFunctionalyViewModel userFunctionalyViewModel)
        {
            ResultViewModel resultViewModel = adminService.EditUser(userFunctionalyViewModel);
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

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("DeleteUser")]
        public IActionResult DeleteUsr(UserFunctionalyViewModel userFunctionalyViewModel)
        {
            ResultViewModel resultViewModel = adminService.DeleteUser(userFunctionalyViewModel);
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
    }
}