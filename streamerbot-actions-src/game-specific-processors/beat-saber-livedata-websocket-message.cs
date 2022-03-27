using System;
using Newtonsoft.Json.Linq;

public class CPHInline
{
	public bool Execute()
	{
		var beatSaberEvent = JObject.Parse(args["message"].ToString());

		foreach (JProperty prop in (JToken)beatSaberEvent) {
			CPH.SetArgument(prop.Name, prop.Value.ToString());
		}

        CPH.SetArgument("score", (int)beatSaberEvent["ScoreWithMultipliers"]);
        CPH.SetArgument("highScore", (int)beatSaberEvent["MaxScoreWithMultipliers"]);
        CPH.SetArgument("combo", (int)beatSaberEvent["Combo"]);
        CPH.SetArgument("playerHealth", (string)beatSaberEvent["PlayerHealth"]);
        CPH.SetArgument("songPosition", (int)beatSaberEvent["TimeElapsed"]);

		return true;
	}
}
