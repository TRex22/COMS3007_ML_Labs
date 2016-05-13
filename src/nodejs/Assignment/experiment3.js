'use strict'
//Experiment 3

 //node imageLearn.js /c/dataTest/node/Experiment1/output/all.csv /c/dataTest/node/Experiment1/bw/all.csv /c/dataTest/node/Experiment1/output/5.csv /c/dataTest/node/Experiment2/results/all_predict_5_result_2hnodes_100.csv 100,100
 
var il = require('./imageLearn.js');

var filenameInputReal = "/dataTest/node/Experiment2/output/all_real.csv";
var filenameTargetReal = "/dataTest/node/Experiment2/output/all_real_bw.csv";
var filenameNewDataReal = "/dataTest/node/Experiment1/output/5.csv";

var output = "/dataTest/node/Experiment2/results/all_predict_5_result_2hnodes_100.csv";
var method = "ml";

//1
var hiddenLayers = [100,100];
//il.main(filenameInputReal, filenameTargetReal, filenameNewDataReal, output, method, hiddenLayers);

var filenameInputRnd = "/dataTest/node/Experiment2/output/allrnd5.csv";
var filenameTargetRnd = "/dataTest/node/Experiment2/output/allrnd5_bw.csv";
var filenameNewDataRnd = "/dataTest/node/Experiment1/output/5.csv";

output = "/dataTest/node/Experiment2/results/all_rnd_predict_5_result_2hnodes_100.csv";

il.main(filenameInputRnd, filenameTargetRnd, filenameNewDataRnd, output, method, hiddenLayers);

//2
//node imageLearn.js /c/dataTest/node/Experiment2/output/allrnd5.csv /c/dataTest/node/Experiment2/output/bw/allrnd5.csv /c/dataTest/node/Experiment1/output/5.csv /c/dataTest/node/Experiment2/results/all_rnd_predict_5_result_1Hnode_10000.csv 10000
/*hiddenLayers = [10000];
filenameInputReal = "/dataTest/node/Experiment2/output/all_real.csv";
filenameTargetReal = "/dataTest/node/Experiment2/output/all_real_bw.csv";
filenameNewDataReal = "/dataTest/node/Experiment1/output/5.csv";

output = "/dataTest/node/Experiment2/results/all_predict_5_result_1Hnode_10000.csv";
il.main(filenameInputReal, filenameTargetReal, filenameNewDataReal, output, method, hiddenLayers);

filenameInputRnd = "/dataTest/node/Experiment2/output/allrnd5.csv";
filenameTargetRnd = "/dataTest/node/Experiment2/output/allrnd5_bw.csv";
filenameNewDataRnd = "/dataTest/node/Experiment1/output/5.csv";

output = "/dataTest/node/Experiment2/results/all_rnd_predict_5_result_1Hnode_10000.csv";

method = "ml";
hiddenLayers = [10000];
il.main(filenameInputRnd, filenameTargetRnd, filenameNewDataRnd, output, method, hiddenLayers);*/

