using WebAppAlexey.BLL.ViewModels;

namespace WebAppAlexey.BLL.Interfaces
{
    public interface ICustomer 
    {
        ResultViewModel Registration(UserFunctionalyViewModel registrationViewModel);
        ResultViewModel Login(UserFunctionalyViewModel t);
        ResultViewModel AddCarrier(string email, CarrierViewModel carrierViewModel);
        ResultViewModel DeleteCarrier(string email, CarrierViewModel carrierViewModel);
        ResultViewModel GetAvaliableCarriers(string email);
        ResultViewModel GetMyCarriers(string email);
        ResultViewModel AddAddress(string email, AdressViewModel adressViewModel);
        ResultViewModel EditAddress(string email, AdressViewModel adressViewModel);
        ResultViewModel DeleteAddress(string email, AdressViewModel adressViewModel);
        ResultViewModel GetMyAddressBook(string email);
        ResultViewModel SelectPlan(string email);
        ResultViewModel TestM();
    }
}
