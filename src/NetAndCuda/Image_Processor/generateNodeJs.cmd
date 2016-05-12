@echo off
ImageProcessor batch c:\dataTest\input c:\dataTest\node\Experiment2\output scale csv 100 100
ImageProcessor batch c:\dataTest\input c:\dataTest\node\Experiment2\output scale png 100 100

rem ImageProcessor batch c:\dataTest\output c:\dataTest\node\output convert rgb csv 100 100
ImageProcessor batch c:\dataTest\node\Experiment2\output c:\dataTest\node\bw convert bw csv 100 100
ImageProcessor batch c:\dataTest\node\Experiment2\output c:\dataTest\node\bw convert bw png 100 100