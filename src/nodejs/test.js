//Jason Chalom Feb 15 2016
//labs and tests

var perceptron = require("./perceptron.js");

labs();

function labs(){
	console.log('Lab 1 Number 1: perceptron');

	lab1();
	console.log('Lab 1 Number 2: perceptron learning algorithm');
	lab1_2();
	console.log('Lab 1 Number 4: perceptron learning algorithm');
	lab_1_4()
}

function lab1(){
	var mInputs = {
	    weights: JSON.parse('[2, -1, 1]'),
	    X: JSON.parse('[1.5, 2.5, -1]')
	};

	var percept = require('./core.js').percept(mInputs);
	if (percept === 0){
		console.log('Example 0: %s SUCCESS!', percept);
	}

	//example 1
	mInputs = {
	    weights : JSON.parse('[2, -3, 1]'),
	    X : JSON.parse('[3, 1, -1]')
	};

	percept = require('./core.js').percept(mInputs);
	if (percept === 1){
		console.log('Example 1: %s SUCCESS!', percept);
	}

	console.log('\n');
}

function lab1_2(){
	/*trainingSet: [[0, 0, 1], [0, 1, 1], [1, 0, 1], [1, 1, 0]],
	N: 4,*/
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

function lab_1_4(){
	/*{(0, 0, 0), (0, 1, 1), (1, 0, 1), (1, 1, 0)}*/
	
	var m = 3;
	var weights = perceptron.rndWeights(m, -1, 1);
	var mInputs = {		
		X: [[0, 0, -1], [0, 1, -1], [1, 0, -1], [1, 1, -1]],
		T: [[0], [1], [1], [0]],
		W: weights,
		n: 0.25, //learning rate
		m: m,
		maxCount: 10000
	};

	var PerceptLearn = require('./core.js').PerceptLearn(mInputs);
	console.log('Raw output:\n%s\n\n', JSON.stringify(PerceptLearn));
	perceptron.drawGraph(PerceptLearn);
}