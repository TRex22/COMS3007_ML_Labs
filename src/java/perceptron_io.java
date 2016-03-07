/*Jason Chalom 2016 Perceptron Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;
import java.util.Arrays;

/**
 * @author Jason Chalom
 * Main file is perceptron.java see that for full documentation
 * run javac perceptron.java
 * java perceptron <file>
 */

public class perceptron_io
{
	public static GetPropertyValues config(){
		try{
			GetPropertyValues properties = new GetPropertyValues();
			properties.getPropValues();

			return properties;
		}
		catch (Exception  e){
			System.out.println("An excpetion with the config has occured. e: "+e);
		}
		return null;
	}

	public static perceptron_input getInput(String filePath, GetPropertyValues properties){
		int noLines = 0;
		int noX = 0;
		int noT = 0;
		List<List<Double>> X_al = new ArrayList<List<Double>>(2);
		List<Integer> T_al = new ArrayList<>();

		double[][] X;
		int[] T;

		try{
			File file = new File(filePath);
			Scanner sc = new Scanner(file);

			//each line is an X value until an integer is hit then it is a T value
			String strLine = sc.next();
			while (sc.hasNext()){
				noLines++;				
				Scanner line = new Scanner(strLine).useDelimiter(" ");

				while(line.hasNext()){
					noX = 0;
					List<Double> X_line = new ArrayList<>();
					if(line.hasNextDouble()){
						noX++;
						X_line.add(line.nextDouble());
					}
					else if(line.hasNextInt()){
						noT++;
						T_al.add(line.nextInt());
					}
					else{

					}
					X_al.add(X_line);
				}
				strLine = sc.next();
			}

System.out.print("damn: "+noT);
			//randomize inputs using PRNG trick
			long seed = System.nanoTime();
			Collections.shuffle(X_al, new Random(seed));
			Collections.shuffle(T_al, new Random(seed));

			X = new double[noLines][noX];
			for (int i = 0; i < noLines; i++){
				for (int j = 0; j < noX; j++){
					X[i][j] = X_al.get(i).get(j);
				}
			}
			
			T = new int[noT];
			for (int i = 0; i < noT; i++){
				T[i] = T_al.get(i);
			}

			perceptron_input input = new perceptron_input();
			input.setInput(X, T, noLines, noX, noT);

			return input;

		}
		catch (FileNotFoundException e){
			e.printStackTrace();
		}

		return null;
	}
}