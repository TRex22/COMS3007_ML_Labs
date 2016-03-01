/*Jason Chalom 2016 Perceptron Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;/*
import perceptron_core.Perceptron_Core;*/

class Perceptron
{
	public static void main (String[] args){
		System.out.print("COMS3007: Machine LEarning Perceptron\n");
		System.out.print("Jason Chalom 2016 @TRex22\n\n");

	}

	/*while (stopping condition not satisfied)
    for (k = 0; k < N; k++)
        y = percept(W;X[k])
        for (i = 0; i < m + 1; i++)
            W[i] = W[i] + n * (T[k] - y) * X[k][i]*/

	public PerceptLearn(mInputs){
	     var count = 0;
	     var yOut = [];
	     var oldInputs = null;
	     var continue_ = findTermination(mInputs, oldInputs, count);

	     while (continue_){ //stopping condition
	        for (var k=0;k<mInputs.X.length;k++){
	            var y = percept(
	                {   weights: mInputs.W,
	                    X: mInputs.X[k]
	                }); 
	    
	            yOut.push([y]);


	            var m =mInputs.W.length;
	            for (var i=0;i<m;i++){ //check m+1
	                mInputs.W[i] = mInputs.W[i] + mInputs.n * (mInputs.T[k] - y) * mInputs.X[k][i];
	            }
	        }
	        
	        count++;
	        mInputs.y = yOut
	        yOut = [];
	        oldInputs = mInputs;
	        continue_ = findTermination(mInputs, oldInputs, count);
	     }
	     mInputs.count = count;
	     return mInputs;
	}

	function percept(mInputs){
	    //if w1X1+w2X2...-$ >0 return 1
	    //if W1X1+W2X2...-$ <=0 return 0
	    //using augemented matrix
	    var sum = 0;
	    
	    for (var i =0; i<mInputs.weights.length; i++){
	        sum += mInputs.X[i] * mInputs.weights[i];
	    }
	    if (sum > 0){
	        return 1;
	    }
	    else if (sum <= 0){
	        return 0;
	    }
	}

	function findTermination(mInputs, oldInputs, count){
	    //if false terminate
	    //if true continue
	    if (count +1 > mInputs.maxCount){
	        return false;
	    }
	    
	    if (mInputs.y){
	        //error
	        var error = 0;
	        var errorCount = 0;
	        for (var i=0; i<mInputs.y.length;i++){
	            error += Math.abs(mInputs.T[i] - mInputs.y[i]);
	            if(error>0){
	                errorCount++;
	            }
	        }
	        
	        if (error == 0 || (errorCount/mInputs.y.length) <= 10/100){
	            return false;
	        }
	    }
	    
	    return true;
	}

	public double[] rndWeights(int m, int low, int high){
		double weights[] = new double[m];
		for (int i=0; i<m; i++){ 
			double weight = Math.random() * (high - low) + low;		
			weights[i] = weight;
		}
		return weights;
	}

}