using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class CPHInline
{
	public class SongChangeEvent {
		public string type { get; set; }
		public string artist { get; set; }
		public string title { get; set; }
		public string mapper { get; set; }
		public string difficulty { get; set; }
		public string extraText { get; set; }
		public string albumArt { get; set; }
		public int songLength { get; set; }
		public int songPosition { get; set; }
		public int score { get; set; }
		public int highScore { get; set; }
		public int playerHealth { get; set; }
	}

	private bool inSong = false;

	public SongChangeEvent songEvent;

	public bool Execute()
	{
//        foreach (var arg in args)
//        {
//            CPH.LogInfo($"LogVars :: {arg.Key} = {arg.Value}");
//        }

		if (songEvent == null) {
			songEvent = new SongChangeEvent();
			songEvent.type = "songChange";
        }

		if (args.ContainsKey("songTitle") && args["songTitle"].ToString() == "") {
			// If the title is empty, we assume the song has finished and we should clear our state
			songEvent = null;
			songEvent = new SongChangeEvent();
			songEvent.type = "songChange"; // FIXME: if this is the only event, maybe get rid of this var?
		} else {
			if (args.ContainsKey("songTitle")) {
				songEvent.artist = args["songArtist"].ToString();
				songEvent.title = args["songTitle"].ToString();
				songEvent.difficulty = args["difficulty"].ToString();
				songEvent.mapper = args["mapper"].ToString();

				if (args.ContainsKey("albumArt") && args["albumArt"] != null) {
					songEvent.albumArt = args["albumArt"].ToString(); // Should be a URL (or a base64-encoded data url)
				}
				if (args.ContainsKey("songLength")) {
					songEvent.songLength = (int)Convert.ToInt64(args["songLength"]);
				}
			}

			if (args.ContainsKey("songPosition")) {
				songEvent.songPosition = (int)Convert.ToInt64(args["songPosition"]);
			}

			if (args.ContainsKey("score")) {
				songEvent.score = (int)Convert.ToInt64(args["score"]);
			}

			if (args.ContainsKey("playerHealth")) {
				songEvent.playerHealth = (int)Convert.ToInt64(args["playerHealth"]);
			}

			if (args.ContainsKey("highScore")) {
				songEvent.highScore = (int)Convert.ToInt64(args["highScore"]);
			}
		}

		CPH.WebsocketBroadcastJson(JsonConvert.SerializeObject(songEvent));

		return true;
	}
}
