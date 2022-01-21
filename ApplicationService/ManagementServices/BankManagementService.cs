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
    public class BankManagementService
    {
        public List<BankDTO> GetAllBanks()
        {
            List<BankDTO> banks = new List<BankDTO>();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var bank in unitOfWork.BankRepository.Get())
                {
                    List<Bank_BranchDTO> branches = new List<Bank_BranchDTO>();
                    foreach (var branch in unitOfWork.BankBranchRepository.Get())
                    {
                        City branchCity = unitOfWork.CityRepository.GetByID(branch.CITY_ID);
                        if (branch.BANK_ID == bank.ID)
                        {
                            branches.Add(new Bank_BranchDTO
                            {
                                ID = branch.ID,
                                BANK_ID = branch.BANK_ID,
                                CITY_ID = branch.CITY_ID,
                                BRANCH_NAME = branch.BRANCH_NAME,
                                BRANCH_ADDRESS = branch.BRANCH_ADDRESS,
                                PHONE = branch.PHONE,
                                UPDATED_TIMESTAMP = branch.UPDATED_TIMESTAMP,
                                CITY = new CityDTO
                                {
                                    ID = branchCity.ID,
                                    CITY_NAME = branchCity.CITY_NAME
                                }
                            });
                        }
                    }
                    banks.Add(new BankDTO
                    {
                        ID = bank.ID,
                        BANK_NAME = bank.BANK_NAME,
                        BIC = bank.BIC,
                        BANK_BRANCHES = branches,
                        UPDATED_TIMESTAMP = bank.UPDATED_TIMESTAMP,
                        PHONE = bank.PHONE,
                        CITY_CENTRAL_ID = bank.CITY_CENTRAL_ID,
                        CITY = new CityDTO
                        {
                            ID = bank.CITY.ID,
                            CITY_NAME = bank.CITY.CITY_NAME,
                            UPDATED_TIMESTAMP = bank.CITY.UPDATED_TIMESTAMP
                        }
                    });
                }
                if (banks.Count > 0)
                {
                    return banks;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<BankDTO> GetBankByBIC(string bic)
        {
            List<BankDTO> banks = new List<BankDTO>();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var bank in unitOfWork.BankRepository.Get())
                {
                    if (!bank.BIC.Trim().ToLower().Equals(bic.Trim().ToLower())) continue;
                    List<Bank_BranchDTO> branches = new List<Bank_BranchDTO>();
                    foreach (var branch in unitOfWork.BankBranchRepository.Get())
                    {
                        City branchCity = unitOfWork.CityRepository.GetByID(branch.CITY_ID);
                        if (branch.BANK_ID == bank.ID)
                        {
                            branches.Add(new Bank_BranchDTO
                            {
                                ID = branch.ID,
                                BANK_ID = branch.BANK_ID,
                                CITY_ID = branch.CITY_ID,
                                BRANCH_NAME = branch.BRANCH_NAME,
                                BRANCH_ADDRESS = branch.BRANCH_ADDRESS,
                                PHONE = branch.PHONE,
                                UPDATED_TIMESTAMP = branch.UPDATED_TIMESTAMP,
                                CITY = new CityDTO
                                {
                                    ID = branchCity.ID,
                                    CITY_NAME = branchCity.CITY_NAME
                                }
                            });
                        }
                    }
                    banks.Add(new BankDTO
                    {
                        ID = bank.ID,
                        BANK_NAME = bank.BANK_NAME,
                        BIC = bank.BIC,
                        BANK_BRANCHES = branches,
                        UPDATED_TIMESTAMP = bank.UPDATED_TIMESTAMP,
                        PHONE = bank.PHONE,
                        CITY_CENTRAL_ID = bank.CITY_CENTRAL_ID,
                        CITY = new CityDTO
                        {
                            ID = bank.CITY.ID,
                            CITY_NAME = bank.CITY.CITY_NAME,
                            UPDATED_TIMESTAMP = bank.CITY.UPDATED_TIMESTAMP
                        }
                    });
                }
                if (banks.Count > 0)
                {
                    return banks;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<BankDTO> GetBanksByName(string name)
        {
            List<BankDTO> banks = new List<BankDTO>();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var bank in unitOfWork.BankRepository.Get())
                {
                    if (!bank.BANK_NAME.ToLower().Contains(name.ToLower())) continue;

                    List<Bank_BranchDTO> branches = new List<Bank_BranchDTO>();
                    foreach (var branch in unitOfWork.BankBranchRepository.Get())
                    {
                        City branchCity = unitOfWork.CityRepository.GetByID(branch.CITY_ID);
                        if (branch.BANK_ID == bank.ID)
                        {
                            branches.Add(new Bank_BranchDTO
                            {
                                ID = branch.ID,
                                BANK_ID = branch.BANK_ID,
                                CITY_ID = branch.CITY_ID,
                                BRANCH_NAME = branch.BRANCH_NAME,
                                BRANCH_ADDRESS = branch.BRANCH_ADDRESS,
                                PHONE = branch.PHONE,
                                UPDATED_TIMESTAMP = branch.UPDATED_TIMESTAMP,
                                CITY = new CityDTO
                                {
                                    ID = branchCity.ID,
                                    CITY_NAME = branchCity.CITY_NAME
                                }
                            });
                        }
                    }
                    banks.Add(new BankDTO
                    {
                        ID = bank.ID,
                        BANK_NAME = bank.BANK_NAME,
                        BIC = bank.BIC,
                        BANK_BRANCHES = branches,
                        UPDATED_TIMESTAMP = bank.UPDATED_TIMESTAMP,
                        PHONE = bank.PHONE,
                        CITY_CENTRAL_ID = bank.CITY_CENTRAL_ID,
                        CITY = new CityDTO
                        {
                            ID = bank.CITY.ID,
                            CITY_NAME = bank.CITY.CITY_NAME,
                            UPDATED_TIMESTAMP = bank.CITY.UPDATED_TIMESTAMP
                        }
                    });
                }
                if (banks.Count > 0)
                {
                    return banks;
                }
                else
                {
                    return null;
                }
            }
        }
        public BankDTO GetBankByID(long id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Bank bank = unitOfWork.BankRepository.GetByID(id);
                List<Bank_BranchDTO> branches = new List<Bank_BranchDTO>();
                foreach (var branch in bank.BANK_BRANCHES)
                {
                    City branchCity = unitOfWork.CityRepository.GetByID(branch.CITY_ID);
                        branches.Add(new Bank_BranchDTO
                        {
                            ID = branch.ID,
                            BANK_ID = branch.BANK_ID,
                            CITY_ID = branch.CITY_ID,
                            BRANCH_NAME = branch.BRANCH_NAME,
                            BRANCH_ADDRESS = branch.BRANCH_ADDRESS,
                            PHONE = branch.PHONE,
                            UPDATED_TIMESTAMP = branch.UPDATED_TIMESTAMP,
                            CITY = new CityDTO
                            {
                                ID = branchCity.ID,
                                CITY_NAME = branchCity.CITY_NAME
                            }
                        });
                }
                BankDTO bankDTO=new BankDTO
                {
                    ID = bank.ID,
                    BANK_NAME = bank.BANK_NAME,
                    BIC = bank.BIC,
                    BANK_BRANCHES = branches,
                    UPDATED_TIMESTAMP = bank.UPDATED_TIMESTAMP,
                    PHONE = bank.PHONE,
                    CITY_CENTRAL_ID = bank.CITY_CENTRAL_ID,
                    CITY = new CityDTO
                    {
                        ID = bank.CITY.ID,
                        CITY_NAME = bank.CITY.CITY_NAME,
                        UPDATED_TIMESTAMP = bank.CITY.UPDATED_TIMESTAMP
                    }
                };
                return bankDTO;
            }
        }
        public Tuple<string,bool> Save(BankDTO bankDTO)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (bankDTO == null)
                {
                    return new Tuple<string, bool>("Не е изпратена банка за създаване!", false);
                }
                if (bankDTO.CITY_CENTRAL_ID == 0)
                {
                    return new Tuple<string, bool>("Всяка банка трябва да има централа!", false);
                }
                if (bankDTO.BANK_NAME == null || bankDTO.BANK_NAME.Trim().Equals("") || bankDTO.BIC == null || bankDTO.BIC.Trim().Equals(""))
                {
                    return new Tuple<string, bool>("Липсва информация за името на банката или нейния BIC код!", false);
                }
                if (bankDTO.BANK_NAME.Trim().Length > 90)
                {
                    return new Tuple<string, bool>("Прекалено дълго име на банка!", false);
                }
                if (bankDTO.BIC.Trim().Length != 8)
                {
                    return new Tuple<string, bool>("Грешен формат на BIC код!", false);
                }
                foreach (var character in bankDTO.BIC.Trim())
                {
                    if (!Char.IsDigit(character) && !Char.IsLetter(character))
                    {
                        return new Tuple<string, bool>("Грешен формат на BIC код!", false);
                    }
                }
                if (bankDTO.PHONE == null)
                {
                    return new Tuple<string, bool>("Телефонният номер е задължителен!", false);
                }
                if (bankDTO.PHONE.Trim().Length != 10)
                {
                    return new Tuple<string, bool>("Невалиден телефонен номер!", false);
                }
                foreach (char character in bankDTO.PHONE.Trim())
                {
                    if (!char.IsDigit(character))
                    {
                        return new Tuple<string, bool>("Невалиден телефонен номер!", false);
                    }
                }
                foreach (var bankInDB in unitOfWork.BankRepository.Get())
                {
                    if (bankInDB.BANK_NAME.ToLower().Trim().Equals(bankDTO.BANK_NAME.ToLower().Trim()))
                    {
                        return new Tuple<string, bool>("Банка с това име вече съществува!", false);
                    }
                    if (bankInDB.BIC.ToLower().Trim().Equals(bankDTO.BIC.ToLower().Trim()))
                    {
                        return new Tuple<string, bool>("Банка с такъв BIC код вече съществува!", false);
                    }
                }
                Bank bank = new Bank
                {
                    BANK_NAME = bankDTO.BANK_NAME.Trim(),
                    BIC = bankDTO.BIC.Trim(),
                    UPDATED_TIMESTAMP = DateTime.Now,
                    PHONE = bankDTO.PHONE.Trim(),
                    CITY_CENTRAL_ID = bankDTO.CITY_CENTRAL_ID
                };

                try
                {
                    unitOfWork.BankRepository.Insert(bank);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешно бе добавена нова банка!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Създаването на нова банка не бе успешно!", false);
                }
            }
        }
        public Tuple<string, bool> Edit(BankDTO bankDTO)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (bankDTO == null || bankDTO.BANK_NAME == null || bankDTO.BANK_NAME.Trim().Equals("") || bankDTO.BIC == null || bankDTO.BIC.Trim().Equals(""))
                {
                    return new Tuple<string, bool>("Липсва информация за името на банката или нейния BIC код!", false);
                }
                if (bankDTO.CITY_CENTRAL_ID == 0)
                {
                    return new Tuple<string, bool>("Всяка банка трябва да има централа!", false);
                }
                if (bankDTO.BANK_NAME.Trim().Length > 90)
                {
                    return new Tuple<string, bool>("Прекалено дълго име на банка!", false);
                }
                if (bankDTO.BIC.Trim().Length != 8)
                {
                    return new Tuple<string, bool>("Грешен формат на BIC код!", false);
                }
                foreach (var character in bankDTO.BIC.Trim())
                {
                    if (!Char.IsDigit(character) && !Char.IsLetter(character))
                    {
                        return new Tuple<string, bool>("Грешен формат на BIC код!", false);
                    }
                }
                if (bankDTO.UPDATED_TIMESTAMP == null)
                {
                    return new Tuple<string, bool>("Възникна грешка при промяната (Липса на дата на последна промяна)!", false);
                }
                if (bankDTO.PHONE == null)
                {
                    return new Tuple<string, bool>("Телефонният номер е задължителен!", false);
                }
                if (bankDTO.PHONE.Trim().Length != 10)
                {
                    return new Tuple<string, bool>("Невалиден телефонен номер!", false);
                }
                foreach (char character in bankDTO.PHONE.Trim())
                {
                    if (!char.IsDigit(character))
                    {
                        return new Tuple<string, bool>("Невалиден телефонен номер!", false);
                    }
                }
                Bank bank = unitOfWork.BankRepository.GetByID(bankDTO.ID);
                if (bank == null)
                {
                    return new Tuple<string, bool>("Такава банка не беше намерена!", false);
                }

                if (!bank.UPDATED_TIMESTAMP.Equals(bankDTO.UPDATED_TIMESTAMP))
                {
                    return new Tuple<string, bool>("Била е извършена промяна върху тази банка от друг потребител. Моля опреснете страницата и опитайте отново!", false);
                }
                foreach (var bankInDB in unitOfWork.BankRepository.Get())
                {
                    if (bankInDB.ID != bankDTO.ID)
                    {
                        if (bankInDB.BANK_NAME.ToLower().Trim().Equals(bankDTO.BANK_NAME.ToLower().Trim()))
                        {
                            return new Tuple<string, bool>("Банка с това име вече съществува!", false);
                        }
                        if (bankInDB.BIC.ToLower().Trim().Equals(bankDTO.BIC.ToLower().Trim()))
                        {
                            return new Tuple<string, bool>("Банка с такъв BIC код вече съществува!", false);
                        }
                    }
                }

                bank.BANK_NAME = bankDTO.BANK_NAME.Trim();
                bank.BIC = bankDTO.BIC.Trim();
                bank.UPDATED_TIMESTAMP = DateTime.Now;
                bank.PHONE = bankDTO.PHONE.Trim();
                bank.CITY_CENTRAL_ID = bankDTO.CITY_CENTRAL_ID;
                try
                {
                    unitOfWork.BankRepository.Update(bank);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешно бе променена избраната банка!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Промяната върху избраната банка не бе успешна!", false);
                }
            }
        }
        public Tuple<string, bool> Delete(long id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Bank bank = unitOfWork.BankRepository.GetByID(id);
                if (bank == null)
                {
                    return new Tuple<string, bool>("Такава банка не съществува!", false);
                }
                try
                {
                    if (bank.CLIENTS.Count != 0 || bank.BANK_BRANCHES.Count != 0)
                    {
                        return new Tuple<string, bool>("Избраната банка не може да бъде изтрита докато в нея има клиенти или има клонове!", false);
                    }
                    unitOfWork.BankRepository.Delete(id);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Избраната банка беше изтрита успешно!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Избраната банка не успя да бъде изтрита!", false);
                }
            }
        }
    }
}
