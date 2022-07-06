## Plugins Deployment
To deploy a plugin based on [Plugin Template](https://github.com/PeernetOfficial/SDK) project it is recommanded to first use [dotnet publish](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish) command to produce the output.

For __Peernet.Browser.Plugins.MediaPlayer__ plugin the command would be:

> ~\Peernet.Browser.Plugins.MediaPlayer>__dotnet publish -c Release --no-self-contained__

It compiles the project into _~\Peernet.Browser.Plugins.MediaPlayer\bin\Release\net5.0-windows\publish_ directory. The content of the directory should be deployed
to a new subfolder under the Peernet Browser' __PluginsLocation__ (see [Plugins System](https://github.com/PeernetOfficial/Browser/tree/MvvmCrossRemoval#plugins-system) for more details).  
__.pdb__ files which are symbol files for the debugger and can be omitted as well as __.deps.json__ files.

## Plugins supported Formats
### Peernet.Browser.Plugins.ByteViewer
Binary files

### Peernet.Browser.Plugins.ImageViewer
Image files

### Peernet.Browser.Plugins.MediaPlayer
Video and Audio files

### Peernet.Browser.Plugins.TextViewer
Text files