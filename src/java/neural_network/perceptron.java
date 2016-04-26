/*Jason Chalom 2016 Perceptron Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;
import java.util.Arrays;

/**
 * @author Jason Chalom
 * There are two files!!!!
 *
 * ALgorith back propogation:
 * While(stopping condition holds)
 *	reorder inputs randomly
 *	for each datapoint
 *		feed x into network to get y
 *		calc delta0 delta h
 *		update all Ukj and the all Wik
 *		calc error on training set and then on verification set
 */
 
public class perceptron
{
	//settings probably should be done properly but seems to work for this purpose
	private static int MaxCount = 100;
	private static boolean DisableErrorTermination = false;
	private static double beta = 0.5;
	private static double learningRate = 0.25;

	//some global counters
	private static double error = 0.0;
	private static int noError = 0;		
	
	public static void main (String[] args){
		System.out.print("COMS3007: Machine LEarning Perceptron\n");
		System.out.print("Jason Chalom 2016 @TRex22\n\n");
		
		//get input and randomise it
		perceptron_input input = readFile(args[0]);
		double[][] X = input.getX();
		int[] T = input.getT();
		
		//no hidden nodes
		int noHiddenNodes = input.getNoX();
		int noOutputs = getNoOutputs(T);
		int noLines = input.getNoLines();
		int noX = input.getNoX();	
			
		//setup network
		double[][] W = new double[noHiddenNodes][noX];
		for (int i = 0; i < noHiddenNodes; i++){
			W[i] = rndWeights(noX, -1, 1);
		}
		double[][] U = new double[noOutputs][noHiddenNodes];
		for (int i = 0; i < noOutputs; i++){
			U[i] = rndWeights(noHiddenNodes, -1, 1);
		}
		
		//while(stopping condition holds)
		double[] y = new double[noOutputs];
		double[] hNodes = new double[noHiddenNodes];
    	double[] yErr = new double[noOutputs];
		double[] hErr = new double[noHiddenNodes];
		int count = 0;
		boolean continue_ = findTermination(y, T, count, MaxCount, DisableErrorTermination);

		while(continue_){
			input = rndInputs(X, T, noLines, noX, T.length);
			X = input.getX();
			T = input.getT();

			//for each datapoint find hidden nodes
			for (int i = 0; i < noHiddenNodes; i++){
				hNodes[i] = percept(X[0], W[i]); //super hack since X is always random

				//apply activation fn for each hidden node 1/1+e^(beta*h) => new set of hidden nodes
				hNodes[i] = sigmoidFunction(hNodes[i]);
			}

			//for each hidden node find output nodes
			for (int i = 0; i < noOutputs; i++){
				y[i] = percept(hNodes, U[i]);

				//apply activation fn for each ouput node 1/1+e^(beta*h) => new set of ouput nodes
				y[i] = sigmoidFunction(y[i]);
			}

			//work out the deltas/errors
			//yErr
			for (int i = 0; i < noOutputs; i++){
				yErr[i] = y[i]*(1-y[i])*(T[i]-y[i]);
			}			
			
			//hErr A is the new hidden node values after activation fn
			for (int i = 0; i < noOutputs; i++){
				double sumOfWeights = 0.0;
				for (int j = 0; j < noHiddenNodes; j++){
					sumOfWeights += U[i][j] * yErr[i];
				}
				hErr[i] = hNodes[i]*(1-hNodes[i])*(sumOfWeights);
			}
			
			//Update U (final) weights first
			for (int i = 0; i < noOutputs; i++){
				for (int j = 0; j < noHiddenNodes; j++){
					U[i][j] = U[i][j] + (learningRate * yErr[i] * hNodes[j]);
				}
			}

			//Update W (hidden) weights
			for (int i = 0; i < noHiddenNodes; i++){
				for (int j = 0; j < noX; j++){
					W[i][j] = W[i][j] + ((learningRate * hErr[i]) * X[0][j]);
				}
			}
	
			//only if data is split - no time to do sorry :(
			//calc error on training set
			//calc error on verification set
			
			continue_ = findTermination(y, T, count, MaxCount, DisableErrorTermination);
			count++;
		}
		
		displayOutput (hNodes.length, W, U);		
	}

