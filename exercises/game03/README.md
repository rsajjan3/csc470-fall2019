Game Design Document
======

## Game Idea/Goals
The basic structure of the game is as follows: the player will select from a variety of units which will have different characteristics. Some of the unit types will be: light, heavy, miner. Light units will move fast, do low amounts of damage, and will die quickly. Heavy units will move slow, do high amounts of damage, and will die slowly. Miner units will move very fast, have very little health, but will be able to find resources.


The goal is for the player to mine as many resources as possible in a given period of time. Some resources will readily mineable, meaning a miner unit will be able to pickup the resource without any help. Some resources will be enclosed in structures which will need to be broken down by light or heavy units, depending on how strong the structure is. Some resources will be guarded by enemies. 

A player will have a given number of resources at the start of the game, and the player must work within the given number of resources to mine other resources. Mined resources do not replinish resources for building units. Winning is defined as most number of resources mined, and least amount of units used.

## Technical Overview
1. The game will need to randomly generate the map and the resources within
2. Randomly generate starting resources and place enemies around map
3. Define unit types, and their movement speeds, damage, abilities, etc.
4. Terrain handling for units
5. Define enemy types, difficulty, weapons, etc.