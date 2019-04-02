using WebAppAlexey.BLL.ViewModels;

namespace WebAppAlexey.BLL.Interfaces
{
    public interface IAdmin
    {
        ResultViewModel CreateCarrier(CarrierViewModel carrierViewModel);
        ResultViewModel EditCarrier(CarrierViewModel carrierViewModel);
        ResultViewModel GetListCarrier();
        ResultViewModel CreateUser(UserFunctionalyViewModel userFunctionalyViewModel);
        ResultViewModel GetCarriersUserList(CarrierViewModel carrierViewModel);
        ResultViewModel GetAddedUsersList();
        ResultViewModel EditUser(UserFunctionalyViewModel userFunctionalyViewModel);
        ResultViewModel DeleteUser(UserFunctionalyViewModel userFunctionalyViewModel);
    }
}
