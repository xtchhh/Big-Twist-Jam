# Big Twist Jam 

## Overview

This was a fun project to work on. The Big Twist Game Jam required participants to create a sudden twist in the gameplay whether that be through story, mechanics, visuals, sound, AI, or any combination. I chose to change the game mechanics.

My inspiration was Green Goblin mode from *Spider-Man the Movie Game*. The goal was to recreate the Green Goblin's glider mechanics within the jam timeframe.

---

## Game Flow & The Twist

The player starts on a rooftop walking as Green Goblin. The glider is resting nearby with a prompt: "Press R to ride the glider." This prompt only appears when the player is within a certain distance — calculated by subtracting two position vectors and grabbing the magnitude. If the player is within range and presses R, the scene transitions to the glider-riding phase.

The scene switch is not the best solution, but it worked without terrain inconsistencies because the assets were copied directly into the glider scene.

---

## Glider Movement System

The glider was the core system of this project. Movement works by multiplying the Y component of a `Vector2` move action by the camera's forward transform, binding W and S to move the object forward and back relative to where the camera is pointing. The camera's forward transform is stored as a `Vector3` variable called `forward`, normalized to isolate direction from magnitude.

When there is no player input, the glider moves forward automatically at an idle speed using `cam.transform.forward`. This simulates how an aircraft actually behaves, it doesn't pause mid-air. When the player gives input (W or S), `moveSpeed` is set to a higher value to simulate acceleration. Pressing S sets the speed below idle speed, simulating a slowdown in speed. Under the hood, if `move.y` is `-1`, `moveSpeed` is set to a lower float value like `2`.

---

## Collision

Rather than formal colliders, collision is handled with a raycast shooting forward a short distance from the glider. If it hits something, the scene transitions to a black screen displaying "You crashed." It's blunt, but given the time constraints it got the job done and communicated clearly what happened to the Player, almost obnoxiously haha.

---

## Audio

I'm no audio engineer but it was fun playing around with the gliders engine sound. By storing a reference to an `AudioSource` and adjusting its `pitch` value based on player input - for example, setting `AudioSource.pitch = 3f` - the engine sound rises and falls with acceleration and deceleration. Simple, but it adds a lot to the feel of the glider.
