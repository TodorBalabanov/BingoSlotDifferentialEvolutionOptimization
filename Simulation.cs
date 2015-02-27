using System;

namespace BingoSlotDifferentialEvolutionOptimization
{
	public class Simulation
	{
		
		/**
        * Pseudo-random number generator.
        */
		private static Random prng = new Random ();

		/*
		* Used in bingo cards generation for better suffling.
		*/
		private static int NUMBER_OF_SHAKES = 30;

		private static int BINGO_CARDS_TALONS = 6;

		private static int BINGO_CARDS_WIDTH = 9;

		private static int BINGO_CARDS_LENGTH = 18;

		private static int BINGO_BONUS_DISTRIBUTION_LENGTH = 63;

		/**
		 * Bingo line bonus distribution.
		 */
		private int[] bingoLineBonusDistribution = { 10, 15, 20,
			10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20,
			10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20,
			10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20, 10, 15, 20,
			10, 15, 20, 10, 15, 20
		};

		/**
		 * Bingo line bonus distribution.
		 */
		private int[] bingoBonusDistribution = { 90, 100, 110,
			90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110,
			90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110,
			90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110,
			90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110, 90, 100, 110
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
		private int[][] bingoCards = { new int[] {
				1, 2, 3, 4, 5, 6, 7,
				8, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0
			}, new int[] {
				10, 11, 12, 13, 14, 15, 16, 17, 18,
				19, 0, 0, 0, 0, 0, 0, 0, 0
			}, new int[] {
				20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
				0, 0, 0, 0, 0, 0, 0, 0
			}, new int[] {
				30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 0,
				0, 0, 0, 0, 0, 0, 0
			}, new int[] {
				40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 0, 0,
				0, 0, 0, 0, 0, 0
			}, new int[] {
				50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 0, 0, 0,
				0, 0, 0, 0, 0
			}, new int[] {
				60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 0, 0, 0, 0,
				0, 0, 0, 0
			}, new int[] {
				70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 0, 0, 0, 0, 0,
				0, 0, 0
			}, new int[] {
				80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 0, 0, 0, 0, 0,
				0, 0
			}
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
		private int[] numbersInRow = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0
		};

		/**
        * Slot game paytable.
        */
		private int[][] paytable = {
			new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
			new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
			new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
			new int[]{ 0, 0, 0, 75, 50, 20, 15, 9, 7, 5, 3, 2, 1, 0, 0, 0, 0 },
			new int[]{ 0, 0, 0, 150, 100, 75, 50, 35, 20, 15, 9, 7, 3, 0, 0, 0, 0 }, 
			new int[]{ 0, 0, 0, 300, 250, 200, 150, 100, 75, 50, 30, 20, 10, 0, 0, 0, 0 },
		};

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
	     * List of symbols names.
	     */
		private String[] symbols = {
			"",
			"",
			"",
			"SYM03",
			"SYM04",
			"SYM05",
			"SYM06",
			"SYM07",
			"SYM08",
			"SYM09",
			"SYM10",
			"SYM11",
			"SYM12",
			"",
			"",
			"",
			"",
		};

		/**
	     * Lines combinations.
	     */
		private int[][] reels = {
			new int[] {
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
			},
			new int[] {
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
			},
			new int[] {
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
			},
			new int[] {
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
			},
			new int[] {
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
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
		private static long[][] baseSymbolMoney = {
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
		private static long[][] baseGameSymbolsHitRate = {
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
		private bool fixThreeRows ()
		{
			bool wasItChanged = false;

			for (int i = 0; i < 9; i++) {
				int a = -1;
				int b = -1;

				for (int j = 0; j < 18; j += 3) {
					if (0
					    == (bingoCards [i] [j + 0] != 0 ? 1 : 0)
					    + (bingoCards [i] [j + 1] != 0 ? 1 : 0)
					    + (bingoCards [i] [j + 2] != 0 ? 1 : 0)) {
						a = j + prng.Next (3);
					}
					if (3
					    == (bingoCards [i] [j + 0] != 0 ? 1 : 0)
					    + (bingoCards [i] [j + 1] != 0 ? 1 : 0)
					    + (bingoCards [i] [j + 2] != 0 ? 1 : 0)) {
						b = j + prng.Next (3);
					}
				}

				if (a == -1 && b == -1) {
					continue;
				}
				if (a == -1) {
					do {
						a = prng.Next (18);
					} while (bingoCards [i] [a] != 0);
				}
				if (b == -1) {
					do {
						b = prng.Next (18);
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
		private bool fixRows ()
		{
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
		private void shuffleBingoCards ()
		{
			for (int i = 0; i < BINGO_CARDS_WIDTH; i++) {
				for (int last = BINGO_CARDS_LENGTH - 1, r = -1, swap = -1; last > 0;
						last--) {
					r = prng.Next (last + 1);
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
		private void generateRandomBingoCard ()
		{
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
		private void spin (int[][] reels)
		{
			for (int i = 0; i < view.Length && i < reels.Length; i++) {
				int r = prng.Next (reels [i].Length);
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
		private int lineWin (int[] line)
		{
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
               
			int win = paytable [number] [symbol];
               
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
		private int linesWin (int[][] view)
		{
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
		private int ballOut ()
		{
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
				i = prng.Next (BINGO_CARDS_WIDTH);
				j = prng.Next (BINGO_CARDS_LENGTH);
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
		private bool bingoLine ()
		{
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
		private int bingoLineWin ()
		{
			return (bingoLineBonusDistribution [prng.Next (BINGO_BONUS_DISTRIBUTION_LENGTH)]);
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
		private bool bingo ()
		{
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
		private int bingoWin ()
		{
			return (bingoBonusDistribution [prng.Next (BINGO_BONUS_DISTRIBUTION_LENGTH)]);
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
		public Simulation ()
		{
			totalBet = lines.Length;
		}

		public long getWonMoney ()
		{
			return wonMoney;
		}

		public long getLostMoney ()
		{
			return lostMoney;
		}

		public void loadReels (int[][]reels)
		{
			for (int i = 0; i < reels.Length; i++) {
				for (int j = 0; j < reels [i].Length; j++) {
					this.reels [i] [j] = reels [i] [j];
				}
			}
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
		public void reset ()
		{
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
		private void singleBaseGame ()
		{
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

		public void runBaseGame ()
		{
			if (bingoLineIndex != -1 && bingoCardIndex != -1) {
				generateRandomBingoCard ();
			}

			lostMoney += totalBet;
			singleBaseGame ();
		}
	}
}

