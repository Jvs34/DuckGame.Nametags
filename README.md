# DuckGame.NameTags

Shows nametags above players before a round starts.

## What do I need to build it?

I use Visual Studio 2023 and .NET Framework 4.5.2, which is what Duck Game uses currently.

## How do I build it?

1. Clone the repository anywhere:

```bash
    cd C:\DuckGameMods
    git clone https://github.com/Jvs34/DuckGame.Nametags Nametags
```

2. Copy DuckGame.exe from your install of the game to `.\Nametags\build\ThirdParty`.
3. Make sure your Duck Game data folder is in `%AppData%\DuckGame\` , if it isn't, move it there from your Documents folder as that's only used for the 2017 version.
4. Open Nametags.csproj and build it.
