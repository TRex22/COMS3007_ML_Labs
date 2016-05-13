 #!/bin/bash
 node imageLearn.js /c/dataTest/node/Experiment1/output/all.csv /c/dataTest/node/Experiment1/bw/all.csv /c/dataTest/node/Experiment1/output/5.csv /c/dataTest/node/Experiment2/results/all_predict_5_result_2hnodes_100.csv 100,100
 node imageLearn.js /c/dataTest/node/Experiment2/output/allrnd5.csv /c/dataTest/node/Experiment2/output/bw/allrnd5.csv /c/dataTest/node/Experiment1/output/5.csv /c/dataTest/node/Experiment2/results/all_rnd_predict_5_result_1Hnode_10000.csv 10000


 node imageLearn.js /c/dataTest/node/Experiment1/output/all.csv /c/dataTest/node/Experiment1/bw/all.csv /c/dataTest/node/Experiment1/output/5.csv /c/dataTest/node/Experiment2/results/all_predict_5_result_2hnodes_500.csv 500,500
 node imageLearn.js /c/dataTest/node/Experiment2/output/allrnd5.csv /c/dataTest/node/Experiment2/output/bw/allrnd5.csv /c/dataTest/node/Experiment1/output/5.csv /c/dataTest/node/Experiment2/results/all_rnd_predict_5_result_2Hnode_10000_50.csv 10000,50

  node imageLearn.js /c/dataTest/node/Experiment1/output/all.csv /c/dataTest/node/Experiment1/bw/all.csv /c/dataTest/node/Experiment1/output/5.csv /c/dataTest/node/Experiment2/results/all_predict_5_result_3hnodes_5_10_5.csv 5,10,5
 node imageLearn.js /c/dataTest/node/Experiment2/output/allrnd5.csv /c/dataTest/node/Experiment2/output/bw/allrnd5.csv /c/dataTest/node/Experiment1/output/5.csv /c/dataTest/node/Experiment2/results/all_rnd_predict_5_result_3hnodes_5_10_5.csv 5,10,5