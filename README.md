# Sprinkler Assignment

This project contains the solution for the sprinkler assignment.

## Tandm Programming Challenge
Your mission should you choose to accept it is to calculate the number of sprinklers, their 
positions on the room’s ceiling and connect each sprinkler to the nearest water pipe. 
The room has a rectangular shape. Ceiling coordinates (x, y, z) are:

```
(97500.00, 34000.00, 2500.00)
(85647.67, 43193.61, 2500.00)
(91776.75, 51095.16, 2500.00)
(103629.07, 41901.55, 2500.00)
```
Three water pipes are available:

```
(98242.11, 36588.29, 3000.00) to (87970.10, 44556.09, 3500.00)
(99774.38, 38563.68, 3500.00) to (89502.37, 46531.47, 3000.00)
(101306.65, 40539.07, 3000.00) to (91034.63, 48506.86, 3000.00)
```

Sprinklers are to be placed on the ceiling **2500mm** away from the walls and from each other. 
Please, calculate the number of sprinklers that can be fitted into this room, then calculate 
coordinates (x, y, z) of each sprinkler.
For each sprinkler calculate coordinates (x, y, z) of the connection point to the nearest water 
pipe.

Implement your solution using C# programming language and .Net Core framework. Submit 
source code and a copy of the output of your program.
Thank you

## Description

A C# console application that solves the sprinkler assignment requirements.

## How to Run

```bash
dotnet run
```

## Project Structure

- `Program.cs` - Main application file
- `.gitignore` - Git ignore rules for C# projects