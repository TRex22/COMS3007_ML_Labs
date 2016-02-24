//Jason Chalom Feb 15 2016
'use strict';

// dependencies 
var CliGraph = require("cli-graph");
var fs = require("fs");
var hactarjs = require("hactarjs");

var exposed = {
  rndWeights: rndWeights,
  drawGraph: drawGraph,
  processDataSet: processDataSet
};
module.exports = exposed;

// print process.argv
/*process.argv.forEach(function (val, index, array) {
  console.log(index + ': ' + val);
});*/

console.log('COMS3007: Machine LEarning Perceptron');
console.log('Jason Chalom 2016 @TRex22\n\n');

function processDataSet(){
	//read dataset
	//calc m
	//calc weights
	//do ends

	var m = 3;
	var weights = perceptron.rndWeights(m, -1, 1);
	var mInputs = {		
		X: [[0, 0, -1], [0, 1, -1], [1, 0, -1], [1, 1, -1]],
		T: [[1], [1], [1], [0]],
		W: weights,
		n: 0.25, //learning rate
		m: m,
		maxCount: 1000
	};

	var PerceptLearn = require('./core.js').PerceptLearn(mInputs);
	console.log('Raw output:\n%s\n\n', JSON.stringify(PerceptLearn));
	perceptron.drawGraph(PerceptLearn);
} 

function rndWeights(m, low, high){
	var weights = [];
	for (var i=0; i<m; i++){ 
		var weight = Math.random() * (high - low) + low;		
		weights.push(weight);
	}
	return weights;
}

function drawGraph(mInputs){
	console.log('Graph of Weights');
	console.log('');
		
	// create a new function graph 
	var graph = new CliGraph({
	    height: 30
	  , width: 60
	  , marks: {
	        hAxis: '─'
	      , vAxis: '│'
	      , center: '┼'
	      , point: '•'
	  }
	});
	
	//input data
	for (var i = 0; i < mInputs.X.length; i++) {
		graph.addPoint(mInputs.X[i][0]*10, mInputs.X[i][1]*10, "x");
	}
	
	//w1x1+w2x2-w3=0
	
	for (var j = 0; j < mInputs.X.length; j++) {
		graph.addPoint(mInputs.W[0]*mInputs.X[j][0]*10, mInputs.W[0]*mInputs.X[j][1]*10);
	}
	
	// output graph 
	console.log('\n'+graph.toString());
}
