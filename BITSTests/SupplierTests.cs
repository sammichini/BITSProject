using NUnit.Framework;
using BreweryProject.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace BITSTests
{
    public class SupplierTests
    {

        BitsContext dbContext;
        Supplier s;

        List<Supplier> suppliers;
        [SetUp]
        public void Setup()
        {
            dbContext = new BitsContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetSupplierData");
        }

        [Test]
        public void GetAllTest()
        {
            suppliers = dbContext.Supplier.OrderBy(s => s.Name).ToList();
            Assert.AreEqual(6, suppliers.Count);
            Assert.AreEqual("BSG Craft Brewing", suppliers[0].Name);
            
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            s = dbContext.Supplier.Find(1);
            Assert.IsNotNull(s);
            Assert.AreEqual("BSG Craft Brewing", s.Name);
        }

        [Test]
        public void GetUsingWhere()
        {
            // get list of suppliers witg "B" name 
            suppliers = dbContext.Supplier.Where(s => s.Name.StartsWith("B")).OrderBy(s => s.Name).ToList();
            Assert.AreEqual(1, suppliers.Count);
            Assert.AreEqual("BSG Craft Brewing", suppliers[0].Name);

        }


        [Test]
        public void GetWithJoinTest()
        {
            // get a list of objects that include the Supplier id, name, Website and ContactLastName
            var Supplier = dbContext.Supplier.Join(
               dbContext.IngredientInventoryAddition,
               c => c.SupplierId,
               s => s.SupplierId,
               (c, s) => new { c.SupplierId, c.Name, c.Phone, c.Email, c.Website, c.ContactFirstName, c.ContactLastName, c.ContactPhone, c.ContactEmail, c.Note,  s.IngredientId }).OrderBy(r => r.Name).ToList();
            Assert.AreEqual(35, Supplier.Count);
            Console.WriteLine(Supplier.ToString()); //not right yet
        }

        [Test]
        public void CreateTest()
        {
            Supplier b = new Supplier();
            b.SupplierId = 7;
            b.Name = "TestSupplier";
            b.Phone = "11111";
            b.Email = "ex@ex.ex";
            b.Website = "ex.ex";
            b.ContactFirstName = "Flapjack";
            b.ContactLastName = "Mapleface";
            b.ContactPhone = "111111";
            b.ContactEmail = "ex@ex.ex";
            b.Note = "";
            dbContext.Supplier.Add(b);
            dbContext.SaveChanges();
            Supplier b2 = dbContext.Supplier.Find(b.SupplierId);
            Assert.True(b2.Name == "TestSupplier");
            Assert.True(b2.Phone == "11111");
            Assert.True(b2.Email == "ex@ex.ex");
            Assert.True(b2.Website == "ex.ex");
            Assert.True(b2.ContactFirstName == "Flapjack");
            Assert.True(b2.ContactLastName == "Mapleface");

        }

        [Test]
        public void UpdateTest()
        {
            Supplier s = dbContext.Supplier.Find(5);
            
            s.ContactFirstName = "fnameupdated";
            
            dbContext.Supplier.Update(s);
            dbContext.SaveChanges();
            Supplier c2 = dbContext.Supplier.Find(5);
       
            Assert.True(c2.ContactFirstName == "fnameupdated");
  

        }

        [Test]
        public void DeleteTest()
        {
            s = dbContext.Supplier.Find(7);
            dbContext.Supplier.Remove(s);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Supplier.Find(7));
        }





        public void PrintAll(List<Supplier> Supplier)
        {
            foreach (Supplier c in Supplier)
            {
                Console.WriteLine(c);
            }
        }
    }
}