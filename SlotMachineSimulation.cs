using System;

namespace BingoSlotDifferentialEvolutionOptimization {
class SlotMachineSimulation {
	/*
	* Used in bingo cards generation for better suffling.
	*/
	private const int NUMBER_OF_SHAKES = 30;

	private const int BINGO_CARDS_TALONS = 6;

	private const int BINGO_CARDS_WIDTH = 9;

	private const int BINGO_CARDS_LENGTH = 18;

	private const int BINGO_BONUS_DISTRIBUTION_LENGTH = 63;

	public const long NUMBER_OF_EXPERIMENTS = 10;

	public const long NUMBER_OF_SIMUALTIONS = 10000;//00;

	/**
	* Bingo line bonus distribution.
	*/
	private int[] bingoLineBonusDistribution = { 10, 15, 20,
												 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20,
												 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20,
												 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20,
												 10, 15, 20, 10, 15, 20,
											   };

	/**
	* Bingo line bonus distribution.
	*/
	private int[] bingoBonusDistribution = { 90, 100, 110,
											 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110,
											 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110,
											 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110,
											 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110,
										   };

	/**
	* Numbers out in the bingo game (only flags).
	*/
	private bool[][] bingoNumbersOut = { new bool[] {
			false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false, false
		}, new bool[] {
			false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false,
			false
		}, new bool[] {
			false, false, false, false, false, false,
			false, false, false, false, false, false, false, false,
			false, false, false, false
		}, new bool[] {
			false, false, false,
			false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false
		}, new bool[] {
			false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false,
			false, false
		}, new bool[] {
			false, false, false, false, false,
			false, false, false, false, false, false, false, false,
			false, false, false, false, false
		}, new bool[] {
			false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false,
			false
		}, new bool[] {
			false, false, false, false, false, false,
			false, false, false, false, false, false, false, false,
			false, false, false, false
		}, new bool[] {
			false, false, false,
			false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false
		}
	};

