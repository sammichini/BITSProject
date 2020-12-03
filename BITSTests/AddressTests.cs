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
            Assert.AreEqual(8, addresses.Count);
            Assert.AreEqual(1, addresses[0].AddressId);

        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            s = dbContext.Address.Find(1);
            Assert.IsNotNull(s);
            Assert.AreEqual(1, s.AddressId);
            Console.WriteLine(s.ToString());
        }

        [Test]
        public void GetUsingWhere()
        {
            
            addresses = dbContext.Address.Where(s => s.AddressId == 1).OrderBy(s => s.AddressId).ToList();
            Assert.AreEqual(1, addresses.Count);
           

        }


        [Test]
        public void GetWithJoinTest()
        {
            // get a list of objects that include the Address id, name, State and Country
            var Address = dbContext.Address
                        .Join(
                              dbContext.SupplierAddress,
                           c => c.AddressId,
                           s => s.AddressId, 
                           (c, s) => new { c.AddressId, s.AddressTypeId, s.SupplierId })
                        .Join(
                              dbContext.Supplier,//join also with supplier
                            d => d.SupplierId,
                            f => f.SupplierId,
                            (d, f) => new {d.SupplierId, f.Name, f.Phone, f.Email, f.Website, f.ContactFirstName, f.ContactLastName, f.ContactPhone, f.ContactEmail, f.Note })                                
                            .OrderBy(r => r.SupplierId).ToList();
            Assert.AreEqual(12, Address.Count);
            Console.WriteLine(Address.ToList()); //joined to get count of addresses related to suppliers and the suppliers info
        }

        [Test]
        public void CreateTest()
        {
            Address b = new Address();
            b.AddressId = 8;
            b.StreetLine1 = "TestSupplier";
            b.StreetLine2 = "11111";
            b.City = "ex@ex.ex";
            b.State = "ex.ex";
            b.Zipcode = "Flapjack";
            b.Country = "Mapleface";

            dbContext.Address.Add(b);
            dbContext.SaveChanges();
            Address b2 = dbContext.Address.Find(b.AddressId);
            Assert.True(b2.AddressId == b.AddressId);
            Assert.True(b2.StreetLine2 == "11111");
            Assert.True(b2.City == "ex@ex.ex");
            Assert.True(b2.State == "ex.ex");
            Assert.True(b2.Zipcode == "Flapjack");
            Assert.True(b2.Country == "Mapleface");

        }

        [Test]
        public void UpdateTest()
        {
            Address b = dbContext.Address.Find(7);

            b.StreetLine2 = "";//update from null to empty string

            dbContext.Address.Update(b);
            dbContext.SaveChanges();

            Assert.True(b.StreetLine2 == "");


        }

        [Test]
        public void DeleteTest() 
                                 
        {
            s = dbContext.Address.Find(10);
            dbContext.Address.Remove(s);
            dbContext.SaveChanges();
            Assert.IsNull(s);
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