	public static void displayOutput (int noHNodes, double[][] W, double[][] U){
		System.out.print("Complete!\n\nNumber Hidden Nodes: "+noHNodes);
		System.out.print("\nW (hidden weights) Please note that the array is swapped around:\n");
		
	
		for (int i = 0; i < W.length; i++){
			System.out.print("W["+i+"]:");
			for (int j = 0; j < W[i].length; i++){
				System.out.print(" "+W[i][j]+" ");
			}
		}

		System.out.print("\n\nU (output weights) Please note that the array is swapped around:\n");
		for (int i = 0; i < U.length; i++){
			System.out.print("U["+i+"]:");
			for (int j = 0; j < U[i].length; i++){
				System.out.print(" "+U[i][j]+" ");
			}
		}

		System.out.print("\nError: "+error+" number of errors: "+noError+"\n");
	}

	public static int getNoOutputs(int[] T){
		List<Integer> testList = new ArrayList<>();
		for(int i = 0; i < T.length; i++){
			if (!testList.contains(T[i])){
				testList.add(T[i]);
			}
		}

		System.out.print("Number of Outputs Computed: " + testList.size() + "\n\n");
		return testList.size();
	}
	
	public static perceptron_input readFile(String filePath){
		int noLines = 0;
		int noX = 0;
		int noT = 0;
		List<List<Double>> X_al = new ArrayList<List<Double>>(2);
		List<Integer> T_al = new ArrayList<>();
		String line = null;

		try {
		    FileReader fileReader = new FileReader(filePath);
		    BufferedReader bufferedReader = new BufferedReader(fileReader);

		    while((line = bufferedReader.readLine()) != null) {
		    	//System.out.println(line);
		        noLines++;
		    	List<Double> X_line = new ArrayList<>();
		    	String[] row = line.split(" ");
		    	noX = 0;
		    	for (int i = 0; i < row.length; i++){
		    		String element = row[i];
		    		//System.out.println("element: " + element); 
	    			if(testDouble(element)){
						noX++;
						X_line.add(Double.parseDouble(element));
					}
					else if (!testDouble(element)){
						noT++;
						//System.out.println("element: " + element); 
						T_al.add(Integer.parseInt(element));
					}		
		    	}
		    	X_al.add(X_line);
		    }			
			bufferedReader.close();
			fileReader.close();
	    }   
		catch(FileNotFoundException ex) {
		    System.out.println("Unable to open file '" + filePath + "'");                
		}
		catch(IOException ex) {
		    System.out.println("Error reading file '" + filePath + "'");                  
		}
		
		double[][] X = new double[noLines][noX];
		for (int i = 0; i < noLines; i++){
			for (int j = 0; j < noX; j++){
				//System.out.println(X_al.get(i).get(j));
				X[i][j] = X_al.get(i).get(j);
			}
		}
		
		int[] T = new int[noT];
		for (int i = 0; i < noT; i++){
			T[i] = T_al.get(i);
		}

		perceptron_input input = new perceptron_input();
		input.setInput(X, T, noLines, noX, noT);

		return input;
	}

	private static boolean testDouble(String str){
		if (str.contains(".") || str.contains(",")){
			return true;
		}

		return false;
	}

	private static boolean isInt(String str){
		try{
			Integer.parseInt(str);
			return true;
		}
		catch (Exception e){
			return false;
		}
	}

	private static boolean isDouble(String str){
		try{
			Double.parseDouble(str);
			return true;
		}
		catch (Exception e){
			return false;
		}
	}

	private static void ShuffleIntArray(int[] array)
	{
	    int index, temp;
	    Random random = new Random();
	    for (int i = array.length - 1; i > 0; i--)
	    {
	        index = random.nextInt(i + 1);
	        temp = array[index];
	        array[index] = array[i];
	        array[i] = temp;
	    }
	}

