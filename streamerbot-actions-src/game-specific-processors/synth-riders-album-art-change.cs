using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class CPHInline
{
	public bool Execute()
	{
		string synthSongAlbumArtPath = args["fullPath"].ToString();

		byte[] synthSongAlbumArt = File.ReadAllBytes(synthSongAlbumArtPath);
		if (synthSongAlbumArt.Length <= 0) {
		    // File is empty.  No change.
		    return true;
		}

		string synthSongAlbumArtEncoded = Convert.ToBase64String(synthSongAlbumArt);

        CPH.SetArgument("albumArt", "data:image/png;base64," + synthSongAlbumArtEncoded);

		return true;
	}
}
