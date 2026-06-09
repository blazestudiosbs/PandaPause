namespace PandaPause.Core
{
    public static class PandaResponseGenerator
    {
        public static string GetJournalResponse(string pandaName, string mood, string entryText)
        {
            if (string.IsNullOrWhiteSpace(pandaName))
                pandaName = "Your panda";

            if (string.IsNullOrWhiteSpace(entryText))
                return $"{pandaName} is listening.";

            string lowerEntry = entryText.ToLowerInvariant();

            if (lowerEntry.Contains("thank you") || lowerEntry.Contains("thanks"))
                return $"You're welcome. {pandaName} is glad you shared that.";

            switch (mood)
            {
                case "Great":
                    return $"That's wonderful to hear. {pandaName} is happy today gave you something good.";

                case "Okay":
                    return $"I'm glad you're doing okay. {pandaName} will be here if you want to share more.";

                case "Rough":
                    return $"I'm sorry today felt rough. {pandaName} is here with you.";

                case "Exhausted":
                    return $"That sounds like a lot to carry. {pandaName} hopes you get a little rest.";

                default:
                    return $"Thank you for sharing that. {pandaName} tucked it safely away.";
            }
        }
    }
}