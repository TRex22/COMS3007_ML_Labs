/*Jason Chalom 2016 Perceptron Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;

/**
 * @author Jason Chalom
 * 
 */

class perceptron_core
{
	public perceptron_output PerceptLearn(double[][] X, double[] T, double[] W, GetPropertyValues properties){
		/*while (stopping condition not satisfied)
	    for (k = 0; k < N; k++)
	        y = percept(W;X[k])
	        for (i = 0; i < m + 1; i++)
	            W[i] = W[i] + n * (T[k] - y) * X[k][i]*/

	    int count = 0;
	    int N = X.length;
	    //TODO: JMC number of output nodes linked to T
	    double[] y = new double[N];
	    boolean continue_ = findTermination(y, T, count, properties);
	    while (continue_){
	    	for (int i = 0; i < N; i++){
	    		double currentY = percept(W, X[i]);
	    		int m = W.length;
	    		for (int j = 0; j < m; j++){
	    			W[j] = W[j] + properties.LearningRate * (T[i] - currentY) * X[i][j];
	    		}
	    		y[i] = currentY;
	    	}

	    	count++;
	    	continue_ = findTermination(y, T, count, properties);
	    }

	    perceptron_output output = new perceptron_output();
	    output.setOutput(y, W, count);
	    return output;
	}

	public int percept(double weights[], double X[]){
		//if w1X1+w2X2...-$ >0 return 1
	    //if W1X1+W2X2...-$ <=0 return 0
	    //using augemented matrix

		double sum = 0.0;

		for (int i = 0; i < weights.length; i++){
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
	private boolean findTermination(double[] y, double[] T, int count, GetPropertyValues properties){
		//if false terminate
	    //if true continue
	    if(count + 1 > properties.MaxCount){
	    	return false;
	    }

	    if(y.length > 0){
	    	double error = 0;
		    int errorCount = 1;
		    for (int i = 0; i < y.length; i++){
		    	error += Math.abs(T[i] - y[i]);
		    	if(error > 0){
		    		errorCount++;
		    	}
		    }

		    if (error == 0.0 ){//|| (errorCount/y.length) <= properties.MaxError
		    	return false;
		    }
	    }
	    return true;
	}
}