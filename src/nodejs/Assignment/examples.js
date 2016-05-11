//Examples of these different networks
'use strict'
mainScript();

//pulled examples from github readmes and npmjs
//modified enough to get these things working, see links.txt for sources

//main
function mainScript(){
	//allow me to play
	brainTrain();
	mlTrain();
	nodeMindTrain();
	convnetJSTrain();
	synapticTrain(); //very slow compared to the others
}

//Training brain js
function brainTrain(){
	console.log("Brain Train\n");
	var brain = require("brain");
	var net = new brain.NeuralNetwork();

	var data = [{input: { r: 0.03, g: 0.7, b: 0.5 }, output: { black: 1 }},
	           {input: { r: 0.16, g: 0.09, b: 0.2 }, output: { white: 1 }},
	           {input: { r: 0.5, g: 0.5, b: 1.0 }, output: { white: 1 }}];

	net.train(data, {
	  errorThresh: 0.005,  // error threshold to reach
	  iterations: 20000,   // maximum training iterations
	  log: true,           // console.log() progress periodically
	  logPeriod: 10,       // number of iterations between logging
	  learningRate: 0.3    // learning rate
	});

	var output = net.run({ r: 1, g: 0.4, b: 0 });  // { white: 0.99, black: 0.002 }
	console.log("Output: "+JSON.stringify(output)+"\n");
}

//training machine_learning
function mlTrain(){
	console.log("machine_learning Train\n");
	var ml = require('machine_learning');
	var x = [[0.4, 0.5, 0.5, 0.,  0.,  0.],
	         [0.5, 0.3,  0.5, 0.,  0.,  0.],
	         [0.4, 0.5, 0.5, 0.,  0.,  0.],
	         [0.,  0.,  0.5, 0.3, 0.5, 0.],
	         [0.,  0.,  0.5, 0.4, 0.5, 0.],
	         [0.,  0.,  0.5, 0.5, 0.5, 0.]];
	var y = [[1, 0],
	         [1, 0],
	         [1, 0],
	         [0, 1],
	         [0, 1],
	         [0, 1]];
	 
	var mlp = new ml.MLP({
	    'input' : x,
	    'label' : y,
	    'n_ins' : 6,
	    'n_outs' : 2,
	    'hidden_layer_sizes' : [4,4,5]
	});
	 
	mlp.set('log level',1); // 0 : nothing, 1 : info, 2 : warning.
	 
	mlp.train({
	    'lr' : 0.6,
	    'epochs' : 20000
	});
	 
	a = [[0.5, 0.5, 0., 0., 0., 0.],
	     [0., 0., 0., 0.5, 0.5, 0.],
	     [0.5, 0.5, 0.5, 0.5, 0.5, 0.]];
	 
	console.log(mlp.predict(a)+"\n");
}

//node-mind
function nodeMindTrain(){
	console.log("node-mind Train\n");

	var Mind = require('node-mind');

	/**
	 * Letters.
	 *
	 * - Imagine these # and . represent black and white pixels.
	 */

	var a = character(
	  '.#####.' +
	  '#.....#' +
	  '#.....#' +
	  '#######' +
	  '#.....#' +
	  '#.....#' +
	  '#.....#'
	);

	var b = character(
	  '######.' +
	  '#.....#' +
	  '#.....#' +
	  '######.' +
	  '#.....#' +
	  '#.....#' +
	  '######.'
	);

	var c = character(
	  '#######' +
	  '#......' +
	  '#......' +
	  '#......' +
	  '#......' +
	  '#......' +
	  '#######'
	);

	/**
	 * Learn the letters A through C.
	 */

	var mind = Mind()
	  .learn([
	    { input: a, output: map('a') },
	    { input: b, output: map('b') },
	    { input: c, output: map('c') }
	  ]);

	/**
	 * Predict the letter C, even with a pixel off.
	 */

	var result = mind.predict(character(
	  '#######' +
	  '#......' +
	  '#......' +
	  '#......' +
	  '#......' +
	  '##.....' +
	  '#######'
	));

	console.log(result+"\n"); // ~ 0.5

	/**
	 * Turn the # into 1s and . into 0s.
	 */

	function character(string) {
	  return string
	    .trim()
	    .split('')
	    .map(integer);

	  function integer(symbol) {
	    if ('#' === symbol) return 1;
	    if ('.' === symbol) return 0;
	  }
	}

	/**
	 * Map letter to a number.
	 */

	function map(letter) {
	  if (letter === 'a') return [ 0.1 ];
	  if (letter === 'b') return [ 0.3 ];
	  if (letter === 'c') return [ 0.5 ];
	  return 0;
	}

	/*//XOR
	var mind = Mind()
	  .learn([
	    { input: [0, 0], output: [ 0 ] },
	    { input: [0, 1], output: [ 1 ] },
	    { input: [1, 0], output: [ 1 ] },
	    { input: [1, 1], output: [ 0 ] }
	  ]);

	var xor = mind.download();*/
}

