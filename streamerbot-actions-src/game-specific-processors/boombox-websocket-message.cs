using System;
using Newtonsoft.Json.Linq;

public class CPHInline
{
	private string songTitle;
	private string songArtist;
	private string mapper;
	private string difficulty;
	private int songLength;
	private int playerHealth;

	public bool Execute()
	{
		CPH.LogDebug(args["message"].ToString());
		var boomboxEvent = JObject.Parse(args["message"].ToString());

		CPH.SetArgument("boomboxEventType", (string)boomboxEvent["_event"]);

		if ((string)boomboxEvent["_event"] == "mapInfo") {
			this.songTitle = (string)boomboxEvent["mapInfoChanged"]["name"];
			this.songArtist = (string)boomboxEvent["mapInfoChanged"]["artist"];
			this.mapper = (string)boomboxEvent["mapInfoChanged"]["mapper"];
			this.difficulty = (string)boomboxEvent["mapInfoChanged"]["difficulty"];
			this.songLength = (int)boomboxEvent["mapInfoChanged"]["duration"];
		}

		if ((string)boomboxEvent["_event"] == "gameState") {
			CPH.SetArgument("gameStateChanged", (string)boomboxEvent["gameStateChanged"]);

			if ((string)boomboxEvent["gameStateChanged"] == "Lobby") {
				CPH.SetArgument("eventType","songChange");
				CPH.SetArgument("songTitle","");
			} else if ((string)boomboxEvent["gameStateChanged"] == "InGame") {
				CPH.SetArgument("eventType","songChange");
				CPH.SetArgument("songTitle", this.songTitle);
				CPH.SetArgument("songArtist", this.songArtist);
				CPH.SetArgument("mapper", this.mapper);
				CPH.SetArgument("difficulty", this.difficulty);
				CPH.SetArgument("songLength", this.songLength);
			}
		}

		if ((string)boomboxEvent["_event"] == "score") {
			CPH.SetArgument("score", (int)Convert.ToInt64(boomboxEvent["scoreEvent"]["score"]));
			int playerHealth = (int)Convert.ToInt64(Math.Floor((float)boomboxEvent["scoreEvent"]["currentHealth"] / (float)boomboxEvent["scoreEvent"]["maxHealth"] * 100));
			CPH.LogDebug(Convert.ToString(playerHealth));
			CPH.SetArgument("playerHealth", (int)Convert.ToInt64(Math.Floor((float)boomboxEvent["scoreEvent"]["currentHealth"] / (float)boomboxEvent["scoreEvent"]["maxHealth"] * 100)));
		}

		if ((string)boomboxEvent["_event"] == "songTime") {
			CPH.LogDebug("Song time:" + (string)boomboxEvent["songTimeChanged"]);
			CPH.SetArgument("songPosition", (int)boomboxEvent["songTimeChanged"]);
		}

		return true;
	}
}
