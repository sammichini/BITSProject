using NUnit.Framework;
using BreweryProject.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace BITSTests
{
    public class AddressTests
    {

        BitsContext dbContext;
        Address s;

        List<Address> addresses;
        [SetUp]
        public void Setup()
        {
            dbContext = new BitsContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetAddressData");
        }   //reset stored proc... update problem

        [Test]
        public void GetAllTest()
        {
            addresses = dbContext.Address.OrderBy(s => s.AddressId).ToList();
            Assert.AreEqual(7, addresses.Count);
            Assert.AreEqual(1, addresses[0].AddressId);

        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            s = dbContext.Address.Find(1);
            Assert.IsNotNull(s);
            Assert.AreEqual(1, s.AddressId);
        }

        [Test]
        public void GetUsingWhere()
        {
            
            addresses = dbContext.Address.Where(s => s.AddressId == 1).OrderBy(s => s.AddressId).ToList();
            Assert.AreEqual(1, addresses.Count);
           

        }


        /*[Test]
        public void GetWithJoinTest()
        {
            // get a list of objects that include the Address id, name, Website and ContactLastName
            var Address = dbContext.Address.Join(
               dbContext.IngredientInventoryAddition,
               c => c.AddressId,
               s => s.AddressId,
               (c, s) => new { c.AddressId, c.AddressId, c.Phone, c.Email, c.Website, c.ContactFirstName, c.ContactLastName, c.ContactPhone, c.ContactEmail, c.Note, s.IngredientId }).OrderBy(r => r.AddressId).ToList();
            Assert.AreEqual(35, Address.Count);
            Console.WriteLine(Address.ToString()); //not right yet
        }*/

        /*[Test]
        public void CreateTest()
        {
            Address b = new Address();
            b.AddressId = 7;
            b.AddressId = "TestSupplier";
            b.Phone = "11111";
            b.Email = "ex@ex.ex";
            b.Website = "ex.ex";
            b.ContactFirstName = "Flapjack";
            b.ContactLastName = "Mapleface";
            b.ContactPhone = "111111";
            b.ContactEmail = "ex@ex.ex";
            b.Note = "";
            dbContext.Address.Add(b);
            dbContext.SaveChanges();
            Address b2 = dbContext.Address.Find(b.AddressId);
            Assert.True(b2.AddressId == "TestSupplier");
            Assert.True(b2.Phone == "11111");
            Assert.True(b2.Email == "ex@ex.ex");
            Assert.True(b2.Website == "ex.ex");
            Assert.True(b2.ContactFirstName == "Flapjack");
            Assert.True(b2.ContactLastName == "Mapleface");

        }*/

        /*[Test]
        public void UpdateTest()
        {
            Address s = dbContext.Address.Find(5);

            s.ContactFirstName = "fnameupdated";

            dbContext.Address.Update(s);
            dbContext.SaveChanges();
            Address c2 = dbContext.Address.Find(5);

            Assert.True(c2.ContactFirstName == "fnameupdated");


        }*/

        [Test]
        public void DeleteTest()
        {
            s = dbContext.Address.Find(7);
            dbContext.Address.Remove(s);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Address.Find(7));
        }





        public void PrintAll(List<Address> Address)
        {
            foreach (Address c in Address)
            {
                Console.WriteLine(c);
            }
        }
    }
}