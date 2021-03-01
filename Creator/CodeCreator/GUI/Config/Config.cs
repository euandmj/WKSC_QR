using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Configuration
{
    public class Config
    {
        [JsonProperty]
        public string SpreadsheetFile { get; set; }
        [JsonProperty]
        public int RowColumnn { get; set; }
        [JsonProperty] 
        public int ClassColumn { get; set; }
        [JsonProperty]
        public int SailColumn { get; set; }
        [JsonProperty]
        public int OwnerColumn { get; set; }




    }
}
