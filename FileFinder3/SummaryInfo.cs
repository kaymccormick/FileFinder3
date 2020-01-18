namespace FileFinder3
{
    public class SummaryInfo
    {
        public int NumEntries;

        public long TotalSize;

        //public IDictionary<string, SummaryInfo> Extensions = new Dictionary<string, SummaryInfo>(); 
        public override string ToString()
        {
            return
                $"{nameof( TotalSize )}: {TotalSize}, {nameof( NumEntries )}: {NumEntries}";
        }
    }
}