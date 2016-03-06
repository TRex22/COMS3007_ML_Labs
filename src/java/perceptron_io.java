/*Jason Chalom 2016 Perceptron Java version*/
import java.util.*;
import java.lang.*;
import java.io.*;
import java.util.Random;

/**
 * @author Jason Chalom
 * 
 */

public class perceptron_io
{
	public static void config(){
		try{
			GetPropertyValues properties = new GetPropertyValues();
			properties.getPropValues();
		}
		catch (Exception  e){
			System.out.println("An excpetion with the config has occured. e: "+e);
		}
	}
}