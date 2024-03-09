using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateAPIModel;
using TemplateAPIServices.Extensions;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;
using TemplateShared;

namespace TemplateAPIServices.Services
{
    public class UsersService : IUsersService
    {
        private readonly TemplateContext _context;

        public UsersService(TemplateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserModel> CreateNewUser(UserModel userModel)
        {
            UserExtensions.ValidateUser(userModel);

            if (userModel.ModelStatus == UserModelStatus.OK)
            {
                string salt = Tools.GetSalt();

                var dbUser = new User
                {
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    MiddleName = userModel.MiddleName,
                    Number = userModel.Number,
                    UserName = userModel.UserName,
                    PasswordHash = Tools.GetHash(userModel.Password + userModel.UserName + salt),
                    PasswordSalt = salt,
                    HealthCard = new HealthCard(),
                };

                if (userModel.HealthCardModel != null)
                {
                    dbUser.HealthCard.Insurance = userModel.HealthCardModel.Insurance;
                    dbUser.HealthCard.HealthCardIllnesses = new List<HealthCardIllness>();

                    foreach (IllnessModel illModel in userModel.HealthCardModel.Illnesses)
                    {
                        dbUser.HealthCard.HealthCardIllnesses.Add(new HealthCardIllness
                        {
                            HealthCard = dbUser.HealthCard,
                            HealthCardId = dbUser.HealthCard.Id,
                            Illness = IllnessExtensions.MapViewModel(illModel),
                            IllnessId = illModel.Id
                        });
                    }
                }

                AddressExtensions.ValidateAddress(userModel.AddressModel);

                if (userModel.AddressModel.ModelStatus == AddressModelStatus.OK) 
                {
                    dbUser.Address = new Address
                    {
                        Street = userModel.AddressModel.Street,
                        Building = userModel.AddressModel.Building,
                        Zip = userModel.AddressModel.Zip,
                        City = userModel.AddressModel.City,
                        Country = userModel.AddressModel.Country
                    };
                }
                else
                {
                    userModel.ModelStatus = UserModelStatus.InvalidAddress;
                    return userModel;
                }

                _context.User.Add(dbUser);
                await _context.SaveChangesAsync();

                return UserExtensions.MapDbModel(dbUser);
            }
            else
                return null;
        }


        public async Task<UserModel> UpdateUser(UserModel user)
        {
            UserExtensions.ValidateUser(user);

            if (user.ModelStatus == UserModelStatus.OK)
            {
                var dbUser = await _context.User.Include(u => u.Address).Include(u => u.HealthCard).SingleOrDefaultAsync(u => u.Id == user.Id);

                if (dbUser != null)
                {

                    dbUser.FirstName = user.FirstName;
                    dbUser.MiddleName = user.MiddleName;
                    dbUser.LastName = user.LastName;
                    dbUser.Number = user.Number;
                    dbUser.UserName = user.UserName;

                    AddressExtensions.ValidateAddress(user.AddressModel);

                    if(user.AddressModel.ModelStatus == AddressModelStatus.OK) 
                    {
                        dbUser.Address.Street = user.AddressModel.Street;
                        dbUser.Address.Building = user.AddressModel.Building;
                        dbUser.Address.Zip = user.AddressModel.Zip;
                        dbUser.Address.City = user.AddressModel.City;
                        dbUser.Address.Country = user.AddressModel.Country;
                    }

                    if(user.HealthCardModel != null)
                    {
                        dbUser.HealthCard.Insurance = user.HealthCardModel.Insurance;
                        dbUser.HealthCard.HealthCardIllnesses.Clear();

                        foreach(IllnessModel illM in user.HealthCardModel.Illnesses)
                        {
                            dbUser.HealthCard.HealthCardIllnesses.Add(new HealthCardIllness
                            { 
                                HealthCard = dbUser.HealthCard,
                                HealthCardId = dbUser.HealthCard.Id,
                                Illness = IllnessExtensions.MapViewModel(illM),
                                IllnessId = illM.Id
                            });
                        }
                    }

                    _context.Update(dbUser);
                    await _context.SaveChangesAsync();

                    return UserExtensions.MapDbModel(dbUser);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<UserModel> UpdateUserPass(int id, string pass)
        {
            if (id <= 0 || pass == null)
                return null;

            var dbUser = await _context.User.FirstOrDefaultAsync(u => u.Id == id);

            if (dbUser != null)
            {
                string salt = Tools.GetSalt();
                dbUser.PasswordHash = Tools.GetHash(pass + dbUser.UserName + salt);
                dbUser.PasswordSalt = salt;

                _context.Update(dbUser);
                await _context.SaveChangesAsync();

                return UserExtensions.MapDbModel(dbUser);
            }
            else
                return null;
        }

        public async Task<UserModel> FindUserByNumber(string number)
        {
            var dbUser = await _context.User
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Number == number);

            return dbUser != null ? UserExtensions.MapDbModel(dbUser) : null;
        }

        public async Task<List<UserModel>> FindAll()
        {
            try
            {
                List<UserModel> result = new List<UserModel>();

                List<User> dbUsers = await _context.User
                    .ToListAsync();

                foreach (User user in dbUsers)
                    result.Add(UserExtensions.MapDbModel(user));

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserModel> DeleteUser(int id)
        {
            var dbUser = await _context.User
                .Include(u => u.Address).Include(u => u.HealthCard)
                .SingleOrDefaultAsync(u => u.Id == id);

            if (dbUser != null)
            {
                if (dbUser.Address != null)
                    _context.Address.Remove(dbUser.Address);

                if (dbUser.HealthCard != null)
                    _context.HealthCard.Remove(dbUser.HealthCard);

                _context.User.Remove(dbUser);

                await _context.SaveChangesAsync();

                return UserExtensions.MapDbModel(dbUser);
            }
            else
                return null;
        }
    }
}
