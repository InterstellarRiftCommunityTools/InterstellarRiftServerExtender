# Interstellar Rift Server Extender
An Extended Dedicated Server for Interstellar Rift https://www.interstellarrift.com/

**Discord Group** - https://discord.gg/XqrAkhy

### Websites
**Patreon** - https://www.patreon.com/irse


[![Build status](https://ci.appveyor.com/api/projects/status/vemitxcyep1kgvdm?svg=true)](https://ci.appveyor.com/project/generalwrex/interstellarriftserverextender)


# FEATURES
     You can edit the server.json(Dedicated Server config) in the GUI
     Console commands to use (See the Console Command section below)
     You can read the chat from all players that talk on the console, or on the Chat tab of the GUI.
     You can build and use plugins.
     Custom console/chat commands. (IR Console/Chat commands work when server is running)

# INSTALLING

Just extract thes files and the folder "IRSE" from the zip archive into your Interstellar Rift installation folder.

     IRSE.exe
     IRSE.exe.config  
     IRSE/          
          config/
               NLog.config
          bin/
               NLog.dll
               Octokit.dll
               0Harmony.dll
               MarkdownDeep.dll
               Newtonsoft.Json.dll
               

To start the server, just run IRSE.exe ( make sure the files and folder above exist with IRSE.exe)

# Console Commands
commands start with / (IR's Command system is used when the server is running)

     
     opengui - reopens the GUI if it was closed
     restart - restart IRSE.
     checkupdate - check for IRSE updates
     forceupdate - Force the installation of new IRSE, bypassing prompts.
     start - starts the server

Once the server is running these commands are added to IR's own command system.


# Contributors
Generalwrex
Kyuubi

If you would like to help, fork and make some pull requests!
