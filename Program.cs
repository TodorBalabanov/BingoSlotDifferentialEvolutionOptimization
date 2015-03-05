using System;

namespace BingoSlotDifferentialEvolutionOptimization
{
	class MainClass
	{
		private static long NUMBER_OF_EXPERIMENTS = 1;

		private static long NUMBER_OF_SIMUALTIONS = 1000000;

		public static void Main (string[] args)
		{
			int[][] reels = { new int[] {
					3, 4, 3,
					4, 9, 7, 5, 9, 11, 9, 4, 4, 8, 6, 5, 11, 11, 4, 11, 6, 5, 11, 4, 5,
					9, 5, 11, 4, 11, 8, 7, 4, 10, 6, 4, 6, 9, 5, 7, 7, 12, 11, 11, 6,
					6, 9, 11, 7, 12, 9, 12, 12, 4, 4, 12, 11, 7, 6, 11, 7, 9, 4, 5,
				},
				new int[] {
					3, 4, 3, 
					9, 4, 12, 5, 9, 9, 10, 7, 7, 5, 4, 11, 9, 11, 4, 5,
					11, 6, 11, 11, 7, 11, 11, 11, 5, 11, 7, 9, 4, 6, 10, 6, 11, 12,
					6, 11, 12, 4, 5, 7, 11, 4, 7, 11, 4, 4, 12, 9, 5, 6, 5, 4,
					4, 4, 8, 7, 11, 7, 9, 6,
				}, new int[] {
					3, 4, 3, 
					12, 7, 11, 5, 11,
					4, 6, 5, 5, 4, 9, 10, 5, 5, 9, 9, 7, 6, 9, 4, 5, 11, 7,
					9, 6, 6, 11, 9, 5, 4, 8, 4, 6, 11, 9, 12, 7, 5, 9, 5, 11,
					5, 12, 7, 7, 5, 8, 9, 11, 6, 11, 8, 5, 12, 9, 9, 4, 5, 6,
					11,
				}, new int[] {
					3, 4, 3, 
					11, 11, 11, 9, 6, 8, 11, 7, 9, 5, 4, 11, 11,
					12, 4, 7, 7, 7, 9, 5, 11, 5, 9, 12, 11, 12, 9, 8, 10, 9,
					11, 4, 4, 7, 9, 7, 4, 4, 7, 7, 4, 9, 11, 12, 5, 6, 4, 11,
					11, 6, 4, 4, 4, 9, 11, 8, 6, 9, 4, 12,
				}, new int[] {
					3, 4, 3, 
					11, 9, 11, 8, 6, 4, 5, 12, 11, 11, 4, 11, 5, 8, 5, 6, 4, 7, 7,
					9, 11, 11, 4, 9, 6, 9, 5, 9, 7, 5, 8, 7, 6, 9, 5, 10, 11,
					12, 12, 7, 12, 6, 11, 4, 11, 6, 4, 4, 7, 6, 11, 7, 6, 10,
					12, 6, 4, 10, 9, 4,
				},

			};

			for (int r = 0; r < NUMBER_OF_EXPERIMENTS; r++) {
				Simulation simulation = new Simulation ();
				simulation.loadReels (reels);

				for (long e = 0; e < NUMBER_OF_SIMUALTIONS; e++) {
					simulation.runBaseGame ();
				}

				Console.WriteLine (simulation);

				Console.WriteLine ("RTP difference from target: " + simulation.rtpDifference (0.9));
				Console.WriteLine ("Prize deviation MSE: " + simulation.prizeDeviation ());
				Console.WriteLine ("Symbols diversity: " + simulation.symbolsDiversity (3));
			}
		}
	}
}
