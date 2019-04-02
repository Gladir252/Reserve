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
    class AdminService : IAdmin
    {
        IUnitOfWork Database { get; set; }

        public AdminService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
        public ResultViewModel CreateCarrier(CarrierViewModel carrierViewModel)
        {
            if (carrierViewModel.CarrierName == null ||
                carrierViewModel.CarrierCode == null ||
                carrierViewModel.CarrierLogo == null ||
                carrierViewModel.Phone == null) return new ResultViewModel(400, "Invalid Request");

            Carrier currentCarrier = Database.Carriers.GetByName(e => e.CarrierName == carrierViewModel.CarrierName);

            if (currentCarrier != null) return new ResultViewModel(400, "Carrier has already created");

            currentCarrier = new Carrier
            {
                CarrierName = carrierViewModel.CarrierName,
                CarrierCode = carrierViewModel.CarrierCode,
                Phone = carrierViewModel.Phone,
                CarrierLogo = carrierViewModel.CarrierLogo,
                Active = true,
                SubscriptionStatusId = 1
            };

            Database.Carriers.Create(currentCarrier);
            Database.Save();

            return new ResultViewModel(200, "Carrier " + currentCarrier.CarrierName + " is successfuly created");
        }

        public ResultViewModel EditCarrier(CarrierViewModel carrierViewModel)
        {
            if (carrierViewModel.CarrierId == 0 ||
               carrierViewModel.CarrierName == null ||
               carrierViewModel.CarrierCode == null ||
               carrierViewModel.CarrierLogo == null ||
               carrierViewModel.Phone == null) return new ResultViewModel(400, "Invalid Request");

            Carrier currentCarrier = Database.Carriers.GetByName(e => e.Id == carrierViewModel.CarrierId);

            if (currentCarrier == null) return new ResultViewModel(404, "Carrier not found");

            currentCarrier.CarrierName = carrierViewModel.CarrierName;
            currentCarrier.CarrierCode = carrierViewModel.CarrierCode;
            currentCarrier.CarrierLogo = carrierViewModel.CarrierLogo;
            currentCarrier.Phone = carrierViewModel.Phone;
            currentCarrier.Active = carrierViewModel.Active;
            currentCarrier.SubscriptionStatusId = carrierViewModel.SubscriptionStatusId;

            Database.Carriers.Update(currentCarrier);
            Database.Save();

            return new ResultViewModel(200, "Carrier updated");
        }

        public ResultViewModel GetListCarrier()
        {
            IEnumerable<Carrier> carrierList = Database.Carriers.GetAll();
            List<OutputAdminCarriersViewModel> currentList = new List<OutputAdminCarriersViewModel>();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Carrier, OutputAdminCarriersViewModel>()).CreateMapper();

            foreach (Carrier c in carrierList)
            {
                if (c == null) return new ResultViewModel(200, "List is empty");
                OutputAdminCarriersViewModel currentCarrier = mapper.Map<Carrier, OutputAdminCarriersViewModel>(c);//////////
                currentCarrier.CarrierName = Database.Carriers.GetByName(e => e.Id == c.Id).CarrierName + ", "
                    + Database.Carriers.GetByName(e => e.Id == c.Id).CarrierCode;
                currentCarrier.Status = Database.SubscriptionStatuses.GetByName(e => e.Id == c.SubscriptionStatusId).StatusName;
                currentList.Add(currentCarrier);
            }

            return new ResultViewModel(200, "", currentList);

        }


        public ResultViewModel CreateUser(UserFunctionalyViewModel userFunctionalyViewModel)
        {
            if (userFunctionalyViewModel.FirstName == null ||
                userFunctionalyViewModel.LastName == null ||
                userFunctionalyViewModel.Email == null ||
                userFunctionalyViewModel.Phone == null ||
                userFunctionalyViewModel.PasswordHash == null ||
                userFunctionalyViewModel.CarrierName == null) return new ResultViewModel(400, "Invalid request.");

            new CreatePasswordHash(userFunctionalyViewModel.PasswordHash);

            Carrier currentCarrier = Database.Carriers.GetByName(e => e.CarrierName == userFunctionalyViewModel.CarrierName);

            if (currentCarrier == null) return new ResultViewModel(404, "Carrier not found.");

            User currentUser = Database.Users.GetByName(e => e.Email == userFunctionalyViewModel.Email);

            if (currentUser != null) return new ResultViewModel(400, "User has already created.");

            currentUser = new User
            {
                FirstName = userFunctionalyViewModel.FirstName,
                LastName = userFunctionalyViewModel.LastName,
                Email = userFunctionalyViewModel.Email,
                Phone = userFunctionalyViewModel.Phone,
                PasswordHash = new CreatePasswordHash(userFunctionalyViewModel.PasswordHash).GetHash(),
                RoleId = 3,
                Active = true,
                SubscriptionStatusId = 1
            };

            Database.Users.Create(currentUser);

            UserCarrier currentUserCarrier = new UserCarrier
            {
                UserId = currentUser.Id,
                CarrierId = currentCarrier.Id
            };

            Database.UserCarriers.Create(currentUserCarrier);
            Database.Save();

            return new ResultViewModel(200, "User " + currentUser.Email + " is successfuly created and added to \""
                + currentCarrier.CarrierName + "\" carrier.");
        }

        public ResultViewModel GetCarriersUserList(CarrierViewModel carrierViewModel)
        {
            if (carrierViewModel.CarrierId == 0) return new ResultViewModel(400, "InvalidRequest");

            Carrier currentCarrier = Database.Carriers.GetByName(e => e.Id == carrierViewModel.CarrierId);

            if (currentCarrier == null) return new ResultViewModel(404, "Carrier not found");

            IEnumerable<UserCarrier> userCarriersList = Database.UserCarriers.Find(e => e.CarrierId == carrierViewModel.CarrierId);
            List<OutputAdminCarriersUserViewModel> currentList = new List<OutputAdminCarriersUserViewModel>();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, OutputAdminCarriersUserViewModel>()).CreateMapper();

            foreach (UserCarrier u in userCarriersList)
            {
                User user = Database.Users.GetByName(e => e.Id == u.UserId);
                OutputAdminCarriersUserViewModel currentUser = mapper.Map<User, OutputAdminCarriersUserViewModel>(user);
                currentList.Add(currentUser);
            }

            return new ResultViewModel(200, "Successful", currentList);
        }

        public ResultViewModel GetAddedUsersList()
        {
            IEnumerable<User> userList = Database.Users.Find(e => e.RoleId == 3);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, OutputAdminCarriersUserViewModel>()).CreateMapper();
            IEnumerable<OutputAdminCarriersUserViewModel> currentList = mapper.Map<IEnumerable<User>, IEnumerable<OutputAdminCarriersUserViewModel>>(userList);
            return new ResultViewModel(200, "Successful", currentList);
        }

        public ResultViewModel EditUser(UserFunctionalyViewModel userFunctionalyViewModel)
        {
            if (userFunctionalyViewModel.Id == 0 ||
                userFunctionalyViewModel.FirstName == null ||
                userFunctionalyViewModel.LastName == null ||
                userFunctionalyViewModel.Email == null ||
                userFunctionalyViewModel.Phone == null ||
                userFunctionalyViewModel.PasswordHash == null) return new ResultViewModel(400, "Invalid request.");

            User currentUser = Database.Users.GetByName(e => e.Id == userFunctionalyViewModel.Id);

            if (currentUser == null) return new ResultViewModel(404, "User not found");

            if (currentUser.RoleId != 3) return new ResultViewModel(400, "You are trying to change a user who does not have a " +
                "\"Carrier\" role. Request rejected");//403

            currentUser.FirstName = userFunctionalyViewModel.FirstName;
            currentUser.LastName = userFunctionalyViewModel.LastName;
            currentUser.Email = userFunctionalyViewModel.Email;
            currentUser.Phone = userFunctionalyViewModel.Phone;
            currentUser.PasswordHash = new CreatePasswordHash(userFunctionalyViewModel.PasswordHash).GetHash();

            Database.Users.Update(currentUser);
            Database.Save();

            return new ResultViewModel(200, "User successfuly updated");
        }

        public ResultViewModel DeleteUser(UserFunctionalyViewModel userFunctionalyViewModel)
        {
            if (userFunctionalyViewModel.Id == 0) return new ResultViewModel(400, "Invalid request.");

            User currentUser = Database.Users.GetByName(e => e.Id == userFunctionalyViewModel.Id);

            if (currentUser == null) return new ResultViewModel(404, "User not found.");

            UserCarrier currentUserCarrier = Database.UserCarriers.GetByName(e => e.UserId == userFunctionalyViewModel.Id);

            if (currentUser == null) return new ResultViewModel(404, "UserCarrier connection not found.");

            if (currentUser.RoleId != 3) return new ResultViewModel(400, "You are trying to delete a user who does not have a " +
                   "\"Carrier\" role. Request rejected.");

            Database.UserCarriers.Delete(currentUser.Id, currentUserCarrier.CarrierId);
            Database.Users.Delete(userFunctionalyViewModel.Id, null);

            Database.Save();

            return new ResultViewModel(200, "User " + currentUser.Email + " deleted from the system.");
        }
    }
}
