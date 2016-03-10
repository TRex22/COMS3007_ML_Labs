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
 
 class perceptron
{
	private static int MaxCount = 100;
	private static boolean DisableErrorTermination = false;
	private static double beta = 0.5;
	private static double learningRate = 0.25;
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
		int noOutputs = getNoOutputs(T);//TODO fix
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
			//inherint rnd of inputs but bad ;)
			input = readFile(args[0]);
			X = input.getX();
			T = input.getT();

			//for each datapoint find hidden nodes
			for (int i = 0; i < noHiddenNodes; i++){
				hNodes[i] = percept(X[0], W[i]); //super hack since X is always random
			}

			//apply activation fn for each hidden node 1/1+e^(beta*h) => new set of hidden nodes
			for (int i = 0; i < noHiddenNodes; i++){
				hNodes[i] = 1 / 1+Math.pow(Math.E, beta*hNodes[i]);
			}

			//for each hidden node find output nodes
			for (int i = 0; i < noOutputs; i++){
				y[i] = percept(hNodes, U[i]);
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
		System.out.print("\nComplete!\n\nNumber Hidden Nodes: "+noHNodes);
		System.out.print("\n\nW: "+Arrays.toString(W)+"\n\n");
		System.out.print("\nW (hidden weights) Please note that the array is swapped around:\n\n");
		
	/*
		for (int i = 0; i < W.length; i++){
			System.out.print("W["+i+"]:");
			for (int j = 0; j < W[i].length; i++){
				System.out.print(" "+W[i][j]+" ");
			}
		}
*/
		System.out.print("\nU (output weights) Please note that the array is swapped around:\n\n");
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
		        Scanner scLine = new Scanner(line).useDelimiter(" ");
				List<Double> X_line = new ArrayList<>();
				
				while(scLine.hasNext()){
					noX = 0;
					String element = scLine.next();
					//System.out.println("element: " + element); 
					
					if(isInt(element)){
						noT++;
						T_al.add(Integer.parseInt(element));
					}
					else if(isDouble(element)){
						noX++;
						X_line.add(Double.parseDouble(element));
					}					
				}
				X_al.add(X_line);
		    }   

		    bufferedReader.close();         
		}
		catch(FileNotFoundException ex) {
		    System.out.println("Unable to open file '" + filePath + "'");                
		}
		catch(IOException ex) {
		    System.out.println("Error reading file '" + filePath + "'");                  
		}
		System.out.println("noLines: " + noLines + " noX: "+noX+" noT:"+noT); 
		
		return rndInputs(X_al, T_al, noLines, noX, noT);	
	}

	private static boolean isInt (String str){
		try{
			Integer.parseInt(str);
			return true;
		}
		catch (Exception e){
			return false;
		}
	}

	private static boolean isDouble (String str){
		try{
			Double.parseDouble(str);
			return true;
		}
		catch (Exception e){
			return false;
		}
	}

	private static perceptron_input rndInputs(List<List<Double>> X_al, List<Integer> T_al, int X_Rows, int X_Cols, int noT){
		//randomize inputs using PRNG trick
		long seed = System.nanoTime();
		Collections.shuffle(X_al, new Random(seed));
		Collections.shuffle(T_al, new Random(seed));
		
		double[][] X = new double[X_Rows][X_Cols];
		for (int i = 0; i < X_Rows; i++){
			for (int j = 0; j < X_Cols; j++){
				X[i][j] = X_al.get(i).get(j);
			}
		}
		
		int[] T = new int[noT];
		for (int i = 0; i < noT; i++){
			T[i] = T_al.get(i);
		}
		
		perceptron_input input = new perceptron_input();
		input.setInput(X, T, X_Rows, X_Cols, noT);

		return input;
	}

	private static <T> void printArrayList (List<T> list){
		String str = "";

		for (T t : list)
		{
		    str += t + "\t";
		}

		System.out.println(str);
	}

	private static <T> void printArrayList2D (List<List<T>> list){
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
	
	private static int percept(double X[], double weights[]){
		//if w1X1+w2X2...-$ >0 return 1
	    	//if W1X1+W2X2...-$ <=0 return 0
	    	//using augemented matrix

		double sum = 0.0;

		for (int i = 0; i < X.length; i++){
			sum += X[i] * weights[i];
		}		

		if (sum > 0.0){
			return 1;
		}
		else if (sum <= 0.0){
			return 0;
		}

		System.out.print("Error: Failure In Neuron");
		return 3;		
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
}