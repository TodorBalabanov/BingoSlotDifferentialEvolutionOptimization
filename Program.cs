
using System;

namespace BingoSlotDifferentialEvolutionOptimization {
class MainClass {
	public static void Main (string[] args) {
		int[][] reels = {
			new int[] { 7, 8, 7, 4, 3, 10, 3, 11, 10, 11, 4, 3, 11, 3, 6, 4, 8, 7, 4, 3, 12, 11, 8, 4, 8, 12, 11, 4, 4, 6, 7, 5, 7, 3, 3, 5, 6, 9, 5, 6, 9, 4, 12, 6, 7, 5, 12, 11, 10, 7, 8, 4, 4, 5, 10, 10, 5, 5, 6, 7, 9, 7, 9, },
			new int[] { 9, 5, 11, 10, 11, 12, 5, 8, 4, 4, 12, 8, 7, 6, 11, 4, 4, 6, 6, 12, 7, 6, 10, 8, 11, 9, 5, 11, 6, 12, 6, 3, 9, 11, 7, 3, 4, 6, 3, 7, 4, 5, 7, 12, 10, 6, 9, 4, 8, 3, 10, 4, 4, 7, 8, 7, 5, 12, 11, 3, 8, 5, 12, },
			new int[] { 10, 9, 3, 11, 6, 10, 6, 5, 3, 5, 11, 4, 9, 6, 10, 7, 4, 9, 12, 7, 12, 12, 9, 11, 11, 8, 9, 6, 7, 11, 7, 5, 11, 11, 3, 5, 4, 11, 6, 9, 6, 11, 5, 7, 3, 11, 10, 11, 8, 9, 10, 9, 4, 7, 5, 4, 3, 6, 8, 11, 7, 3, 5, },
			new int[] { 8, 4, 3, 7, 7, 11, 7, 5, 4, 11, 3, 11, 11, 5, 5, 9, 9, 10, 7, 6, 10, 12, 4, 10, 12, 10, 5, 7, 10, 8, 9, 12, 9, 7, 5, 3, 6, 7, 3, 8, 9, 6, 8, 11, 11, 4, 10, 11, 12, 12, 10, 6, 6, 12, 8, 12, 10, 5, 8, 7, 5, 4, 3, },
			new int[] { 5, 6, 5, 6, 11, 12, 4, 3, 7, 8, 3, 11, 5, 5, 8, 8, 9, 3, 7, 7, 11, 7, 10, 11, 11, 3, 11, 8, 7, 5, 7, 11, 4, 9, 6, 11, 11, 6, 9, 11, 5, 11, 7, 6, 8, 11, 12, 11, 8, 7, 5, 7, 3, 12, 12, 6, 8, 10, 5, 11, 5, 9, 4, },
		};

		DiscreteDifferentialEvolution dde = new DiscreteDifferentialEvolution (reels, 0.9, 3);
		dde.optimize ();
		Console.WriteLine (dde);

//			for (int r = 0; r < SlotMachineSimulation.NUMBER_OF_EXPERIMENTS; r++) {
//				SlotMachineSimulation simulation = new SlotMachineSimulation ();
//				simulation.load (reels);
//
//				for (long e = 0; e < SlotMachineSimulation.NUMBER_OF_SIMUALTIONS; e++) {
//					simulation.runBaseGame ();
//				}
//
//				Console.WriteLine (simulation);
//
//				Console.WriteLine ("RTP difference from target: " + simulation.rtpDifference (0.9));
//				Console.WriteLine ("Prize deviation MSE: " + simulation.prizeDeviation ());
//				Console.WriteLine ("Symbols diversity: " + simulation.symbolsDiversity (3));
//				Console.WriteLine ("Cost function: " + simulation.costFunction (0.9, 3));
//			}
	}
}
}
