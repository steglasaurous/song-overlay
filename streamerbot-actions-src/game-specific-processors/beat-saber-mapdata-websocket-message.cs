using System;
using Newtonsoft.Json.Linq;

public class CPHInline
{
	public bool Execute()
	{
		CPH.LogDebug(args["message"].ToString());
		var beatSaberEvent = JObject.Parse(args["message"].ToString());

		foreach (JProperty prop in (JToken)beatSaberEvent) {
			CPH.SetArgument(prop.Name, prop.Value.ToString());
		}

        CPH.SetArgument("songTitle", (string)beatSaberEvent["SongName"]);
        CPH.SetArgument("songArtist", (string)beatSaberEvent["SongAuthor"]);
        CPH.SetArgument("difficulty", (string)beatSaberEvent["Difficulty"]);
        CPH.SetArgument("mapper", (string)beatSaberEvent["Mapper"]);
        CPH.SetArgument("albumArt", (string)beatSaberEvent["coverImage"]);
        CPH.SetArgument("songLength", (string)beatSaberEvent["Length"]);
        CPH.SetArgument("extraText", (string)beatSaberEvent["BSRKey"]);

		return true;
	}
}
