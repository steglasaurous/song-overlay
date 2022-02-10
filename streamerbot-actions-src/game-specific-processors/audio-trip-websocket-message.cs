using System;
using Newtonsoft.Json.Linq;

public class CPHInline
{
	public bool Execute()
	{
		CPH.LogDebug(args["message"].ToString());
		var audioTripEvent = JObject.Parse(args["message"].ToString());

		foreach (JProperty prop in (JToken)audioTripEvent) {
			CPH.SetArgument(prop.Name, prop.Value.ToString());
		}

		if ((bool)audioTripEvent["inSong"] == true) {
			CPH.SetArgument("songTitle", (string)audioTripEvent["songTitle"]);
			CPH.SetArgument("songArtist", (string)audioTripEvent["songArtist"]);
			CPH.SetArgument("difficulty", (string)audioTripEvent["choreoName"]);
			CPH.SetArgument("mapper", (string)audioTripEvent["choreographer"]);
			CPH.SetArgument("songLength", (int)audioTripEvent["songLength"]);

			// FIXME: See if album art is emitted or retrievable from AudioTrip (or does it even exist?)

			// for scoring and song progress
			CPH.SetArgument("score", (int)audioTripEvent["score"]);
			CPH.SetArgument("songPosition", (int)audioTripEvent["curSongTime"]);

			float playerHealth = (float)audioTripEvent["playerHealth"];
			if (playerHealth == -1) {
				CPH.SetArgument("playerHealth", "");
			} else {
				CPH.SetArgument("playerHealth", playerHealth * 100);
			}
		} else {
			CPH.SetArgument("songTitle", "");
		}

		return true;
	}
}
