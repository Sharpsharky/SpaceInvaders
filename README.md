<img src="Images/Insight.PNG" align="middle" width="3000"/>

# Space Invaders

The project represents a clone of a well known game [Space Invaders](https://www.youtube.com/watch?v=D1jZaIPeD5w&ab_channel=316whatupz).

The game starts with a loading screen, in main menu we can proceed to start game and high score page. High score page contains 10 last saved highest scores. It takes an advantage of

In the beginning, the enemies slide towards the middle of the screen and move right and left. We acquire score points everytime we destroy an enemy. The game is an infinite loop and a new wave appears when a player kills all the space ships on the screen.

## Build
The build is playable on any Android device (with Internet) and can be downloaded from main folder. File: **"Space Invaders.apk"**. It uses Amazon S3 Server to hold **AssetBundles**.


## Features
- Unity.Addressables with **AssetBundles** already on server,
- [Amazon S3 Server for Unity.Addressables](https://s3.console.aws.amazon.com/s3/buckets/spaceinvadersbucket?region=eu-central-1&prefix=Android/&showversions=false),
- Dependency Injection,
- Obsever Pattern,
- Singleton Pattern (uncommonly).



## Additional Resources
[Dynamic Space Background Lite](https://assetstore.unity.com/packages/2d/textures-materials/dynamic-space-background-lite-104606#description) by DinV Studio
