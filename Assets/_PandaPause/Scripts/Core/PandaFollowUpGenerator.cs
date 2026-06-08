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
    }
}