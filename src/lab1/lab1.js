//Jason Chalom Feb 15 2016
'use strict';

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
	var rndWeights = rndWeights(-1, 1);
	var mInputes = {		
		X: [[0, 0, -1], [0, 1, -1], [1, 0, -1], [1, 1, -1]],
		T: [[1], [1], [1], [0]],
		W: rndWeights,
		n: 0.25, //learning rate
		maxCount: 1000
	};


} 

function rndWeights(from, to){

}
