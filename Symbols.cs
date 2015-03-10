using System;

namespace BingoSlotDifferentialEvolutionOptimization
{
	class Symbols
	{
		/**
		 * List of symbols names.
		 */
		public static String[] names = {
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

		public static bool isValid(int symbol) {
			if (symbol < 0 || symbol >= names.Length) {
				return false;
			}

			if(names[symbol].Equals("") == true) {
				return false;
			}

			return true;
		}

		public static int randomValid() {
			int symbol = -1;

			do {
				symbol = Util.prng.Next(names.Length);
			}while(names[symbol].Equals("") == true);

			return symbol;
		}
	}
}
