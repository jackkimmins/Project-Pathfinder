# Project-Pathfinder
My simple pathfinding algorithm that navigates between two points and avoids obstacles.

This project is nothing serious and I wouldn't suggest using it for anything important. Basically, it randomly generates 32x32 terrain grid and disperses several obstacles in the form of the letter 'X'. Then the pathfinder tries to navigate the most optimal path between the randomly set start and end points.

On every step, it checks the 8 tiles surrounding its current position. Whatever tile gets it closer to the goal and is not an obstacle is the one that it will pick. Unlike other pathfinding algorithms such as Minimax, it only checks to a depth of one, meaning that it might loop back on itself because it reached a dead end. At some point in the future, I will improve this.
