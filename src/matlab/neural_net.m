%for ref
%FUNCTION S = sigmoid(x)
%  %s = 1 / (1+E^(-x));
%  s = sigmf(x);
%end  


#Neural Network basic idea only feeding forward
clear all;
clear; 

noHiddenNodes = 3;
noOutputs = 1;
maxCount = 1000;

%Input Matrix
X = [[0,0,1]; [0,1,1]; [1,0,1]; [1,1,1]];

%Output Matrix
Y = [[0]; [1]; [1]; [0]];

%Weights
[m,n] = size(X);
W = rand(noHiddenNodes, n);
U = rand(noOutputs, noHiddenNodes);

%run 1000 times
for i=1:m %no inputs for now
  %First Layer
  currentInput = [X(i,:), -1];
  a = dot(transpose(W), currentInput);
    
  %Second Layer
  pastLayer = [W, -1];
  b = dot(transpose(U), pastLayer);
  
  disp('Result for: ' + i);
  disp('W');
  disp(W);
  disp('U');
  disp(U);
  disp('a');
  disp(a);
  disp('output: '+ b);
  
end