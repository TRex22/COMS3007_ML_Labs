//Jason Chalom Feb 15 2016
'use strict';
var weights = JSON.parse(process.argv[2]);
var X = JSON.parse(process.argv[3]);

var mInputs = {
    weights : weights,
    X : X
};

var percept = require('./Lab1.js').percept(mInputs);

console.log('output: %s', percept);

function trainingDay(trainingData){
    
}