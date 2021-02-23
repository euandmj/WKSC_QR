using CodeCreator.Commands;
using CodeCreator.View;
using Core.Models;
using QR;
using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Mobile;

namespace CodeCreator.ViewModel
{
    public class VModel
        : BaseViewModel
    {
        private readonly IQREncoder<YardItem> _qRSerialiser = new QRBase64Encoder<YardItem>();

        private object _selectedItem;


        public VModel()
        {
            DataSource = new List<YardItem>()
            {
                new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = BoatClass.Falcon, Owner = "Bob Jones" },
                new YardItem(Guid.Parse("58de5eac-5d74-49d8-91b0-3fb148c58b39")) { Zone = "B13", BoatClass = BoatClass.Laser, Owner = "Rob Jones", DueDate = DateTime.Now.AddDays(1) },
                new YardItem(Guid.Parse("82253571-c5f4-42f2-81e3-20aaa0f3551c")) { Zone = "C8", BoatClass = BoatClass.GP14, Owner = "Paul Jones" },
                new YardItem(Guid.Parse("b8ae8d13-fd78-460b-ab83-e26ac0540afe")) { Zone = "D2", BoatClass = BoatClass.Falcon, Owner = "Jeff Jones", DueDate = DateTime.Now.AddDays(16) },
                new YardItem() { Zone = "E2", BoatClass = BoatClass.GP14, Owner = "Obi Wan Jones", DueDate = DateTime.Now.AddDays(1) },
                new YardItem() { Zone = "F12", BoatClass = BoatClass.Laser, Owner = "Indiana Jones" }
            };
        }
        
        private YardItem? SelectedYardItem => SelectedItem as YardItem;


        private Command OnCreateCommand()
        {
            return new Command((x) =>
            {
                var bmp = _qRSerialiser.Encode(SelectedYardItem, null);


                if (bmp != null)
                {
                    var win = new ExportItemsPage(bmp);
                    win.ShowDialog();
                }
            });
        }




        public Command CreateCommand { get => OnCreateCommand(); }


        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged(nameof(IsExportEnabled));
            }

        }
        public ICollection<YardItem> DataSource { get; private set; }
        public bool IsExportEnabled => SelectedYardItem != null;

    }
}
