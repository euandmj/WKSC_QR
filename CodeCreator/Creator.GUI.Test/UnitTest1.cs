using Core.Encoding;
using Core.Models;
using Core.PDFArranger;
using GUI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Creator.GUI.Test
{
    public class Tests
    {
        private static readonly List<YardItem> Items
               = new List<YardItem>()
               {
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "BoatClass.Falcon", Owner = "Bob Jones" },
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "BoatClass.Falcon", Owner = "Bob Jones" },
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "BoatClass.Falcon", Owner = "Bob Jones" },
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "BoatClass.Falcon", Owner = "Bob Jones" },
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "BoatClass.Falcon", Owner = "Bob Jones" },
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "BoatClass.Falcon", Owner = "Bob Jones" },
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "BoatClass.Falcon", Owner = "Bob Jones" },
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "BoatClass.Falcon", Owner = "Bob Jones" },
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "BoatClass.Falcon", Owner = "Bob Jones" },
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = "BoatClass.Falcon", Owner = "Bob Jones" }
               };


        [Test]
        public void Build()
        {
            var encoder = new ZXingQREncoder<YardItem>();

            var bmps = encoder.Encode(Items);

            using (var pagebuilder = new BitmapPageBuilder(bmps))
            {
                pagebuilder.Build();
                pagebuilder.Save(@"C:\WKSC_SCNR\out").ToList();
            }
            foreach (var bmp in bmps) bmp.Dispose();


        }
    }
}