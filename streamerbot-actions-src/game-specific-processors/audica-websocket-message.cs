using System;
using Newtonsoft.Json.Linq;

public class CPHInline
{
	public bool Execute()
	{
		CPH.LogDebug(args["message"].ToString());
		var audicaEvent = JObject.Parse(args["message"].ToString());

		CPH.SetArgument("audicaEventType", (string)audicaEvent["eventType"]);
		foreach (JProperty prop in (JToken)audicaEvent["data"]) {
			CPH.SetArgument(prop.Name, prop.Value.ToString());
		}

		if ((string)audicaEvent["eventType"] == "SongSelected") {
			CPH.SetArgument("songTitle", (string)audicaEvent["data"]["songName"]);
			CPH.SetArgument("songArtist", (string)audicaEvent["data"]["songArtist"]);
			CPH.SetArgument("difficulty", (string)audicaEvent["data"]["difficulty"]);
			CPH.SetArgument("mapper", (string)audicaEvent["data"]["songAuthor"]);
			CPH.SetArgument("albumArt", (string)audicaEvent["data"]["albumArtData"]);
			CPH.SetArgument("songLength", (int)audicaEvent["data"]["songLengthSeconds"]);
		}

		if ((string)audicaEvent["eventType"] == "SongPlayerStatus") {
			CPH.SetArgument("score", (int)audicaEvent["data"]["score"]);
			// Multiply player health by 100 before returning it.
			float playerHealth = (float)audicaEvent["data"]["health"] * 100;

			CPH.SetArgument("playerHealth", playerHealth);
			CPH.SetArgument("highScore", (int)audicaEvent["data"]["highScore"]);
		}

		if ((string)audicaEvent["eventType"] == "SongProgress") {
			CPH.SetArgument("songPosition", (int)audicaEvent["data"]["timeElapsedSeconds"]);
		}

		if ((string)audicaEvent["eventType"] == "ReturnToSongList") {
			CPH.SetArgument("songTitle", "");
		}

		return true;
	}
}
