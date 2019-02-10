using MVC5_Template.DbContexts;
using MVC5_Template.Models.MVC5_TemplateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5_Template.DAL
{
    /// <summary>
    /// Class that handles CRUD operations on Localization entity in database
    /// </summary>
    public class LocaleDAL : MVC5_TemplateDAL
    {
        public LocaleDAL(MVC5_TemplateDbContext MVC5_TemplateDbContext) : base(MVC5_TemplateDbContext) { }

        public Locale GetLoacleByUserID(string userID)
        {
            var localization = from locale in MVC5_TemplateDbContext.Localization
                               join userSettings in MVC5_TemplateDbContext.UserSettings on locale.ID equals userSettings.LocalizationID
                               where userSettings.UserID == userID
                               select locale;

            return localization.First();
        }

        //////////////////////
        // Overriden members//
        //////////////////////

        /// <summary>
        /// Get Locale by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public override T GetByID<T>(int id)
        {
            Locale userSettings = MVC5_TemplateDbContext.Localization.Find(id);

            return (T)Convert.ChangeType(userSettings, typeof(Locale));
        }

        /// <summary>
        /// Add Locale to DbSet. (Call DbSet.SaveChanges() to insert it into database)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public override void CreateEntity<T>(T entity)
        {
            MVC5_TemplateDbContext.Localization.Add((Locale)Convert.ChangeType(entity, typeof(Locale)));
        }
    }
}