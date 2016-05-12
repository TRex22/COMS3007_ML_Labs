'use strict'
var ml = require('machine_learning');
var brain = require("brain");
var NeuralN = require('neuraln');

var parser = require('data4node').babelFish.csv;
var hactar = require('hactarjs');

var fs = require('fs');

//node --max_old_space_size=4096 imageLearn.js /c/dataTest/node/output/1.csv /c/dataTest/node/bw/1.csv /c/dataTest/node/output/2.csv /c/dataTest/node/result/2_result.csv
var filenameInput = process.argv[2];
var filenameTarget = process.argv[3];
var filenameNewData = process.argv[4];
var output = process.argv[5];

//main script here

main(filenameInput, filenameTarget, filenameNewData, output, "");

function main(filenameInput, filenameTarget, filenameNewData, output, method){
	var noNodes = 0;
	var inputData;
	fs.readFile(filenameInput, 'utf8', function(err, data) {
		if (err) throw err;
		 inputData = parseCSVImage(data);
		
		var targetData;
		fs.readFile(filenameTarget, 'utf8', function(err, data) {
			if (err) throw err;
			targetData = parseCSVImage(data);
			
			var newData;
			fs.readFile(filenameNewData, 'utf8', function(err, data) {
				if (err) throw err;
				newData = parseCSVImage(data);
				noNodes = inputData.pixels[0].length;
				
				var imageSet = {"inputData": inputData, "targetData": targetData, "newData": newData};

				run_ml_network_whole_image_set(imageSet, noNodes, output);
				//ml_km_Train(imageSet.inputData.pixels);
				//ml_lr_Train(imageSet, noNodes, output);
				//brainTrain(imageSet, noNodes, output);
				//nuralnTrain(imageSet, noNodes, output)
				
				//synapticTrain(imageSet, noNodes, output);
				//console.log(JSON.stringify(newData));
			});
		});	
	});	
}

function ml_lr_Train(imageSet, noNodes, output){
	var beginTime = new Date();
	console.log("Started: "+beginTime+"\n");

	var x = imageSet.inputData.pixels;
	var y = imageSet.targetData.pixels;
	 
	var classifier = new ml.LogisticRegression({
	    'input' : x,
	    'label' : y,
	    'n_in' : noNodes,
	    'n_out' : noNodes
	});
	 
	classifier.set('log level',1);
	 
	var training_epochs = 800, lr = 0.25;
	 
	classifier.train({
	    'lr' : lr,
	    'epochs' : training_epochs
	});
	 
	x = imageSet.newData.pixels;

	var result = classifier.predict(x)
	
	var endTime = new Date();
	console.log("Ended: "+endTime+"\n");

	var outputStr = imageSet.newData.topLine+" beginTime: "+ beginTime+" endTime: "+endTime+"\n"+result;
	hactar.saveFile(outputStr, output);
	 
	console.log("Result : "+result+"\n");
}

function run_ml_network_whole_image_set(imageSet, noNodes, output){
	var beginTime = new Date();
	console.log("Started: "+beginTime+"\n");

	var x = [imageSet.inputData.pixels[0], imageSet.inputData.pixels[1], imageSet.inputData.pixels[2], imageSet.inputData.pixels[3], imageSet.inputData.pixels[4]];
	var y = [imageSet.targetData.pixels[0], imageSet.targetData.pixels[1], imageSet.targetData.pixels[2], imageSet.targetData.pixels[3], imageSet.inputData.pixels[4]];
	//console.log(x[0].length +" y: "+y[0].length);
	 
	var mlp = new ml.MLP({
	    'input' : x,
	    'label' : y,
	    'n_ins' : noNodes,
	    'n_outs' : noNodes,
	    'hidden_layer_sizes' : [4,4,5]
	});
	 
	mlp.set('log level',1); // 0 : nothing, 1 : info, 2 : warning.
	 
	mlp.train({
	    'lr' : 0.6,
	    'epochs' : 20000
	});
	 
	a = imageSet.newData.pixels;
	
	var result = mlp.predict(a);
	
	var endTime = new Date();
	console.log("Ended: "+endTime+"\n");

	var outputStr = imageSet.newData.topLine+" beginTime: "+ beginTime+" endTime: "+endTime+"\n"+result;
	hactar.saveFile(outputStr, output);

	console.log(result+"\n");
}

function ml_km_Train(data){ 
	console.log(JSON.stringify(data));
	var beginTime = new Date();
	console.log("Started: "+beginTime+"\n");

	var result = ml.kmeans.cluster({
	    data : data,
	    k : 4,
	    epochs: 100,
	 
	    distance : {type : 'euclidean'}
	    // default : {type : 'euclidean'}
	    // {type : 'pearson'}
	    // Or you can use your own distance function
	    // distance : function(vecx, vecy) {return Math.abs(dot(vecx,vecy));}
	});

	var endTime = new Date();
	console.log("Ended: "+endTime+"\n");

	var outputStr = imageSet.newData.topLine+" beginTime: "+ beginTime+" endTime: "+endTime+"\n"+JSON.stringify(result);
	hactar.saveFile(outputStr, output);

	 
	console.log("clusters : ", result.clusters);
	console.log("means : ", result.means+"\n");
}

