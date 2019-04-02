using System;
using WebAppAlexey.DAL.DataBaseContext;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey.DAL.Repositories
{
    class EFUnitOfWork : IUnitOfWork
    {
        private WebAppDataBaseContext db;
        private UserCarrierRepository userCarrierRepository;
        private UserRepository userRepository;
        private CompanyRepository companyRepository;
        private CarrierRepository carrierRepository;
        private SubscriptionStatusRepository subscriptionStatusRepository;
        private RoleRepository roleRepository;
        private AdressRepository adressRepository;

        private bool disposed = false;


        public EFUnitOfWork()
        {
            db = new WebAppDataBaseContext();
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Carrier> Carriers
        {
            get
            {
                if (carrierRepository == null)
                    carrierRepository = new CarrierRepository(db);
                return carrierRepository;
            }
        }

        public IRepository<Company> Companies
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new CompanyRepository(db);
                return companyRepository;
            }
        }

        public IRepository<SubscriptionStatus> SubscriptionStatuses
        {
            get
            {
                if (subscriptionStatusRepository == null)
                    subscriptionStatusRepository = new SubscriptionStatusRepository(db);
                return subscriptionStatusRepository;
            }
        }

        public IRepository<Adress> Adresses
        {
            get
            {
                if (adressRepository == null)
                    adressRepository = new AdressRepository(db);
                return adressRepository;
            }
        }


        public IRepository<UserCarrier> UserCarriers
        {
            get
            {
                if (userCarrierRepository == null)
                    userCarrierRepository = new UserCarrierRepository(db);
                return userCarrierRepository;
            }
        }


        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);
                return roleRepository;
            }
        }



        public void Save()
        {
            db.SaveChanges();
        }



        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



    }
}

