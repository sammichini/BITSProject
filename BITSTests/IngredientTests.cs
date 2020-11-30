using NUnit.Framework;
using BreweryProject.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace BITSTests
{
    public class IngredientTests
    {

        BitsContext dbContext;
        Ingredient s;

        List<Ingredient> ingredients;
        [SetUp]
        public void Setup()
        {
            dbContext = new BitsContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetIngredientData");
        }   //reset stored proc... update problem

        [Test]
        public void GetAllTest()
        {
            ingredients = dbContext.Ingredient.OrderBy(s => s.Name).ToList();
            Assert.AreEqual(1149, ingredients.Count);
            Assert.AreEqual("Abbaye Belgian", ingredients[0].Name);

        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            s = dbContext.Ingredient.Find(1);
            Assert.IsNotNull(s);
            Assert.AreEqual("Acid Malt", s.Name);
        }

        [Test]
        public void GetUsingWhere()
        {

            ingredients = dbContext.Ingredient.Where(s => s.Name.StartsWith("B")).OrderBy(s => s.Name).ToList();
            Assert.AreEqual(166, ingredients.Count);


        }


        /*[Test]
        public void GetWithJoinTest()
        {
            // get a list of objects that include the Ingredient id, name, Website and ContactLastName
            var Ingredient = dbContext.Ingredient.Join(
               dbContext.IngredientInventoryAddition,
               c => c.Name,
               s => s.Name,
               (c, s) => new { c.Name, c.Name, c.Phone, c.Email, c.Website, c.ContactFirstName, c.ContactLastName, c.ContactPhone, c.ContactEmail, c.Note, s.IngredientId }).OrderBy(r => r.Name).ToList();
            Assert.AreEqual(35, Ingredient.Count);
            Console.WriteLine(Ingredient.ToString()); //not right yet
        }*/

        /*[Test]
        public void CreateTest()
        {
            Ingredient b = new Ingredient();
            b.Name = 7;
            b.Name = "TestSupplier";
            b.Phone = "11111";
            b.Email = "ex@ex.ex";
            b.Website = "ex.ex";
            b.ContactFirstName = "Flapjack";
            b.ContactLastName = "Mapleface";
            b.ContactPhone = "111111";
            b.ContactEmail = "ex@ex.ex";
            b.Note = "";
            dbContext.Ingredient.Add(b);
            dbContext.SaveChanges();
            Ingredient b2 = dbContext.Ingredient.Find(b.Name);
            Assert.True(b2.Name == "TestSupplier");
            Assert.True(b2.Phone == "11111");
            Assert.True(b2.Email == "ex@ex.ex");
            Assert.True(b2.Website == "ex.ex");
            Assert.True(b2.ContactFirstName == "Flapjack");
            Assert.True(b2.ContactLastName == "Mapleface");

        }*/

        /*[Test]
        public void UpdateTest()
        {
            Ingredient s = dbContext.Ingredient.Find(5);

            s.ContactFirstName = "fnameupdated";

            dbContext.Ingredient.Update(s);
            dbContext.SaveChanges();
            Ingredient c2 = dbContext.Ingredient.Find(5);

            Assert.True(c2.ContactFirstName == "fnameupdated");


        }*/

        [Test]
        public void DeleteTest()
        {
            s = dbContext.Ingredient.Find(7);
            dbContext.Ingredient.Remove(s);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Ingredient.Find(7));
        }





        public void PrintAll(List<Ingredient> Ingredient)
        {
            foreach (Ingredient c in Ingredient)
            {
                Console.WriteLine(c);
            }
        }
    }
}