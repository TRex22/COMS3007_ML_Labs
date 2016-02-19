1.  Code up method called percept that models a perceptron with m -inputs and 1-output.
Your  method  should  take  as  arguments  a  weight  array  and  an  input  array  and  return  1  if  the
perceptron fires and 0 if it does not fire.  The weight array should be augmented, i.e., have length
m + 1, where the last value is the threshold value.  The input array should also be augmented, i.e.,
have length m + 1, where the m + 1st entry is -1

Test your code on some simple inputs
For example, take weight array W = [2, -1, 1]  and input array X = [1.5 , 2.5 , -1].

Test your code on some simple inputs.
For example, take weight array W = [2, −1, 1] and input array X = [1.5, 2.5, −1].
The output should be 0 because (2)(1.5) + (−1)(2.5) + (1)(−1) = −0.5 ≤ 0.
For weight array W = [2, −3, 1] and input array X = [3, 1, −1],
the output should be 1 because (2)(3) + (−1)(2.5) + (1)(−1) = 2.5 > 0.