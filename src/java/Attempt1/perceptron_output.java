/**
 * @author Jason Chalom
 * Main file is perceptron.java see that for full documentation
 * run javac perceptron.java
 * java perceptron <file>
 */

class perceptron_output
{
	private double[] y;
	private double[] weights;
	private int count;

	public double[] getY(){
		return y;
	}

	public double[] getWeights(){
		return weights;
	}

	public int getCount(){
		return count;
	}

	public void setY(double[] y){
		this.y = y;
	}

	public void setWeights(double[] weights){
		this.weights = weights;
	}

	public void setCount(int count){
		this.count = count;
	}

	public void setOutput(double[] y, double[] weights, int count){
		this.y = y;
		this.weights = weights;
		this.count = count;
	}
}