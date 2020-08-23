# Conways-Game-of-Life

## Explanation

Refer website for more details: [https://joasus.wixsite.com/portfolio/conways-game-of-life](https://joasus.wixsite.com/portfolio/conways-game-of-life)

Each cell has one [<em>CellBehaviour.cs</em>](Assets/Scripts/CellBehaviour.cs) component. It subscribes to the 2 C# actions <em>OnEnable</em> and <em>OnDisable</em>. On the first C# action invocation, <em>GiveAliveCountToNeighbours</em> gets calls (explained above). On the second C# action call, <em>Game2()</em> gets calls and this has the major rules of the game of life.

Instead of C# actions, we could also use normal for loops to loop through the cells in the same manner. So two for loops, one for neighbour count and the second for the rules and alive and dead updation. We could optimise this as well, by running the second for loop only among or near previously alive cells since only their neighbours have a chance of living again. But of course this would depend on the rules provided.

### Sample Video

[![Conway's game of Life in Unity](https://img.youtube.com/vi/lZbP1kLvBSU/0.jpg)](https://www.youtube.com/watch?v=lZbP1kLvBSU)
