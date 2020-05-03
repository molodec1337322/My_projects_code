import numpy as np
from numpy import linalg
import matplotlib.pyplot as plt
#from urllib.request import urlopen

confirmed = np.array([[1, 3472653], [1, 2994885], [1, 2555266], [1, 2162516], [1, 1779786], [1, 0]])
deaths = np.array([[243998], [206917], [177412], [145430], [108740], [0]])

X = confirmed
Y = deaths

b = linalg.inv(X.T.dot(X)).dot(X.T).dot(Y)
epsilon = Y - X.dot(b)
err = epsilon.T.dot(epsilon)


x = np.arange(5000000)
y = b[0] + b[1] * x

fig = plt.figure()
for i in range(deaths.size):
    plt.scatter(confirmed[i][1], deaths[i])

plt.plot(x, y, label='line', color='red')

plt.show()

print(b, err)
