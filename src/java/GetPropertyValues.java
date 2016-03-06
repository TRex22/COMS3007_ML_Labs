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
 
	public String getPropValues() throws IOException {
 
		try {
			Properties prop = new Properties();
			String propFileName = "config.properties";
 
			inputStream = getClass().getClassLoader().getResourceAsStream(propFileName);
 
			if (inputStream != null) {
				prop.load(inputStream);
			} else {
				//throw new FileNotFoundException("property file '" + propFileName + "' not found in the classpath");
				//so no config file, lets make one
				prop.setProperty("debug", false);
				prop.setProperty("MaxCount", 10000);
				prop.setProperty("MaxError", 0.1);
				prop.setProperty("LearningRate", 0.25);
				prop.store(new FileOutputStream(new File(Paths.get(".").toAbsolutePath().normalize().toString())), "#perceptron Properties");
			}
 
			Date time = new Date(System.currentTimeMillis());
 
			// get the property value and print it out
			String debug = prop.getProperty("debug");
 
			System.out.println(result + "\nProgram Ran on " + time + " debug=" + debug);
		} catch (Exception e) {
			System.out.println("Exception: " + e);
		} finally {
			inputStream.close();
		}
		return result;
	}
}