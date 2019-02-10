using MVC5_Template.DbContexts;
using MVC5_Template.Models.MVC5_TemplateModels;
using System;
using System.Linq;

namespace MVC5_Template.DAL
{
  /// <summary>
  /// Class that handles CRUD operations on UserSettings entity in database
  /// </summary>
  public class UserSettingsDAL : MVC5_TemplateDAL
    {
        public UserSettingsDAL(MVC5_TemplateDbContext MVC5_TemplateDbContext) : base(MVC5_TemplateDbContext) { }

        /// <summary>
        /// Get UserSettings by userID.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserSettings GetByUserID(string userID)
        {
            _UserSettings = (from userSettings in MVC5_TemplateDbContext.UserSettings
                                where userSettings.UserID == userID
                                select userSettings).First();

            return _UserSettings;
        }

        public void CreateUserSettings(UserSettings userSettings)
        {
            MVC5_TemplateDbContext.UserSettings.Add(userSettings);
        }

        //////////////////////
        // Overriden members//
        //////////////////////

        /// <summary>
        /// Get UserSettings by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public override T GetByID<T>(int id)
        {
            UserSettings userSettings = MVC5_TemplateDbContext.UserSettings.Find(id);

            return (T)Convert.ChangeType(userSettings, typeof(UserSettings));
        }

        /// <summary>
        /// Add UserSettings to DbSet. (Call DbSet.SaveChanges() to insert it into database)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public override void CreateEntity<T>(T entity)
        {
            MVC5_TemplateDbContext.UserSettings.Add((UserSettings)Convert.ChangeType(entity, typeof(UserSettings)));
        }

        //Private members
        private UserSettings _UserSettings { get; set; }
    }
}