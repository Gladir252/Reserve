using AutoMapper;
using System.Collections.Generic;
using WebAppAlexey.BLL.BusinessModels;
using WebAppAlexey.BLL.Interfaces;
using WebAppAlexey.BLL.ViewModels;
using WebAppAlexey.BLL.ViewModels.OutputViewModels;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey.BLL.Services
{
    class CustomerService : ICustomer
    {
        IUnitOfWork Database { get; set; }

        public CustomerService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public ResultViewModel Registration(UserFunctionalyViewModel registrationViewModel)
        {
            if (registrationViewModel == null)
            {
                return new ResultViewModel(400, "Bad Request.");
            }

            var currentUser = Database.Users.GetByName(e => e.Email == registrationViewModel.Email);

            Company currentCompany = Database.Companies.GetByName(e => e.CompanyName == registrationViewModel.CompanyName);

            if (currentUser != null)
            {
                return new ResultViewModel(400, "This user has already created.");
            }

            CreatePasswordHash hash = new CreatePasswordHash(registrationViewModel.PasswordHash);

            if (currentCompany == null)
            {
                currentCompany = new Company
                {
                    CompanyName = registrationViewModel.CompanyName
                };
                Database.Companies.Create(currentCompany);
                Database.Save();
            }

            User user = new User
            {
                Email = registrationViewModel.Email,
                PasswordHash = hash.GetHash(),
                LastName = registrationViewModel.LastName,
                FirstName = registrationViewModel.FirstName,

                SubscriptionStatusId = 1,
                Phone = registrationViewModel.Phone,
                Active = true,
                RoleId = 2,
                CompanyId = currentCompany.Id
            };

            Database.Users.Create(user);
            Database.Save();

            return new ResultViewModel(200, new CreateJWT(user.Email,
                    Database.Roles.GetByName(e => e.Id == user.RoleId).RoleName,
                    Database.SubscriptionStatuses.GetByName(e => e.Id == user.SubscriptionStatusId).StatusName).GetJwt());
        }

        public ResultViewModel Login(UserFunctionalyViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return new ResultViewModel(400, "Invalid request ^-^");
            }

            var currentUser = Database.Users.GetByName(e => e.Email == userViewModel.Email && e.PasswordHash == new CreatePasswordHash(userViewModel.PasswordHash).GetHash());

            if (currentUser == null)
            {
                return new ResultViewModel(400, "Incorrect login or password ^_-");
            }

            return new ResultViewModel(200, new CreateJWT(currentUser.Email,
                    Database.Roles.GetByName(e => e.Id == currentUser.RoleId).RoleName,
                    Database.SubscriptionStatuses.GetByName(e => e.Id == currentUser.SubscriptionStatusId).StatusName).GetJwt());
        }

        public ResultViewModel AddCarrier(string email, CarrierViewModel addDeleteCarrierViewModel)
        {
            if (addDeleteCarrierViewModel == null)
            {
                return new ResultViewModel(400, "Invalid request ^-^");
            }

            Carrier currentCarrier = Database.Carriers.GetByName(e => e.CarrierName == addDeleteCarrierViewModel.CarrierName);
            User currentUser = Database.Users.GetByName(e => e.Email == email);

            if (currentUser == null) return new ResultViewModel(404, "User not found ^_^");

            if (currentCarrier == null) return new ResultViewModel(404, "Carrier not found ^_^");


            UserCarrier currentUserCarrier = new UserCarrier
            {
                UserId = currentUser.Id,
                CarrierId = currentCarrier.Id
            };



            var userCarrier = Database.UserCarriers.GetByName(e => e.UserId == currentUserCarrier.UserId && e.CarrierId == currentUserCarrier.CarrierId);


            if (userCarrier == null)
            {
                Database.UserCarriers.Create(currentUserCarrier);
                Database.Save();

                return new ResultViewModel(200, "Carrier Added");
            }

            else return new ResultViewModel(400, "User-Carrier already created " + currentUserCarrier);

        }

        public ResultViewModel DeleteCarrier(string email, CarrierViewModel addDeleteCarrierViewModel)
        {
            if (addDeleteCarrierViewModel == null)
            {
                return new ResultViewModel(400, "Invalid request ^-^");
            }

            Carrier currentCarrier = Database.Carriers.GetByName(e => e.CarrierName == addDeleteCarrierViewModel.CarrierName);
            User currentUser = Database.Users.GetByName(e => e.Email == email);

            if (currentUser == null) return new ResultViewModel(404, "User not found ^_^");
            if (currentCarrier == null) return new ResultViewModel(404, "Carrier not found ^_^");

            UserCarrier currentUserCarrier = new UserCarrier
            {
                UserId = currentUser.Id,
                CarrierId = currentCarrier.Id
            };

            var userCarrier = Database.UserCarriers.GetByName(e => e.UserId == currentUserCarrier.UserId && e.CarrierId == currentUserCarrier.CarrierId);

            if (userCarrier != null)
            {

                Database.UserCarriers.Delete(currentUserCarrier.UserId, currentUserCarrier.CarrierId);
                Database.Save();

                return new ResultViewModel(200, "Carrier Deleted");
            }
            else return new ResultViewModel(404, "User-Carrier not found");

        }

        public ResultViewModel AddAddress(string email, AdressViewModel addDeleteAdressViewModel)
        {
            if (addDeleteAdressViewModel == null)
            {
                return new ResultViewModel(400, "Invalid request");
            }

            User currentUser = Database.Users.GetByName(e => e.Email == email);

            if (currentUser == null) return new ResultViewModel(404, "Bad current user == " + currentUser.Id);

            Adress currentAddress = Database.Adresses.GetByName(e => e.UserId == currentUser.Id && e.StreetLine1 == addDeleteAdressViewModel.StreetLine1);

            if (currentUser != null && currentAddress == null)
            {
                Adress address = new Adress
                {
                    Active = true,
                    StreetLine1 = addDeleteAdressViewModel.StreetLine1,
                    StreetLine2 = addDeleteAdressViewModel.StreetLine2,
                    State = addDeleteAdressViewModel.State,
                    City = addDeleteAdressViewModel.City,
                    UserId = currentUser.Id
                };
                Database.Adresses.Create(address);
                Database.Save();

                return new ResultViewModel(200, "Address created - " + address.StreetLine1);
            }
            else return new ResultViewModel(400, "Has already created");
        }

        public ResultViewModel EditAddress(string email, AdressViewModel addDeleteAdressViewModel)
        {
            if (addDeleteAdressViewModel == null)
            {
                return new ResultViewModel(400, "Invalid request");
            }

            User currentUser = Database.Users.GetByName(e => e.Email == email);

            if (currentUser == null) return new ResultViewModel(404, "Bad current user == " + currentUser.Id);

            Adress currentAddress = Database.Adresses.GetByName(e => e.UserId == currentUser.Id && e.Id == addDeleteAdressViewModel.AdressId);

            if (currentUser != null && currentAddress != null)
            {
                currentAddress.StreetLine1 = addDeleteAdressViewModel.StreetLine1;
                currentAddress.StreetLine2 = addDeleteAdressViewModel.StreetLine2;
                currentAddress.State = addDeleteAdressViewModel.State;
                currentAddress.City = addDeleteAdressViewModel.City;

                Database.Adresses.Update(currentAddress);
                Database.Save();

                return new ResultViewModel(200, "Address update to -> " + currentAddress.StreetLine1);
            }

            else return new ResultViewModel(400, "Has already updated");
        }

        public ResultViewModel DeleteAddress(string email, AdressViewModel addDeleteAdressViewModel)
        {
            if (addDeleteAdressViewModel == null)
            {
                return new ResultViewModel(400, "Invalid request");
            }

            User currentUser = Database.Users.GetByName(e => e.Email == email);

            if (currentUser == null) return new ResultViewModel(404, "Bad current user == " + currentUser.Id);

            Adress currentAddress = Database.Adresses.GetByName(e => e.UserId == currentUser.Id && e.Id == addDeleteAdressViewModel.AdressId);

            if (currentUser != null && currentAddress != null)
            {
                Database.Adresses.Delete(currentAddress.Id, null);
                Database.Save();

                return new ResultViewModel(200, "Address deleted " + currentAddress.StreetLine1);
            }

            else return new ResultViewModel(404, "Address not found");

        }

        public ResultViewModel GetMyAddressBook(string email)
        {
            if (email == null)
            {
                return new ResultViewModel(400, "Invalid request ^-^");
            }

            User currentUser = Database.Users.GetByName(e => e.Email == email);

            if (currentUser == null) return new ResultViewModel(404, "User not found ^_^");

            IEnumerable<Adress> addressList = Database.Adresses.Find(e => e.UserId == currentUser.Id);

            if (addressList == null) return new ResultViewModel(404, "List is empty", addressList);//202!!!!!!!!!!!!!

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Adress, OutputAddressBookViewModel>()).CreateMapper();
            List<OutputAddressBookViewModel> currentList = new List<OutputAddressBookViewModel>();

            foreach (Adress a in addressList)
            {
                OutputAddressBookViewModel currentAddress = mapper.Map<Adress, OutputAddressBookViewModel>(a);
                currentAddress.Address = a.StreetLine1 + ", " + a.StreetLine2 + ", " + a.City + ", " + a.State;
                currentAddress.CompanyName = Database.Companies.GetByName(e => e.Id == currentUser.CompanyId).CompanyName;
                currentList.Add(currentAddress);
            }

            return new ResultViewModel(200, " ", currentList);

        }


        public ResultViewModel GetAvaliableCarriers(string email)
        {
            if (email == null)
            {
                return new ResultViewModel(400, "Invalid request");
            }

            IEnumerable<Carrier> carrierList;
            User currentUser = Database.Users.GetByName(e => e.Email == email);

            if (currentUser == null) return new ResultViewModel(404, "User not found ^_^");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Carrier, OutputCustomerCarriersViewModel>()).CreateMapper();
            List<OutputCustomerCarriersViewModel> currentList = new List<OutputCustomerCarriersViewModel>();

            if (currentUser.SubscriptionStatusId == 2)
            {
                carrierList = Database.Carriers.GetAll();
                foreach (Carrier c in carrierList)
                {
                    if (c == null) return new ResultViewModel(200, "List is empty", carrierList);
                    OutputCustomerCarriersViewModel currentCarrier = mapper.Map<Carrier, OutputCustomerCarriersViewModel>(c);
                    currentCarrier.Status = Database.SubscriptionStatuses.GetByName(e => e.Id == c.SubscriptionStatusId).StatusName;
                    currentList.Add(currentCarrier);
                }
                return new ResultViewModel(200, "Successful", currentList);
            }
            else
            {
                carrierList = Database.Carriers.Find(e => e.Active == true && e.SubscriptionStatusId == currentUser.SubscriptionStatusId);
                foreach (Carrier c in carrierList)
                {
                    if (c == null) return new ResultViewModel(200, "List is empty", carrierList);
                    OutputCustomerCarriersViewModel currentCarrier = mapper.Map<Carrier, OutputCustomerCarriersViewModel>(c);
                    currentCarrier.Status = Database.SubscriptionStatuses.GetByName(e => e.Id == c.SubscriptionStatusId).StatusName;
                    currentList.Add(currentCarrier);
                }
                //currentList = mapper.Map<IEnumerable<Carrier>, IEnumerable<OutputMyCarriersViewModel>>(carrierList);
                return new ResultViewModel(200, "Successful", currentList);
            }
        }

        public ResultViewModel GetMyCarriers(string email)
        {
            if (email == null)
            {
                return new ResultViewModel(400, "Invalid request ^-^");
            }

            User currentUser = Database.Users.GetByName(e => e.Email == email);

            if (currentUser == null) return new ResultViewModel(404, "User not found ^_^");

            IEnumerable<UserCarrier> userCarrierList = Database.UserCarriers.Find(e => e.UserId == currentUser.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Carrier, OutputCustomerCarriersViewModel>()).CreateMapper();
            List<OutputCustomerCarriersViewModel> myCarrierListOne = new List<OutputCustomerCarriersViewModel>();


            foreach (UserCarrier u in userCarrierList)
            {
                if (u == null) return new ResultViewModel(200, "List is empty", userCarrierList);
                Carrier c = Database.Carriers.Get(u.CarrierId);
                OutputCustomerCarriersViewModel currentCarrier = mapper.Map<Carrier, OutputCustomerCarriersViewModel>(c);
                currentCarrier.Status = Database.SubscriptionStatuses.GetByName(e => e.Id == c.SubscriptionStatusId).StatusName;
                myCarrierListOne.Add(currentCarrier);
            }

            return new ResultViewModel(200, "", myCarrierListOne);
        }


        public ResultViewModel SelectPlan(string email)
        {
            if (email == null)
            {
                return new ResultViewModel(400, "Invalid request ^-^");
            }

            User currentUser = Database.Users.GetByName(e => e.Email == email);

            if (currentUser == null) return new ResultViewModel(404, "User not found ^_^");

            if (currentUser.SubscriptionStatusId == 1) currentUser.SubscriptionStatusId = 2;//change!!!
            else currentUser.SubscriptionStatusId = 1;

            Database.Users.Update(currentUser);
            Database.Save();

            return new ResultViewModel(200, "Your status is changed to \"" + Database.SubscriptionStatuses.GetByName(e => e.Id == currentUser.SubscriptionStatusId).StatusName + "\"");
        }

    }
}
