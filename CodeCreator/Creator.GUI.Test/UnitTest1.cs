using Core.Models;
using Core.PDFArranger;
using GUI;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Creator.GUI.Test
{
    [TestFixture]
    class PagingTests
    {

        [Test]
        public void Foo()
        {
            var items = new List<YardItem>()
            {
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "Falcon", Owner = "Bob Jones" },
                new YardItem(Guid.Parse("58de5eac-5d74-49d8-91b0-3fb148c58b39")) { Zone = "B13", BoatClass = "Laser", Owner = "Rob Jones", DueDate = DateTime.Now.AddDays(1) },
                new YardItem(Guid.Parse("82253571-c5f4-42f2-81e3-20aaa0f3551c")) { Zone = "C8", BoatClass = "GP14", Owner = "Paul Jones" },
                new YardItem(Guid.Parse("b8ae8d13-fd78-460b-ab83-e26ac0540afe")) { Zone = "D2", BoatClass = "Falcon", Owner = "Jeff Jones", DueDate = DateTime.Now.AddDays(16) },
                new YardItem() { Zone = "E2", BoatClass = "GP14", Owner = "Obi Wan Jones", DueDate = DateTime.Now.AddDays(1) },
                new YardItem() { Zone = "F12", BoatClass = "Laser", Owner = "Indiana Jones" },
                new YardItem() { Zone = "E2", BoatClass = "GP14", Owner = "Obi Wan Jones", DueDate = DateTime.Now.AddDays(1) },
                new YardItem() { Zone = "F12", BoatClass = "Laser", Owner = "Indiana Jones" },
                new YardItem() { Zone = "E2", BoatClass = "GP14", Owner = "Obi Wan Jones", DueDate = DateTime.Now.AddDays(1) },
                new YardItem() { Zone = "F12", BoatClass = "Laser", Owner = "Indiana Jones" }
            };


            var encoder = new ZXingQREncoder<YardItem>();

            var bitmaps = encoder.Encode(items);


            using (var page = new BitmapPageBuilder(bitmaps))
            {


                page.Build();
                page.Save("c:/");
            }




        }
    }


}
