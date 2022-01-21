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
    public class BankBranchManagementService
    {
        public List<Bank_BranchDTO> GetBankBranchesByBank(long bankID)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                List<Bank_BranchDTO> bankBranches = new List<Bank_BranchDTO>();
                foreach (var bankBranch in unitOfWork.BankBranchRepository.Get())
                {
                    if (bankBranch.BANK_ID != bankID) continue;
                    City branchCity = unitOfWork.CityRepository.GetByID(bankBranch.CITY_ID);
                    Bank branchBank = unitOfWork.BankRepository.GetByID(bankBranch.BANK_ID);
                    bankBranches.Add(new Bank_BranchDTO
                    {
                        ID = bankBranch.ID,
                        BANK_ID = bankBranch.BANK_ID,
                        CITY_ID = bankBranch.CITY_ID,
                        BRANCH_NAME = bankBranch.BRANCH_NAME,
                        BRANCH_ADDRESS = bankBranch.BRANCH_ADDRESS,
                        PHONE = bankBranch.PHONE,
                        UPDATED_TIMESTAMP = bankBranch.UPDATED_TIMESTAMP,
                        CITY = new CityDTO
                        {
                            ID = branchCity.ID,
                            CITY_NAME = branchCity.CITY_NAME,
                            UPDATED_TIMESTAMP = branchCity.UPDATED_TIMESTAMP
                        },
                        BANK = new BankDTO
                        {
                            ID = branchBank.ID,
                            BANK_NAME = branchBank.BANK_NAME,
                            BIC = branchBank.BIC,
                            UPDATED_TIMESTAMP = branchBank.UPDATED_TIMESTAMP
                        }
                    });
                }
                if (bankBranches.Count > 0)
                {
                    return bankBranches;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<Bank_BranchDTO> GetBankBranchesByCity(long cityID)
        {
            List<Bank_BranchDTO> bankBranches = new List<Bank_BranchDTO>();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var bankBranch in unitOfWork.BankBranchRepository.Get())
                {
                    if (bankBranch.CITY_ID != cityID) continue;

                    City branchCity = unitOfWork.CityRepository.GetByID(bankBranch.CITY_ID);
                    Bank branchBank = unitOfWork.BankRepository.GetByID(bankBranch.BANK_ID);
                    bankBranches.Add(new Bank_BranchDTO
                    {
                        ID = bankBranch.ID,
                        BANK_ID = bankBranch.BANK_ID,
                        CITY_ID = bankBranch.CITY_ID,
                        BRANCH_NAME = bankBranch.BRANCH_NAME,
                        BRANCH_ADDRESS = bankBranch.BRANCH_ADDRESS,
                        PHONE = bankBranch.PHONE,
                        UPDATED_TIMESTAMP = bankBranch.UPDATED_TIMESTAMP,
                        CITY = new CityDTO
                        {
                            ID = branchCity.ID,
                            CITY_NAME = branchCity.CITY_NAME,
                            UPDATED_TIMESTAMP = branchCity.UPDATED_TIMESTAMP
                        },
                        BANK = new BankDTO
                        {
                            ID = branchBank.ID,
                            BANK_NAME = branchBank.BANK_NAME,
                            BIC = branchBank.BIC,
                            UPDATED_TIMESTAMP = branchBank.UPDATED_TIMESTAMP
                        }
                    });
                }
                if (bankBranches.Count > 0)
                {
                    return bankBranches;
                }
                else
                {
                    return null;
                }
            }
        }
        public Bank_BranchDTO GetBankBranchByID(long id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Bank_Branch bankBranch = unitOfWork.BankBranchRepository.GetByID(id);
                if (bankBranch == null)
                {
                    return null;
                }
                City branchCity = unitOfWork.CityRepository.GetByID(bankBranch.CITY_ID);
                Bank branchBank = unitOfWork.BankRepository.GetByID(bankBranch.BANK_ID);
                Bank_BranchDTO branchDTO=new Bank_BranchDTO
                    {
                        ID = bankBranch.ID,
                        BANK_ID = bankBranch.BANK_ID,
                        CITY_ID = bankBranch.CITY_ID,
                        BRANCH_NAME = bankBranch.BRANCH_NAME,
                        BRANCH_ADDRESS = bankBranch.BRANCH_ADDRESS,
                        PHONE = bankBranch.PHONE,
                        UPDATED_TIMESTAMP = bankBranch.UPDATED_TIMESTAMP,
                        CITY = new CityDTO
                        {
                            ID = branchCity.ID,
                            CITY_NAME = branchCity.CITY_NAME,
                            UPDATED_TIMESTAMP = branchCity.UPDATED_TIMESTAMP
                        },
                        BANK = new BankDTO
                        {
                            ID = branchBank.ID,
                            BANK_NAME = branchBank.BANK_NAME,
                            PHONE=branchBank.PHONE,
                            CITY_CENTRAL_ID=branchBank.CITY_CENTRAL_ID,
                            BIC = branchBank.BIC,
                            UPDATED_TIMESTAMP = branchBank.UPDATED_TIMESTAMP
                        }
                    };
                return branchDTO;
            }
        }
        public Tuple<string,bool> Save(Bank_BranchDTO branchDTO)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (branchDTO == null)
                {
                    return new Tuple<string, bool>("Не е изпратен банков клон за регистриране!", false);
                }
                if (branchDTO.BANK_ID == 0 || branchDTO.CITY_ID == 0)
                {
                    return new Tuple<string, bool>("Всеки клон на банка трябва да има град, в който се намира, и банка, към която принадлежи!", false);
                }
                if (branchDTO.BRANCH_NAME == null || branchDTO.BRANCH_NAME.Trim().Equals(""))
                {
                    return new Tuple<string, bool>("Всеки клон на банка трябва да притежава име!", false);
                }
                if (branchDTO.BRANCH_NAME.Trim().Length > 60)
                {
                    return new Tuple<string, bool>("Името на клона е прекалено дълго!", false);
                }
                if (branchDTO.BRANCH_ADDRESS == null || branchDTO.BRANCH_ADDRESS.Trim().Equals(""))
                {
                    return new Tuple<string, bool>("За всеки клон на банка трябва да бъде указан адрес!", false);
                }
                if (branchDTO.BRANCH_ADDRESS.Trim().Length > 70)
                {
                    return new Tuple<string, bool>("Името на адреса на този клон е прекалено дълго!", false);
                }
                if (branchDTO.PHONE == null || branchDTO.PHONE.Trim().Length != 10)
                {
                    return new Tuple<string, bool>("Грешен или невалиден телефонен номер!", false);
                }
                foreach (char character in branchDTO.PHONE.Trim())
                {
                    if (!char.IsDigit(character))
                    {
                        return new Tuple<string, bool>("Невалиден телефонен номер!", false);
                    }
                }
                foreach (var branch in unitOfWork.BankBranchRepository.Get())
                {
                    if (branchDTO.BRANCH_NAME.ToLower().Trim().Equals(branch.BRANCH_NAME.ToLower().Trim()))
                    {
                        return new Tuple<string, bool>("Такъв клон вече съществува!", false);
                    }
                    if (branchDTO.BRANCH_ADDRESS.ToLower().Trim().Equals(branch.BRANCH_ADDRESS.ToLower().Trim()))
                    {
                        return new Tuple<string, bool>("Банков клон на този адрес вече съществува!", false);
                    }
                    if (branchDTO.PHONE.ToLower().Trim().Equals(branch.PHONE.ToLower().Trim()))
                    {
                        return new Tuple<string, bool>("Този телефонен номер вече е записан към друг банков клон!", false);
                    }
                }
                Bank_Branch bankBranch = new Bank_Branch
                {
                    ID = branchDTO.ID,
                    BANK_ID = branchDTO.BANK_ID,
                    CITY_ID = branchDTO.CITY_ID,
                    BRANCH_NAME = branchDTO.BRANCH_NAME.Trim(),
                    BRANCH_ADDRESS = branchDTO.BRANCH_ADDRESS.Trim(),
                    UPDATED_TIMESTAMP = DateTime.Now,
                    PHONE = branchDTO.PHONE.Trim()
                };
                try
                {
                    unitOfWork.BankBranchRepository.Insert(bankBranch);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешно бе добавена нов клон!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Създаването на нов клон не бе успешно!", false);
                }
            }
        }
        public Tuple<string, bool> Edit(Bank_BranchDTO branchDTO)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (branchDTO == null)
                {
                    return new Tuple<string, bool>("Не е изпратен банков клон за регистриране!", false);
                }
                if (branchDTO.BANK_ID == 0 || branchDTO.CITY_ID == 0)
                {
                    return new Tuple<string, bool>("Всеки клон на банка трябва да има град, в който се намира, и банка, към която принадлежи!", false);
                }
                if (branchDTO.BRANCH_NAME == null || branchDTO.BRANCH_NAME.Trim().Equals(""))
                {
                    return new Tuple<string, bool>("Всеки клон на банка трябва да притежава име!", false);
                }
                if (branchDTO.BRANCH_NAME.Trim().Length > 60)
                {
                    return new Tuple<string, bool>("Името на клона е прекалено дълго!", false);
                }
                if (branchDTO.BRANCH_ADDRESS == null || branchDTO.BRANCH_ADDRESS.Trim().Equals(""))
                {
                    return new Tuple<string, bool>("За всеки клон на банка трябва да бъде указан адрес!", false);
                }
                if (branchDTO.BRANCH_ADDRESS.Trim().Length > 70)
                {
                    return new Tuple<string, bool>("Името на адреса на този клон е прекалено дълго!", false);
                }
                if (branchDTO.PHONE == null || branchDTO.PHONE.Trim().Length != 10)
                {
                    return new Tuple<string, bool>("Грешен или невалиден телефонен номер!", false);
                }
                foreach (char character in branchDTO.PHONE.Trim())
                {
                    if (!char.IsDigit(character))
                    {
                        return new Tuple<string, bool>("Невалиден телефонен номер!", false);
                    }
                }
                if (branchDTO.UPDATED_TIMESTAMP == null)
                {
                    return new Tuple<string, bool>("Възникна грешка при промяната (Липса на дата на последна промяна)!", false);
                }
                Bank_Branch branch = unitOfWork.BankBranchRepository.GetByID(branchDTO.ID);
                if (branch == null)
                {
                    return new Tuple<string, bool>("Този клон не беше намерен в базата данни!", false);
                }

                if (!branch.UPDATED_TIMESTAMP.Equals(branchDTO.UPDATED_TIMESTAMP))
                {
                    return new Tuple<string, bool>("Била е извършена промяна върху този клон от друг потребител. Моля опреснете страницата и опитайте отново!", false);
                }

                foreach (var branchInDB in unitOfWork.BankBranchRepository.Get())
                {
                    if (branchInDB.ID != branchDTO.ID)
                    {
                        if (branchDTO.BRANCH_NAME.ToLower().Trim().Equals(branchInDB.BRANCH_NAME.ToLower().Trim()))
                        {
                            return new Tuple<string, bool>("Такъв клон вече съществува!", false);
                        }
                        if (branchDTO.BRANCH_ADDRESS.ToLower().Trim().Equals(branchInDB.BRANCH_ADDRESS.ToLower().Trim()) && branchDTO.CITY_ID==branchInDB.CITY_ID)
                        {
                            return new Tuple<string, bool>("Банков клон на този адрес вече съществува!", false);
                        }
                        if (branchDTO.PHONE.ToLower().Trim().Equals(branchInDB.PHONE.ToLower().Trim()))
                        {
                            return new Tuple<string, bool>("Този телефонен номер вече е записан към друг банков клон!", false);
                        }
                    }
                }
                branch.BRANCH_NAME = branchDTO.BRANCH_NAME.Trim();
                branch.BRANCH_ADDRESS = branchDTO.BRANCH_ADDRESS.Trim();
                branch.PHONE = branchDTO.PHONE.Trim();
                branch.UPDATED_TIMESTAMP = DateTime.Now;
                try
                {
                    unitOfWork.BankBranchRepository.Update(branch);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Успешно бе променен избрания клон!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Промяната на ибрания клон не бе успешно!", false);
                }
            }
        }
        public Tuple<string, bool> Delete(long id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Bank_Branch bankBranch = unitOfWork.BankBranchRepository.GetByID(id);
                if (bankBranch == null)
                {
                    return new Tuple<string, bool>("Избраният клон не съществува!", false);
                }
                try
                {
                    unitOfWork.BankBranchRepository.Delete(id);
                    unitOfWork.Save();
                    return new Tuple<string, bool>("Избраният клон беше изтрит успешно!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Избраният клон не успя да бъде изтрит!", false);
                }
            }
        }
    }
}
