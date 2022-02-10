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
		public string albumArt { get; set; }
	}

	public bool Execute()
	{
		string synthSongFilePath = args["fullPath"].ToString();
		string synthSongAlbumArtPath = args["fullPath"].ToString().Replace(args["fileName"].ToString(),"SongStatusImage.png");

		// Read the song text file into a string
		string[] synthSongInfo = File.ReadAllLines(synthSongFilePath);

		byte[] synthSongAlbumArt = File.ReadAllBytes(synthSongAlbumArtPath);
		string synthSongAlbumArtEncoded = Convert.ToBase64String(synthSongAlbumArt);

		SongChangeEvent songEvent = new SongChangeEvent();
		songEvent.type = "songChange";
		if (synthSongInfo.Length > 0) {
			CPH.SetArgument("songArtist", synthSongInfo[0]);
    		CPH.SetArgument("songTitle", synthSongInfo[1]);
			CPH.SetArgument("difficulty", synthSongInfo[2]);
			CPH.SetArgument("mapper", synthSongInfo[3]);
			CPH.SetArgument("albumArt", "data:image/png;base64," + synthSongAlbumArtEncoded);
		} else {
		    CPH.SetArgument("songTitle", "");
		}

		return true;
	}
}
