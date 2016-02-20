//Jason Chalom Feb 15 2016
'use strict';

// print process.argv
/*process.argv.forEach(function (val, index, array) {
  console.log(index + ': ' + val);
});*/

var exposed = {
  percept: percept,
  trainingDay: trainingDay
};
module.exports = exposed;

/*while (stopping condition not satisfied)
    for (k = 0; k < N; k++)
        y = percept(W;X[k])
        for (i = 0; i < m + 1; i++)
            W[i] = W[i] + n * (T[k] - y) * X[k][i]*/

function PerceptLearn(mInputs){
     var count = 0;
     while (count < mInputs.maxCount){ //stopping condition
        for (var k=0;k<mInputs.N;k++){
            var y=percept(
            {   weights: W,
                X: X[k]
            });
            var m =mInputs.W.length;
            for (var i=0;i<m+1;i++){ //check m+1
                W[i] = W[i] + mInputs.n * (T[k] - y) * X[k][i];
            }
        }
     }
}

function percept(mInputs){
    //if w1X1+w2X2...-$ >0 return 1
    //if W1X1+W2X2...-$ <=0 return 0
    //using augemented matrix
    var sum = 0;
    
    for (var i =0; i<mInputs.weights.length; i++){
        sum += mInputs.X[i] * mInputs.weights[i];
    }
    if (sum > 0){
        return 1;
    }
    else if (sum <= 0){
        return 0;
    }
}