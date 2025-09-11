<div class="header" align="center">
<img alt="Space Station 14" width="880" height="300" src="https://raw.githubusercontent.com/space-wizards/asset-dump/de329a7898bb716b9d5ba9a0cd07f38e61f1ed05/github-logo.svg">
</div>

Dune Station 14 is a fork of Space Station 14 which is a remake of SS13 that runs on [Robust Toolbox](https://github.com/space-wizards/RobustToolbox).

To prevent people forking RobustToolbox, a "content" pack is loaded by the client and server. This content pack contains everything needed to play the game on one specific server.

If you want to host or create content for SS14, this is the repo you need. It contains both RobustToolbox and the content pack for development of new content packs.

## Links

<div class="header" align="center">

[Dune Station Discord server](https://discord.gg/bm6kcQkh)

</div>

## Building

1. Clone this repo:
```shell
git clone https://github.com/space-wizards/space-station-14.git
```
2. Go to the project folder and run `RUN_THIS.py` to initialize the submodules and load the engine:
```shell
cd space-station-14
python RUN_THIS.py
```
3. Compile the solution:

Build the server using `dotnet build`.

[More detailed instructions on building the project.](https://docs.spacestation14.com/en/general-development/setup.html)

## License

Everything after the commit edebcad49f4dd974ddc79da12084b90bfe693d5b is licenced under AGPL-3.0-or-later licence, unless stated otherwise.
Most media assets are licenced under CC-BY-SA 3.0, unless stated otherwise.

