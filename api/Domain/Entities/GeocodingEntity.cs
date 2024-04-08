using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GeocodingEntity
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("local_names")]
        public LocalNames local_names { get; set; }

        [JsonProperty("lat")]
        public double lat { get; set; }

        [JsonProperty("lon")]
        public double lon { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("state")]
        public string state { get; set; }
    }


    public class LocalNames
    {
        [JsonProperty("sr")]
        public string sr { get; set; }

        [JsonProperty("ml")]
        public string ml { get; set; }

        [JsonProperty("nl")]
        public string nl { get; set; }

        [JsonProperty("pt")]
        public string pt { get; set; }

        [JsonProperty("ce")]
        public string ce { get; set; }

        [JsonProperty("tg")]
        public string tg { get; set; }

        [JsonProperty("uk")]
        public string uk { get; set; }

        [JsonProperty("ne")]
        public string ne { get; set; }

        [JsonProperty("dv")]
        public string dv { get; set; }

        [JsonProperty("gl")]
        public string gl { get; set; }

        [JsonProperty("lv")]
        public string lv { get; set; }

        [JsonProperty("sh")]
        public string sh { get; set; }

        [JsonProperty("bg")]
        public string bg { get; set; }

        [JsonProperty("yo")]
        public string yo { get; set; }

        [JsonProperty("uz")]
        public string uz { get; set; }

        [JsonProperty("fa")]
        public string fa { get; set; }

        [JsonProperty("eo")]
        public string eo { get; set; }

        [JsonProperty("bn")]
        public string bn { get; set; }

        [JsonProperty("mk")]
        public string mk { get; set; }

        [JsonProperty("cv")]
        public string cv { get; set; }

        [JsonProperty("ab")]
        public string ab { get; set; }

        [JsonProperty("sa")]
        public string sa { get; set; }

        [JsonProperty("el")]
        public string el { get; set; }

        [JsonProperty("pa")]
        public string pa { get; set; }

        [JsonProperty("ba")]
        public string ba { get; set; }

        [JsonProperty("be")]
        public string be { get; set; }

        [JsonProperty("hy")]
        public string hy { get; set; }

        [JsonProperty("ay")]
        public string ay { get; set; }

        [JsonProperty("tr")]
        public string tr { get; set; }

        [JsonProperty("ta")]
        public string ta { get; set; }

        [JsonProperty("os")]
        public string os { get; set; }

        [JsonProperty("tk")]
        public string tk { get; set; }

        [JsonProperty("mn")]
        public string mn { get; set; }

        [JsonProperty("oc")]
        public string oc { get; set; }

        [JsonProperty("it")]
        public string it { get; set; }

        [JsonProperty("en")]
        public string en { get; set; }

        [JsonProperty("he")]
        public string he { get; set; }

        [JsonProperty("ja")]
        public string ja { get; set; }

        [JsonProperty("ru")]
        public string ru { get; set; }

        [JsonProperty("kk")]
        public string kk { get; set; }

        [JsonProperty("ko")]
        public string ko { get; set; }

        [JsonProperty("az")]
        public string az { get; set; }

        [JsonProperty("io")]
        public string io { get; set; }

        [JsonProperty("th")]
        public string th { get; set; }

        [JsonProperty("fr")]
        public string fr { get; set; }

        [JsonProperty("ur")]
        public string ur { get; set; }

        [JsonProperty("am")]
        public string am { get; set; }

        [JsonProperty("no")]
        public string no { get; set; }

        [JsonProperty("de")]
        public string de { get; set; }

        [JsonProperty("kv")]
        public string kv { get; set; }

        [JsonProperty("zh")]
        public string zh { get; set; }

        [JsonProperty("wa")]
        public string wa { get; set; }

        [JsonProperty("ug")]
        public string ug { get; set; }

        [JsonProperty("sv")]
        public string sv { get; set; }

        [JsonProperty("yi")]
        public string yi { get; set; }

        [JsonProperty("lt")]
        public string lt { get; set; }

        [JsonProperty("mr")]
        public string mr { get; set; }

        [JsonProperty("ar")]
        public string ar { get; set; }

        [JsonProperty("es")]
        public string es { get; set; }

        [JsonProperty("hi")]
        public string hi { get; set; }

        [JsonProperty("la")]
        public string la { get; set; }

        [JsonProperty("pl")]
        public string pl { get; set; }

        [JsonProperty("kn")]
        public string kn { get; set; }

        [JsonProperty("ka")]
        public string ka { get; set; }

        [JsonProperty("tt")]
        public string tt { get; set; }
    }

}
