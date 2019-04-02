using System;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Adress> Adresses { get; }
        IRepository<Carrier> Carriers { get; }
        IRepository<Role> Roles { get; }
        IRepository<Company> Companies { get; }
        IRepository<UserCarrier> UserCarriers { get; }
        IRepository<SubscriptionStatus> SubscriptionStatuses { get; }
        void Save();
    }
}
