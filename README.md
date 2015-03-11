# skymod-remotecon

A remote debugging console for developing Cities: Skylines mods. Allows you to write debug information to a separate console window while the game is running. The contents of the console are preserved after the game closes.

## Usage:

Add a reference to SkyMod.RemoteConsole.Client to your project (see test plugin for example). Import the SkyMod.RemoteConsole.Client namespace and start logging!

    RemoteConsole.Client.ConsoleClient.Warn("This should be yellow (warning).");
    RemoteConsole.Client.ConsoleClient.Error("This should be red (error).");
    RemoteConsole.Client.ConsoleClient.Info("This should be normal.");

## Console Host:

The SkyMod Remote Console host runs on port `44324`. The application (or Visual Studio, if running through the debugger) must be run as admin.

## Known Issues:

Currently, the Remote Console Client may send messages out of order. This is due to how the Colossal Framework queues web requests. Any pull requests to queue the messages to preserve order are accepted.
