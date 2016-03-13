/**
 * @author Jason Chalom
 * Main file is perceptron.java see that for full documentation
 * run javac perceptron.java
 * java perceptron <file>
 */

import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;
import java.util.Arrays;

class perceptron_input
{
	private double[][] X;
	private int[] T;
	private int noLines;
	private int noX;
	private int noT;

	public void setInput(double[][] X, int[] T, int noLines, int noX, int noT){
		this.X = X;
		this.T = T;
		this.noLines = noLines;
		this.noX = noX;
		this.noT = noT;
	}

	public double[][] getX(){
		return X;
	}

	public int[] getT(){
		return T;
	}

	public int getNoLines(){
		return noLines;
	}

	public int getNoX(){
		return noX;
	}

	public int getNoT(){
		return noT;
	}
}