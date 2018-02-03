using System;

namespace LinzLinienEfa.Domain
{
    public class Departure
    {
        public uint CountdownInMinutes { get; set; }
        public DateTime Time { get; set; }
        public Line Line { get; set; }
    }
}