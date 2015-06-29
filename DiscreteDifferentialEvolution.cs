using System;
using System.Diagnostics;
using System.Globalization;

namespace BingoSlotDifferentialEvolutionOptimization {
class DiscreteDifferentialEvolution {
	public const long NUMBER_OF_RECOMBINATIONS = 100;

	public const int POPULATION_SIZE = 17;

	public const int NUMBER_OF_REELS = 5;

	public const int REEL_LENGTH = 63;

	private double targetRtp;

	private int symbolsDiversity;

	private double[] fitness = new double[ POPULATION_SIZE ];

	private int[][][] population = new int[POPULATION_SIZE][][];

	private int[][] offspring = null;

	private int targetIndex = -1;

	private int baseIndex = -1;

	private int aIndex = -1;

	private int bIndex = -1;

	private int bestIndex = -1;

	public int[][] best() {
		return population[bestIndex];
	}

	public void select () {
		do {
			targetIndex = Util.prng.Next (population.Length);
			baseIndex = Util.prng.Next (population.Length);
			aIndex = Util.prng.Next (population.Length);
			bIndex = Util.prng.Next (population.Length);
		} while(targetIndex == baseIndex || targetIndex == aIndex || targetIndex == bIndex || baseIndex == aIndex || baseIndex == bIndex || aIndex == bIndex);
	}

	public void differs () {
		int min = int.MaxValue;
		int max = int.MinValue;
		for (int i = 0; i < offspring.Length; i++) {
			for (int j = 0; j < offspring [i].Length; j++) {
				offspring [i] [j] = population [aIndex] [i] [j] - population [bIndex] [i] [j];
				if (min > offspring [i] [j]) {
					min = offspring [i] [j];
				}
				if (max < offspring [i] [j]) {
					max = offspring [i] [j];
				}
			}
		}

		/*
		* Normalize.
		*/
		for (int i = 0; i < offspring.Length; i++) {
			for (int j = 0; j < offspring [i].Length; j++) {
				offspring [i] [j] -= min;
				if (min < max) {
					offspring [i] [j] = 3 * offspring [i] [j] / (max - min + 1) - 1;
				}
			}
		}
	}

	public void mutate () {
		for (int i = 0; i < offspring.Length; i++) {
			for (int j = 0; j < offspring [i].Length; j++) {
				offspring [i] [j] += population [baseIndex] [i] [j];
			}
		}

		/*
		 * Validate reels.
		 */
		for (int i = 0; i < offspring.Length; i++) {
			for (int j = 0; j < offspring [i].Length; j++) {
				if (Symbols.isValid (offspring [i] [j]) == false) {
					offspring [i] [j] = Symbols.randomValid ();
				}
			}
		}
	}

	public void crossover () {
		for (int i = 0; i < offspring.Length; i++) {
			for (int j = 0; j < offspring [i].Length; j++) {
				if (Util.prng.NextDouble () < 0.5) {
					offspring [i] [j] = population [targetIndex] [i] [j];
				}
			}
		}
	}

	public void survive () {
		SlotMachineSimulation simulation = null;

		/*
		 * Re-evaluate target.
		 */
		if(Util.REEVALUATE_TARGET_VECTOR == true) {
			simulation = new SlotMachineSimulation ();
			simulation.load (population [targetIndex]);
			simulation.simulate (symbolsDiversity);
			fitness [targetIndex] = simulation.costFunction (targetRtp, symbolsDiversity);

			if (fitness [bestIndex] > fitness [targetIndex]) {
				bestIndex = targetIndex;
			}
		}

		/*
		 * Evaluate new solution.
		 */
		simulation = new SlotMachineSimulation ();
		simulation.load (offspring);
		simulation.simulate (symbolsDiversity);
		double cost = simulation.costFunction (targetRtp, symbolsDiversity);

		/*
		 * If better solution is not found - exit.
		 */
		if (cost >= fitness [targetIndex]) {
			return;
		}

		fitness [targetIndex] = cost;

		for (int i = 0; i < offspring.Length; i++) {
			for (int j = 0; j < offspring [i].Length; j++) {
				population [targetIndex] [i] [j] = offspring [i] [j];
			}
		}

		if (fitness [bestIndex] > fitness [targetIndex]) {
			bestIndex = targetIndex;
		}
	}

