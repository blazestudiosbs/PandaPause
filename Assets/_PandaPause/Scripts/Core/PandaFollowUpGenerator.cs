namespace PandaPause.Core
{
    public static class PandaFollowUpGenerator
    {
        public static string GetFollowUp(string pandaName, string lastMood)
        {
            if (string.IsNullOrWhiteSpace(pandaName))
                pandaName = "Your panda";

            switch (lastMood)
            {
                case "Great":
                    return $"{pandaName} remembers yesterday felt good. Did anything good happen today?";

                case "Okay":
                    return $"{pandaName} remembers yesterday was okay. Want to tell {pandaName} how today is going?";

                case "Rough":
                    return $"{pandaName} remembers yesterday felt rough. Is today feeling any lighter?";

                case "Exhausted":
                    return $"{pandaName} remembers you were exhausted. Did you get any rest?";

                default:
                    return $"{pandaName} is here. How are you feeling today?";
            }
        }

        public static string GetFollowUp(string pandaName, string lastMood, string lastEntry)
        {
            if (string.IsNullOrWhiteSpace(pandaName))
                pandaName = "Your panda";

            if (!string.IsNullOrWhiteSpace(lastEntry))
            {
                string lowerEntry = lastEntry.ToLowerInvariant();

                if (ContainsAny(lowerEntry, "work", "job", "boss", "meeting", "office", "coworker", "coworkers"))
                    return "How did work go today?";

                if (ContainsAny(lowerEntry, "family", "wife", "husband", "daughter", "son", "kids", "children", "child"))
                    return "How is your family doing today?";

                if (ContainsAny(lowerEntry, "tired", "exhausted", "sleep", "rest", "rested"))
                    return "Did you get a chance to rest?";

                if (ContainsAny(lowerEntry, "school", "class", "teacher", "homework", "college"))
                    return "How did school go today?";

                if (ContainsAny(lowerEntry, "success", "finished", "completed", "done", "accomplished"))
                    return "How did that progress today?";

                return "How did that go today?";
            }

            return GetFollowUp(pandaName, lastMood);
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