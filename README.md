# Midi_File


This is a simple midi file player, written to learn more about the file structure and the playback requirements.


> Privacy note: For convenience LastMidiFile and LastMidiOutput are stored in the App Data folder through MySettings.

---

### The Main view

![Small_Midi_File_Main](https://user-images.githubusercontent.com/88147904/137456960-2c716a6e-ad70-4cd5-8c4a-a1fc89d17dfb.png)
---
### Realtime visualisation on keyboard

![Midi_File_Keyboard](https://user-images.githubusercontent.com/88147904/137457844-6094b904-f9f8-44d1-b991-126acfe62260.PNG)

---

### Eventlist by track
![Midi_File_EventList](https://user-images.githubusercontent.com/88147904/137457014-7d88996d-5cf6-423b-89fe-aa15a2759e8e.PNG)

This is only a view, no editing is available

---

### Tracks can be muted
![Midi_File_Mute](https://user-images.githubusercontent.com/88147904/137457033-db12035c-6942-4fc9-b65a-dafc4bd8f2ac.PNG)
In this example, only bass and drums are played

---

## Programming details

The Midi-File library contains all the parts for the file -reader and -player.
The Test Midi_File application contains a reference to Midi_File and defines the User Interface.

The ReadMidiFile function of Midi_File does:
- loading the file
- do some checks
- convert the file to the internal structure
- convert Delta-Times to absolute time (in player ticks)

After StartPlayer() the player runs through the lists oft TrackEvents and raises OutShortMsg events.
It's then up to the main application to send the Midi-messages to the Midi output port.



