/*Jason Chalom 2016 Perceptron Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;

/**
 * @author Jason Chalom
 * 
 */

class perceptron
{
	public static void main (String[] args){
		perceptron_io.config();
		System.out.print("COMS3007: Machine LEarning Perceptron\n");
		System.out.print("Jason Chalom 2016 @TRex22\n\n");

		test();
	}

	public static double[] rndWeights(int m, int low, int high){
		double weights[] = new double[m];
		for (int i=0; i<m; i++){ 
			double weight = Math.random() * (high - low) + low;		
			weights[i] = weight;
		}
		return weights;
	}

	//test scheme using data not in textfiles
	public static void test(){
		GetPropertyValues properties = perceptron_io.config();
		
		if (properties != null){
			double[][] X = {{0, 0, -1}, {0, 1, -1}, {1, 0, -1}, {1, 1, -1}};
			double[] T = {1, 1, 1, 0};
			double[] W = rndWeights(X[0].length, -1, 1); //just get length of X

			double[] Wfinal = perceptron_core.PerceptLearn(X, T, W, properties);
		}
	}

}