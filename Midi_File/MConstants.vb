Public Module MConstatnts

    Friend Const MiErr_NoError = 0

    Friend Const MiErr_EmptyName = 11                  ' fullname is nothing or empty
    Friend Const MiErr_FileNotExists = 12              ' fullname not exists
    Friend Const MiErr_FileTooShort = 13               ' file is < 14 bytes
    Friend Const MiErr_FileTooLong = 14                ' file is > Max. bytes
    Friend Const MiErr_HeaderChunkSignature = 15       ' Header-Chunk <> "MThd"
    Friend Const MiErr_MinHeaderDataLength = 16        ' Length < 6
    Friend Const MiErr_TimeFormat_SMPTE = 17           ' TimeFormat SMPTE is not supported by this application
    Friend Const MiErr_InvalidMidiFormat = 18          ' Format not 0 or 1 or 2
    Friend Const MiErr_HeaderNoTracks = 19             ' Number of Track in Header = 0
    Friend Const MiErr_Format0_MoreThanOneTrack = 20   ' Format 0 can only have 1 track
    Friend Const MiErr_Format1_TooManyTracks = 21      ' Format 1 has application-defined limits for the number of tracks
    Friend Const MiErr_Format2_MoreThanOneTrack = 22   ' Format 2 can only have 1 track
    Friend Const MiErr_DivisionIsNull = 23             ' Division / Ticks per QuaterNote can not be 0
    Friend Const MiErr_ReadingChunkChain = 24          ' ChunkHeader + DataLen points to next Chunk until eof

    Friend Const MiErr_Unknown = &H10001

    ''' <summary>
    ''' Returns the GM VoiceName from GM VoiceNumber
    ''' </summary>
    ''' <param name="VoiceNum"></param>
    ''' <returns></returns>
    Public Function GetVoiceName(VoiceNum As Byte) As String

        Dim str As String = ""

        If GM_VoiceNames.TryGetValue(VoiceNum, str) = True Then
            Return str
        Else

            Return VoiceNum & " - unknown Voice"
        End If

    End Function

    Public Function GetControllerName(CtrlNum As Byte) As String

        Dim str As String = ""

        If ControllerNames.TryGetValue(CtrlNum, str) = True Then
            Return str
        Else

            Return CtrlNum & " - unknown Controller"
        End If

    End Function

    ''' <summary>
    ''' GemeralMidi VoiceNames sorted by VoiceNumber
    ''' </summary>
    Public ReadOnly GM_VoiceNames As New SortedList(Of Integer, String) From
          {
{0, "Acoustic Grand Piano"},
{1, "Bright Acoustic Piano"},
{2, "Electric Grand Piano"},
{3, "Honky-tonk Piano"},
{4, "Electric Piano 1"},
{5, "Electric Piano 2"},
{6, "Harpsichord"},
{7, "Clavi"},
{8, "Celesta"},
{9, "Glockenspiel"},
{10, "Music Box"},
{11, "Vibraphone"},
{12, "Marimba"},
{13, "Xylophone"},
{14, "Tubular Bells"},
{15, "Dulcimer"},
{16, "Drawbar Organ"},
{17, "Percussive Organ"},
{18, "Rock Organ"},
{19, "Church Organ"},
{20, "Reed Organ"},
{21, "Accordion"},
{22, "Harmonica"},
{23, "Tango Accordion"},
{24, "Acoustic Guitar (nylon)"},
{25, "Acoustic Guitar (steel)"},
{26, "Electric Guitar (jazz)"},
{27, "Electric Guitar (clean)"},
{28, "Electric Guitar (muted"},
{29, "Overdriven Guitar"},
{30, "Distortion Guitar"},
{31, "Guitar harmonics"},
{32, "Acoustic Bass"},
{33, "Electric Bass (finger)"},
{34, "Electric Bass (pick)"},
{35, "Fretless Bass"},
{36, "Slap Bass 1"},
{37, "Slap Bass 2"},
{38, "Synth Bass 1"},
{39, "Synth Bass 2"},
{40, "Violin"},
{41, "Viola"},
{42, "Cello"},
{43, "Contrabass"},
{44, "Tremolo Strings"},
{45, "Pizzicato Strings"},
{46, "Orchestral Harp"},
{47, "Timpani"},
{48, "String Ensemble 1"},
{49, "String Ensemble 2"},
{50, "SynthStrings 1"},
{51, "SynthStrings 2"},
{52, "Choir Aahs"},
{53, "Voice Oohs"},
{54, "Synth Voice"},
{55, "Orchestra Hit"},
{56, "Trumpet"},
{57, "Trombone"},
{58, "Tuba"},
{59, "Muted Trumpet"},
{60, "French Horn"},
{61, "Brass Section"},
{62, "SynthBrass 1"},
{63, "SynthBrass 2"},
{64, "Soprano Sax"},
{65, "Alto Sax"},
{66, "Tenor Sax"},
{67, "Baritone Sax"},
{68, "Oboe"},
{69, "English Horn"},
{70, "Bassoon"},
{71, "Clarinet"},
{72, "Piccolo"},
{73, "Flute"},
{74, "Recorder"},
{75, "Pan Flute"},
{76, "Blown Bottle"},
{77, "Shakuhachi"},
{78, "Whistle"},
{79, "Ocarina"},
{80, "Lead 1 (square)"},
{81, "Lead 2 (sawtooth)"},
{82, "Lead 3 (calliope)"},
{83, "Lead 4 (chiff)"},
{84, "Lead 5 (charang)"},
{85, "Lead 6 (voice)"},
{86, "Lead 7 (fifths)"},
{87, "Lead 8 (bass + lead)"},
{88, "Pad 1 (New age)"},
{89, "Pad 2 (warm)"},
{90, "Pad 3 (polysynth)"},
{91, "Pad 4 (choir)"},
{92, "Pad 5 (bowed)"},
{93, "Pad 6 (metallic)"},
{94, "Pad 7 (halo)"},
{95, "Pad 8 (sweep)"},
{96, "FX 1 (rain)"},
{97, "FX 2 (soundtrack)"},
{98, "FX 3 (crystal)"},
{99, "FX 4 (atmosphere)"},
{100, "FX 5 (brightness)"},
{101, "FX 6 (goblins)"},
{102, "FX 7 (echoes)"},
{103, "FX 8 (sci-fi)"},
{104, "Sitar"},
{105, "Banjo"},
{106, "Shamisen"},
{107, "Koto"},
{108, "Kalimba"},
{109, "Bag pipe"},
{110, "Fiddle"},
{111, "Shanai"},
{112, "Tinkle Bell"},
{113, "Agogo"},
{114, "Steel Drums"},
{115, "Woodblock"},
{116, "Taiko Drum"},
{117, "Melodic Tom"},
{118, "Synth Drum"},
{119, "Reverse Cymbal"},
{120, "Guitar Fret Noise"},
{121, "Breath Noise"},
{122, "Seashore"},
{123, "Bird Tweet"},
{124, "Telephone Ring"},
{125, "Helicopter"},
{126, "Applause"},
{127, "Gunshot"}
}

    ''' <summary>
    ''' Midi ControllerNames sorted by ContollerNumber
    ''' </summary>
    Public ReadOnly ControllerNames As New SortedList(Of Integer, String) From
        {
{0, "Bank Select MSB"},
{1, "Modulation MSB"},           ' wheel or lever
{2, "Breath Controller MSB"},
{3, "Undefined"},
{4, "Foot Controller  MSB"},
{5, "Portamento Time  MSB"},
{6, "Data entry MSB"},
{7, "Channel Volume  MSB"},
{8, "Balance  MSB"},
{9, "Undefined"},
{10, "Pan  MSB"},
{11, "Expression Controller  MSB"},
{12, "Effect Control 1  MSB"},
{13, "Effect Control 2  MSB"},
{14, "Undefined"},
{15, "Undefined"},
{16, "General Purpose Controller 1  MSB"},
{17, "General Purpose Controller 2  MSB"},
{18, "General Purpose Controller 3  MSB"},
{19, "General Purpose Controller 4  MSB"},
{20, "Undefined"},
{21, "Undefined"},
{22, "Undefined"},
{23, "Undefined"},
{24, "Undefined"},
{25, "Undefined"},
{26, "Undefined"},
{27, "Undefined"},
{28, "Undefined"},
{29, "Undefined"},
{30, "Undefined"},
{31, "Undefined"},
{32, "Bank Select LSB"},
{33, "Modulation LSB"},      ' wheel or lever
{34, "Breath Controller LSB"},
{35, "Undefined"},
{36, "Foot Controller LSB"},
{37, "Portamento Time  LSB"},
{38, "Data entry LSB"},
{39, "Channel Volume  LSB"},
{40, "Balance  LSB"},
{41, "Undefined"},
{42, "Pan  LSB"},
{43, "Expression Controller  LSB"},
{44, "Effect Control 1  LSB"},
{45, "Effect Control 2  LSB"},
{46, "Undefined"},
{47, "Undefined"},
{48, "General Purpose Controller 1  LSB"},
{49, "General Purpose Controller 2  LSB"},
{50, "General Purpose Controller 3  LSB"},
{51, "General Purpose Controller 4  LSB"},
{52, "Undefined"},
{53, "Undefined"},
{54, "Undefined"},
{55, "Undefined"},
{56, "Undefined"},
{57, "Undefined"},
{58, "Undefined"},
{59, "Undefined"},
{60, "Undefined"},
{61, "Undefined"},
{62, "Undefined"},
{63, "Undefined"},
{64, "Damper Pedal (sustain)"},
{65, "Portamento On/Off"},
{66, "Sostenuto"},
{67, "Soft pedal"},
{68, "Legato Footswitch"},
{69, "Hold 2"},
{70, "Sound Controller 1"},
{71, "Sound Controller 2"},
{72, "Sound Controller 3"},
{73, "Sound Controller 4"},
{74, "Sound Controller 5"},
{75, "Sound Controller 6"},
{76, "Sound Controller 7"},
{77, "Sound Controller 8"},
{78, "Sound Controller 9"},
{79, "Sound Controller 10"},
{80, "General Purpose Controller 5"},
{81, "General Purpose Controller 6"},
{82, "General Purpose Controller 7"},
{83, "General Purpose Controller 8"},
{84, "Portamento Control"},
{85, "Undefined"},
{86, "Undefined"},
{87, "Undefined"},
{88, "Undefined"},
{89, "Undefined"},
{90, "Undefined"},
{91, "Effects 1 Depth"},
{92, "Effects 2 Depth"},
{93, "Effects 3 Depth"},
{94, "Effects 4 Depth"},
{95, "Effects 5 Depth"},
{96, "Data increment"},
{97, "Data decrement"},
{98, "Non-Registered Parameter Number LSB"},
{99, "Non-Registered Parameter Number MSB"},
{100, "Registered Parameter Number LSB"},
{101, "Registered Parameter Number MSB"},
{102, "Undefined"},
{103, "Undefined"},
{104, "Undefined"},
{105, "Undefined"},
{106, "Undefined"},
{107, "Undefined"},
{108, "Undefined"},
{109, "Undefined"},
{110, "Undefined"},
{111, "Undefined"},
{112, "Undefined"},
{113, "Undefined"},
{114, "Undefined"},
{115, "Undefined"},
{116, "Undefined"},
{117, "Undefined"},
{118, "Undefined"},
{119, "Undefined"},
{120, "All Sounds Off"},
{121, "Controller Reset"},
{122, "Local Control On/Off"},
{123, "All Notes Off"},
{124, "Omni Off"},
{125, "Omni On"},
{126, "mono on / poly off"},
{127, "poly on / mono off"}
    }



End Module
