using Core.Models;
using Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Test
{
    [TestFixture]
    public class Class1
    {
        //private const string validData


        //[SetUp]
        //public void Setup()
        //{

        //}


        [Test]
        public void foo()
        {

            var csv = new AdapterCSV<YardItem>(new YardItemCSVSchema());

            csv.ReadCSV("fakedb.csv");

            var ooo = csv.GetItems().ToList();
        }
    }
}