//trianing synaptic
function synapticTrain(imageSet, noNodes, output){
	/*var synaptic = require('synaptic');
	this.network = new synaptic.Architect.Perceptron(40, 25, 3);*/
	console.log("Synaptic Train\n");

	var synaptic = require('synaptic');

	var Layer = synaptic.Layer;
	var Network = synaptic.Network;
	var Trainer = synaptic.Trainer;

	//var mnist = require('mnist'); 
	//var set = mnist.set(700, 20);

	var trainingSet = imageSet.inputData.pixels;//set.training;
	var testSet = imageSet.targetData.pixels;//set.test;

	var inputLayer = new Layer(784);
	var hiddenLayer = new Layer(100);
	var outputLayer = new Layer(10);

	inputLayer.project(hiddenLayer);
	hiddenLayer.project(outputLayer);

	var myNetwork = new Network({
	    input: inputLayer,
	    hidden: [hiddenLayer],
	    output: outputLayer
	});

	var trainer = new Trainer(myNetwork);
	trainer.train(trainingSet, {
	    rate: .3,
	    iterations: 10,
	    error: .5,
	    shuffle: true,
	    log: 1,
	    cost: Trainer.cost.CROSS_ENTROPY
	});

	console.log(myNetwork.activate(testSet[0].input));
	console.log(testSet[0].output+"\n");
}

function nuralnTrain(imageSet, noNodes, output){
	var beginTime = new Date();
	console.log("Started: "+beginTime+"\n");

	/* Create a neural network with 4 layers (2 hidden layers) */
	//var network = new NeuralN([ 1, 4, 3, 1 ]);
	var layers = [4, 4];
	var momentum = 0.3;
	var learning_rate = 0.3;
	var bias = -1;

	var network = new NeuralN(layers, momentum, learning_rate, bias);

	/* Add points to the training set */
	/*for(var i = -1; i < 1; i+=0.1) {
	  network.train_set_add([ i ], [ Math.abs(Math.sin(i)) ]);
	}*/
	// /network.train_set_add(input, output)
	/*for (var i=0; i<imageSet.inputData.pixels.length; i++){
		var input = imageSet.inputData.pixels[i];
		var output = imageSet.targetData.pixels[i];
		network.train_set_add(input, output);
	}*/

	var input = imageSet.inputData.pixels[0];
	var output = imageSet.targetData.pixels[0];
	network.train_set_add(input, output);

	/* Train the network with one of the two available methods */
	/* monothread (blocking) vs multithread (non-blocking)     */
	network.train({
	  target_error: 0.01,
	  iterations: 20000,

	  multithread: true,
	  /* Relevant only when multithread is true: */
	  step_size: 100,
	  threads: 4
	}, function(err) {

	});

	/* Run */
	//var result = network.run([ (Math.random() * 2) - 1 ]);

	/*for (var i=0; i<imageSet.newData.pixels.length; i++){
		var newData = imageSet.newData.pixels[i];
		
	}*/
	var result = network.run(imageSet.newData.pixels);

	/* Retrieve the network's string representation */
	var string = network.to_string();

	/* Retrieve the network's state string */
	var state = network.get_state();

	//var result = 
	
	var endTime = new Date();
	console.log("Ended: "+endTime+"\n");

	/*var outputStr = imageSet.newData.topLine+" beginTime: "+ beginTime+" endTime: "+endTime+"\n"+result;
	hactar.saveFile(outputStr, output);
	 
	console.log("Result : "+result+"\n");*/
}

function brainTrain(imageSet, noNodes, output){
	console.log("Brain Train\n");
	var brain = require("brain");
	var net = new brain.NeuralNetwork();

	//var data = [{input: { r: 0.03, g: 0.7, b: 0.5 }, output: { black: 1 }},
	 //          {input: { r: 0.16, g: 0.09, b: 0.2 }, output: { white: 1 }},
	  //         {input: { r: 0.5, g: 0.5, b: 1.0 }, output: { white: 1 }}];
  	var data = {input: imageSet.inputData.pixels, output: imageSet.targetData.pixels};

	net.train(data, {
	  errorThresh: 0.005,  // error threshold to reach
	  iterations: 20000,   // maximum training iterations
	  log: true,           // console.log() progress periodically
	  logPeriod: 10,       // number of iterations between logging
	  learningRate: 0.3    // learning rate
	});

	var output = net.run(imageSet.newData.pixels);  // { white: 0.99, black: 0.002 }
	console.log("Output: "+JSON.stringify(output)+"\n");
}

//parse ImageProessor 0.1.1 csv format (first line is info, then each line is RGB)
function parseCSVImage(csvFile){
	// babelFish.parseToDataObj
	//remove first line
	var lines = csvFile.split('\n');
	var topLine = lines[0];
	lines.splice(0,1);
	var pixels = parser.parseToDataObj(lines.join('\n'));

	var imageObj = {pixels: pixels, topLine: topLine};
	return imageObj;
}


//perhaps do see later
//open .dat files 
//see image processor project in NetAndCuda
function openDatImageFile(){

}

function saveDatImageFile(data){

}

