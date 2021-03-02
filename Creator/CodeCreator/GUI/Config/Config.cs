using Data.CSV;
using Data.Models;
using Newtonsoft.Json;

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


        [JsonIgnore]
        public IColumnSchema Schema
        {
            get => new YardItemColumnSchema(
                    (YardItemCSVSchema.ZONE_Col, RowColumnn),
                    (YardItemCSVSchema.CLASS_Col, ClassColumn),
                    (YardItemCSVSchema.SAIL_Col, SailColumn),
                    (YardItemCSVSchema.OWNER_Col, OwnerColumn));
            }

    }
}
