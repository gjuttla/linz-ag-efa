﻿namespace LinzLinienEfa.Common.Domain
{
    public class Line
    {
        public uint Number { get; set; }
        public TransportationMean Type { get; set; }
        public string Direction { get; set; }
        public string InitialOriginStopName { get; set; }
    }
}