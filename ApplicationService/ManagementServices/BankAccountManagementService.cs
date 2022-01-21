using ApplicationService.DTOs;
using Data_Layer.Context;
using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ManagementServices
{
    public class BankAccountManagementService
    {
        private string GenerateIBAN()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Random rand = new Random();
                string iban = "";
                string charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                bool stopFlag = false;
                while (!stopFlag)
                {
                    iban = "";
                    for (int i = 0; i < 22; i++)
                    {
                        iban += charPool[rand.Next(charPool.Length)];
                    }
                    stopFlag = true;
                    foreach (var accountInDB in unitOfWork.BankAccountRepository.Get())
                    {
                        if (accountInDB.IBAN.Equals(iban))
                        {
                            stopFlag = false;
                            break;
                        }
                    }
                }
                return iban;
            }
        }
        public List<Bank_AccountDTO> GetAllBankAccounts()
        {
            List<Bank_AccountDTO> accounts = new List<Bank_AccountDTO>();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var account in unitOfWork.BankAccountRepository.Get())
                {
                    Bank accountBank = unitOfWork.BankRepository.GetByID(account.BANK_ID);
                    if (accountBank == null)
                    {
                        return null;
                    }
                    List<ClientDTO> clients = new List<ClientDTO>();
                    foreach (var client in account.ACCOUNT_HOLDERS)
                    {
                        clients.Add(new ClientDTO
                        {
                            ID = client.ID,
                            BANK_ID = client.BANK_ID,
                            CLIENT_NAME = client.CLIENT_NAME,
                            UPDATED_TIMESTAMP = client.UPDATED_TIMESTAMP
                        });
                    }
                    accounts.Add(new Bank_AccountDTO
                    {
                        ID = account.ID,
                        BANK_ID = account.BANK_ID,
                        IBAN = account.IBAN,
                        IS_ACTIVE = account.IS_ACTIVE,
                        UPDATED_TIMESTAMP = account.UPDATED_TIMESTAMP,
                        BANK = new BankDTO
                        {
                            ID = accountBank.ID,
                            BANK_NAME = accountBank.BANK_NAME,
                            BIC = accountBank.BIC,
                            UPDATED_TIMESTAMP = accountBank.UPDATED_TIMESTAMP
                        },
                        ACCOUNT_HOLDERS = clients
                    });
                }

                if (accounts.Count > 0)
                {
                    return accounts;
                }
                else
                {
                    return null;
                }
            }
        }
        public Bank_AccountDetailedDTO GetBankAccountByID(long id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Bank_Account account =unitOfWork.BankAccountRepository.GetByID(id);
                if (account == null)
                {
                    return null;
                }
                List<ClientDTO> accountHolders = new List<ClientDTO>();
                foreach (var accountHolder in account.ACCOUNT_HOLDERS)
                {
                    accountHolders.Add(new ClientDTO
                    {
                        ID = accountHolder.ID,
                        CLIENT_NAME = accountHolder.CLIENT_NAME,
                        UPDATED_TIMESTAMP = account.UPDATED_TIMESTAMP
                    });
                }
                Bank_AccountDetailedDTO accountDTO = new Bank_AccountDetailedDTO
                {
                    ID = account.ID,
                    BANK_ID = account.BANK_ID,
                    IBAN = account.IBAN,
                    IS_ACTIVE = account.IS_ACTIVE,
                    CURRENT_BALANCE = account.CURRENT_BALANCE,
                    CREATED_AT = account.CREATED_AT,
                    UPDATED_TIMESTAMP = account.UPDATED_TIMESTAMP,
                    BANK = new BankDTO
                    {
                        ID = account.BANK.ID,
                        BANK_NAME = account.BANK.BANK_NAME,
                        BIC = account.BANK.BIC,
                        UPDATED_TIMESTAMP = account.BANK.UPDATED_TIMESTAMP
                    },
                    ACCOUNT_HOLDERS = accountHolders
                };
                return accountDTO;
            }
        }
        public Bank_AccountDTO GetBankAccountByIban(string iban)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                long id = 0;
                foreach (var bankAccount in unitOfWork.BankAccountRepository.Get())
                {
                    if (bankAccount.IBAN.ToLower().Trim().Equals(iban.ToLower().Trim()))
                    {
                        id = bankAccount.ID;
                    }
                }
                Bank_Account account = unitOfWork.BankAccountRepository.GetByID(id);
                if (account == null)
                {
                    return null;
                }
                Bank accountBank = unitOfWork.BankRepository.GetByID(account.BANK_ID);
                if (account == null)
                {
                    return null;
                }
                List<ClientDTO> clients = new List<ClientDTO>();
                foreach (var client in account.ACCOUNT_HOLDERS)
                {
                    clients.Add(new ClientDTO
                    {
                        ID = client.ID,
                        CLIENT_NAME = client.CLIENT_NAME,
                        UPDATED_TIMESTAMP = client.UPDATED_TIMESTAMP
                    });
                }
                Bank_AccountDTO accountDTO = new Bank_AccountDTO
                {
                    ID = account.ID,
                    BANK_ID = account.BANK_ID,
                    IBAN = account.IBAN,
                    IS_ACTIVE = account.IS_ACTIVE,
                    UPDATED_TIMESTAMP = account.UPDATED_TIMESTAMP,
                    BANK = new BankDTO
                    {
                        ID = accountBank.ID,
                        BANK_NAME = accountBank.BANK_NAME,
                        PHONE=accountBank.PHONE,
                        BIC = accountBank.BIC,
                        UPDATED_TIMESTAMP = accountBank.UPDATED_TIMESTAMP
                    },
                    ACCOUNT_HOLDERS = clients
                };
                return accountDTO;
            }
        }
        //public Tuple<string,bool> Save(long bankID)
        //{
        //    using (UnitOfWork unitOfWork = new UnitOfWork())
        //    {
        //        if (bankID == 0 || unitOfWork.BankRepository.GetByID(bankID) == null)
        //        {
        //            return new Tuple<string, bool>("Всяка банкова сметкя трябва да принадлежи към съществуваща банка!", false);
        //        }

        //        Bank_Account account = new Bank_Account();
        //        string newAccountIBAN = GenerateIBAN();
        //        account.IBAN = newAccountIBAN;
        //        account.BANK_ID = bankID;
        //        account.CURRENT_BALANCE = 0;
        //        account.CREATED_AT = DateTime.Now;
        //        account.IS_ACTIVE = true;
        //        account.UPDATED_TIMESTAMP = DateTime.Now;

        //        if (account.ACCOUNT_HOLDERS.Count == 0)
        //        {
        //            return new Tuple<string, bool>("Невъзможно създаване на банкова сметка без поне един титуляр!", false);
        //        }
        //        try
        //        {
        //            unitOfWork.BankAccountRepository.Insert(account);
        //            unitOfWork.Save();
        //            return new Tuple<string, bool>("Успешно бе създадена банкова сметка!", true);
        //        }
        //        catch (Exception)
        //        {
        //            return new Tuple<string, bool>("Създаването на банкова сметка не бе успешно!", false);
        //        }
        //    }
        //}
        public Bank_Account Create(long bankID)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (bankID == 0 || unitOfWork.BankRepository.GetByID(bankID) == null)
                {
                    return null;
                }

                Bank_Account account = new Bank_Account();
                string newAccountIBAN = GenerateIBAN();
                account.IBAN = newAccountIBAN;
                account.IS_ACTIVE = true;
                account.BANK_ID = bankID;
                account.CURRENT_BALANCE = 0;
                account.CREATED_AT = DateTime.Now;
                account.UPDATED_TIMESTAMP = DateTime.Now;

                try
                {

                    return account;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public Tuple<string, bool> AddAccountHolder(long clientID,long accountID)
        {
            using (UnitOfWork unitOfWork=new UnitOfWork())
            {
                if(clientID==0 || accountID == 0)
                {
                    return new Tuple<string, bool>("Не е бяха изпратени необходимите параметри!", false);
                }
                Client client = unitOfWork.ClientRepository.GetByID(clientID);
                Bank_Account account = unitOfWork.BankAccountRepository.GetByID(accountID);
                if (client == null)
                {
                    return new Tuple<string, bool>("Не е намерен такъв клиент!", false);
                }
                if (account == null)
                {
                    return new Tuple<string, bool>("Не е намерена такава банкова сметка!", false);
                }
                try
                {
                    if (!client.BANK_ACCOUNTS.Contains(account))
                    {
                        client.BANK_ACCOUNTS.Add(account);
                    }
                    else
                    {
                        return new Tuple<string, bool>("Този клиент вече е титуляр на избраната сметка!", false);
                    }
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешно е добавен клиент като титуляр на сметката!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Добавянето на клиент като титуляр на сметката е неуспешно!", true);
                    throw;
                }
            }
        }
        public Tuple<string,bool> SetActiveStatus(Bank_AccountDetailedDTO accountDTO)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (accountDTO == null)
                {
                    return new Tuple<string, bool>("Не е посочена банкова сметка!", false);
                }
                if (accountDTO.UPDATED_TIMESTAMP == null)
                {
                    return new Tuple<string, bool>("Възникна грешка при промяната (Непълни данни за банкова сметка)!", false);
                }
                Bank_Account account = unitOfWork.BankAccountRepository.GetByID(accountDTO.ID);
                if (account == null)
                {
                    return new Tuple<string, bool>("Не е намерен такъв банков акаунт!", false);
                }
                if (!accountDTO.UPDATED_TIMESTAMP.Equals(account.UPDATED_TIMESTAMP))
                {
                    return new Tuple<string, bool>("Била е извършена промяна върху информацията за тази банкова сметка от друг потребител. Моля опреснете страницата и опитайте отново!", false);
                }

                account.IS_ACTIVE = accountDTO.IS_ACTIVE;
                account.UPDATED_TIMESTAMP = DateTime.Now;
                try
                {
                    unitOfWork.BankAccountRepository.Update(account);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешно е променена банковата сметка!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Промяната на банкова сметка не е успешно!", false);
                }
            }
        }
        public Tuple<string, bool> DepositSumToAccount(TransactionManager manager)
        {
            string ibanReceiver = manager.IBAN_RECEIVER;
            decimal sum = manager.SUM;
            if (ibanReceiver == null)
            {
                return new Tuple<string, bool>("Липсват данни за сметка!", false);
            }
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var account in unitOfWork.BankAccountRepository.Get())
                {
                    if (account.IBAN.ToLower().Equals(manager.IBAN_RECEIVER.ToLower().Trim()) && account.IS_ACTIVE)
                    {
                        account.CURRENT_BALANCE += manager.SUM;
                    }
                }
                try
                {
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешно добавяне на средства към банкова сметка!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Добавянето на средства не е успешено!", false);
                }
            }
        }
        public Tuple<string, bool> TransferSumToAccount(TransactionManager manager)
        {
            string ibanSender = manager.IBAN_SENDER;
            string ibanReceiver = manager.IBAN_RECEIVER;
            string senderPersonalNumber = manager.SENDER_PERSONAL_NUMBER;
            long senderBankID = manager.SENDER_BANK_ID;
            decimal sum = manager.SUM;
            if (ibanSender==null || ibanReceiver == null||senderPersonalNumber==null||senderBankID==0)
            {
                return new Tuple<string, bool>("Липсват данни за сметки!", false);
            }
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Bank_Account sender = null;
                Bank_Account receiver = null;
                if (ibanSender.ToLower().Trim().Equals(ibanReceiver.ToLower().Trim()))
                {
                    return new Tuple<string, bool>("Не може да се пращат средства към същата сметка, от която са изпратени!", false);
                }
                foreach (var account in unitOfWork.BankAccountRepository.Get())
                {
                    if (account.IBAN.ToLower().Equals(ibanSender.ToLower().Trim()) && account.IS_ACTIVE)
                    {
                        foreach (var holder in account.ACCOUNT_HOLDERS)
                        {
                            if (holder.PERSONAL_NUMBER.ToLower().Equals(senderPersonalNumber.ToLower().Trim()) && holder.BANK_ID==senderBankID)
                            {
                                sender = account;
                            }
                        }
                       
                    }
                    else if(account.IBAN.ToLower().Equals(ibanReceiver.ToLower().Trim()) && account.IS_ACTIVE)
                    {
                        receiver = account;
                    }
                }
                if (sender == null)
                {
                    return new Tuple<string, bool>("Нямате право да изпращате средства от тази сметка или тя не съществува!", false);
                }
                if (receiver == null)
                {
                    return new Tuple<string, bool>("Грешна или несъществуваща сметка на получател!", false);
                }
                if (sender.CURRENT_BALANCE - sum >= 0)
                {
                    sender.CURRENT_BALANCE -= sum;
                    receiver.CURRENT_BALANCE += sum;
                }
                else
                {
                    return new Tuple<string, bool>("Недостатъчно средства за извършване на превод!", false);
                }
                try
                {
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешен превод на средства към банкова сметка!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Преводът на средства не е успешен!", false);
                }
            }
        }
        public Tuple<string, bool> RemoveAccountHolder(long clientID, long accountID)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (clientID == 0 || accountID == 0)
                {
                    return new Tuple<string, bool>("Не е бяха изпратени необходимите параметри!", false);
                }
                Client client = unitOfWork.ClientRepository.GetByID(clientID);
                Bank_Account account = unitOfWork.BankAccountRepository.GetByID(accountID);
                if (client == null)
                {
                    return new Tuple<string, bool>("Не е намерен такъв клиент!", false);
                }
                if (account == null)
                {
                    return new Tuple<string, bool>("Не е намерена такава банкова сметка!", false);
                }
                try
                {
                    if (client.BANK_ACCOUNTS.Contains(account))
                    {
                        client.BANK_ACCOUNTS.Remove(account);
                        if (client.BANK_ACCOUNTS.Count == 0)
                        {
                            unitOfWork.ClientRepository.Delete(client);
                        }
                        if (account.ACCOUNT_HOLDERS.Count == 0)
                        {
                            unitOfWork.BankAccountRepository.Delete(account);
                        }
                    }
                    else
                    {
                        return new Tuple<string, bool>("Този клиент не титуляр на избраната сметка!", false);
                    }
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешно е добавен клиент като титуляр на сметката!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Добавянето на клиент като титуляр на сметката е неуспешно!", true);
                    throw;
                }
            }
        }
        public Tuple<string, bool> Delete(long id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Bank_Account account = unitOfWork.BankAccountRepository.GetByID(id);
                if (account == null)
                {
                    return new Tuple<string, bool>("Такава банкова сметка не съществува!", false);
                }
                try
                {
                    List<Client> holdersToDelete = new List<Client>();
                    foreach (var holder in account.ACCOUNT_HOLDERS)
                    {
                        holder.BANK_ACCOUNTS.Remove(account);
                        if (holder.BANK_ACCOUNTS.Count == 0)
                        {
                            holdersToDelete.Add(holder);
                        }
                    }
                    foreach (var holderToDelete in holdersToDelete)
                    {
                        unitOfWork.ClientRepository.Delete(holderToDelete);
                    }
                    unitOfWork.BankAccountRepository.Delete(id);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Избраната банкова сметка бе изтрита успешно!", true);
                }
                catch (ArgumentException)
                {
                    return new Tuple<string, bool>("Избраната банкова сметка не успя да бъде изтрита!", false);
                }
            }
        }
    }
}
