'use strict'
var ml = require('machine_learning');
var brain = require("brain");

var parser = require('data4node').babelFish.csv;
var hactar = require('hactarjs');

var fs = require('fs')

//node --max_old_space_size=4096 imageLearn.js /c/dataTest/node/output/1.csv /c/dataTest/node/bw/1.csv /c/dataTest/node/output/2.csv /c/dataTest/node/result/2_result.csv
var filenameInput = process.argv[2];
var filenameTarget = process.argv[3];
var filenameNewData = process.argv[4];
var output = process.argv[5];

//main script here

main();

function main(){
	var noNodes = 0;
	var inputData;
	fs.readFile(filenameInput, 'utf8', function(err, data) {
		if (err) throw err;
		 inputData = parseCSVImage(data);
		
		var targetData;
		fs.readFile(filenameTarget, 'utf8', function(err, data) {
			if (err) throw err;
			targetData = parseCSVImage(data);
			
			var newData;
			fs.readFile(filenameNewData, 'utf8', function(err, data) {
				if (err) throw err;
				newData = parseCSVImage(data);
				noNodes = inputData.pixels[0].length;
				var imageSet = {"inputData": inputData, "targetData": targetData, "newData": newData};

				run_ml_network_whole_image_set(imageSet, noNodes, output);
				
				//console.log(JSON.stringify(newData));
			});
		});	
	});	
}

function run_ml_network_whole_image_set(imageSet, noNodes, output){
	var beginTime = new Date();
	console.log("Started: "+beginTime+"\n");

	var x = imageSet.inputData.pixels;
	var y = imageSet.targetData.pixels;
	 
	var mlp = new ml.MLP({
	    'input' : x,
	    'label' : y,
	    'n_ins' : noNodes,
	    'n_outs' : noNodes,
	    'hidden_layer_sizes' : [25, 25]
	});
	 
	mlp.set('log level',1); // 0 : nothing, 1 : info, 2 : warning.
	 
	mlp.train({
	    'lr' : 0.6,
	    'epochs' : 20000
	});
	 
	a = imageSet.newData.pixels;
	
	var result = mlp.predict(a);
	
	var endTime = new Date();
	console.log("Ended: "+endTime+"\n");

	var outputStr = "beginTime: "+ beginTime+" endTime: "+endTime+"\n"+result;
	hactar.saveFile(outputStr, output);

	console.log(result+"\n");
}

//parse ImageProessor 0.1.1 csv format (first line is info, then each line is RGB)
function parseCSVImage(csvFile){
	// babelFish.parseToDataObj
	//remove first line
	var lines = csvFile.split('\n');
	var topLine = lines[0];
	lines.splice(0,1);
	var pixels = parser.parseToDataObj(lines.join('\n'));

	var imageObj = {pixels: pixels, topLine: topLine};
	return imageObj;
}


//perhaps do see later
//open .dat files 
//see image processor project in NetAndCuda
function openDatImageFile(){

}

function saveDatImageFile(data){

}

