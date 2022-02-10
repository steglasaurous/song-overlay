# Usage

* Import streamer.bot actions
* Add overlay browser source to OBS
* Configure game(s) in streamer.bot (see below)

## Individual Game Configuration

### Beat Saber

### Synth Riders

### Audica

### Boombox

### Audio Trip

# Customization

## Configuration Options

### Hiding/Showing parts of overlay

* Using multiple instances of the browser source, only showing certain parts

### Connect to alternate websocket address

* `websocket_host`
* `websocket_port`

## Advanced Customization

### Change colors, fonts, etc, via CSS

# Internal Details

## Websocket Event Structure

```json
{
  "title": "Brain Power",
  "artist": "Nova",
  "difficulty": "Expert",
  "mapper": "Some Mapper",
  "albumArt": "data:image/png;base64,",
  "score": 412,
  "highScore": 5749349,
  "songLength": 120,
  "songPosition": 8,
  "extraText": "Extra detail to share - ex: BSR ID",
  "playerHealth": 100
}
```

## Notes

* `albumArt` can be either a data url with base64-encoded image data OR a normal URL to an image.  Set to null if not available.
* `score`, `highScore`, `playerHealth`, `songLength` and `songPosition` are considered optional and if not available from the game, will be set to null.
* Full block is emitted as often as game data emits it, which can be very frequent.
