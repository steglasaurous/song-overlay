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
		public string? difficulty { get; set; }
		public string? extraText { get; set; }
		public string? albumArt { get; set; } // either a normal URL or a base64-encoded PNG.
		public int? songLength { get; set; } // In seconds.
		public int? songPosition { get; set; } // In seconds.
		public int? score { get; set; }
		public int? scoreMultiplier { get; set; } // Multiplier usually renders in 2x, 3x, 4x, etc
		public int? highScore { get; set; }
		public int? combo { get; set; } // Combo (or streak) - how many successful consecutive notes hit.
		public int? playerHealth { get; set; } // in a range of 0-100
	}

	private bool inSong = false;

	public SongChangeEvent songEvent;

	public bool Execute()
	{
		if (songEvent == null) {
			songEvent = new SongChangeEvent();
			songEvent.type = "songChange";
			songEvent.score = null;
			songEvent.highScore = null;
			songEvent.playerHealth = null;
        }

		if (args.ContainsKey("songTitle") && args["songTitle"].ToString() == "") {
			if (songEvent.title != null && songEvent.title != "") {
				CPH.SetGlobalVar("lastSong", songEvent.title + " by " + songEvent.artist + " (mapped by " + songEvent.mapper + ")");
			}
			// If the title is empty, we assume the song has finished and we should clear our state
			songEvent = null;
			songEvent = new SongChangeEvent();
			songEvent.type = "songChange"; // FIXME: if this is the only event, maybe get rid of this var?
		} else {
			if (args.ContainsKey("songTitle")) {
				songEvent.title = args["songTitle"].ToString();
			}
			if (args.ContainsKey("songArtist")) {
				songEvent.artist = args["songArtist"].ToString();
			}

			if (args.ContainsKey("difficulty")) {
				songEvent.difficulty = args["difficulty"].ToString();
			}

			if (args.ContainsKey("mapper")) {
				songEvent.mapper = args["mapper"].ToString();
			}

			if (args.ContainsKey("albumArt") && args["albumArt"] != null) {
				songEvent.albumArt = args["albumArt"].ToString(); // Should be a URL (or a base64-encoded data url)
			}
			if (args.ContainsKey("songLength")) {
				songEvent.songLength = (int)Convert.ToInt64(args["songLength"]);
			}

			if (args.ContainsKey("songPosition")) {

				songEvent.songPosition = (int)Convert.ToInt64(args["songPosition"]);
				CPH.LogDebug("" + songEvent.songPosition);
				CPH.LogDebug(args["songPosition"].ToString());
			}

			if (args.ContainsKey("score")) {
				songEvent.score = (int)Convert.ToInt64(args["score"]);
			}

			if (args.ContainsKey("scoreMultiplier")) {
				songEvent.scoreMultiplier = (int)Convert.ToInt64(args["scoreMultiplier"]);
			}

			if (args.ContainsKey("playerHealth")) {
				songEvent.playerHealth = (int)Convert.ToInt64(args["playerHealth"]);
			}

			if (args.ContainsKey("highScore")) {
				songEvent.highScore = (int)Convert.ToInt64(args["highScore"]);
			}

			if (args.ContainsKey("combo")) {
				songEvent.combo = (int)Convert.ToInt64(args["combo"]);
			}
		}

		CPH.WebsocketBroadcastJson(JsonConvert.SerializeObject(songEvent));

		return true;
	}
}
