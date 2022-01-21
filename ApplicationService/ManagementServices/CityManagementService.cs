using ApplicationService.DTOs;
using Data_Layer.Context;
using Data_Layer.Entities;
using System;
using System.Collections.Generic;

namespace ApplicationService.ManagementServices
{
    public class CityManagementService
    {
        public List<CityDTO> GetAllCities()
        {
            List<CityDTO> cities = new List<CityDTO>();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.CityRepository.Get())
                {
                    cities.Add(new CityDTO
                    {
                        ID = item.ID,
                        CITY_NAME = item.CITY_NAME,
                        UPDATED_TIMESTAMP = item.UPDATED_TIMESTAMP
                    });
                }
            }
            if (cities.Count > 0)
            {
                return cities;
            }
            else
            {
                return null;
            }
        }
        public CityDTO GetCityById(long id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                City city = unitOfWork.CityRepository.GetByID(id);

                if (city != null)
                {
                    CityDTO cityDTO = new CityDTO
                    {
                        ID = city.ID,
                        CITY_NAME = city.CITY_NAME,
                        UPDATED_TIMESTAMP = city.UPDATED_TIMESTAMP
                    };
                    return cityDTO;
                }
                return null;
            }
        }
        public Tuple<string,bool> Save(CityDTO cityDTO)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (cityDTO == null)
                {
                    return new Tuple<string, bool>("Не е изпратен град за създаване!", false);
                }
                if (cityDTO.CITY_NAME == null || cityDTO.CITY_NAME.Trim().Equals(""))
                {
                    return new Tuple<string, bool>("Името на града не може да бъде празно!", false);
                }
                if (cityDTO.CITY_NAME.Trim().Length > 35)
                {
                    return new Tuple<string, bool>("Името на града е прекалено дълго!", false);
                }
                //if (!dbCtx.CITIES.ToList().Find(x => x.CITY_NAME.Equals(cityDTO.CITY_NAME)).Equals(null))
                foreach (var item in unitOfWork.CityRepository.Get())
                {
                    if (item.CITY_NAME.ToLower().Trim().Equals(cityDTO.CITY_NAME.ToLower().Trim()))
                    {
                        return new Tuple<string, bool>("Този град вече съществува!", false);
                    }
                }
                City city = new City
                {
                    CITY_NAME = cityDTO.CITY_NAME.Trim(),
                    UPDATED_TIMESTAMP = DateTime.Now
                };
                try
                {

                    unitOfWork.CityRepository.Insert(city);
                    unitOfWork.Save();

                    return new Tuple<string, bool>("Градът беше успешно запазен!", true);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.ToLower().Contains("constraint"))
                        {
                            return new Tuple<string, bool>("Имената на градовете могат да съдържат само букви на кирилица и разстояние!", false);
                        }
                    }
                    return new Tuple<string, bool>("Градът не успя да бъде запазен!", false);
                }
            }
        }
        public Tuple<string, bool> Edit(CityDTO cityDTO)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (cityDTO == null)
                {
                    return new Tuple<string, bool>("Не е изпратен град за създаване!", false);
                }
                if (cityDTO.CITY_NAME == null || cityDTO.CITY_NAME.Trim().Equals(""))
                {
                    return new Tuple<string, bool>("Името на града не може да бъде празно!", false);
                }

                if (cityDTO.CITY_NAME.Trim().Length > 35)
                {
                    return new Tuple<string, bool>("Името на града е прекалено дълго!", false);
                }
                if (cityDTO.UPDATED_TIMESTAMP == null)
                {
                    return new Tuple<string, bool>("Възникна грешка при промяната (Липса на дата на последна промяна)!", false);
                }
                City city = unitOfWork.CityRepository.GetByID(cityDTO.ID);
                if (city == null)
                {
                    return new Tuple<string, bool>("Такъв град не беше намерен!", false);
                }

                if (!city.UPDATED_TIMESTAMP.Equals(cityDTO.UPDATED_TIMESTAMP))
                {
                    return new Tuple<string, bool>("Била е извършена промяна върху този град от друг потребител. Моля опреснете страницата и опитайте отново!", false);
                }
                if (city.BANK_BRANCHES.Count != 0)
                {
                    return new Tuple<string, bool>("Градът не може да бъде променян докато на него има отворени клонове на банки!", false);
                }
                if (city.BANKS.Count != 0)
                {
                    return new Tuple<string, bool>("Градът не може да бъде променян докато има банки с център този град!", false);
                }
                foreach (var cityInDB in unitOfWork.CityRepository.Get())
                {
                    if (cityInDB.ID != cityDTO.ID && cityInDB.CITY_NAME.ToLower().Trim().Equals(cityDTO.CITY_NAME.ToLower().Trim()))
                    {
                        return new Tuple<string, bool>("Този град вече съществува!", false);
                    }
                }
                city.CITY_NAME = cityDTO.CITY_NAME.Trim();
                city.UPDATED_TIMESTAMP = DateTime.Now;
                try
                {

                    unitOfWork.CityRepository.Update(city);
                    unitOfWork.Save();

                    return new Tuple<string, bool>("Градът беше успешно променен!", true);
                }
                catch (ArgumentException ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.ToLower().Contains("constraint"))
                        {
                            return new Tuple<string, bool>("Имената на градовете могат да съдържат само букви и разстояние!", false);
                        }
                    }
                    return new Tuple<string, bool>("Градът не успя да бъде променен!", false);
                }
            }
        }
        public Tuple<string, bool> Delete(long id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                City city = unitOfWork.CityRepository.GetByID(id);
                if (city == null)
                {
                    return new Tuple<string, bool>("Такъв град не съществува!", false);
                }
                try
                {
                    if (city.BANK_BRANCHES.Count != 0)
                    {
                        return new Tuple<string, bool>("Градът не може да бъде изтрит докато на него има отворени клонове на банки!", false);
                    }
                    if (city.BANKS.Count != 0)
                    {
                        return new Tuple<string, bool>("Градът не може да бъде изтрит докато има банки с център този град!", false);
                    }

                    unitOfWork.CityRepository.Delete(id);
                    unitOfWork.Save();

                    return new Tuple<string, bool>("Градът беше изтрит успешно!", true);
                }
                catch (Exception)
                {
                    return new Tuple<string, bool>("Градът не успя да бъде изтрит!", false);
                }
            }
        }
    }
}
