'use strict'
var parser = require('data4node').babelFish.csv;
var hactar = require('hactarjs');

var fs = require('fs');

var synaptic = require('synaptic');


synapticTrain();

//trianing synaptic
function synapticTrain(){
	var beginTime = new Date();
	console.log("Started: "+beginTime+"\n");

	/*var synaptic = require('synaptic');
	this.network = new synaptic.Architect.Perceptron(40, 25, 3);*/
	console.log("Synaptic Train\n");

	var Layer = synaptic.Layer;
	var Network = synaptic.Network;
	var Trainer = synaptic.Trainer;

	var mnist = require('mnist'); 

	var set = mnist.set(700, 20);

	var trainingSet = set.training;
	var testSet = set.test;

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

	console.log("Complete!! \n");
	var endTime = new Date();
	console.log("Ended: "+endTime+"\n");

	var outputStr = imageSet.newData.topLine+" beginTime: "+ beginTime+" endTime: "+endTime+"\n"+result;
	hactar.saveFile(outputStr, output);

	//console.log(result+"\n");

	console.log(myNetwork.activate(testSet[0].input));
	console.log(testSet[0].output+"\n");
}