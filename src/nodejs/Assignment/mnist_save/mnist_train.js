'use strict'
var parser = require('data4node').babelFish.csv;
var hactar = require('hactarjs');

var fs = require('fs');

var synaptic = require('synaptic');

var mnist = require('mnist'); 


var output = process.argv[2];
var learningRate = process.argv[3];

var hiddenLayerSize = process.argv[4];

console.log("LearningRate: "+learningRate+" hiddenLayerSize: "+hiddenLayerSize);

if (process.argv.length == 6+1){
	var hiddenLayer2Size = process.argv[5];
	console.log("hiddenLayer2Size: "+hiddenLayer2Size);
	synapticTrain2Layers();
}
if (process.argv.length == 7+1){
	var hiddenLayer2Size = process.argv[5];
	var hiddenLayer3Size = process.argv[6];
	console.log("hiddenLayer2Size: "+hiddenLayer2Size+" hiddenLayer3Size: "+hiddenLayer3Size);
	synapticTrain3Layers();
}
else
{
	synapticTrain();
}

//trianing synaptic
function synapticTrain(){
	console.log("Training 1 Hidden Layer MNIST Dataset: \n");
	var beginTime = new Date();
	console.log("Started: "+beginTime+"\n");

	/*var synaptic = require('synaptic');
	this.network = new synaptic.Architect.Perceptron(40, 25, 3);*/
	console.log("Synaptic Train\n");

	var Layer = synaptic.Layer;
	var Network = synaptic.Network;
	var Trainer = synaptic.Trainer;

	var set = mnist.set(700, 20);

	var trainingSet = set.training;
	var testSet = set.test;

	var inputLayer = new Layer(784);
	var hiddenLayer = new Layer(hiddenLayerSize);
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
	    rate: learningRate,//.3,
	    iterations: 10,
	    error: .5,
	    shuffle: true,
	    log: 1,
	    cost: Trainer.cost.CROSS_ENTROPY
	});

	console.log("Complete!! \n");

	var endTime = new Date();
	console.log("Ended: "+endTime+"\n");

	/*var outputStr = imageSet.newData.topLine+" beginTime: "+ beginTime+" endTime: "+endTime+"\n"+result;
	hactar.saveFile(outputStr, output);*/

	//console.log(result+"\n");

	for (var i=0; i<testSet.length; i++){
		console.log("Result using testSet["+i+"]: "+testSet[i].output+"\n");
		console.log(myNetwork.activate(testSet[i].input));
	}
	
}

function synapticTrain2Layers(){
	console.log("Training 2 Hidden Layer MNIST Dataset: \n");
	var beginTime = new Date();
	console.log("Started: "+beginTime+"\n");

	/*var synaptic = require('synaptic');
	this.network = new synaptic.Architect.Perceptron(40, 25, 3);*/
	console.log("Synaptic Train\n");

	var Layer = synaptic.Layer;
	var Network = synaptic.Network;
	var Trainer = synaptic.Trainer;

	var set = mnist.set(700, 20);

	var trainingSet = set.training;
	var testSet = set.test;

	var inputLayer = new Layer(784);
	var hiddenLayer = new Layer(hiddenLayerSize);
	var hiddenLayer2 = new Layer(hiddenLayer2Size);
	var outputLayer = new Layer(10);

	inputLayer.project(hiddenLayer);
	hiddenLayer.project(hiddenLayer2);
	hiddenLayer2.project(outputLayer);

	var myNetwork = new Network({
	    input: inputLayer,
	    hidden: [hiddenLayer, hiddenLayer2],
	    output: outputLayer
	});

	var trainer = new Trainer(myNetwork);
	trainer.train(trainingSet, {
	    rate: learningRate,//.3,
	    iterations: 10,
	    error: .5,
	    shuffle: true,
	    log: 1,
	    cost: Trainer.cost.CROSS_ENTROPY
	});

	console.log("Complete!! \n");

	var endTime = new Date();
	console.log("Ended: "+endTime+"\n");

	/*var outputStr = imageSet.newData.topLine+" beginTime: "+ beginTime+" endTime: "+endTime+"\n"+result;
	hactar.saveFile(outputStr, output);*/

	//console.log(result+"\n");

	for (var i=0; i<testSet.length; i++){
		console.log("Result using testSet["+i+"]: "+testSet[i].output+"\n");
		console.log(myNetwork.activate(testSet[i].input));
	}
	
}

function synapticTrain3Layers(){
	console.log("Training 3 Hidden Layer MNIST Dataset: \n");
	var beginTime = new Date();
	console.log("Started: "+beginTime+"\n");

	/*var synaptic = require('synaptic');
	this.network = new synaptic.Architect.Perceptron(40, 25, 3);*/
	console.log("Synaptic Train\n");

	var Layer = synaptic.Layer;
	var Network = synaptic.Network;
	var Trainer = synaptic.Trainer;

	var set = mnist.set(700, 20);

	var trainingSet = set.training;
	var testSet = set.test;

	var inputLayer = new Layer(784);
	var hiddenLayer = new Layer(hiddenLayerSize);
	var hiddenLayer2 = new Layer(hiddenLayer2Size);
	var hiddenLayer3 = new Layer(hiddenLayer3Size);
	var outputLayer = new Layer(10);

	inputLayer.project(hiddenLayer);
	hiddenLayer.project(hiddenLayer2);
	hiddenLayer2.project(hiddenLayer3);
	hiddenLayer3.project(outputLayer);

	var myNetwork = new Network({
	    input: inputLayer,
	    hidden: [hiddenLayer, hiddenLayer2, hiddenLayer3],
	    output: outputLayer
	});

	var trainer = new Trainer(myNetwork);
	trainer.train(trainingSet, {
	    rate: learningRate,//.3,
	    iterations: 10,
	    error: .5,
	    shuffle: true,
	    log: 1,
	    cost: Trainer.cost.CROSS_ENTROPY
	});

	console.log("Complete!! \n");

	var endTime = new Date();
	console.log("Ended: "+endTime+"\n");

	/*var outputStr = imageSet.newData.topLine+" beginTime: "+ beginTime+" endTime: "+endTime+"\n"+result;
	hactar.saveFile(outputStr, output);*/

	//console.log(result+"\n");

	for (var i=0; i<testSet.length; i++){
		console.log("Result using testSet["+i+"]: "+testSet[i].output+"\n");
		console.log(myNetwork.activate(testSet[i].input));
	}
	
}