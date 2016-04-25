/*Jason Chalom 2016 K_means offline Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;
import java.util.Arrays;

/**
 * @author Jason Chalom
 * 
 * m is collection mew of centers
 * Algorithm 1 K-means Algorithm (offline variant):
 * 
 * Given dataset normalise data
 * Choose k -> the no of clusters
 * 
 */
 
public class k_means_offline
{
    private static int MaxCount = 1000;
    private static double MaxError = 0.1;
    private static int MaxClusters = 25;
    private static int OverrideClusterNo = 0;

    private static double currentError = 0.0;
    
    public static void main(String[] args){
    	String filePath = args[0];
    	System.out.println("Clustering K-means offline, dataset: "+filePath);

        //read data into
        double[][] X = readFile(filePath);
        int noRows = X.length;
        int dataDim = X[0].length;
        //normalise
        X = normaliseDataForClustering(X);
        //find no K
        //TODO JMC fix at somepoint in the next decade
        int k = findNoK(X);
        //choose K cluster centres
        double[][] m = chooseKCluserCentres(k, X[0].length);
        double[][] oldM = m;
        
        //while loop and stopping condition
        int count = 0;
        
        //the index of each m associated to X, done below
        //first array is list of m's, second will be associated X_row indexes
        /*int[][] X_Assignments = new int[k][noRows];*/
        List<List<Integer>> X_Assignments = initXAssignments(m);

        boolean continue_ = findTermination(count, oldM, m);
        while(continue_){
        	oldM = m;

        	for(int i=0; i<X.length; i++){
        		int minClusterIndex = findMinClusterIndex(X[i], m);
        		X_Assignments.get(minClusterIndex).add(i); //add to the list of clusters to points
        	}

        	for (int i=0; i<m.length; i++){
        		double clusterMean = findClusterMean(X_Assignments, i);
        		m[i] = clusterMean;
        	}

        	continue_ = findTermination(count, oldM, m);
        	count++;
        }

        printOutput(X_Assignments, m, currentError, k, count);
    }

    private static void printOutput(List<List<Integer>> X_Assignments, double[] m, double error, int noK, int count){
    	System.out.println("Final Output: ");
    	System.out.println("Number of Clusters: "+noK);
    	System.out.println("Final Error: "+error+" Final Count: "+count);
    }

    private static double findClusterMean(double[][] X, List<List<Integer>> X_Assignments, int indexM){
    	double clusterMean = 0.0;
    	double sumOfAllMPoints = 0.0;

    	for (int i=0; i<X_Assignments.get(indexM).size(); i++){
    		sumOfAllMPoints += X_Assignments.get(indexM).get(i);
    	}

    	System.out.println(X_Assignments.get(indexM).size());
    	clusterMean = (1 / X_Assignments.get(indexM).size()) * sumOfAllMPoints;
    	return clusterMean;
    }

    private static int findMinClusterIndex(double[] X_row, double[] m){
    	double currentMin = euclideanDistance(X_row, m[0]);
    	int currentMinIndex = 0;
    	for (int i=1; i<m.length; i++){
    		if (m[i] < currentMin){
    			currentMin = euclideanDistance(X_row, m[i]);
    			currentMinIndex = i;
    		}
    	}
    	return currentMinIndex;
    }

    private static double euclideanDistance(double[] X_row, double m){
    	double distance = 0.0;
    	double squaredSum = 0.0;

    	for (int i=0; i<X_row.length; i++){
    		squaredSum += X_row[i] - m;
    	}

    	distance = Math.sqrt(squaredSum);

    	return distance;
    }

    private static List<List<Integer>> initXAssignments(double[] m){
    	List<List<Integer>> X_Assignments = new ArrayList<List<Integer>>();
    	for (int i=0; i<m.length; i++){
    		List<Integer> m_list = new ArrayList<Integer>();
    		X_Assignments.add(m_list);
    	}
    	return X_Assignments;
    }
    
    private static boolean findTermination(int count, double[] oldM, double[] m){
        //if false terminate
	    //if true continue
	    boolean check = true;
	    if(count + 1 > MaxCount){
	    	check = false;	    	
	    }	    

	    for (int i = 0; i <m.length; i++){
	        double err = m[i] - oldM[i];
	        if ((err < MaxError && err > 0) || (err == 0 && count > 1)){
	            check = false;
	        }
	        currentError = err;
	    }
	    
	    return check;
    }
    
    private static double[][] chooseKCluserCentres(int noK, int dimensionality){
        double[][] m = new double[noK][dimensionality];
        for (int i = 0; i < m.length; i++){
            m[i] = Math.random();
        }
        return m;
    }
    
    private static int findNoK(double[][] X){
    	//TODO some magic here to make this better
        int noK = 0;
        int high = X.length / 2;

        if (OverrideClusterNo > 0){
        	return OverrideClusterNo;
        }
        
        if (high <= 0){
        	noK = -1; //TODO JMC Break
        }
        else if (high == 1){
            noK = 1;
        }
        else if (high == 2){
            noK = 2;
        }
        else if (high > MaxClusters){
        	noK = MaxClusters;
        }
        else{
            noK = (int)(Math.random() * (high - 2) + 2) + 1;
        }
        
        return noK;
    }
    
    private static double[][] normaliseDataForClustering(double[][] X){
        double normX = 0.0;
        for (int i = 0; i < X.length; i++){
			for (int j = 0; j < X[0].length; j++){
				normX += Math.pow(X[i][j], 2);
			}
		}
		normX = Math.sqrt(normX);
		
        for (int i = 0; i < X.length; i++){
			for (int j = 0; j < X[0].length; j++){
				X[i][j] = (1/normX) * X[i][j];
			}
		}
        
        return X;
    }
    
    
	private static double[][] readFile(String filePath){
	    int noLines = 0;
		int noX = 0;
		
		List<List<Double>> dataPoints = new ArrayList<List<Double>>();
		String line = null;

		try {
		    FileReader fileReader = new FileReader(filePath);
		    BufferedReader bufferedReader = new BufferedReader(fileReader);

		    while((line = bufferedReader.readLine()) != null) {
		    	//System.out.println(line);
		        noLines++;
		    	List<Double> X_line = new ArrayList<Double>();
		    	String[] row = line.split(" ");
		    	noX = 0;
		    	for (int i = 0; i < row.length; i++){
		    		String element = row[i];
		    		noX++;
					X_line.add(Double.parseDouble(element));
		    	}
		    	dataPoints.add(X_line);
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
				X[i][j] = dataPoints.get(i).get(j);
			}
		}

		return X;
	}
}