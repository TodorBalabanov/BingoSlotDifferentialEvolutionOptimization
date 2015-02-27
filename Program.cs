using System;

namespace BingoSlotDifferentialEvolutionOptimization
{
	class MainClass
	{
		private static long NUMBER_OF_SIMUALTIONS = 10000000L;

		public static void Main (string[] args)
		{
			int[][] reels = { new int[] {
					3, 9, 10,
					4, 9, 7, 5, 9, 3, 9, 4, 4, 8, 6, 5, 11, 3, 4, 3, 6, 5, 3, 4, 5,
					9, 5, 3, 4, 3, 8, 7, 4, 10, 6, 4, 6, 9, 5, 7, 7, 12, 3, 11, 6,
					6, 9, 3, 7, 12, 9, 12, 12, 4, 4, 12, 3, 7, 6, 3, 7, 9, 4, 5,
				},
				new int[] {
					10, 7, 12, 9, 4, 12, 5, 9, 9, 10, 7, 7, 5, 4, 3, 9, 3, 4, 5,
					3, 6, 11, 3, 7, 3, 3, 3, 5, 3, 7, 9, 4, 6, 10, 6, 3, 12,
					6, 3, 12, 4, 5, 7, 3, 4, 7, 3, 4, 4, 12, 9, 5, 6, 5, 4,
					4, 4, 8, 7, 11, 7, 9, 6,
				}, new int[] {
					3, 4, 11, 12, 7, 3, 5, 11,
					4, 6, 5, 5, 4, 9, 10, 5, 5, 9, 9, 7, 6, 9, 4, 5, 3, 7,
					9, 6, 6, 3, 9, 5, 4, 8, 4, 6, 3, 9, 12, 7, 5, 9, 5, 3,
					5, 12, 7, 7, 5, 8, 9, 11, 6, 3, 8, 5, 12, 9, 9, 4, 5, 6,
					3,
				}, new int[] {
					4, 3, 3, 3, 3, 3, 9, 6, 8, 3, 7, 9, 5, 4, 3, 3,
					12, 4, 7, 7, 7, 9, 5, 11, 5, 9, 12, 11, 12, 9, 8, 10, 9,
					3, 4, 4, 7, 9, 7, 4, 4, 7, 7, 4, 9, 3, 12, 5, 6, 4, 11,
					11, 6, 4, 4, 4, 9, 3, 8, 6, 9, 4, 12,
				}, new int[] {
					3, 9, 4, 11,
					9, 3, 8, 6, 4, 5, 12, 3, 3, 4, 3, 5, 8, 5, 6, 4, 7, 7,
					9, 3, 3, 4, 9, 6, 9, 5, 9, 7, 5, 8, 7, 6, 9, 5, 10, 11,
					12, 12, 7, 12, 6, 3, 4, 3, 6, 4, 4, 7, 6, 3, 7, 6, 10,
					12, 6, 4, 10, 9, 4,
				},

			};

			for (int r = 0; r < 10; r++) {
				Simulation simulation = new Simulation ();
				simulation.loadReels (reels);

				for (long e = 0; e < NUMBER_OF_SIMUALTIONS; e++) {
					simulation.runBaseGame ();
				} 

				Console.WriteLine ((double)simulation.getWonMoney () / (double)simulation.getLostMoney () * 100D);
			}
		}
	}
}
