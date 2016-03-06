/*Jason Chalom 2016 Perceptron Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;

class perceptron_core
{
	public double[] PerceptLearn(double[][] X, double[] T, double[] W, GetPropertyValues properties){
		/*while (stopping condition not satisfied)
	    for (k = 0; k < N; k++)
	        y = percept(W;X[k])
	        for (i = 0; i < m + 1; i++)
	            W[i] = W[i] + n * (T[k] - y) * X[k][i]*/

	    double[] y = [];
	    int count = 0;
	    while (findTermination(double[] y, T, count, properties)){

	    }
	}

	public int percept(double[] weights, double[][] X){
		//if w1X1+w2X2...-$ >0 return 1
	    //if W1X1+W2X2...-$ <=0 return 0
	    //using augemented matrix

		double sum = 0.0;

		for (int i = 0; i < weights.length; i++){
			sum += X[i] * weights[i];
		}

		if (sum > 0){
			return 1;
		}
		return 0;
	}

	//find the termination conditions
	public boolean findTermination(double[] y, double[] T, int count, GetPropertyValues properties){
		//if false terminate
	    //if true continue
	    if(count + 1 > properties.MaxCount){
	    	return false;
	    }

	    if(y.length > 0){
	    	double error = 0;
		    int errorCount = 1;
		    for (var i = 0; i < y.length; i++){
		    	error += Math.abs(T[i] - y[i]);
		    	if(error > 0){
		    		errorCount++;
		    	}
		    }

		    if ((errorCount/mInputs.y.length) <= properties.MaxError){
		    	return false;
		    }
	    }

	    return true;
	}
}