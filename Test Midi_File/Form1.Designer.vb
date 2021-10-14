<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Mi_File_Open = New System.Windows.Forms.ToolStripMenuItem()
        Me.Mi_File_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Mi_File_Exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Mi_Info_About = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TsBtnStop = New System.Windows.Forms.ToolStripButton()
        Me.TsBtnPause = New System.Windows.Forms.ToolStripButton()
        Me.Tsbtn_Play = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnGM_on = New System.Windows.Forms.Button()
        Me.btnResetAllControllers = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblEventsCount = New System.Windows.Forms.Label()
        Me.cbRecordEvents = New System.Windows.Forms.CheckBox()
        Me.lblPositionMBT = New System.Windows.Forms.Label()
        Me.lblPosition = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.trkbPosition = New System.Windows.Forms.TrackBar()
        Me.btnKeyboard = New System.Windows.Forms.Button()
        Me.lblVolumeValue = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.trkbVolume = New System.Windows.Forms.TrackBar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbMidiOutDevices = New System.Windows.Forms.ComboBox()
        Me.btnTbMessage_clear = New System.Windows.Forms.Button()
        Me.tbMessage = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblNumOfItems = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lvEvents = New System.Windows.Forms.ListView()
        Me.colNum = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTime = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colEvType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSubType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colChannel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colData = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDuration = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmbTrackList = New System.Windows.Forms.ComboBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnMuteNone = New System.Windows.Forms.Button()
        Me.btnMuteAll = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.clbMute = New System.Windows.Forms.CheckedListBox()
        Me.tpRecordedEvents = New System.Windows.Forms.TabPage()
        Me.tbVoiceMap = New System.Windows.Forms.TextBox()
        Me.cbSaveXML = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbPatternName = New System.Windows.Forms.TextBox()
        Me.btnExportPattern = New System.Windows.Forms.Button()
        Me.tbRecordedEvents = New System.Windows.Forms.TextBox()
        Me.lblRecordedEventsCount = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.tmDisplay = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.trkbPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.trkbVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.tpRecordedEvents.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.InfoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(699, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Mi_File_Open, Me.Mi_File_Close, Me.ToolStripSeparator1, Me.Mi_File_Exit})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'Mi_File_Open
        '
        Me.Mi_File_Open.Name = "Mi_File_Open"
        Me.Mi_File_Open.Size = New System.Drawing.Size(103, 22)
        Me.Mi_File_Open.Text = "&Open"
        '
        'Mi_File_Close
        '
        Me.Mi_File_Close.Name = "Mi_File_Close"
        Me.Mi_File_Close.Size = New System.Drawing.Size(103, 22)
        Me.Mi_File_Close.Text = "&Close"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(100, 6)
        '
        'Mi_File_Exit
        '
        Me.Mi_File_Exit.Name = "Mi_File_Exit"
        Me.Mi_File_Exit.Size = New System.Drawing.Size(103, 22)
        Me.Mi_File_Exit.Text = "E&xit"
        '
        'InfoToolStripMenuItem
        '
        Me.InfoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Mi_Info_About})
        Me.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem"
        Me.InfoToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.InfoToolStripMenuItem.Text = "&Info"
        '
        'Mi_Info_About
        '
        Me.Mi_Info_About.Name = "Mi_Info_About"
        Me.Mi_Info_About.Size = New System.Drawing.Size(180, 22)
        Me.Mi_Info_About.Text = "&About"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TsBtnStop, Me.TsBtnPause, Me.Tsbtn_Play})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(699, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.TabStop = True
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'TsBtnStop
        '
        Me.TsBtnStop.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TsBtnStop.Image = Global.Test_Midi_File.My.Resources.Resources.Stop_x22
        Me.TsBtnStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsBtnStop.Margin = New System.Windows.Forms.Padding(0, 1, 50, 2)
        Me.TsBtnStop.Name = "TsBtnStop"
        Me.TsBtnStop.Size = New System.Drawing.Size(51, 22)
        Me.TsBtnStop.Text = "Stop"
        '
        'TsBtnPause
        '
        Me.TsBtnPause.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TsBtnPause.Enabled = False
        Me.TsBtnPause.Image = Global.Test_Midi_File.My.Resources.Resources.Pause_x22
        Me.TsBtnPause.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsBtnPause.Name = "TsBtnPause"
        Me.TsBtnPause.Size = New System.Drawing.Size(58, 22)
        Me.TsBtnPause.Text = "Pause"
        '
        'Tsbtn_Play
        '
        Me.Tsbtn_Play.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Tsbtn_Play.Image = Global.Test_Midi_File.My.Resources.Resources.Play_x22
        Me.Tsbtn_Play.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Tsbtn_Play.Name = "Tsbtn_Play"
        Me.Tsbtn_Play.Size = New System.Drawing.Size(49, 22)
        Me.Tsbtn_Play.Text = "Play"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 428)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(699, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.tpRecordedEvents)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 49)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(699, 379)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.Controls.Add(Me.btnGM_on)
        Me.TabPage1.Controls.Add(Me.btnResetAllControllers)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.lblEventsCount)
        Me.TabPage1.Controls.Add(Me.cbRecordEvents)
        Me.TabPage1.Controls.Add(Me.lblPositionMBT)
        Me.TabPage1.Controls.Add(Me.lblPosition)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.trkbPosition)
        Me.TabPage1.Controls.Add(Me.btnKeyboard)
        Me.TabPage1.Controls.Add(Me.lblVolumeValue)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.trkbVolume)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.cmbMidiOutDevices)
        Me.TabPage1.Controls.Add(Me.btnTbMessage_clear)
        Me.TabPage1.Controls.Add(Me.tbMessage)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(691, 353)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Main"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnGM_on
        '
        Me.btnGM_on.Location = New System.Drawing.Point(394, 93)
        Me.btnGM_on.Name = "btnGM_on"
        Me.btnGM_on.Size = New System.Drawing.Size(75, 23)
        Me.btnGM_on.TabIndex = 14
        Me.btnGM_on.Text = "GM on"
        Me.ToolTip1.SetToolTip(Me.btnGM_on, resources.GetString("btnGM_on.ToolTip"))
        Me.btnGM_on.UseVisualStyleBackColor = True
        '
        'btnResetAllControllers
        '
        Me.btnResetAllControllers.Location = New System.Drawing.Point(506, 93)
        Me.btnResetAllControllers.Name = "btnResetAllControllers"
        Me.btnResetAllControllers.Size = New System.Drawing.Size(114, 23)
        Me.btnResetAllControllers.TabIndex = 15
        Me.btnResetAllControllers.Text = "Reset all Controllers"
        Me.ToolTip1.SetToolTip(Me.btnResetAllControllers, "Try to send 'Reset all controllers' to all channels of the selected output port.")
        Me.btnResetAllControllers.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(393, 165)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(132, 13)
        Me.Label9.TabIndex = 55
        Me.Label9.Text = "Click keyboard to visualize"
        '
        'lblEventsCount
        '
        Me.lblEventsCount.AutoSize = True
        Me.lblEventsCount.Location = New System.Drawing.Point(498, 323)
        Me.lblEventsCount.Name = "lblEventsCount"
        Me.lblEventsCount.Size = New System.Drawing.Size(22, 13)
        Me.lblEventsCount.TabIndex = 54
        Me.lblEventsCount.Text = "xxx"
        Me.ToolTip1.SetToolTip(Me.lblEventsCount, "Number of Events in EventFIFO")
        '
        'cbRecordEvents
        '
        Me.cbRecordEvents.AutoSize = True
        Me.cbRecordEvents.Location = New System.Drawing.Point(394, 322)
        Me.cbRecordEvents.Name = "cbRecordEvents"
        Me.cbRecordEvents.Size = New System.Drawing.Size(97, 17)
        Me.cbRecordEvents.TabIndex = 55
        Me.cbRecordEvents.Text = "Record Events"
        Me.ToolTip1.SetToolTip(Me.cbRecordEvents, "If checked:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "CMidi.Enable_PlayerEvent property is set to TRUE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Player raises Play" &
        "erEvent if Track is not Muted" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "PlayerEvent Handler stores events to EventFIFO")
        Me.cbRecordEvents.UseVisualStyleBackColor = True
        '
        'lblPositionMBT
        '
        Me.lblPositionMBT.AutoSize = True
        Me.lblPositionMBT.Location = New System.Drawing.Point(281, 303)
        Me.lblPositionMBT.Name = "lblPositionMBT"
        Me.lblPositionMBT.Size = New System.Drawing.Size(22, 13)
        Me.lblPositionMBT.TabIndex = 52
        Me.lblPositionMBT.Text = "xxx"
        Me.ToolTip1.SetToolTip(Me.lblPositionMBT, "Measures : Beats : ticks" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Time Signature 4/4 assumed)")
        '
        'lblPosition
        '
        Me.lblPosition.AutoSize = True
        Me.lblPosition.Location = New System.Drawing.Point(281, 325)
        Me.lblPosition.Name = "lblPosition"
        Me.lblPosition.Size = New System.Drawing.Size(22, 13)
        Me.lblPosition.TabIndex = 49
        Me.lblPosition.Text = "xxx"
        Me.ToolTip1.SetToolTip(Me.lblPosition, "Sequencer ticks")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(8, 325)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 15)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Position:"
        '
        'trkbPosition
        '
        Me.trkbPosition.AutoSize = False
        Me.trkbPosition.BackColor = System.Drawing.Color.AliceBlue
        Me.trkbPosition.LargeChange = 12
        Me.trkbPosition.Location = New System.Drawing.Point(61, 322)
        Me.trkbPosition.Maximum = 1
        Me.trkbPosition.Name = "trkbPosition"
        Me.trkbPosition.Size = New System.Drawing.Size(214, 21)
        Me.trkbPosition.TabIndex = 41
        Me.trkbPosition.TickFrequency = 10
        Me.trkbPosition.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'btnKeyboard
        '
        Me.btnKeyboard.Image = CType(resources.GetObject("btnKeyboard.Image"), System.Drawing.Image)
        Me.btnKeyboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnKeyboard.Location = New System.Drawing.Point(394, 181)
        Me.btnKeyboard.Name = "btnKeyboard"
        Me.btnKeyboard.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.btnKeyboard.Size = New System.Drawing.Size(97, 23)
        Me.btnKeyboard.TabIndex = 21
        Me.btnKeyboard.Text = "Keyboard"
        Me.btnKeyboard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnKeyboard, "Show Display-Keyboard" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to visualize Note messages from the player")
        Me.btnKeyboard.UseVisualStyleBackColor = True
        '
        'lblVolumeValue
        '
        Me.lblVolumeValue.AutoSize = True
        Me.lblVolumeValue.Location = New System.Drawing.Point(606, 250)
        Me.lblVolumeValue.Name = "lblVolumeValue"
        Me.lblVolumeValue.Size = New System.Drawing.Size(22, 13)
        Me.lblVolumeValue.TabIndex = 45
        Me.lblVolumeValue.Text = "xxx"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(394, 250)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 15)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Main Volume:"
        '
        'trkbVolume
        '
        Me.trkbVolume.AutoSize = False
        Me.trkbVolume.BackColor = System.Drawing.Color.AliceBlue
        Me.trkbVolume.LargeChange = 12
        Me.trkbVolume.Location = New System.Drawing.Point(479, 246)
        Me.trkbVolume.Maximum = 127
        Me.trkbVolume.Name = "trkbVolume"
        Me.trkbVolume.Size = New System.Drawing.Size(115, 21)
        Me.trkbVolume.TabIndex = 31
        Me.trkbVolume.TickFrequency = 10
        Me.trkbVolume.TickStyle = System.Windows.Forms.TickStyle.None
        Me.ToolTip1.SetToolTip(Me.trkbVolume, "set the main volume of the output device")
        Me.trkbVolume.Value = 100
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(392, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Output Port:"
        '
        'cmbMidiOutDevices
        '
        Me.cmbMidiOutDevices.FormattingEnabled = True
        Me.cmbMidiOutDevices.Location = New System.Drawing.Point(394, 36)
        Me.cmbMidiOutDevices.Name = "cmbMidiOutDevices"
        Me.cmbMidiOutDevices.Size = New System.Drawing.Size(226, 21)
        Me.cmbMidiOutDevices.TabIndex = 13
        '
        'btnTbMessage_clear
        '
        Me.btnTbMessage_clear.AutoSize = True
        Me.btnTbMessage_clear.Location = New System.Drawing.Point(281, 31)
        Me.btnTbMessage_clear.Name = "btnTbMessage_clear"
        Me.btnTbMessage_clear.Size = New System.Drawing.Size(40, 23)
        Me.btnTbMessage_clear.TabIndex = 12
        Me.btnTbMessage_clear.Text = "clear"
        Me.btnTbMessage_clear.UseVisualStyleBackColor = True
        '
        'tbMessage
        '
        Me.tbMessage.Location = New System.Drawing.Point(8, 31)
        Me.tbMessage.Multiline = True
        Me.tbMessage.Name = "tbMessage"
        Me.tbMessage.ReadOnly = True
        Me.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbMessage.Size = New System.Drawing.Size(267, 245)
        Me.tbMessage.TabIndex = 11
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.lblNumOfItems)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.lvEvents)
        Me.TabPage2.Controls.Add(Me.cmbTrackList)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(691, 353)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Event List"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(268, 12)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "select a track"
        '
        'lblNumOfItems
        '
        Me.lblNumOfItems.AutoSize = True
        Me.lblNumOfItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumOfItems.Location = New System.Drawing.Point(497, 12)
        Me.lblNumOfItems.Name = "lblNumOfItems"
        Me.lblNumOfItems.Size = New System.Drawing.Size(24, 15)
        Me.lblNumOfItems.TabIndex = 7
        Me.lblNumOfItems.Text = "xxx"
        Me.ToolTip1.SetToolTip(Me.lblNumOfItems, "Number of events in this track")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(404, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Number of Items:"
        '
        'lvEvents
        '
        Me.lvEvents.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvEvents.BackColor = System.Drawing.SystemColors.Window
        Me.lvEvents.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colNum, Me.colTime, Me.colEvType, Me.colSubType, Me.colChannel, Me.colData, Me.colDuration})
        Me.lvEvents.FullRowSelect = True
        Me.lvEvents.GridLines = True
        Me.lvEvents.HideSelection = False
        Me.lvEvents.Location = New System.Drawing.Point(10, 42)
        Me.lvEvents.MultiSelect = False
        Me.lvEvents.Name = "lvEvents"
        Me.lvEvents.Size = New System.Drawing.Size(673, 306)
        Me.lvEvents.TabIndex = 5
        Me.lvEvents.UseCompatibleStateImageBehavior = False
        Me.lvEvents.View = System.Windows.Forms.View.Details
        '
        'colNum
        '
        Me.colNum.Text = "Num."
        Me.colNum.Width = 40
        '
        'colTime
        '
        Me.colTime.Text = "Time"
        Me.colTime.Width = 70
        '
        'colEvType
        '
        Me.colEvType.Text = "Ev.Type"
        Me.colEvType.Width = 80
        '
        'colSubType
        '
        Me.colSubType.Text = "SubType"
        Me.colSubType.Width = 120
        '
        'colChannel
        '
        Me.colChannel.Text = "ch."
        Me.colChannel.Width = 30
        '
        'colData
        '
        Me.colData.Text = "Data"
        Me.colData.Width = 238
        '
        'colDuration
        '
        Me.colDuration.Text = "Duration"
        '
        'cmbTrackList
        '
        Me.cmbTrackList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTrackList.FormattingEnabled = True
        Me.cmbTrackList.Location = New System.Drawing.Point(10, 9)
        Me.cmbTrackList.Name = "cmbTrackList"
        Me.cmbTrackList.Size = New System.Drawing.Size(250, 21)
        Me.cmbTrackList.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbTrackList, "Select Track")
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.btnMuteNone)
        Me.TabPage3.Controls.Add(Me.btnMuteAll)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Controls.Add(Me.clbMute)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(691, 353)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Filter"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(272, 51)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(353, 45)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Please note that these are checkboxes for mute." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "To play a single Track, click 'A" &
    "ll' then uncheck the desired Track." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "To play all Tracks, click 'None' (filter/mu" &
    "te none of the tracks)"
        '
        'btnMuteNone
        '
        Me.btnMuteNone.Location = New System.Drawing.Point(184, 25)
        Me.btnMuteNone.Name = "btnMuteNone"
        Me.btnMuteNone.Size = New System.Drawing.Size(43, 23)
        Me.btnMuteNone.TabIndex = 3
        Me.btnMuteNone.Text = "None"
        Me.ToolTip1.SetToolTip(Me.btnMuteNone, "Mute no tracks (Play all tracks)")
        Me.btnMuteNone.UseVisualStyleBackColor = True
        '
        'btnMuteAll
        '
        Me.btnMuteAll.Location = New System.Drawing.Point(135, 25)
        Me.btnMuteAll.Name = "btnMuteAll"
        Me.btnMuteAll.Size = New System.Drawing.Size(43, 23)
        Me.btnMuteAll.TabIndex = 2
        Me.btnMuteAll.Text = "All"
        Me.ToolTip1.SetToolTip(Me.btnMuteAll, "Mute all tracks")
        Me.btnMuteAll.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(39, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Mute:"
        '
        'clbMute
        '
        Me.clbMute.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.clbMute.CheckOnClick = True
        Me.clbMute.FormattingEnabled = True
        Me.clbMute.Items.AddRange(New Object() {"Item 1", "Item 2", "Item 3", "Item 4"})
        Me.clbMute.Location = New System.Drawing.Point(42, 51)
        Me.clbMute.Name = "clbMute"
        Me.clbMute.Size = New System.Drawing.Size(185, 289)
        Me.clbMute.TabIndex = 0
        '
        'tpRecordedEvents
        '
        Me.tpRecordedEvents.AutoScroll = True
        Me.tpRecordedEvents.Controls.Add(Me.tbVoiceMap)
        Me.tpRecordedEvents.Controls.Add(Me.cbSaveXML)
        Me.tpRecordedEvents.Controls.Add(Me.Label4)
        Me.tpRecordedEvents.Controls.Add(Me.tbPatternName)
        Me.tpRecordedEvents.Controls.Add(Me.btnExportPattern)
        Me.tpRecordedEvents.Controls.Add(Me.tbRecordedEvents)
        Me.tpRecordedEvents.Controls.Add(Me.lblRecordedEventsCount)
        Me.tpRecordedEvents.Controls.Add(Me.Label7)
        Me.tpRecordedEvents.Location = New System.Drawing.Point(4, 22)
        Me.tpRecordedEvents.Name = "tpRecordedEvents"
        Me.tpRecordedEvents.Size = New System.Drawing.Size(691, 353)
        Me.tpRecordedEvents.TabIndex = 3
        Me.tpRecordedEvents.Text = "Recorded Events"
        Me.tpRecordedEvents.UseVisualStyleBackColor = True
        '
        'tbVoiceMap
        '
        Me.tbVoiceMap.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbVoiceMap.Location = New System.Drawing.Point(386, 44)
        Me.tbVoiceMap.Multiline = True
        Me.tbVoiceMap.Name = "tbVoiceMap"
        Me.tbVoiceMap.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbVoiceMap.Size = New System.Drawing.Size(295, 176)
        Me.tbVoiceMap.TabIndex = 15
        '
        'cbSaveXML
        '
        Me.cbSaveXML.AutoSize = True
        Me.cbSaveXML.Location = New System.Drawing.Point(532, 292)
        Me.cbSaveXML.Name = "cbSaveXML"
        Me.cbSaveXML.Size = New System.Drawing.Size(90, 17)
        Me.cbSaveXML.TabIndex = 14
        Me.cbSaveXML.Text = "Save as XML"
        Me.cbSaveXML.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(404, 238)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Pattern Name:"
        '
        'tbPatternName
        '
        Me.tbPatternName.Location = New System.Drawing.Point(407, 254)
        Me.tbPatternName.MaxLength = 12
        Me.tbPatternName.Name = "tbPatternName"
        Me.tbPatternName.Size = New System.Drawing.Size(124, 20)
        Me.tbPatternName.TabIndex = 12
        Me.tbPatternName.Text = "Pattern"
        '
        'btnExportPattern
        '
        Me.btnExportPattern.Location = New System.Drawing.Point(407, 288)
        Me.btnExportPattern.Name = "btnExportPattern"
        Me.btnExportPattern.Size = New System.Drawing.Size(89, 23)
        Me.btnExportPattern.TabIndex = 11
        Me.btnExportPattern.Text = "Export Pattern"
        Me.btnExportPattern.UseVisualStyleBackColor = True
        '
        'tbRecordedEvents
        '
        Me.tbRecordedEvents.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbRecordedEvents.Location = New System.Drawing.Point(8, 44)
        Me.tbRecordedEvents.Multiline = True
        Me.tbRecordedEvents.Name = "tbRecordedEvents"
        Me.tbRecordedEvents.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbRecordedEvents.Size = New System.Drawing.Size(349, 267)
        Me.tbRecordedEvents.TabIndex = 10
        Me.tbRecordedEvents.WordWrap = False
        '
        'lblRecordedEventsCount
        '
        Me.lblRecordedEventsCount.AutoSize = True
        Me.lblRecordedEventsCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRecordedEventsCount.Location = New System.Drawing.Point(215, 10)
        Me.lblRecordedEventsCount.Name = "lblRecordedEventsCount"
        Me.lblRecordedEventsCount.Size = New System.Drawing.Size(24, 15)
        Me.lblRecordedEventsCount.TabIndex = 9
        Me.lblRecordedEventsCount.Text = "xxx"
        Me.ToolTip1.SetToolTip(Me.lblRecordedEventsCount, "Number of events in this track")
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(148, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Nr of Items:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'tmDisplay
        '
        Me.tmDisplay.Interval = 250
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 450)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Test Midi_File"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.trkbPosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.trkbVolume, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.tpRecordedEvents.ResumeLayout(False)
        Me.tpRecordedEvents.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents btnKeyboard As Button
    Friend WithEvents lblVolumeValue As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents trkbVolume As TrackBar
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbMidiOutDevices As ComboBox
    Friend WithEvents btnTbMessage_clear As Button
    Friend WithEvents tbMessage As TextBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents lblNumOfItems As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lvEvents As ListView
    Friend WithEvents colNum As ColumnHeader
    Friend WithEvents colTime As ColumnHeader
    Friend WithEvents colEvType As ColumnHeader
    Friend WithEvents colSubType As ColumnHeader
    Friend WithEvents colChannel As ColumnHeader
    Friend WithEvents colData As ColumnHeader
    Friend WithEvents cmbTrackList As ComboBox
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Mi_File_Open As ToolStripMenuItem
    Friend WithEvents Mi_File_Close As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents Mi_File_Exit As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Tsbtn_Play As ToolStripButton
    Friend WithEvents TsBtnPause As ToolStripButton
    Friend WithEvents TsBtnStop As ToolStripButton
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents clbMute As CheckedListBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnMuteNone As Button
    Friend WithEvents btnMuteAll As Button
    Friend WithEvents lblPosition As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents trkbPosition As TrackBar
    Friend WithEvents tmDisplay As Timer
    Friend WithEvents colDuration As ColumnHeader
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents lblPositionMBT As Label
    Friend WithEvents lblEventsCount As Label
    Friend WithEvents cbRecordEvents As CheckBox
    Friend WithEvents tpRecordedEvents As TabPage
    Friend WithEvents lblRecordedEventsCount As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents tbRecordedEvents As TextBox
    Friend WithEvents btnExportPattern As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents tbPatternName As TextBox
    Friend WithEvents cbSaveXML As CheckBox
    Friend WithEvents tbVoiceMap As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents InfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Mi_Info_About As ToolStripMenuItem
    Friend WithEvents btnResetAllControllers As Button
    Friend WithEvents btnGM_on As Button
End Class
