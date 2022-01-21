using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Web;
using ApplicationService.ManagementServices;
using Data_Layer.Entities;
using Data_Layer.Context;

namespace ApplicationService.ManagementServices
{
    public class TokenAuthenticationManagementService
    {
        private readonly string secret;
        private readonly SymmetricSecurityKey securityKey;
        private readonly string issuer;
        private readonly string audience;
        private BankSystemAPIDBContext dbCtx;

        public TokenAuthenticationManagementService()
        {
            secret = "This is a very big secret";
            securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            issuer = "BankApi";
            audience = "BankApi";
            dbCtx = new BankSystemAPIDBContext();
        }

        public string GenerateClientToken(string personalNumber, string bankBIC)
        {
            long clientID = 0;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var client in unitOfWork.ClientRepository.Get())
                {
                    if (client.PERSONAL_NUMBER.Trim().Equals(personalNumber.Trim()) && client.BANK.BIC.ToLower().Trim().Equals(bankBIC.ToLower().Trim()))
                    {
                        clientID = client.ID;
                    }
                }
            }
            if (clientID == 0)
            {
                return null;
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Client", clientID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(14),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public bool ValidateClientToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey=true,
                    ValidateIssuer=true,
                    ValidateAudience=true,
                    ValidIssuer=issuer,
                    ValidAudience=audience,
                    IssuerSigningKey=securityKey,
                },out SecurityToken validatedToken);
                JwtSecurityToken securityToken = tokenHandler.ReadJwtToken(token);
                string clientID = securityToken.Claims.First(claim => claim.Type == "Client").Value;
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (unitOfWork.ClientRepository.GetByID(int.Parse(clientID)) == null)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool ValidateClientTokenAndConfirmIdentity(string token,long id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
                JwtSecurityToken securityToken = tokenHandler.ReadJwtToken(token);
                string clientID = securityToken.Claims.First(claim => claim.Type == "Client").Value;
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (unitOfWork.ClientRepository.GetByID(int.Parse(clientID)) == null || id != int.Parse(clientID))
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public string GenerateBankEmployeeToken(string personalNumber,string bankBIC)
        {
            long employeeID = 0;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var bankEmployee in unitOfWork.BankEmployeeRepository.Get())
                {
                    if (bankEmployee.PERSONAL_NUMBER.Trim().Equals(personalNumber.Trim()) && bankEmployee.BANK.BIC.ToLower().Trim().Equals(bankBIC.ToLower().Trim()))
                    {
                        employeeID = bankEmployee.ID;
                        break;
                    }
                }
            }
            if (employeeID == 0)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Employee", employeeID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(14),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public bool ValidateBankEmployeeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
                JwtSecurityToken securityToken = tokenHandler.ReadJwtToken(token);
                string employeeID = securityToken.Claims.First(claim => claim.Type == "Employee").Value;
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (unitOfWork.BankEmployeeRepository.GetByID(int.Parse(employeeID)) == null)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool ValidateBankEmployeeExecToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
                JwtSecurityToken securityToken = tokenHandler.ReadJwtToken(token);
                string employeeID = securityToken.Claims.First(claim => claim.Type == "Employee").Value;
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Bank_Employee employee = unitOfWork.BankEmployeeRepository.GetByID(int.Parse(employeeID));
                    if (employee == null)
                    {
                        return false;
                    }
                    else if (employee.EXEC_CODE == null)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}