	/**
	* Bingo cards for the bonus game.
	*/
	private int[][] bingoCards = {
		new int[] { 1, 2, 3, 4, 5, 6, 7,	8, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new int[] {	10, 11, 12, 13, 14, 15, 16, 17, 18,	19, 0, 0, 0, 0, 0, 0, 0, 0 },
		new int[] {	20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 0, 0, 0, 0, 0, 0, 0, 0	},
		new int[] {	30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 0, 0, 0, 0, 0, 0, 0, 0	},
		new int[] { 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 0, 0, 0, 0, 0, 0, 0, 0	},
		new int[] {	50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 0, 0, 0, 0, 0, 0, 0, 0	},
		new int[] {	60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 0, 0, 0, 0, 0, 0, 0, 0	},
		new int[] {	70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 0, 0, 0, 0, 0, 0, 0, 0	},
		new int[] {	80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 0, 0, 0, 0, 0,	0, 0 },
	};

	/**
	* Index of the bingo line in the bingo card.
	*/
	private int bingoLineIndex = -1;

	/**
	* Index of the card with the bingo in it;
	*/
	private int bingoCardIndex = -1;

	/**
	* Counting numbers in rows in order to keep track of the balance.
	*/
	private int[] numbersInRow = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

	/**
	* Lines combinations.
	*/
	private int[][] lines = {
		new int[]{ 1, 1, 1, 1, 1 },
		new int[]{ 0, 0, 0, 0, 0 },
		new int[]{ 2, 2, 2, 2, 2 },
		new int[]{ 0, 1, 2, 1, 0 },
		new int[]{ 2, 1, 0, 1, 2 },
		new int[]{ 0, 0, 1, 2, 2 },
		new int[]{ 2, 2, 1, 0, 0 },
		new int[]{ 1, 0, 1, 2, 1 },
		new int[]{ 1, 2, 1, 0, 1 },
	};

	/**
	* Lines combinations.
	*/
	private int[][] reels = {
		new int[] {
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0, 0, 0,
		},
		new int[] {
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0, 0, 0,
		},
		new int[] {
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0, 0, 0,
		},
		new int[] {
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0, 0, 0,
		},
		new int[] {
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0,	0,	0, 0, 0, 0,	0,	0, 0, 0,
			0, 0, 0,
		},
	};

	/**
	* Current visible symbols on the screen.
	*/
	private int[][] view = {
		new int[]{ -1, -1, -1 },
		new int[]{ -1, -1, -1 },
		new int[]{ -1, -1, -1 },
		new int[]{ -1, -1, -1 },
		new int[]{ -1, -1, -1 },
	};

	/**
	* Total bet in single base game spin.
	*/
	private int totalBet = 0;

	/**
	* Total amount of won money.
	*/
	private long wonMoney = 0L;

	/**
	* Total amount of lost money.
	*/
	private long lostMoney = 0L;

	/**
	* Total amount of won money in base game.
	*/
	private long baseMoney = 0L;

	/**
	* Total amount of won money in bonus game.
	*/
	private long bonusMoney = 0L;

	/**
	* Max amount of won money in base game.
	*/
	private long baseMaxWin = 0L;

	/**
	* Total number of base games played.
	*/
	private long totalNumberOfGames = 0L;

	/**
	* Hit rate of wins in base game.
	*/
	private long baseGameHitRate = 0L;

	/**
	* Hit rate of wins in bonus game.
	*/
	private long bonusGameHitRate = 0L;

	/**
	* Symbols win hit rate in base game.
	*/
	private long[][] baseSymbolMoney = {
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
	};

	/**
	* Symbols hit rate in base game.
	*/
	private long[][] baseGameSymbolsHitRate = {
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		new long[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
	};

	/**
	* Fix by columns not to have 0 or 3 numbers in column.
	*
	* @return
	*/
	private bool fixThreeRows () {
		bool wasItChanged = false;

		for (int i = 0; i < 9; i++) {
			int a = -1;
			int b = -1;

			for (int j = 0; j < 18; j += 3) {
				if (0
						== (bingoCards [i] [j + 0] != 0 ? 1 : 0)
						+ (bingoCards [i] [j + 1] != 0 ? 1 : 0)
						+ (bingoCards [i] [j + 2] != 0 ? 1 : 0)) {
					a = j + Util.prng.Next (3);
				}
				if (3
						== (bingoCards [i] [j + 0] != 0 ? 1 : 0)
						+ (bingoCards [i] [j + 1] != 0 ? 1 : 0)
						+ (bingoCards [i] [j + 2] != 0 ? 1 : 0)) {
					b = j + Util.prng.Next (3);
				}
			}

			if (a == -1 && b == -1) {
				continue;
			}
			if (a == -1) {
				do {
					a = Util.prng.Next (18);
				} while (bingoCards [i] [a] != 0);
			}
			if (b == -1) {
				do {
					b = Util.prng.Next (18);
				} while (bingoCards [i] [b] == 0);
			}

			int swap = bingoCards [i] [a];
			bingoCards [i] [a] = bingoCards [i] [b];
			bingoCards [i] [b] = swap;
			numbersInRow [a]++;
			numbersInRow [b]--;
			wasItChanged = true;
		}

		return (wasItChanged);
	}

	/**
	* Fix all rows to have only 5 numbers.
	*
	* @return True if fix was done, false otherwise.
	*/
	private bool fixRows () {
		bool wasItChanged = false;

		bool done = false;
		do {
			done = false;

			int a = -1;
			int b = -1;

			for (int j = 0; j < 18; j++) {
				if (numbersInRow [j] < 5) {
					a = j;
				}
				if (numbersInRow [j] > 5) {
					b = j;
				}
			}
			if (a == -1 || b == -1) {
				done = true;
				break;
			}

			int x = -1;
			for (int i = 0; i < 9; i++) {
				if (bingoCards [i] [a] == 0 && bingoCards [i] [b] != 0) {
					x = i;
					break;
				}
			}

			if (x == -1) {
				done = false;
				continue;
			}

			int swap = bingoCards [x] [a];
			bingoCards [x] [a] = bingoCards [x] [b];
			bingoCards [x] [b] = swap;
			numbersInRow [a]++;
			numbersInRow [b]--;
			wasItChanged = true;
		} while (done == false);

		return (wasItChanged);
	}

	/**
	* Shuffle the numbers in single bingo card.
	*/
	private void shuffleBingoCards () {
		for (int i = 0; i < BINGO_CARDS_WIDTH; i++) {
			for (int last = BINGO_CARDS_LENGTH - 1, r = -1, swap = -1; last > 0;
					last--) {
				r = Util.prng.Next (last + 1);
				swap = bingoCards [i] [last];
				bingoCards [i] [last] = bingoCards [i] [r];
				bingoCards [i] [r] = swap;
			}
		}

		for (int j = 0; j < BINGO_CARDS_LENGTH; j++) {
			numbersInRow [j] = 0;
		}

		for (int j = 0; j < BINGO_CARDS_LENGTH; j++) {
			for (int i = 0; i < BINGO_CARDS_WIDTH; i++) {
				numbersInRow [j] += (bingoCards [i] [j] != 0 ? 1 : 0);
			}
		}
	}

	/**
	* Generate random bingo card with 6 talons in it. Also mark the card as empty.
	*/
	private void generateRandomBingoCard () {
		int shakes = 0;
		bool goOn = false;
		do {
			if (shakes <= 0) {
				shuffleBingoCards ();
				shakes = NUMBER_OF_SHAKES;
			}

			goOn = fixRows ();
			goOn = fixThreeRows () || goOn;
			shakes--;
		} while (goOn == true);

		for (int j = 0; j < BINGO_CARDS_LENGTH; j++) {
			for (int i = 0; i < BINGO_CARDS_WIDTH; i++) {
				bingoNumbersOut [i] [j] = false;
			}
		}

		bingoLineIndex = -1;
		bingoCardIndex = -1;
	}

	/**
	* Single reels spin to fill view with symbols.
	*
	* @param reels Reels strips.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 10 Mar 2013
	*/
	private void spin (int[][] reels) {
		for (int i = 0; i < view.Length && i < reels.Length; i++) {
			int r = Util.prng.Next (reels [i].Length);
			int u = r - 1;
			int d = r + 1;

			if (u < 0) {
				u = reels [i].Length - 1;
			}

			if (d >= reels [i].Length) {
				d = 0;
			}

			view [i] [0] = reels [i] [u];
			view [i] [1] = reels [i] [r];
			view [i] [2] = reels [i] [d];
		}
	}

	/**
	* Calculate win in particular line.
	*
	* @param line Single line.
	*
	* @return Calculated win.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 10 Mar 2013
	*/
	private int lineWin (int[] line) {
		/*
		* Keep first symbol in the line.
		*/
		int symbol = line [0];

		/*
		* Count symbols in winning line.
		*/
		int number = 0;
		for (int i = 0; i < line.Length; i++) {
			if (line [i] == symbol) {
				number++;
			} else {
				break;
			}
		}

		/*
		* Cleare unused symbols.
		*/
		for (int i = number; i < line.Length; i++) {
			line [i] = -1;
		}

		int win = Paytable.values [number] [symbol];

		if (win > 0) {
			baseSymbolMoney [number] [symbol] += win;
			baseGameSymbolsHitRate [number] [symbol]++;
		}

		return(win);
	}

	/**
	* Calculate win in all possible lines.
	*
	* @param view Symbols visible in screen view.
	*
	* @return Calculated win.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 10 Mar 2013
	*/
	private int linesWin (int[][] view) {
		int win = 0;

		/*
		* Check wins in all possible lines.
		*/
		for (int l = 0; l < lines.Length; l++) {
			int[] line = { -1, -1, -1, -1, -1 };

			/*
			* Prepare line for combination check.
			*/
			for (int i = 0; i < line.Length; i++) {
				int index = lines [l] [i];
				line [i] = view [i] [index];
			}

			int result = lineWin (line);

			/*
			* Accumulate line win.
			*/
			win += result;
		}

		return(win);
	}

	/**
	* Mark bingo number and return it to the caller.
	*
	* @return The number marked.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 27 Feb 2015
	*/
	private int ballOut () {
		bool canBeFound = false;

		/*
		* Check for available numbers.
		*/
		for (int b = 0; b < BINGO_CARDS_LENGTH; b++) {
			for (int a = 0; a < BINGO_CARDS_WIDTH; a++) {
				if (bingoNumbersOut [a] [b] == false && bingoCards [a] [b] != 0) {
					canBeFound = true;
				}
			}
		}

		/*
		* It should not be possible to search for numbers when there is no any.
		*/
		if (canBeFound == false) {
			return (-1);
		}

		int i = -1;
		int j = -1;
		do {
			i = Util.prng.Next (BINGO_CARDS_WIDTH);
			j = Util.prng.Next (BINGO_CARDS_LENGTH);
		} while (bingoNumbersOut [i] [j] == true || bingoCards [i] [j] == 0);

		bingoNumbersOut [i] [j] = true;

		return (bingoCards [i] [j]);
	}

	/**
	* Check is there a bingo line combination.
	*
	* @return True if there is a bingo line, false otherwise.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 27 Feb 2015
	*/
	private bool bingoLine () {
		if (bingoLineIndex != -1) {
			return (false);
		}

		for (int j = 0; j < BINGO_CARDS_LENGTH; j++) {
			int count = 0;
			for (int i = 0; i < BINGO_CARDS_WIDTH; i++) {
				if (bingoNumbersOut [i] [j] == true && bingoCards [i] [j] != 0) {
					count++;
				}
			}

			if (count > 5) {
				//TODO It should not be possible.
			} else if (count == 5) {
				bingoLineIndex = j;
				return (true);
			}
		}

		return (false);
	}

	/**
	* Bingo line bonus win.
	*
	* @return Amount of the win according discrete distribution.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 27 Feb 2015
	*/
	private int bingoLineWin () {
		return (bingoLineBonusDistribution [Util.prng.Next (BINGO_BONUS_DISTRIBUTION_LENGTH)]);
	}

	/**
	* Check is there a bingo combination.
	*
	* @return True if there is a bingo, false otherwise.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 27 Feb 2015
	*/
	private bool bingo () {
		if (bingoCardIndex != -1) {
			return (false);
		}

		int count = 0;
		for (int j = 0; j < BINGO_CARDS_LENGTH; j++) {
			if (j % 3 == 0) {
				count = 0;
			}

			for (int i = 0; i < BINGO_CARDS_WIDTH; i++) {
				if (bingoNumbersOut [i] [j] == true && bingoCards [i] [j] != 0) {
					count++;
				}
			}

			if (count > 15) {
				//TODO It should not be possible.
			} else if (count == 15) {
				bingoCardIndex = j / 3;
				return (true);
			}
		}

		return (false);
	}

	/**
	* Bingo bonus win.
	*
	* @return Amount of the win according discrete distribution.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 27 Feb 2015
	*/
	private int bingoWin () {
		return (bingoBonusDistribution [Util.prng.Next (BINGO_BONUS_DISTRIBUTION_LENGTH)]);
	}

	/**
	* Simulation constructor.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 27 Feb 2015
	*/
	public SlotMachineSimulation () {
		totalBet = lines.Length;
	}

	public long getWonMoney () {
		return wonMoney;
	}

	public long getLostMoney () {
		return lostMoney;
	}

	public void load (int[][]reels) {
		this.reels = reels;
//		for (int i = 0; i < reels.Length; i++) {
//			for (int j = 0; j < reels [i].Length; j++) {
//				this.reels [i] [j] = reels [i] [j];
//			}
//		}
	}

	/**
	* Simulation variables reset.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 27 Feb 2015
	*/
	public void reset () {
	}

	/**
	* Play single base game.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 10 Mar 2013
	*/
	private void singleBaseGame () {
		/*
		* Spin reels.
		*/
		spin (reels);

		/*
		* Win accumulated by lines.
		*/
		int win = linesWin (view);

		/*
		* Add win to the statistics.
		*/
		baseMoney += win;
		wonMoney += win;
		if (baseMaxWin < win) {
			baseMaxWin = win;
		}

		/*
		* Count base game hit rate.
		*/
		if (win > 0) {
			baseGameHitRate++;
		}

		/*
		* Run bonus game.
		*/
		ballOut ();
		win = 0;

		/*
		* Check for bingo line bonus.
		*/
		if (bingoLine () == true) {
			win = bingoLineWin ();
			bonusGameHitRate++;
		}

		/*
		* Add win to the statistics.
		*/
		bonusMoney += win;
		wonMoney += win;

		/*
		* Check for bingo bonus.
		*/
		if (bingo () == true) {
			win = bingoWin ();
			bonusGameHitRate++;
		}

		/*
		* Add win to the statistics.
		*/
		bonusMoney += win;
		wonMoney += win;
	}

	public void runBaseGame () {
		if (bingoLineIndex != -1 && bingoCardIndex != -1) {
			generateRandomBingoCard ();
		}

		lostMoney += totalBet;
		singleBaseGame ();
		totalNumberOfGames++;
	}

	public void enforceSymbolsDiversity (int length) {
		int matches = 0;

		for (int i = 0; i < reels.Length; i++) {
			for (int j = 0; j < reels [i].Length; j++) {
				for (int k = 0; k < length - 1; k++) {
					for (int l = k + 1; l < length; l++) {
						if ((reels [i] [(j + k) % reels [i].Length]) == (reels [i] [(j + l) % reels [i].Length])) {
							reels [i] [(j + l) % reels [i].Length] = Symbols.randomValid ();
							matches++;
						}
					}
				}
			}
		}

		if (matches > 0) {
			enforceSymbolsDiversity (length);
		}
	}

	public double rtpDifference (double target) {
		return Math.Abs (target - (double)wonMoney / (double)lostMoney);
	}

	public double prizeDeviation () {
		int k = 0;
		double average = 0;
		for (int i = 0; i < baseSymbolMoney.Length; i++) {
			for (int j = 0; j < baseSymbolMoney [i].Length; j++) {
				if (baseSymbolMoney [i] [j] > 0) {
					average += baseSymbolMoney [i] [j] / (double)totalNumberOfGames;
					k++;
				}
			}
		}
		if (k > 0) {
			average /= k;
		}

		k = 0;
		double result = 0;
		for (int i = 0; i < baseSymbolMoney.Length; i++) {
			for (int j = 0; j < baseSymbolMoney [i].Length; j++) {
				if (baseSymbolMoney [i] [j] > 0) {
					result += (baseSymbolMoney [i] [j] / (double)totalNumberOfGames - average) * (baseSymbolMoney [i] [j] / (double)totalNumberOfGames - average);
					k++;
				}
			}
		}
		if (k > 0) {
			result /= k;
		}

		return result;
	}

	public double symbolsDiversity (int length) {
		int count = 0;
		int matches = 0;

		for (int i = 0; i < reels.Length; i++) {
			for (int j = 0; j < reels [i].Length; j++) {
				for (int k = 0; k < length - 1; k++) {
					for (int l = k + 1; l < length; l++) {
						if ((reels [i] [(j + k) % reels [i].Length]) == (reels [i] [(j + l) % reels [i].Length])) {
							matches++;
						}
						count++;
					}
				}
			}
		}

		return (double)matches / (double)count;
	}

	public double costFunction (double target, int length) {
		return symbolsDiversity (length) * 1 + rtpDifference (target) * 100 + prizeDeviation () * 10;
	}

	public void simulate (int length) {
		if (Util.STRICT_SYMBOLS_DIVERSITY == true) {
			enforceSymbolsDiversity (length);
		}

		for (int r = 0; r < NUMBER_OF_EXPERIMENTS; r++) {
			for (long e = 0; e < NUMBER_OF_SIMUALTIONS; e++) {
				runBaseGame ();
			}
		}
	}

	/**
	* Print all simulation input data structures.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 10 Mar 2013
	*/
	public String printDataStructures () {
		String result = "";

		result += "Paytable:";
		result += "\r\n";
		for (int i = 0; i < Paytable.values.Length; i++) {
			result += "\t" + i + " of";
		}
		result += "\r\n";
		for (int j = 0; j < Paytable.values [0].Length; j++) {
			result += "SYM" + j + "\t";
			for (int i = 0; i < Paytable.values.Length; i++) {
				result += Paytable.values [i] [j] + "\t";
			}
			result += "\r\n";
		}
		result += "\r\n";

		result += "Lines:";
		result += "\r\n";
		for (int i = 0; i < lines.Length; i++) {
			for (int j = 0; j < lines [0].Length; j++) {
				result += lines [i] [j] + " ";
			}
			result += "\r\n";
		}
		result += "\r\n";

		result += "Base Game Reels:";
		result += "\r\n";
		for (int i = 0; i < reels.Length; i++) {
			for (int j = 0; j < reels [i].Length; j++) {
				if (j % 10 == 0) {
					result += "\r\n";
				}
				result += "SYM" + reels [i] [j] + " ";
			}
			result += "\r\n";
		}
		result += "\r\n";

		result += "Base Game Reels:";
		result += "\r\n";
		/* Count symbols in reels. */
		{
			int[][] counters = {
				new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};
			for (int i = 0; i < reels.Length; i++) {
				for (int j = 0; j < reels [i].Length; j++) {
					counters [i] [reels [i] [j]]++;
				}
			}
			for (int i = 0; i < reels.Length; i++) {
				result += "\tReel " + (i + 1);
			}
			result += "\r\n";
			for (int j = 0; j < counters [0].Length; j++) {
				result += "SYM" + j + "\t";
				for (int i = 0; i < counters.Length; i++) {
					result += counters [i] [j] + "\t";
				}
				result += "\r\n";
			}
			result += "---------------------------------------------";
			result += "\r\n";
			result += "Total:\t";
			long combinations = 1L;
			for (int i = 0; i < counters.Length; i++) {
				int sum = 0;
				for (int j = 0; j < counters [0].Length; j++) {
					sum += counters [i] [j];
				}
				result += sum + "\t";
				if (sum != 0) {
					combinations *= sum;
				}
			}
			result += "\r\n";
			result += "---------------------------------------------";
			result += "\r\n";
			result += "Combinations:\t" + combinations;
			result += "\r\n";
		}
		result += "\r\n";

		//TODO Print bing bonus data structures.

		return result;
	}

	/**
	* Print simulation statistics.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 10 Mar 2013
	*/
	public String printStatistics () {
		String result = "";

		result += ("Won money:\t" + wonMoney);
		result += "\r\n";
		result += ("Lost money:\t" + lostMoney);
		result += "\r\n";
		result += ("Total Number of Games:\t" + totalNumberOfGames);
		result += "\r\n";
		result += "\r\n";
		result += ("Total RTP:\t" + ((double)wonMoney / (double)lostMoney) + "\t\t" + (100.0D * (double)wonMoney / (double)lostMoney) + "%");
		result += "\r\n";
		result += ("Base Game RTP:\t" + ((double)baseMoney / (double)lostMoney) + "\t\t" + (100.0D * (double)baseMoney / (double)lostMoney) + "%");
		result += "\r\n";
		result += ("Bonus Game RTP:\t" + ((double)bonusMoney / (double)lostMoney) + "\t\t" + (100.0D * (double)bonusMoney / (double)lostMoney) + "%");
		result += "\r\n";
		result += "\r\n";
		result += ("Hit Frequency in Base Game:\t" + ((double)baseGameHitRate / (double)totalNumberOfGames) + "\t\t" + (100.0D * (double)baseGameHitRate / (double)totalNumberOfGames) + "%");
		result += "\r\n";
		result += ("Hit Frequency in Bonus Game:\t" + ((double)bonusGameHitRate / (double)totalNumberOfGames) + "\t\t" + (100.0D * (double)bonusGameHitRate / (double)totalNumberOfGames) + "%");
		result += "\r\n";
		result += "\r\n";
		result += ("Max Win in Base Game:\t" + baseMaxWin);
		result += "\r\n";

		result += "\r\n";
		result += ("Base Game Symbols RTP:");
		result += "\r\n";
		result += ("\t");
		for (int i = 0; i < baseSymbolMoney.Length; i++) {
			result += ("" + i + "of\t");
		}
		result += "\r\n";
		for (int j = 0; j < baseSymbolMoney [0].Length; j++) {
			result += ("SYM" + j + "\t");
			for (int i = 0; i < baseSymbolMoney.Length; i++) {
				result += ((double)baseSymbolMoney [i] [j] / (double)lostMoney + "\t");
			}
			result += "\r\n";
		}
		result += "\r\n";
		result += ("Base Game Symbols Hit Frequency:");
		result += "\r\n";
		result += ("\t");
		for (int i = 0; i < baseGameSymbolsHitRate.Length; i++) {
			result += ("" + i + "of\t");
		}
		result += "\r\n";
		for (int j = 0; j < baseGameSymbolsHitRate [0].Length; j++) {
			result += ("SYM" + j + "\t");
			for (int i = 0; i < baseGameSymbolsHitRate.Length; i++) {
				result += ((double)baseGameSymbolsHitRate [i] [j] / (double)totalNumberOfGames + "\t");
			}
			result += "\r\n";
		}

		return result;
	}

	/**
	* Print screen view.
	*
	* @author Todor Balabanov
	*
	* @email tdb@tbsoft.eu
	*
	* @date 10 Mar 2013
	*/
	public String printView () {
		String result = "";

		int max = view [0].Length;
		for (int i = 0; i < view.Length; i++) {
			if (max < view [i].Length) {
				max = view [i].Length;
			}
		}

		for (int j = 0; j < max; j++) {
			for (int i = 0; i < view.Length && j < view [i].Length; i++) {
				if (view [i] [j] < 10 && view [i] [j] >= 0) {
					result += (" ");
				}
				result += (view [i] [j] + " ");
			}

			result += "\r\n";
		}

		return result;
	}

	public override string ToString () {
		return printDataStructures () + printStatistics ();
	}
}
}

