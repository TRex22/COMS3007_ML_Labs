/*Jason Chalom 2016 Perceptron Java version*/
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.Date;
import java.util.Properties;
import java.io.FileOutputStream;
import java.io.File;
 
/**
 * @author Jason Chalom
 * 
 */
 
public class GetPropertyValues {
	String result = "";
	InputStream inputStream;

	public boolean debug;
	public int MaxCount;
	public double MaxError;
	public double LearningRate;
 
	public String getPropValues() throws IOException {
 
		try {
			Properties prop = new Properties();
			String propFileName = "config.properties";
 
			inputStream = getClass().getClassLoader().getResourceAsStream(propFileName);
 
			if (inputStream != null) {
				prop.load(inputStream);
			} 
			else {
				//throw new FileNotFoundException("property file '" + propFileName + "' not found in the classpath");
				//so no config file, lets make one
				prop.setProperty("debug", "false");
				prop.setProperty("MaxCount", "10000");
				prop.setProperty("MaxError", "0.01");
				prop.setProperty("LearningRate", "0.25");
				prop.store(new FileOutputStream(new File(GetPropertyValues.class.getProtectionDomain().getCodeSource().getLocation().getPath())), "#perceptron Properties");
			}
 
			Date time = new Date(System.currentTimeMillis());

			debug = Boolean.parseBoolean(prop.getProperty("debug"));
			MaxCount = Integer.parseInt(prop.getProperty("MaxCount"));
			MaxError = Double.parseDouble(prop.getProperty("MaxError"));
			LearningRate = Double.parseDouble(prop.getProperty("LearningRate"));
 
			System.out.println(result + "\nProgram Ran on " + time + " debug=" + debug);
		} 
		catch (Exception e) 		{
			System.out.println("Exception: " + e);
		} 
		finally {
			inputStream.close();
		}

		return result;
	}
}