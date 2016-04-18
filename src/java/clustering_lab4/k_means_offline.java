/*Jason Chalom 2016 K_means offline Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;
import java.util.Arrays;

/**
 * @author Jason Chalom
 * 
 * 
 */
 
public class k_means_offline
{
    private static int MaxCount = 1000;
    private static double MaxError = 0.1;
    
    public static void main (String filePath){
        //read data into
        double[][] X = readFile(filePath);
        int dataDim = X[0].length;
        //normalise
        X = normaliseDataForClustering(X);
        //find no K
        //TODO JMC fix at somepoint in the next decade
        int k = findNoK(X);
        //choose K cluster centres
        double[] m = chooseKCluserCentres(int k);
        double[] oldM = m;
        
        //while loop and stopping condition
        int count = 0;
        boolean continue_ = findTermination(count, oldM, m);
        
    }
    
    private static boolean findTermination(int count, double[] oldM, double[] m){
        //if false terminate
	    //if true continue
	    if(count + 1 > MaxCount){
	    	return false;
	    }
	    
	    boolean check = true;
	    for (int i = 0; i <m.length; i++){
	        double err = m[i] - oldM[i];
	        if (err < MaxError || err == 0){
	            check = false;
	        }
	    }
	    
	    return check;
    }
    
    private static double[] chooseKCluserCentres(int noK){
        double[] m = new double[noK];
        for (int i = 0; i < m.length; i++){
            m[i] = Math.random();
        }
        return m;
    }
    
    private static double findNoK(double[][] X){
        int noK = 0;
        
        if (high <= 0){
            throw new Exception();
        }
        else if (high == 1){
            noK = 1;
        }
        else if (high == 2){
            noK = 2;
        }
        else{
            int high = X.length / 2;
            noK = (int) (Math.random() * (high - 2) + 2) + 1;
        }
        
        
    }
    
    private static double[][] normaliseDataForClustering(double[][] X){
        double normX = 0.0;
        for (int i = 0; i < X.length; i++){
			for (int j = 0; j < X[0].length; j++){
				normX += math.pow(X[i][j], 2);
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

		return X;
	}
}