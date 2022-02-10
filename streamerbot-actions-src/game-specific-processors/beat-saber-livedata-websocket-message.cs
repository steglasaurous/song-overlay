using System;
using Newtonsoft.Json.Linq;

public class CPHInline
{
	public bool Execute()
	{
//		CPH.LogDebug(args["message"].ToString());
		var beatSaberEvent = JObject.Parse(args["message"].ToString());

		foreach (JProperty prop in (JToken)beatSaberEvent) {
			CPH.SetArgument(prop.Name, prop.Value.ToString());
		}

        CPH.SetArgument("score", (int)beatSaberEvent["ScoreWithMultipliers"]);
        CPH.SetArgument("highScore", (int)beatSaberEvent["MaxScoreWithMultipliers"]);
        CPH.SetArgument("playerHealth", (string)beatSaberEvent["PlayerHealth"]);
        CPH.SetArgument("songPosition", (int)beatSaberEvent["TimeElapsed"]);

		// FIXME: Map values to what Song Change expects
		return true;
	}
}
