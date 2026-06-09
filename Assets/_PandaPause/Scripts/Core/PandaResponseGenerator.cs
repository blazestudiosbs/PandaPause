namespace PandaPause.Core
{
    public static class PandaResponseGenerator
    {
        public static string GetJournalResponse(
            string pandaName,
            string mood,
            string entryText,
            string previousMemory = "")
        {
            if (string.IsNullOrWhiteSpace(pandaName))
                pandaName = "Your panda";

            if (string.IsNullOrWhiteSpace(entryText))
                return $"{pandaName} is listening.";

            string lowerEntry = entryText.ToLowerInvariant();
            string memoryPrefix = GetMemoryPrefix(pandaName, previousMemory, entryText);

            if (lowerEntry.Contains("thank you") || lowerEntry.Contains("thanks"))
                return memoryPrefix + $"You're welcome. {pandaName} is glad you shared that.";

            switch (mood)
            {
                case "Great":
                    return memoryPrefix + $"That's wonderful to hear. {pandaName} is glad something good happened today.";

                case "Okay":
                    return memoryPrefix + $"I'm glad you're doing okay. {pandaName} will be here if you want to share more.";

                case "Rough":
                    return memoryPrefix + $"I'm sorry today felt rough. {pandaName} is here with you.";

                case "Exhausted":
                    return memoryPrefix + $"That sounds like a lot to carry. {pandaName} hopes you get a little rest.";

                default:
                    return memoryPrefix + $"Thank you for sharing that. {pandaName} tucked it safely away.";
            }
        }

        private static string GetMemoryPrefix(string pandaName, string previousMemory, string currentEntry)
        {
            if (string.IsNullOrWhiteSpace(previousMemory))
                return "";

            if (previousMemory == currentEntry)
                return "";

            string topic = GetMemoryTopic(previousMemory);

            if (!string.IsNullOrWhiteSpace(topic))
                return $"{pandaName} remembers you've mentioned {topic} before. ";

            string shortMemory = Shorten(previousMemory, 45);

            return $"{pandaName} remembers \"{shortMemory}\". ";
        }

        private static string GetMemoryTopic(string memory)
        {
            if (string.IsNullOrWhiteSpace(memory))
                return "";

            string lower = memory.ToLowerInvariant();

            if (ContainsAny(lower, "work", "job", "boss", "meeting", "office", "coworker", "coworkers"))
                return "work";

            if (ContainsAny(lower, "family", "wife", "husband", "daughter", "son", "kids", "children", "child"))
                return "family";

            if (ContainsAny(lower, "tired", "exhausted", "sleep", "rest", "rested"))
                return "rest";

            if (ContainsAny(lower, "school", "class", "teacher", "homework", "college"))
                return "school";

            if (ContainsAny(lower, "unity", "game", "coding", "project", "app", "build", "release"))
                return "your project";

            return "";
        }

        private static string Shorten(string text, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            text = text.Trim();

            if (text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength).TrimEnd() + "...";
        }

        private static bool ContainsAny(string source, params string[] keywords)
        {
            foreach (string keyword in keywords)
            {
                if (source.Contains(keyword))
                    return true;
            }

            return false;
        }
    }
}