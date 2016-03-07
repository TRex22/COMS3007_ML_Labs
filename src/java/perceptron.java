/*Jason Chalom 2016 Perceptron Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;

/**
 * @author Jason Chalom
 * Readme and notes to follow:
 * The test function just runs tests to make sure things work. The debug property handles this.
 * runPerceptron is the main procedure that is executed
 */

class perceptron
{
	public static void main (String[] args){
		GetPropertyValues properties = perceptron_io.config();
		if (properties != null){
			System.out.print("COMS3007: Machine LEarning Perceptron\n");
			System.out.print("Jason Chalom 2016 @TRex22\n\n");

			if (properties.debug){
				System.out.print("Debug Mode is on. Tests Will now run.\n");
				test(properties);
			}
			else{
				//do the normal flow of the project
				runPerceptron(args, properties);
			}
		}
	}

	private static void runPerceptron(String[] args, GetPropertyValues properties){
		perceptron_core perceptron = new perceptron_core();
		if(args.length > 0) {
			String filePath = args[0];
			perceptron_input input = perceptron_io.getInput(filePath, properties);

			double[] W = rndWeights(input.getNoX(), -1, 1); //just get length of X
			System.out.println(Arrays.toString(W));
			perceptron_output output = perceptron.PerceptLearn(input.getX(), input.getT(), W, properties);

			displayOutput(output);
		}
		else{
			System.out.print("Error! Please specify the file as the first argument.");
		}
	}

	private static double[] rndWeights(int m, int low, int high){
		double weights[] = new double[m];
		for (int i=0; i<m; i++){ 
			double weight = Math.random() * (high - low) + low;		
			weights[i] = weight;
		}
		return weights;
	}

	//test scheme using data not in textfiles
	private static void test(GetPropertyValues properties){	
		perceptron_core perceptron = new perceptron_core();
		System.out.print("Testing rndWeights function:\n");
		double[] weightsTest = rndWeights(3, -1, 1);
		if (weightsTest.length == 3){
			double sum = 0.0;
			for (int i = 0; i < weightsTest.length; i++){
				sum += Math.abs(weightsTest[i]);
			}
			if (sum >= 0 && sum <= 3){
				System.out.print("rndWeights function: PASS\n");
			}
			else{
				System.out.print("rndWeights function: FAIL!!!!\n");
			}
		}
		else{
			System.out.print("rndWeights function: FAIL!!!!\n");
		}

		System.out.print("\nTesting internal percept function:\n");

		double[] W_test = {2, -1, 1}; 
		double[] X_test = {1.5, 2.5, -1};
		int test1 = perceptron.percept(W_test, X_test);
		String result = "test1 result: "+ test1;
		if (test1 == 0){
			result += " PASS \n";
		}
		else {
			result += " FAIL!!! \n";
		}
		System.out.print(result);

		W_test = new double[] {2, -3, 1}; 
		X_test = new double[] {3, 1, -1};
		int test2 = perceptron.percept(W_test, X_test);
		result = "test2 result: "+ test2;
		if (test2 == 1){
			result += " PASS \n";
		}
		else {
			result += " FAIL!!! \n\n";
		}
		System.out.print(result);

		System.out.print("\nTesting internal PerceptLearn function:\n");
		double[][] X = {{0, 0, -1}, {0, 1, -1}, {1, 0, -1}, {1, 1, -1}};
		int[] T = {1, 1, 1, 0};
		double[] W = rndWeights(X[0].length, -1, 1); //just get length of X

		perceptron_output output = perceptron.PerceptLearn(X, T, W, properties);
		//check y to T
		if (output.getY().length == T.length){
			boolean test = true;
			for (int i = 0; i < output.getY().length; i++){
				if (output.getY()[i] != T[i]){
					test = false;
				}
			}
			if(test){
				System.out.print("PerceptLearn function: PASS\n");
			}
			else{
				System.out.print("PerceptLearn function: T and y dont match FAIL!!!!\n");
			}
		}
		else{
			System.out.print("PerceptLearn function: T and y lengths dont match FAIL!!!!\n");
		}

		displayOutput(output);
		
	}

	private static void displayOutput(perceptron_output output){
		System.out.print("Final Weights: ");
		for (int i = 0; i < output.getWeights().length; i++){	
			System.out.print(" "+output.getWeights()[i]);
		}
		System.out.print("\nFinal Output values (y): ");
		for (int i = 0; i < output.getY().length; i++){	
			System.out.print(" "+output.getY()[i]);
		}
		System.out.print("\nFinal Count: "+output.getCount());
		System.out.print("\n");
	}
}