//convnetjs
function convnetJSTrain(){
	console.log("convnetjs Train\n");
	var convnetjs = require("convnetjs");

	var net = new convnetjs.Net();

	//Here's a minimum example of defining a 2-layer neural network and training it on a single data point:
	/*// species a 2-layer neural network with one hidden layer of 20 neurons
	var layer_defs = [];
	// input layer declares size of input. here: 2-D data
	// ConvNetJS works on 3-Dimensional volumes (sx, sy, depth), but if you're not dealing with images
	// then the first two dimensions (sx, sy) will always be kept at size 1
	layer_defs.push({type:'input', out_sx:1, out_sy:1, out_depth:2});
	// declare 20 neurons, followed by ReLU (rectified linear unit non-linearity)
	layer_defs.push({type:'fc', num_neurons:20, activation:'relu'}); 
	// declare the linear classifier on top of the previous hidden layer
	layer_defs.push({type:'softmax', num_classes:10});

	//var net = new convnetjs.Net();
	net.makeLayers(layer_defs);

	// forward a random data point through the network
	var x = new convnetjs.Vol([0.3, -0.5]);
	var prob = net.forward(x); 

	// prob is a Vol. Vols have a field .w that stores the raw data, and .dw that stores gradients
	console.log('probability that x is class 0: ' + prob.w[0]); // prints 0.50101

	var trainer = new convnetjs.SGDTrainer(net, {learning_rate:0.01, l2_decay:0.001});
	trainer.train(x, 0); // train the network, specifying that x is class zero

	var prob2 = net.forward(x);
	console.log('probability that x is class 0: ' + prob2.w[0]);
	// now prints 0.50374, slightly higher than previous 0.50101: the networks
	// weights have been adjusted by the Trainer to give a higher probability to
	// the class we trained the network with (zero)*/

	//and here is a small Convolutional Neural Network if you wish to predict on images:
	var layer_defs = [];
	layer_defs.push({type:'input', out_sx:32, out_sy:32, out_depth:3}); // declare size of input
	// output Vol is of size 32x32x3 here
	layer_defs.push({type:'conv', sx:5, filters:16, stride:1, pad:2, activation:'relu'});
	// the layer will perform convolution with 16 kernels, each of size 5x5.
	// the input will be padded with 2 pixels on all sides to make the output Vol of the same size
	// output Vol will thus be 32x32x16 at this point
	layer_defs.push({type:'pool', sx:2, stride:2});
	// output Vol is of size 16x16x16 here
	layer_defs.push({type:'conv', sx:5, filters:20, stride:1, pad:2, activation:'relu'});
	// output Vol is of size 16x16x20 here
	layer_defs.push({type:'pool', sx:2, stride:2});
	// output Vol is of size 8x8x20 here
	layer_defs.push({type:'conv', sx:5, filters:20, stride:1, pad:2, activation:'relu'});
	// output Vol is of size 8x8x20 here
	layer_defs.push({type:'pool', sx:2, stride:2});
	// output Vol is of size 4x4x20 here
	layer_defs.push({type:'softmax', num_classes:10});
	// output Vol is of size 1x1x10 here

	net = new convnetjs.Net();
	net.makeLayers(layer_defs);

	var mnist = require('mnist'); 

	var set = mnist.set(700, 20);

	var trainingSet = set.training;
	var testSet = set.test;

	// helpful utility for converting images into Vols is included
	//var x = convnetjs.img_to_vol(document.getElementById('#some_image'))
	var output_probabilities_vol = net.forward(trainingSet);

	console.log(JSON.stringify(output_probabilities_vol)+"\n");

}

//trianing synaptic
function synapticTrain(){
	/*var synaptic = require('synaptic');
	this.network = new synaptic.Architect.Perceptron(40, 25, 3);*/
	console.log("Synaptic Train\n");

	var synaptic = require('synaptic');

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

	console.log(myNetwork.activate(testSet[0].input));
	console.log(testSet[0].output+"\n");
}