//Jason Chalom Feb 15 2016
'use strict';

// print process.argv
/*process.argv.forEach(function (val, index, array) {
  console.log(index + ': ' + val);
});*/

console.log('COMS3007: Machine LEarning');
console.log('Lab 1 Number 1: perceptron');

lab1();
console.log('Lab 1 Number 2: perceptron learning algorithm');
lab1_2();

function lab1(){
	var mInputs = {
	    weights: JSON.parse('[2, -1, 1]'),
	    X: JSON.parse('[1.5, 2.5, -1]')
	};

	var percept = require('./core.js').percept(mInputs);
	console.log('Example 0: %s', percept);

	//example 1
	mInputs = {
	    weights : JSON.parse('[2, -3, 1]'),
	    X : JSON.parse('[3, 1, -1]')
	};

	percept = require('./core.js').percept(mInputs);
	console.log('Example 1: %s', percept);
}

function lab1_2(){
	/*trainingSet: [[0, 0, 1], [0, 1, 1], [1, 0, 1], [1, 1, 0]],
	N: 4,*/
	var m = 3;
	var weights = rndWeights(m, -1, 1);
	var mInputs = {		
		X: [[0, 0, -1], [0, 1, -1], [1, 0, -1], [1, 1, -1]],
		T: [[1], [1], [1], [0]],
		W: weights,
		n: 0.25, //learning rate
		m: m,
		maxCount: 100
	};

	var PerceptLearn = require('./core.js').PerceptLearn(mInputs);
	console.log('Raw output:\n%s\n\n', JSON.stringify(PerceptLearn));
	drawGraph(PerceptLearn);
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
	// dependencies 
	var FunctionGraph = require("function-graph");
	
	// create a new function graph 
	var graph = new FunctionGraph ({
	    height: 30
	  , width: 100
	  , marks: {
	        hAxis: '─'
	      , vAxis: '│'
	      , center: '┼'
	      , point: '•'
	  }
	});
	
	//w1x1+w2x2-w3=0
	
	var points = [];
	for (var i = 0; i < mInputs.W.length-1; i++) {
		points.push(mInputs.W[i]*mInputs.y[i]);
	}
	points.push(mInputs.W[mInputs.W.length]);

	for (var i = 0; i < points.length; i++) {
		graph.addPoint(mInputs.W[i], mInputs.y[i]);
	}
	
	// output graph 
	console.log(graph.toString());
}
