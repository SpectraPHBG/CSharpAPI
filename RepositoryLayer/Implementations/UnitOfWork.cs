using Data_Layer.Entities;
using RepositoryLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Context
{
    public class UnitOfWork : IDisposable
    {
        private BankSystemAPIDBContext context = new BankSystemAPIDBContext();
        private GenericRepository<Client> clientRepository;
        private GenericRepository<City> cityRepository;
        private GenericRepository<Bank_Employee> bankEmployeeRepository;
        private GenericRepository<Bank_Branch> bankBranchRepository;
        private GenericRepository<Bank_Account> bankAccountRepository;
        private GenericRepository<Bank> bankRepository;

        public GenericRepository<Client> ClientRepository
        {
            get
            {

                if (this.clientRepository == null)
                {
                    this.clientRepository = new GenericRepository<Client>(context);
                }
                return clientRepository;
            }
        }

        public GenericRepository<City> CityRepository
        {
            get
            {

                if (this.cityRepository == null)
                {
                    this.cityRepository = new GenericRepository<City>(context);
                }
                return cityRepository;
            }
        }
        public GenericRepository<Bank_Employee> BankEmployeeRepository
        {
            get
            {

                if (this.bankEmployeeRepository == null)
                {
                    this.bankEmployeeRepository = new GenericRepository<Bank_Employee>(context);
                }
                return bankEmployeeRepository;
            }
        }
        public GenericRepository<Bank_Branch> BankBranchRepository
        {
            get
            {

                if (this.bankBranchRepository == null)
                {
                    this.bankBranchRepository = new GenericRepository<Bank_Branch>(context);
                }
                return bankBranchRepository;
            }
        }
        public GenericRepository<Bank_Account> BankAccountRepository
        {
            get
            {

                if (this.bankAccountRepository == null)
                {
                    this.bankAccountRepository = new GenericRepository<Bank_Account>(context);
                }
                return bankAccountRepository;
            }
        }
        public GenericRepository<Bank> BankRepository
        {
            get
            {

                if (this.bankRepository == null)
                {
                    this.bankRepository = new GenericRepository<Bank>(context);
                }
                return bankRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
