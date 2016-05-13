#!/bin/bash
#node mnist_train.js "output" lr hLayer1 hLayer2 hLayer3  --max-old-space-size=4096
node mnist_train.js "output" 0.3 100 --max-old-space-size=4096 > Experiment1_lr0.3_1Layer100.txt
node mnist_train.js "output" 0.3 50 50 --max-old-space-size=4096 > Experiment2_lr0.3_2Layer50_50.txt