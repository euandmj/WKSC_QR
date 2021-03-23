using Data.CSV;
using Data.Models;
using Newtonsoft.Json;
using System;

namespace GUI.Configuration
{
    public class Config
    {
        [JsonProperty]
        public string SpreadsheetFile { get; set; }
        [JsonProperty]
        public string RowColumnn { get; set; } = "A";
        [JsonProperty] 
        public string ClassColumn { get; set; } = "A";
        [JsonProperty]
        public string SailColumn { get; set; } = "A";
        [JsonProperty]
        public string OwnerColumn { get; set; } = "A";

        [JsonProperty]
        public DateTime ValidUntil { get; set; } = DateTime.Now.AddYears(1);


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
