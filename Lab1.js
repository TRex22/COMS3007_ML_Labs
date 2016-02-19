//Jason Chalom Feb 15 2016
'use strict';
console.log('COMS3007: Machine LEarning');
console.log('Lab 1: perceptron');

// print process.argv
/*process.argv.forEach(function (val, index, array) {
  console.log(index + ': ' + val);
});*/

var exposed = {
  percept: percept
};
module.exports = exposed;


function percept(mInputs){
    //if w1X1+w2X2...-$ >0 return 1
    //if W1X1+W2X2...-$ <=0 return 0
    var sum = 0;
    var arrLength = mInputs.weights.length;
    
    for (var i =0; i<arrLength-1; i++){
        sum += mInputs.X[i] * mInputs.weights[i];
    }
    if (sum > mInputs.weights[arrLength-1]){
        return 1;
    }
    else if (sum <= mInputs.weights[arrLength-1]){
        return 0;
    }
}