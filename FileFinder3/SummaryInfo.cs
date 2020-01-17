using System;
using System.Collections.Generic;

namespace FileFinder3
{
    public class SummaryInfo
    {
        public Int64 TotalSize;
        public int NumEntries;
        //public IDictionary<string, SummaryInfo> Extensions = new Dictionary<string, SummaryInfo>(); 
        public override string ToString()
        {
            return $"{nameof(TotalSize)}: {TotalSize}, {nameof(NumEntries)}: {NumEntries}";

        }
    }
}