	public void optimize () {
		for (int r = 0; r < NUMBER_OF_RECOMBINATIONS; r++) {
			Stopwatch watch = Stopwatch.StartNew ();
			select ();
			differs ();
			mutate ();
			crossover ();
			survive ();
			//TODO Remove absolutely identical individuals from the population.
			watch.Stop ();

			if (Util.VERBOSE == true) {
				Console.WriteLine (targetRtp);
				CultureInfo ci = new CultureInfo ("en-us");
				Console.WriteLine ("{0}:{1}:{2}", ((int)watch.Elapsed.TotalHours).ToString ("D2", ci), ((int)watch.Elapsed.TotalMinutes % 60).ToString ("D2", ci), ((int)watch.Elapsed.TotalSeconds % 60).ToString ("D2", ci));
				Console.WriteLine (this);
			}
		}
	}

	public void initialSinglePoint(int[][] reels) {
		for (int p = 0; p < population.Length; p++) {
			for (int i = 0; i < reels.Length; i++) {
				for (int j = 0; j < reels [i].Length; j++) {
					population [p] [i] [j] = reels [i] [j];
				}
			}
		}

		/*
		 * Move around, but keep the first unchanged.
		 */
		for (int p = 1; p < population.Length; p++) {
			for (int i = 0; i < reels.Length; i++) {
				for (int j = 0; j < reels [i].Length; j++) {
					int q = Util.prng.Next (reels [i].Length);
					int swap = population [p] [i] [q];
					population [p] [i] [q] = population [p] [i] [j];
					population [p] [i] [j] = swap;
				}
			}
		}
	}

	public void initialRandomPoints() {
		for (int p = 0; p < population.Length; p++) {
			for (int i = 0; i < population [p].Length; i++) {
				for (int j = 0; j < population [p] [i].Length; j++) {
					population [p] [i] [j] = Symbols.randomValid();
				}
			}
		}
	}

	public DiscreteDifferentialEvolution (int[][] reels, double targetRtp, int symbolsDiversity) {
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

		for (int p = 0; p < POPULATION_SIZE; p++) {
			population [p] = new int[NUMBER_OF_REELS] [];
			for (int i = 0; i < NUMBER_OF_REELS; i++) {
				population [p] [i] = new int[REEL_LENGTH];
			}
		}

		offspring = new int[NUMBER_OF_REELS] [];
		for (int i = 0; i < NUMBER_OF_REELS; i++) {
			offspring [i] = new int[REEL_LENGTH];
		}

		this.targetRtp = targetRtp;
		this.symbolsDiversity = symbolsDiversity;

		if(Util.RANDOM_INITIAL_REELS == true){
				initialRandomPoints();
		} else {
			initialSinglePoint(reels);
		}

		/*
		 * Just put a big number (bad fitness), when there is no evaluation.
		 */
		for (int p = 0; p < population.Length; p++) {
			fitness [p] = int.MaxValue;
		}

		/*
		 * Evaluate population fintess.
		 */
		bestIndex = 0;
		for (int p = 0; p < population.Length; p++) {
			/*
			 * It will be evaluated in the first surviver.
			 */
			if(Util.REEVALUATE_TARGET_VECTOR == true) {
				break;
			}
			
			SlotMachineSimulation simulation = new SlotMachineSimulation ();
			simulation.load (population [p]);
			simulation.simulate (symbolsDiversity);
			fitness [p] = simulation.costFunction (targetRtp, symbolsDiversity);
			if (fitness [bestIndex] > fitness [p]) {
				bestIndex = p;
			}
		}
	}

	public override string ToString () {
		string result = "";

		for (int p = 0; p < population.Length; p++) {
			if (p != bestIndex) {
				continue;
			}

			result += fitness [p];
			result += "\r\n";
			for (int i = 0; i < population [p].Length; i++) {
				result += "new int[] {";
				for (int j = 0; j < population [p] [i].Length; j++) {
					result += population [p] [i] [j];
					result += ",";
					//result += "\t";
				}
				result += "},";
				result += "\r\n";
			}
		}

		return result;
	}
}
}
