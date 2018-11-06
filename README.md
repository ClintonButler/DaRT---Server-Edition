# DaRT - Server Edition with Player Login/Logout Announcement and Player Kill Announcement

Feel free to fork and modify for your own needs. This repo will not be actively developed futher at this time, as of DayZ SA 0.63.148873.
Current Release is considered stable.

WARNING: THIS IS LIKELY TO BREAK IF LOG FILE SYNTAX CHANGES AFTER 0.63.148873.

### Player Login/Logout Announcement
- Setting added to enable/disable announcement (working)
- Uses Player Login and Logout internal detects to send command say -1 (working)
- Strips UID from string (working)

### Player Kill Announcement
- Setting added to enable/disable announcement (working)
- Setting added for DayZServer_x64.ADM location (working)
- StreamReader optimized and Threaded (working)
- Strip Garbage from String (working)

### ORIGINAL README FROM DomiStyle/DaRT

# DaRT

### Description
DaRT (DayZ RCon Tool) is a graphical interface for the BattlEye RCon protocol.

It utilizes the [BattleNET](https://github.com/marceldev89/BattleNET) library by [marceldev89](https://github.com/marceldev89).

### Notes

If DaRT should crash the first time you build it just build it once more - looking into the problem.

I do not actively develop DaRT anymore. Feel free to fork DaRT though.
