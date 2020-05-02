import numpy as np
from numpy import linalg
#from urllib.request import urlopen

confirmed = np.array([[1, 3472653], [1, 2994885], [1, 2555266], [1, 2162516], [1, 1779786]])
deaths = np.array([[243998], [206917], [177412], [145430], [108740]])

X = confirmed
Y = deaths

b = linalg.inv(X.T.dot(X)).dot(X.T).dot(Y)
epsilon = Y - X.dot(b)
err = epsilon.T.dot(epsilon)

print("Введите количество: ")
user_confirmed = int(input())

print(b[0] + user_confirmed * b[1])
