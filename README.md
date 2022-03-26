# Rhythm Game Universal Song Overlay

This system provides a browser overlay for OBS that works with a number of VR rhythm games, showing
information on the song being played, album art, song progress, score and player health (where supported).

Currently supported games:

* [Synth Riders](https://synthridersvr.com/)
* [Beat Saber](https://beatsaber.com/)
* [Audica](https://audicagame.com/)
* [Boombox VR](https://www.boomboxvr.com/)
* [Audio Trip](http://www.kinemotik.com/audiotrip/)

# Prerequisites

* [Streamer.bot](https://streamer.bot) - it's the brains and glue that connects the games to the overlay.

# Setting Up

There's two parts:

* Initial setup of streamer.bot actions and browser source in OBS (one-time setup)
* Setting up individual games to connect with Streamer.bot.



## Initial Setup (one-time)

### 1. Import Streamer.bot actions

1. In Streamer.bot under the Actions tab, right-click on the action list (left side) and click "Import".
2. In the "import string" text box, copy the contents of [this file](streamerbot-actions.txt) and paste it into that text box.
3. Click on import.

### 2. Enable the Streamer.bot Websocket Server

Note: Some may already have the streamer.bot websocket server enabled.  In which case you can skip this step.

1. In streamer.bot, under the `Servers/Clients tab`, open the `Websocket Server` tab.
2. Make sure `Auto Start` is checked, and click on the `Start Server` button.  Note that if the button is greyed out, it means the websocket server is already running and you're good to go.

### 3. Add the OBS browser source

1.  In OBS, add a new browser source with the following URL:  https://steglasaurous.github.io/song-overlay/overlay.html

There's some levels of customization you can do with this.  See [here](#overlay-options) for further details.

You're done with initial setup!  Next, find the game(s) you want to configure and follow the instructions.

## Individual Game Configuration

### Synth Riders

1. Install the [Synth Riders Websocket mod](https://github.com/KK964/SynthRiders-Websockets-Mod) - this is what publishes game data used by the overlay.

2. Configure Synth's Song Status as per step 1 of [these instructions](https://docs.google.com/document/d/13Ei4bYQRvvhUBIl4Uc5rwls-gvzsQ78bXoJQKQ_qaLo/edit#heading=h.xsyyveoj8zvr). You do not need to do step 2. Note that the important part is making sure that `{{CoverImage}}` is included in the file.  This is what produces the album art for the current song.

3. In Streamer.bot, under `Servers/Clients > Websocket Clients`, add a new websocket client with the following settings:

   | Setting                 | Value                          |
   | ----------------------- | ------------------------------ |
   | Name                    | Synth Riders Websocket         |
   | Endpoint                | ws://localhost:9000/           |
   | Auto Connect on Startup | Checked                        |
   | Reconnect on Disconnct  | Checked                        |
   | Actions > Message       | Synth Riders Websocket Message |

### Beat Saber

1. Install the [Data Puller](https://github.com/ReadieFur/BSDataPuller) beat saber mod, which exposes a websocket server for the game.
   You can install this with the [Mod Assistant](https://github.com/Assistant/ModAssistant) app.
2. In Streamer.bot, navigate to `Servers/Clients > Websocket Clients`. There are two websocket clients to setup in streamer.bot.
   1. Add a new websocket client with the following settings.  This is for map data. 

       | Setting                 | Value                                    |
       |-------------------------|------------------------------------------|
       | Name                    | Beat Saber Map Data                      |
       | Endpoint                | ws://localhost:2946/BSDataPuller/MapData |
       | Auto Connect on Startup | Checked                                  |
       | Reconnect on Disconnct  | Checked                                  |
       | Actions > Message       | Beat Saber MapData Message               |

   2. Add another websocket client with the following settings.  This is for live data (score, etc)

       | Setting                 | Value                                    |
       |-------------------------|------------------------------------------|
       | Name                    | Beat Saber Live Data                     |
       | Endpoint                | ws://localhost:2946/BSDataPuller/MapData |
       | Auto Connect on Startup | Checked                                  |
       | Reconnect on Disconnct  | Checked                                  |
       | Actions > Message       | Beat Saber LiveData Message              |

### Audica

1. Install the [Audica Websocket Server mod](https://github.com/steglasaurous/audica-websocket-server) as per its instructions.

2. In Streamer.bot, under `Servers/Clients` > `Websocket Clients`, add a new websocket client with the following settings:

   | Setting                 | Value                           |
   | ----------------------- | ------------------------------- |
   | Name                    | Audica Websocket                |
   | Endpoint                | ws://localhost:8085/AudicaStats |
   | Auto Connect on Startup | Checked                         |
   | Reconnect on Disconnct  | Checked                         |
   | Actions > Message       | Audica Websocket Message        |

Added bonus!  When this is setup, it also will emit bot responses to `!asr` requests and other bot commands for Audica.

### Boombox

1. In Streamer.bot, under `Servers/Clients` > `Websocket Clients`, add a new websocket client with the following settings:

   | Setting                 | Value                       |
   | ----------------------- | --------------------------- |
   | Name                    | Boombox Websocket           |
   | Endpoint                | ws://localhost:42338/socket |
   | Auto Connect on Startup | Checked                     |
   | Reconnect on Disconnct  | Checked                     |
   | Actions > Message       | Boombox Websocket Message   |

### Audio Trip

1. In Streamer.bot, under `Servers/Clients` > `Websocket Clients`, add a new websocket client with the following settings:

   | Setting                 | Value                        |
   | ----------------------- | ---------------------------- |
   | Name                    | Audio Trip Websocket         |
   | Endpoint                | ws://127.0.0.1:48998/        |
   | Auto Connect on Startup | Checked                      |
   | Reconnect on Disconnct  | Checked                      |
   | Actions > Message       | Audio Trip Websocket Message |

# Customization

These are all optional customizations for the overlay.  They are not required for normal operation/setup.

## Browser Overlay Configuration Options

The following options can be passed to the browser overlay by adding a query string at the end of the URL.

For example, to show ONLY the song information without score and health, that would look like:

```
https://steglasaurous.github.io/song-overlay/overlay.html?show=song_display
```

### Options List

`show` - a comma-delimited list of parts of the overlay to show.  If not provided, everything is shown by default.   This is useful to split up parts of the overlay to different parts of the screen instead of everything being together in one place. You would do this by creating multiple browser sources in OBS with different URLs.

This list can include any of the following:

* `song_display` - Show details about the song includinig title, artist, mapper, difficulty and album art.
* `song_progress` - Show the progress of the song as it plays, with time and a progress bar.
* `score` - Show current score and personal best score (if available)
* `player_health` - Show player's health, if available, with a life bar and percentage.

`websocket_host` - The IP address or host name of the streamer.bot websocket server.  Useful for 2-pc stream setups where
                   streamer.bot may be running on a different machine.  Default is localhost.
`websocket_port` - Port of the streamer.bot websocket server.

Examples:

```
# To show song display and song progress, but hide the score and player health parts:
https://steglasaurous.github.io/song-overlay/overlay.html?show=song_display,song_progress

# Only show score
https://steglasaurous.github.io/song-overlay/overlay.html?show=score

# Show song_display and connect to streamer.bot on another machine (for 2-pc stream setups):
https://steglasaurous.github.io/song-overlay/overlay.html?show=song_display&websocket_host=10.0.0.29
```

# Internal Details

Useful if you want to know more about the guts about this implementation.  Anyone is welcome to modify my work and/or submit pull requests for improvements!

## Websocket Event Structure

This is the structure that's emitted from the Song Change streamer.bot action and what the overlay expects.

```json
{
  "title": "Brain Power",
  "artist": "Nova",
  "difficulty": "Expert",
  "mapper": "Some Mapper",
  "albumArt": "data:image/png;base64,",
  "score": 412,
  "highScore": 5749349,
  "multiplier": 1,
  "songLength": 120,
  "songPosition": 8,
  "extraText": "Extra detail to share - ex: BSR ID",
  "playerHealth": 100
}
```

## Notes

* `albumArt` can be either a data url with base64-encoded image data OR a normal URL to an image.  Set to null if not available.
* `score`, `highScore`, `multiplier`, `playerHealth`, `songLength` and `songPosition` are considered optional and if not available from the game, will be set to null.
* `extraText` is optional -  used for games where it's useful. Ex: for Beat Saber, shows the bsr id of the song.
* Full block is emitted as often as game data emits it, which can be very frequent.
