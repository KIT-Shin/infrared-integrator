using System;

namespace irid.Models
{
    public class Device
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte Theta { get; set; }
        public byte Phi { get; set; }
        public byte[] Data { get; set; }
        public bool Available { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}