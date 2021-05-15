using Core.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Test
{
    [TestFixture]
    class YardItemTest
    {
        [Test]
        public void Foo()
        {

            var a = new YardItem(Guid.Parse("58de5eac-5d74-49d8-91b0-3fb148c58b39")) { Zone = "B13", BoatClass = "Laser", Owner = "Rob Jones", DueDate = DateTime.Now.AddDays(1) };
            var a2 = new YardItem(Guid.Parse("58de5eac-5d74-49d8-91b0-3fb148c58b39")) { Zone = "B13", BoatClass = "Laser", Owner = "Rob Jones", DueDate = DateTime.Now.AddDays(1) };
            var b = new YardItem(Guid.Parse("82253571-c5f4-42f2-81e3-20aaa0f3551c")) { Zone = "C8", BoatClass = "GP14", Owner = "Paul Jones" };
            var c = new YardItem(Guid.Parse("b8ae8d13-fd78-460b-ab83-e26ac0540afe")) { Zone = "D2", BoatClass = "Falcon", Owner = "Jeff Jones", DueDate = DateTime.Now.AddDays(16) };

            var hs = new HashSet<YardItem>();
            hs.Add(a);
            hs.Add(b); 
            hs.Add(c);

            hs.Add(a);
            hs.Add(a2);


        }
    }
}
