using System;

namespace PandaPause.Core
{
    [Serializable]
    public class JournalEntry
    {
        public string dateUtc;
        public string mood;
        public string entryText;
        public string pandaResponse;
    }
    
}