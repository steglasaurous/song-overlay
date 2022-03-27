using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class CPHInline
{
	public bool Execute()
	{
		string synthSongFilePath = args["fullPath"].ToString();

		// Read the song text file into a string
		string[] synthSongInfo = File.ReadAllLines(synthSongFilePath);

        // If the file is empty, consider the song done and set title to empty.
		if (synthSongInfo.Length <= 0) {
		    CPH.SetArgument("songTitle", "");
		}

		return true;
	}
}