	private static void ShuffleDoubleArray(double[] array)
	{
	    int index;
	    double temp;
	    Random random = new Random();
	    for (int i = array.length - 1; i > 0; i--)
	    {
	        index = random.nextInt(i + 1);
	        temp = array[index];
	        array[index] = array[i];
	        array[i] = temp;
	    }
	}

	/*private void ShuffleDoubleDoubleArray(double[][] array)
	{
	    int index
	    double[] temp;
	    Random random = new Random();
	    for (int i = array.length - 1; i > 0; i--)
	    {
	        index = random.nextInt(i + 1);
	        temp = array[index];
	        array[index] = array[i];
	        array[i] = temp;
	    }
	}*/

	/** Shuffles a 2D array with the same number of columns for each row. */
	public static void shuffleTwoDArray(double[][] matrix) 
	{
		int columns = matrix[0].length;
		Random rnd = new Random();
	    int size = matrix.length * columns;
	    for (int i = size; i > 1; i--)
	        swap(matrix, columns, i - 1, rnd.nextInt(i));
	}

	/** 
	 * Swaps two entries in a 2D array, where i and j are 1-dimensional indexes, looking at the 
	 * array from left to right and top to bottom.
	 */
	public static void swap(double[][] matrix, int columns, int i, int j) 
	{
	    double tmp = matrix[i / columns][i % columns];
	    matrix[i / columns][i % columns] = matrix[j / columns][j % columns];
	    matrix[j / columns][j % columns] = tmp;
	}

	private static perceptron_input rndInputs(double[][] X, int[] T, int X_Rows, int X_Cols, int noT){
		
		shuffleTwoDArray(X);
		ShuffleIntArray(T);
		
		perceptron_input input = new perceptron_input();
		input.setInput(X, T, X_Rows, X_Cols, noT);

		return input;
	}

	private static <T> void printArrayList(List<T> list){
		String str = "";

		for (T t : list)
		{
		    str += t + "\t";
		}

		System.out.println(str);
	}

	private static <T> void printArrayList2D(List<List<T>> list){
		String str = "";

		for (List<T> t : list)
		{
		    for (T r : t)
			{
			    str += r + "\t";
			}
		}

		System.out.println(str);
	}
	
	private static double percept(double X[], double weights[]){
		//if w1X1+w2X2...-$ >0 return 1
	    	//if W1X1+W2X2...-$ <=0 return 0
	    	//using augemented matrix

		double sum = 0.0;

		for (int i = 0; i < X.length; i++){
			sum += X[i] * weights[i];
		}

		//Okay so no need to descretize

		/*if (sum > 0.0){
			return 1;
		}
		else if (sum <= 0.0){
			return 0;
		}*/

		//System.out.print("Error: Failure In Neuron");
		return sum;		
	}
	
	//find the termination conditions
	private static boolean findTermination(double[] y, int[] T, int count, int MaxCount, boolean DisableErrorTermination){
		//if false terminate
	    //if true continue
	    if(count + 1 > MaxCount){
	    	return false;
	    }

	    if(y.length > 0 && !DisableErrorTermination){
	    	error = 0.0;
		    int errorCount = 1;
		    for (int i = 0; i < y.length; i++){
		    	error += Math.abs(T[i] - y[i]);
		    	if(error > 0){
		    		errorCount++;
		    	}
		    }

		    if (error == 0.0){//|| (errorCount/y.length) <= properties.MaxError
			noError = errorCount;
		    	return false;
		    }
	    }
	    return true;
	}


	private static double[] rndWeights(int m, int low, int high){
		double weights[] = new double[m];
		//List<Double> weights = new ArrayList<>();
		for (int i=0; i<m; i++){ 
			double weight = Math.random() * (high - low) + low;		
			weights[i] = weight;
		}
		return weights;
	}

	private static double sigmoidFunction(double input){
		double pow = -(beta)*(input);
		double output = 1 / 1+Math.pow(Math.E, pow);
		return output;
	}
}
