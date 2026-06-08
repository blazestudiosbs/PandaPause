using System;
using System.Collections.Generic;

namespace PandaPause.Core
{
    [Serializable]
    public class JournalDatabase
    {
        public List<JournalEntry> entries = new();
    }
}