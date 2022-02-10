using System;
using Newtonsoft.Json.Linq;

public class CPHInline
{
	private string songTitle;
	private string songArtist;
	private string mapper;
	private string difficulty;

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
			}
		}

		return true;
	}
}
