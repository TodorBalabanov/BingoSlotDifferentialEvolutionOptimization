using System;

namespace BingoSlotDifferentialEvolutionOptimization
{
	public class DiscreteDifferentialEvolution
	{
		private const int POPULATION_SIZE = 17;

		private const int NUMBER_OF_REELS = 5;

		private const int REEL_LENGTH = 63;

		private double[] fitness = new double[ POPULATION_SIZE ];

		private int[,,] population = new int[POPULATION_SIZE, NUMBER_OF_REELS, REEL_LENGTH];

		private int[,] difference = new int[NUMBER_OF_REELS, REEL_LENGTH];

		private int targetIndex = -1;

		private int baseIndex = -1;

		private int aIndex = -1;

		private int bIndex = -1;

		public void select ()
		{
			do {
				targetIndex = Util.prng.Next (population.GetLength (0));
				baseIndex = Util.prng.Next (population.GetLength (0));
				aIndex = Util.prng.Next (population.GetLength (0));
				bIndex = Util.prng.Next (population.GetLength (0));
			} while(targetIndex == baseIndex || targetIndex == aIndex || targetIndex == bIndex || baseIndex == aIndex || baseIndex == bIndex || aIndex == bIndex);
		}

		public void differs ()
		{
			int min = int.MaxValue;
			int max = int.MinValue;
			for (int i = 0; i < difference.GetLength (0); i++) {
				for (int j = 0; j < difference.GetLength (1); j++) {
					difference [i, j] = population [aIndex, i, j] - population [bIndex, i, j];
					if (min > difference [i, j]) {
						min = difference [i, j];
					}
					if (max < difference [i, j]) {
						max = difference [i, j];
					}
				}
			}

			/*
			* Normalize.
			*/
			for (int i = 0; i < difference.GetLength (0); i++) {
				for (int j = 0; j < difference.GetLength (1); j++) {
					difference [i, j] -= min; 
					if (min < max) {
						difference [i, j] = 3 * difference [i, j] / (max - min + 1) - 1; 
					}
				}
			}
		}

		public void mutate ()
		{
			for (int i = 0; i < difference.GetLength (0); i++) {
				for (int j = 0; j < difference.GetLength (1); j++) {
					difference [i, j] += population [baseIndex, i, j];
				}
			}

			//TODO Validate reels.
		}

		public DiscreteDifferentialEvolution (int[][] reels)
		{
			if (reels.Length != NUMBER_OF_REELS) {
				Console.WriteLine ("Number of reals is incorrect!");
				return;
			}

			for (int i = 0; i < reels.Length; i++) {
				if (reels [i].Length != REEL_LENGTH) {
					Console.WriteLine ("Reel length is incorrect!");
					return;
				}
			}

			for (int p = 0; p < population.GetLength (0); p++) {
				for (int i = 0; i < reels.Length; i++) {
					for (int j = 0; j < reels [i].Length; j++) {
						population [p, i, j] = reels [i] [j];
					}
				}
			}

			/*
			* Move around, but keep the first unchanged.
			*/
			for (int p = 1; p < population.GetLength (0); p++) {
				for (int i = 0; i < reels.Length; i++) {
					for (int j = 0; j < reels [i].Length; j++) {
						int q = Util.prng.Next (reels [i].Length);
						int swap = population [p, i, q];
						population [p, i, q] = population [p, i, j];
						population [p, i, j] = swap;
					}
				}
			}

		}
	}
}

