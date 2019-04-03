using Microsoft.AspNetCore.Mvc;
using WebAppAlexey.BLL.ViewModels;
using WebAppAlexey.DAL.Models;
using System.Collections.Generic;

namespace WebAppAlexey.BLL.Interfaces
{
    public interface ICustomer 
    {
        ResultViewModel Registration(UserFunctionalyViewModel registrationViewModel);
        ResultViewModel Login(UserFunctionalyViewModel t);
        ResultViewModel AddCarrier(string email, CarrierViewModel CarrierViewModel);
        ResultViewModel DeleteCarrier(string email, CarrierViewModel CarrierViewModel);
        ResultViewModel GetAvaliableCarriers(string email);
        ResultViewModel GetMyCarriers(string email);
        ResultViewModel AddAddress(string email, AdressViewModel AdressViewModel);
        ResultViewModel EditAddress(string email, AdressViewModel AdressViewModel);
        ResultViewModel DeleteAddress(string email, AdressViewModel AdressViewModel);
        ResultViewModel GetMyAddressBook(string email);
        ResultViewModel SelectPlan(string email);
        ResultViewModel TestM();
    }
}
