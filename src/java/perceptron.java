/*Jason Chalom 2016 Perceptron Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;

class perceptron
{
	public static void main (String[] args){
		System.out.print("COMS3007: Machine LEarning Perceptron\n");
		System.out.print("Jason Chalom 2016 @TRex22\n\n");

		perceptron_core.test();

	}

	/*while (stopping condition not satisfied)
    for (k = 0; k < N; k++)
        y = percept(W;X[k])
        for (i = 0; i < m + 1; i++)
            W[i] = W[i] + n * (T[k] - y) * X[k][i]*/


	public double[] rndWeights(int m, int low, int high){
		double weights[] = new double[m];
		for (int i=0; i<m; i++){ 
			double weight = Math.random() * (high - low) + low;		
			weights[i] = weight;
		}
		return weights;
	}

}