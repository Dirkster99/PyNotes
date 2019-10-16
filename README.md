# PyNotes

## May the Spark :star: be with you

My notebook on using Python with Jupyter Notebook, PySpark and other well known machine learning frameworks.

- [PySpark Versus Panda DataFrames](PySpark_VS_Panda_DataFrame/PySpark.md)

- [Debug and Optimize PySpark Pipelines](DebugPySpark/Readme.md)

- [How to use Dataframe in PySpark with SQL](https://www.jie-tao.com/how-to-use-dataframe-in-pyspark/)

- [PySpark SQL Cheat Sheet](https://www.datacamp.com/community/blog/pyspark-sql-cheat-sheet)

- [PySpark Reference Docs](http://spark.apache.org/docs/2.1.0/api/python/pyspark.sql.html)

# Write a PySpark Array of Strings as String into ONE Parquet File

## Use Case

You can use a PySpark Tokenizer to convert a string into tokens and apply machine learning algorithms on it. The code snippets below might be useful if you want to inspect the result of the tokenizer (an array of unicode strings) via csv file (saved in a Parquet environment).

## Code
```Python
df.select("words").show()
```

```
+--------------------+
|               words|
+--------------------+
| [I, am, looking,...|
|     [not, today,...|
|   [but, tomorrow...|
+--------------------+
```



```Python
# Select column with array of words into seperate DataFrame
dfSave = df.select("words")

#dfSave.printSchema()
# root
#  |-- words: array (nullable = true)
#  |    |-- element: string (containsNull = true)
#

import pyspark.sql.functions as F

# Convert Array of unicode strings into a string using PySpark's function
# https://stackoverflow.com/questions/38924762/how-to-convert-column-of-arrays-of-strings-to-strings
dfSave = dfSave.withColumn("words_str", F.concat_ws(" ", dfSave["words"]))

# drop arrays of strings column
dfSave = dfSave.drop("words")

# Write dataframe with string into ONE parquet file
# https://stackoverflow.com/questions/42022890/how-can-i-write-a-parquet-file-using-spark-pyspark
# https://stackoverflow.com/questions/36162055/pyspark-spit-out-single-file-when-writing-instead-of-multiple-part-files
dfSave.coalesce(1).write.format('csv').save('/home/me/tokenized.csv')
```

# Neural Networks

- [A Perceptron demo program](NeuralNetworks/00_PerceptronDemo/Readme.md)

