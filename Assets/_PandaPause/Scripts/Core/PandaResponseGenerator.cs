namespace PandaPause.Core
{
    public static class PandaResponseGenerator
    {
        public static string GetResponse(PandaMood mood)
        {
            switch (mood)
            {
                case PandaMood.Great:
                    return "I like hearing that. What went well today?";

                case PandaMood.Okay:
                    return "One step at a time. Anything on your mind?";

                case PandaMood.Rough:
                    return "I'm listening. What made today difficult?";

                case PandaMood.Exhausted:
                    return "Sounds like today took a lot of energy. Tell me about it.";

                default:
                    return "How has your day been?";
            }
        }
    }
}