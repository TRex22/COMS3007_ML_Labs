'use strict'
//Experiment 3

//node imageLearn.js /c/dataTest/node/output/1.csv /c/dataTest/node/bw/1.csv 
///c/dataTest/node/output/2.csv /c/dataTest/node/Experiment1/k_means/1_result.csv

var il = require('imageLearn.js');

var filenameInput = "/c/dataTest/node/output/1.csv";
var filenameTarget = "/c/dataTest/node/bw/1.csv";
var filenameNewData = "/c/dataTest/node/output/2.csv";
var output = "/c/dataTest/node/Experiment1/k_means/1_result.csv";

var method = "km";

il.main(filenameInput, filenameTarget, filenameNewData, output, method);