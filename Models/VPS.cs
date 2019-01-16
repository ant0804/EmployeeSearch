using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace EmployeeSearch.Models
{
    public class Datum
    {

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("network_in_bytes")]
        public string NetworkInBytes { get; set; }

        [JsonProperty("network_out_bytes")]
        public string NetworkOutBytes { get; set; }

        [JsonProperty("network_conntrack")]
        public string NetworkConntrack { get; set; }

        [JsonProperty("disk_read_bytes")]
        public string DiskReadBytes { get; set; }

        [JsonProperty("disk_write_bytes")]
        public string DiskWriteBytes { get; set; }

        [JsonProperty("la_5min_100")]
        public string La5min100 { get; set; }

        [JsonProperty("mem_alloc_kbytes")]
        public string MemAllocKbytes { get; set; }

        [JsonProperty("mem_used_kbytes")]
        public string MemUsedKbytes { get; set; }

        [JsonProperty("numproc")]
        public string Numproc { get; set; }

        [JsonProperty("failcnt")]
        public string Failcnt { get; set; }
    }

    public class VPS
    {

        [JsonProperty("data")]
        public IList<Datum> Data { get; set; }

        [JsonProperty("vm_type")]
        public string VmType { get; set; }

        [JsonProperty("error")]
        public int Error { get; set; }
    }

}