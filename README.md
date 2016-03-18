# Connect4

Console version of Connect 4 for arbitrary number of players (human or robot) in C#

## Features

 * Arbitrary number of players (in code, `_totalPlayers`)
 * Player names and colours
 * Human or robot players (only one robot per game)
 * Starting player chosen randomly
 * Victory detection
 * Can be adapted to be connect-n (in code)
 * Arbitrary grid size (in code)
 
## Limitations

 * Not much input validation
 * Robot player makes random moves
 * Only one robot per game

## Technical

The victory detection uses a cool recursive algorithm to determine the length of a line. Lines are counted from left-to-right and top-to-bottom (except for the `/` line, which is counted top-right-to-bottom-left) so that there are no loops while counting length. So the direction of a line is expressed as one of:

	Single		. (not a line)
	Down		|
	Right		-
	LeftDown	/
	RightDown	\
	

This is the code for the algorithm (a win is determined if LineLength == 4):

	public int LineLength(Counter thisCounter, Direction direction)
	{
		if (direction == Direction.Single)
		{
			//Direction not known
			direction = GetDirection(thisCounter);
			if (direction == Direction.Single)
			{
				//Not connected to anything
				return 1;
			}
			return LineLength(thisCounter, direction);
		}
		else
		{
			Counter next = GetNeighbour(thisCounter, direction);
			//if the neighbour is empty or owned by a rival, end the line
			if (next == null || !thisCounter.Owner.Owns(next))
			{
				return 1;
			}
			return 1 + LineLength(next, direction);

		}

	}

--------

## License

[UNLICENSE](http://unlicense.org/UNLICENSE)

## Contributing

Contributions are not anticipated.

If you really would like to contribute, please open an Issue to discuss it first :)

--------

Cheers!

Made with <3 by @SteGriff