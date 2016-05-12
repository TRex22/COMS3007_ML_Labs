@echo off
ImageProcessor batch C:\dataTest\node\Experiment2\inputs\real c:\dataTest\node\Experiment2\output\real scale csv 100 100
ImageProcessor batch C:\dataTest\node\Experiment2\inputs\real c:\dataTest\node\Experiment2\output\real scale png 100 100

rem ImageProcessor batch c:\dataTest\output c:\dataTest\node\output convert rgb csv 100 100
ImageProcessor batch c:\dataTest\node\Experiment2\output\real c:\dataTest\node\Experiment2\output\bw\real convert bw csv 100 100
ImageProcessor batch c:\dataTest\node\Experiment2\output\real c:\dataTest\node\Experiment2\output\bw\real convert bw png 100 100


ImageProcessor batch C:\dataTest\node\Experiment2\inputs\rnd c:\dataTest\node\Experiment2\output\rnd scale csv 100 100
ImageProcessor batch C:\dataTest\node\Experiment2\inputs\rnd c:\dataTest\node\Experiment2\output\rnd scale png 100 100

ImageProcessor batch c:\dataTest\node\Experiment2\output\rnd c:\dataTest\node\Experiment2\output\bw\rnd convert bw csv 100 100
ImageProcessor batch c:\dataTest\node\Experiment2\output\rnd c:\dataTest\node\Experiment2\output\bw\rnd convert bw png 100 100