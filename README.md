# AutoSavestateMaker

After much frustration from having to restart long segments I'm trying to practice, I had the idea for a program to create savestates for me. After finally developing it enough, I'm ready to share it.

The program makes savestates every few seconds to make long, failable challenges easier to practice. It works by sending the savestate hotkey to Project64 every few seconds, with means to rewind to whichever savestate you want. This has proven to be quite convenient in practicing long kaizo stars, and it's especially perfect for IronMario practice runs.

![Window](readme_images/running-window.png)

----------
### Compatibility
- This is intended only for Luna's Project64, but it should easily work on Project 64 v1.6 on savestates 0-9.
- Parallel might work with some tinkering.

----------
### Usage
- Pick your options, then hit run at the top-left.
- *Important* Savestates will only be made when Project64 is focused. This is so that you can easily do other things while the program is running. The "focus game with A" option helps make this easy to manage.

----------
### Setup
- To prevent the constant "making-a-savestate stutters", it's important to turn off the "Automaticaly compress saved states" option in Luna. This will however make savestates take up 8MB each instead of more like 1MB. (Shoutouts to Shin3 for this idea <3)
![Compressed Savestates](readme_images/luna-compressed-savestates.png)
- Then simply open the .exe file.
- A config file will be created after running the .exe for the first time. There you can edit the default values and some hidden settings. Make sure to restart the program if you manually edit the config.
- At the bottom-left, pick your controller then pick "edit" to set your hotkeys. These are optional, and you can leave the controller blank to not use them.


----------
### Options
- Interval: how many seconds before creating the next savestate.
- Savestates: the number of savestate slots. Luna supports up to 80 with the default hotkeys.
- Focus game with A: this option pulls back focus to Project64 when you start playing again. (This is highly recommended since savestates will only be made while the game is focused)
- Hotkeys: this turns on hotkey usage for your controller. See the edit screen for the list. Note that "focus game with A" works without this setting on.
- Require shift: hotkeys will now require a shift hotkey to function. Pick the shift hotkey in the "edit" menu.

#### Hidden config Options
- "rewindAtLeastBySeconds": this handles whether to rewind by an extra slot. Sometimes a savestate is created a moment before you die, so this helps rewind to a point where you are more likely to be safe. Some low value of 2-4 is recommended.
- "extraPauseSecondsOnLoad": this adds extra pause seconds when you load a savestate. For cases where you have to repeatedly load to get safe, this helps prevent the interval from creating another savestate before you are safe.