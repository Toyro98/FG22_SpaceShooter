# FG22_SpaceShooter
 
The code is split up in different folders (Aspect, Job, System, and etc). It follows CamelCase code style and uses '_' for private members/fields and camelCase for variables inside functions.

After installing the memory profiler package and taking several screenshots and analysing it. Nothing stood up as an issue and could be improved on. There were a lot of usage of `System.Byte[]` which I think is from compiled code? 

##

Early on, I tried implementing collision as I had with the pre-optimized version. Adding a collider would drop the frames a lot. Specially when there are a lot of enemies, in the build there are 250, so it would not drop frames that much, with +7500 enemies, it probably wouldn't get over 5 fps (on my pc).
Physics was used to move the player around. This was removed due to unable to figure out how to add physics in dots.

## Pre-optimized
[branch](https://github.com/Toyro98/FG22_SpaceShooter/tree/pre-optimized)

![](https://i.imgur.com/DGWPzdA.png)

## main
[branch](https://github.com/Toyro98/FG22_SpaceShooter/tree/main)

![](https://i.imgur.com/v7wqJzO.png)
