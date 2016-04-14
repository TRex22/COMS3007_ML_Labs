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
	private List<List<Double>> X;
	private List<Integer> T;
	private int noLines;
	private int noX;
	private int noT;

	public void setInput(List<List<Double>> X, List<Integer> T, int noLines, int noX, int noT){
		this.X = X;
		this.T = T;
		this.noLines = noLines;
		this.noX = noX;
		this.noT = noT;
	}

	public List<List<Double>> X getX(){
		return X;
	}

	public List<Integer> T getT(){
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