using System;
using Newtonsoft.Json.Linq;

public class CPHInline
{
    // Cache the current song title for the Song Change action
    // A blank title means the image
	public string songTitle = "";

	public bool Execute()
	{
		var synthEvent = JObject.Parse(args["message"].ToString());
		string synthEventName = (string)synthEvent["eventType"];

		CPH.SetArgument("songTitle", this.songTitle);

		if (synthEventName == "SongStart") {
			CPH.SetArgument("songTitle", (string)synthEvent["data"]["song"]);
			this.songTitle = (string)synthEvent["data"]["song"];
			CPH.SetArgument("songArtist", (string)synthEvent["author"]);
			CPH.SetArgument("difficulty", (string)synthEvent["difficulty"]);
			CPH.SetArgument("mapper", (string)synthEvent["beatMapper"]);
			CPH.SetArgument("songLength", (int)synthEvent["data"]["length"]);
		}

		if (synthEventName == "PlayTime") {
			CPH.SetArgument("songPosition", (float)synthEvent["data"]["playTimeMS"] / 1000);
		}

		if (synthEventName == "NoteHit") {
			CPH.SetArgument("score", (int)synthEvent["data"]["score"]);
			CPH.SetArgument("scoreMultiplier", (int)synthEvent["data"]["multiplier"]);
			CPH.SetArgument("playerHealth", (float)synthEvent["data"]["lifeBarPercent"] * 100);
		}

		if (synthEventName == "NoteMiss") {
			CPH.SetArgument("scoreMultiplier", (int)synthEvent["data"]["multiplier"]);
			CPH.SetArgument("playerHealth", (float)synthEvent["data"]["lifeBarPercent"] * 100);
		}

		// Other events from the socket server:
		// EnterSpecial
		// CompleteSpecial
		// FailSpecial

		return true;
	}
}
