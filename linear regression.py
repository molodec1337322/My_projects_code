import numpy as np
from numpy import linalg

speed = np.array([[1, 60], [1, 50], [1, 75]])
slowing_path = np.array([[10], [7], [12]])

X = speed
Y = slowing_path

step1 = X.T.dot(X)
step2 = linalg.inv(step1)
step3 = step2.dot(X.T)
b = step3.dot(Y)

print(b)
