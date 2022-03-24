
<h1 align="center">
  <br>
   <a>Minecraft Style World - Starter Project</a>
  <br>
</h1>

<p align="center">
  <a href="#description">Description</a> •
  <a href="#how-to-use">How To Use</a> •
  <a href="#credits">Credits</a> •
  <a href="#license">License</a>
</p>

<p align="center">
  <img src="./preview.gif" />
</p>

## Description

A minimalistic starter project of a 3D world with a generated cube-based terrain using perlin noise to get a minecraft feel build on top of [Monogame](https://www.monogame.net/). 
It features : 
- Fast F5 xp ready and should just run out of the box. 
- Minimal and simple set of classes to get a 3D world with a basic camera to move around, with no other dependencies than Monogame. 
- Very close to the implementation of monogame, so highly customisable. 
- High FPS count (500fps with the default world size) / low draw times (2ms total loop on a Geforce 1080), offers a confortable time budget to do game logic (in my case AI logic).
- Lightweight 413K triangles, 1080p at 500fps on a 280Mb working set.

I'm using this starter project as a base for implementing Reinforcement Learning (RL) and other artificial intelligence tests & simulations.

## How To Use

To clone and run this application, you'll need [Git](https://git-scm.com), with [Visual Studio 2019 or 2022 Community](https://visualstudio.microsoft.com/downloads/) installed on your computer. Make sure to follow those [monogame install instructions](https://docs.monogame.net/articles/getting_started/1_setting_up_your_development_environment_windows.html) too. You can skip the install of MonoGame extension and MGCB Editor.
Then from your command line:

```bash
# Clone this repository
$ git clone https://github.com/EtienneLorthoy/starter.git

# Go into the repository
$ cd starter/src

# Launch
$ ./StarterMinecraftStyleWorld.sln

# Run F5 or Ctrl + F5
```

## Credits

This software uses the following open source packages:

- [Monogame](https://www.monogame.net/)

## License

MIT

---

> [etiennelorthoy.com](https://etiennelorthoy.com) &nbsp;&middot;&nbsp;
> LinkedIn [@etiennelorthoy](https://www.linkedin.com/in/etienne-lorthoy/) &nbsp;&middot;&nbsp;
> GitHub [@etiennelorthoy](https://github.com/EtienneLorthoy) &nbsp;&middot;&nbsp;
> Twitter [@ELorthoy](https://twitter.com/ELorthoy)
