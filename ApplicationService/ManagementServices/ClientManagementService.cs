using ApplicationService.DTOs;
using Data_Layer.Context;
using Data_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationService.ManagementServices
{
    public class ClientManagementService
    {
        public List<ClientDTO> GetAllClientsStandard()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<ClientDTO> clients = new List<ClientDTO>();
                foreach (var client in unitOfWork.ClientRepository.Get())
                {
                    List<Bank_AccountDTO> bankAccounts = new List<Bank_AccountDTO>();
                    foreach (var account in client.BANK_ACCOUNTS)
                    {
                        bankAccounts.Add(new Bank_AccountDTO
                        {
                            ID = account.ID,
                            BANK_ID = account.BANK_ID,
                            IBAN = account.IBAN,
                            IS_ACTIVE = account.IS_ACTIVE,
                            UPDATED_TIMESTAMP = account.UPDATED_TIMESTAMP
                        });
                    }
                    clients.Add(new ClientDTO
                    {
                        ID = client.ID,
                        CLIENT_NAME = client.CLIENT_NAME,
                        UPDATED_TIMESTAMP = client.UPDATED_TIMESTAMP,
                        BANK_ACCOUNTS = bankAccounts,
                        BANK_ID = client.BANK_ID,
                        BANK = new BankDTO
                        {
                            ID = client.BANK.ID,
                            BANK_NAME = client.BANK.BANK_NAME,
                            PHONE=client.BANK.PHONE,
                            CITY_CENTRAL_ID=client.BANK.CITY_CENTRAL_ID,
                            BIC = client.BANK.BIC,
                            UPDATED_TIMESTAMP = client.BANK.UPDATED_TIMESTAMP,
                            CITY=new CityDTO
                            {
                                ID=client.BANK.CITY.ID,
                                CITY_NAME=client.BANK.CITY.CITY_NAME,
                                UPDATED_TIMESTAMP=client.BANK.CITY.UPDATED_TIMESTAMP
                            }
                        }
                    });
                }
                if (clients.Count > 0)
                {
                    return clients;
                }
                else
                {
                    return null;
                }
            }
        }
        public ClientDetailedDTO GetClientByID(long id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Client clientFromDB = unitOfWork.ClientRepository.GetByID(id);
                if (clientFromDB == null)
                {
                    return null;
                }
                ClientDetailedDTO client = new ClientDetailedDTO();

                Bank clientBank = unitOfWork.BankRepository.GetByID(clientFromDB.BANK_ID);
                List<Bank_AccountDTO> bankAccounts = new List<Bank_AccountDTO>();
                foreach (var account in clientFromDB.BANK_ACCOUNTS)
                {

                    bankAccounts.Add(new Bank_AccountDetailedDTO
                    {
                        ID = account.ID,
                        BANK_ID = account.BANK_ID,
                        CURRENT_BALANCE=account.CURRENT_BALANCE,
                        CREATED_AT=account.CREATED_AT,
                        IBAN = account.IBAN,
                        IS_ACTIVE = account.IS_ACTIVE,
                        UPDATED_TIMESTAMP = account.UPDATED_TIMESTAMP
                    });
                }

                client.ID = clientFromDB.ID;
                client.BANK_ID = clientFromDB.BANK_ID;
                client.CLIENT_NAME = clientFromDB.CLIENT_NAME;
                client.IDENTITY_CARD_NUMBER = clientFromDB.IDENTITY_CARD_NUMBER;
                client.PHONE = clientFromDB.PHONE;
                client.PERSONAL_NUMBER = clientFromDB.PERSONAL_NUMBER;
                client.UPDATED_TIMESTAMP = clientFromDB.UPDATED_TIMESTAMP;
                client.BANK_ACCOUNTS = bankAccounts;
                client.BANK = new BankDTO
                {
                    ID = clientBank.ID,
                    BANK_NAME = clientBank.BANK_NAME,
                    PHONE = clientBank.PHONE,
                    BIC = clientBank.BIC,
                    UPDATED_TIMESTAMP = clientBank.UPDATED_TIMESTAMP,
                    CITY = new CityDTO
                    {
                        ID = clientBank.CITY.ID,
                        CITY_NAME = clientBank.CITY.CITY_NAME,
                        UPDATED_TIMESTAMP = clientBank.CITY.UPDATED_TIMESTAMP
                    }
                };

                return client;
            }
        }

        public List<ClientDTO> GetAllClientsByName(string name)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<ClientDTO> clients = new List<ClientDTO>();
                foreach (var client in unitOfWork.ClientRepository.Get())
                {
                    if (!client.CLIENT_NAME.ToLower().Contains(name.ToLower())) continue;
                    List<Bank_AccountDTO> bankAccounts = new List<Bank_AccountDTO>();
                    foreach (var account in client.BANK_ACCOUNTS)
                    {
                        bankAccounts.Add(new Bank_AccountDTO
                        {
                            ID = account.ID,
                            BANK_ID = account.BANK_ID,
                            IBAN = account.IBAN,
                            IS_ACTIVE = account.IS_ACTIVE,
                            UPDATED_TIMESTAMP = account.UPDATED_TIMESTAMP
                        });
                    }
                    clients.Add(new ClientDTO
                    {
                        ID = client.ID,
                        CLIENT_NAME = client.CLIENT_NAME,
                        UPDATED_TIMESTAMP = client.UPDATED_TIMESTAMP,
                        BANK_ACCOUNTS = bankAccounts,
                        BANK_ID = client.BANK_ID,
                        BANK = new BankDTO
                        {
                            ID = client.BANK.ID,
                            BANK_NAME = client.BANK.BANK_NAME,
                            PHONE = client.BANK.PHONE,
                            BIC = client.BANK.BIC,
                            UPDATED_TIMESTAMP = client.BANK.UPDATED_TIMESTAMP,
                            CITY = new CityDTO
                            {
                                ID = client.BANK.CITY.ID,
                                CITY_NAME = client.BANK.CITY.CITY_NAME,
                                UPDATED_TIMESTAMP = client.BANK.CITY.UPDATED_TIMESTAMP
                            }
                        }
                    });
                }
                if (clients.Count > 0)
                {
                    return clients;
                }
                else
                {
                    return null;
                }
            }
        }
        public ClientDetailedDTO GetClientByPersonalNumber(string personalNumber)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Client clientFromDB=null;
                foreach (var clientToFind in unitOfWork.ClientRepository.Get())
                {
                    if (!clientToFind.PERSONAL_NUMBER.Equals(personalNumber.Trim().ToLower()))
                    {
                        continue;
                    }
                    else
                    {
                        clientFromDB = clientToFind;
                        break;
                    }
                }
                if (clientFromDB == null)
                {
                    return null;
                }
                ClientDetailedDTO client = new ClientDetailedDTO();

                Bank clientBank = unitOfWork.BankRepository.GetByID(clientFromDB.BANK_ID);
                List<Bank_AccountDTO> bankAccounts = new List<Bank_AccountDTO>();
                foreach (var account in clientFromDB.BANK_ACCOUNTS)
                {

                    bankAccounts.Add(new Bank_AccountDetailedDTO
                    {
                        ID = account.ID,
                        BANK_ID = account.BANK_ID,
                        CURRENT_BALANCE = account.CURRENT_BALANCE,
                        CREATED_AT = account.CREATED_AT,
                        IBAN = account.IBAN,
                        IS_ACTIVE = account.IS_ACTIVE,
                        UPDATED_TIMESTAMP = account.UPDATED_TIMESTAMP
                    });
                }

                client.ID = clientFromDB.ID;
                client.BANK_ID = clientFromDB.BANK_ID;
                client.CLIENT_NAME = clientFromDB.CLIENT_NAME;
                client.IDENTITY_CARD_NUMBER = clientFromDB.IDENTITY_CARD_NUMBER;
                client.PHONE = clientFromDB.PHONE;
                client.PERSONAL_NUMBER = clientFromDB.PERSONAL_NUMBER;
                client.UPDATED_TIMESTAMP = clientFromDB.UPDATED_TIMESTAMP;
                client.BANK_ACCOUNTS = bankAccounts;
                client.BANK = new BankDTO
                {
                    ID = clientBank.ID,
                    BANK_NAME = clientBank.BANK_NAME,
                    PHONE = clientBank.PHONE,
                    BIC = clientBank.BIC,
                    UPDATED_TIMESTAMP = clientBank.UPDATED_TIMESTAMP,
                    CITY = new CityDTO
                    {
                        ID = clientBank.CITY.ID,
                        CITY_NAME = clientBank.CITY.CITY_NAME,
                        UPDATED_TIMESTAMP = clientBank.CITY.UPDATED_TIMESTAMP
                    }
                };

                return client;
            }
        }

        public Tuple<string,bool> Save(ClientDetailedDTO clientDTO)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (clientDTO == null)
                {
                    return new Tuple<string, bool>("Не е изпратен клиент за създаване!", false);
                }
                if (clientDTO.CLIENT_NAME == null || clientDTO.CLIENT_NAME.Trim().Equals(""))
                {
                    return new Tuple<string, bool>("Не може да бъде изпращан клиент с празно име!", false);
                }
                if (!Regex.IsMatch(clientDTO.CLIENT_NAME, @"^[\p{IsCyrillic}\s]+$"))
                {
                    return new Tuple<string, bool>("Името на клиент може да съдържа само символи на кирилица и разстояние!", false);
                }
                if (clientDTO.CLIENT_NAME.Trim().Length > 150)
                {
                    return new Tuple<string, bool>("Името на този клиент е прекалено дълго!", false);
                }
                if (clientDTO.IDENTITY_CARD_NUMBER == null)
                {
                    return new Tuple<string, bool>("Номерът на личната карта е задължителен!", false);
                }
                if (clientDTO.PHONE == null)
                {
                    return new Tuple<string, bool>("Телефонният номер е задължителен!", false);
                }
                if (clientDTO.PERSONAL_NUMBER == null)
                {
                    return new Tuple<string, bool>("ЕГН е задължителен!", false);
                }
                if (clientDTO.IDENTITY_CARD_NUMBER.Trim().Length > 15)
                {
                    return new Tuple<string, bool>("Невалиден номер на лична карта!", false);
                }
                if (clientDTO.PHONE.Trim().Length != 10)
                {
                    return new Tuple<string, bool>("Невалиден телефонен номер!", false);
                }
                if (clientDTO.PERSONAL_NUMBER.Trim().Length != 10)
                {
                    return new Tuple<string, bool>("Невалидно ЕГН!", false);
                }
                foreach (char character in clientDTO.IDENTITY_CARD_NUMBER.Trim())
                {
                    if (!char.IsDigit(character))
                    {
                        return new Tuple<string, bool>("Невалиден номер на лична карта!", false);
                    }
                }
                foreach (char character in clientDTO.PHONE.Trim())
                {
                    if (!char.IsDigit(character))
                    {
                        return new Tuple<string, bool>("Невалиден телефонен номер!", false);
                    }
                }
                foreach (char character in clientDTO.PERSONAL_NUMBER.Trim())
                {
                    if (!char.IsDigit(character))
                    {
                        return new Tuple<string, bool>("Невалидно ЕГН!", false);
                    }
                }
                if (clientDTO.BANK_ID == 0)
                {
                    return new Tuple<string, bool>("Всеки клиент трябва да бъде записан към банка!", false);
                }
                foreach (var existingClient in unitOfWork.ClientRepository.Get())
                {
                    if (clientDTO.BANK_ID == existingClient.BANK_ID && (clientDTO.PERSONAL_NUMBER.Trim().ToLower().Equals(existingClient.PERSONAL_NUMBER.Trim().ToLower()) || clientDTO.IDENTITY_CARD_NUMBER.Trim().ToLower().Equals(existingClient.IDENTITY_CARD_NUMBER.Trim().ToLower()) || clientDTO.PHONE.Trim().ToLower().Equals(existingClient.PHONE.Trim().ToLower())))
                    {
                        return new Tuple<string, bool>("Този клиент вече съществува!", false);
                    }
                }

                Client client = new Client
                {
                    ID = clientDTO.ID,
                    BANK_ID = clientDTO.BANK_ID,
                    CLIENT_NAME = clientDTO.CLIENT_NAME.Trim(),
                    IDENTITY_CARD_NUMBER = clientDTO.IDENTITY_CARD_NUMBER.Trim(),
                    PHONE = clientDTO.PHONE.Trim(),
                    PERSONAL_NUMBER = clientDTO.PERSONAL_NUMBER.Trim(),
                    UPDATED_TIMESTAMP = DateTime.Now
                };
                BankAccountManagementService accountService = new BankAccountManagementService();
                Bank_Account account = accountService.Create(client.BANK_ID);
                if (account == null)
                {
                    return new Tuple<string, bool>("Създаването на клиент не бе успешно (Неуспешно създаване на банкова сметка)!", false);
                }
                client.BANK_ACCOUNTS.Add(account);
                try
                {
                    unitOfWork.ClientRepository.Insert(client);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешно бе добавен клиент!", true);
                }
                catch (ArgumentException)
                {
                    return new Tuple<string, bool>("Създаването на клиент не бе успешно!", false);
                }
            }
        }
        public Tuple<string, bool> Edit(ClientDetailedDTO clientDTO)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (clientDTO == null)
                {
                    return new Tuple<string, bool>("Не е изпратен клиент за създаване!", false);
                }
                if (clientDTO.CLIENT_NAME == null || clientDTO.CLIENT_NAME.Trim().Equals(""))
                {
                    return new Tuple<string, bool>("Не може да бъде изпращан клиент с празно име!", false);
                }
                if (!Regex.IsMatch(clientDTO.CLIENT_NAME, @"^[\p{IsCyrillic}\s]+$"))
                {
                    return new Tuple<string, bool>("Името на клиент може да съдържа само символи на кирилица и разстояние!", false);
                }
                if (clientDTO.CLIENT_NAME.Trim().Length > 150)
                {
                    return new Tuple<string, bool>("Името на този клиент е прекалено дълго!", false);
                }
                if (clientDTO.IDENTITY_CARD_NUMBER == null)
                {
                    return new Tuple<string, bool>("Номерът на личната карта е задължителен!", false);
                }
                if (clientDTO.PHONE == null)
                {
                    return new Tuple<string, bool>("Телефонният номер е задължителен!", false);
                }
                if (clientDTO.PERSONAL_NUMBER == null)
                {
                    return new Tuple<string, bool>("ЕГН е задължителен!", false);
                }
                if (clientDTO.IDENTITY_CARD_NUMBER.Trim().Length > 15)
                {
                    return new Tuple<string, bool>("Невалиден номер на лична карта!", false);
                }
                if (clientDTO.PHONE.Trim().Length != 10)
                {
                    return new Tuple<string, bool>("Невалиден телефонен номер!", false);
                }
                if (clientDTO.PERSONAL_NUMBER.Trim().Length != 10)
                {
                    return new Tuple<string, bool>("Невалидно ЕГН!", false);
                }
                foreach (char character in clientDTO.IDENTITY_CARD_NUMBER.Trim())
                {
                    if (!char.IsDigit(character))
                    {
                        return new Tuple<string, bool>("Невалиден номер на лична карта!", false);
                    }
                }
                foreach (char character in clientDTO.PHONE.Trim())
                {
                    if (!char.IsDigit(character))
                    {
                        return new Tuple<string, bool>("Невалиден телефонен номер!", false);
                    }
                }
                foreach (char character in clientDTO.PERSONAL_NUMBER.Trim())
                {
                    if (!char.IsDigit(character))
                    {
                        return new Tuple<string, bool>("Невалидно ЕГН!", false);
                    }
                }
                if (clientDTO.BANK_ID == 0)
                {
                    return new Tuple<string, bool>("Всеки клиент трябва да бъде записан към банка!", false);
                }
                if (clientDTO.UPDATED_TIMESTAMP == null)
                {
                    return new Tuple<string, bool>("Възникна грешка при промяната (Липса на дата на последна промяна)!", false);
                }
                Client client = unitOfWork.ClientRepository.GetByID(clientDTO.ID);
                if (client == null)
                {
                    return new Tuple<string, bool>("Такъв клиент не съществува", false);
                }
                if (!client.UPDATED_TIMESTAMP.Equals(clientDTO.UPDATED_TIMESTAMP))
                {
                    return new Tuple<string, bool>("Била е извършена промяна върху информацията за този клиент от друг потребител. Моля опреснете страницата и опитайте отново!", false);
                }
                client.CLIENT_NAME = clientDTO.CLIENT_NAME.Trim();
                client.BANK_ID = clientDTO.BANK_ID;
                client.IDENTITY_CARD_NUMBER = clientDTO.IDENTITY_CARD_NUMBER.Trim();
                client.PERSONAL_NUMBER = clientDTO.PERSONAL_NUMBER.Trim();
                client.UPDATED_TIMESTAMP = DateTime.Now;
                foreach (var existingClient in unitOfWork.ClientRepository.Get())
                {
                    if (clientDTO.ID != existingClient.ID && clientDTO.BANK_ID == existingClient.BANK_ID && clientDTO.PERSONAL_NUMBER.Trim().Equals(existingClient.PERSONAL_NUMBER.Trim()) && clientDTO.IDENTITY_CARD_NUMBER.Trim().Equals(existingClient.IDENTITY_CARD_NUMBER.Trim()) && clientDTO.PHONE.Trim().Equals(existingClient.PHONE.Trim()))
                    {
                        return new Tuple<string, bool>("Този клиент вече съществува!", false);
                    }
                }
                try
                {
                    unitOfWork.ClientRepository.Update(client);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешно бе променен клиент!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Промяната на клиент не бе успешна!", false);
                }
            }
        }
        public Tuple<string, bool> Delete(long id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Client client = unitOfWork.ClientRepository.GetByID(id);
                if (client == null)
                {
                    return new Tuple<string, bool>("Такъв клиент не съществува!", false);
                }
                try
                {
                    if (client.BANK_ACCOUNTS.Count != 0)
                    {
                        return new Tuple<string, bool>("Акаунтът на клиент не може да бъде изтрит докато има активна банкова сметка на негово име!", false);
                    }
                    unitOfWork.ClientRepository.Delete(id);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Избраният клиент бе изтрит успешно!", true);
                }
                catch (ArgumentException)
                {
                    return new Tuple<string, bool>("Избраният клиент не успя да бъде изтрит!", false);
                }
            }
        }
    }
}
