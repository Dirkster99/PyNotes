
Andrew Ray
https://www.youtube.com/watch?v=XrpSRCwISdk

# Pandas DataFrames vs. PySpark DataFrames

# Load CSV

## Pandas
`df = pd.read_csv("mtcars.csv")`

## PySpark
`df = spark.read \
   .opions(header=True, inferSchema=True) \
   .csv("mtcars.csv"))`

# IF THINGS GO WRONG

- Don't panic!
- Read the error
- Research it
- Search/Ask StackOverflow (tag apache-spark)
- Search/Ask the userlist: user@spark.apache.org
- Find a bug? Make JIRA ticket:  
  https://issues.apache.org/jira/browser/SPARK/

# View DataFrame

## Pandas
`df`

## PySpark
`df.show()`

# View DataFrame

## Pandas
`df`

`df.head(10)`

## PySpark
`df.show()`

`df.show(10)`

# Columns and Data Types

## Pandas
`df.columns`

`df.dtypes`

## PySpark
`df.columns`

`df.dtypes`

# Rename Columns

## Pandas
`df.columns = ['a', 'b', 'c']`

`df.rename([columns = {'old': 'new'})`

## PySpark
`df.toDF('a', 'b', 'c')`

`df.withColumnRenamed('old', 'new')`

# Drop Column

## Pandas
`df.drop('mpg', axis=1)`

## PySpark
`df = df.drop(mpg)`

# Filtering

## Pandas
`df[df.mpg < 20]`

## PySpark
`df[df.mpg < 20]`

# Filtering

## Pandas
`df[df.mpg < 20]`

`df[(df.mpg < 20) & (df.cyl == 6)]`

## PySpark
`df[df.mpg < 20]`

`df[(df.mpg < 20) & (df.cyl == 6)]`

# Add Column

## Pandas
`df['gpm'] = 1 / df.mpg`

## PySpark
`df.withColumn('gpm'], 1 / df.mpg)`

## Note

The result of the above PySpark expression is a new dataframe object. You need to assign it to a new
or exsiting `df` variable in order to query/process the value later on.

Be careful with division by zero because its NULL in PySpark wheras its infinity in Pandas.

# Check for NULL values in a column

## PySpark

```Python
import pyspark.sql.functions as F 

data.where(F.col("Label").isNull()).show()
```

## Note
The result is a DataFrame with the rows where column label has a value of **NULL** (DataFrame may not have any rows if there are no null values to be had.).

# Fill Null Values

## Pandas
`df.fillna(0)` (Many more options)

## PySpark
`df.fillna(0)`

## Note

The result of the above PySpark expression is a new dataframe object. You need to assign it to a new
or exsiting `df` variable in order to query/process the value later on.

# Compose new Conditional Column Value from 2 Existing Columns

## Summary
The value in the column 'filtered' was created through a filter condition
from the 'words' column. Some 'filtered' values ar empty since the filter
removed in some cases all values from 'words' column. The issue to solve
here was to compose another **non-null** column value out of 'filtered'
and 'words' values.

## PySpark
```Python
# Column filtered contains an array of strings
# Column words contains an array of strings
# -> New Column: FilteredWords contains a filtered value if that array is not empty
#                otherwise FilteredWords is assigned the value from 'words' column
#                ('words' column values are always present)
#
NewDF = DF.withColumn('FilteredWords',   \
  F.when(F.size('filtered') == 0, DF['words']) \
   .otherwise(DF['filtered']))
```


# Aggregation

## Pandas
```Python
df.groupby(['cyl', 'gear'])  \
   .agg({'mpg': 'mean', 'disp': 'min'})
```

## PySpark
```Python
df.groupby(['cyl', 'gear'])  \
   .agg({'mpg': 'mean', 'disp': 'min'})
```

# Standard Transformations

## Pandas
```Python
import numpy as np
df['logdisp'] = np.log(df.disp)
```

## PySpark
```Python
import pyspark.sql.functions as F
df.withColumn('logdisp'. F.log(df.disp))
```

# Performance Tip: Keep it in the JVM whenever you can

```Python
import pyspark.sql.functions as F

 abs,acos,add_months,approxCountDistinctD,approx_count_distinct,array,array_contains,asc,ascii,asin,atan,atan2,avg
,base64,bin,bitwiseNOT,broadcast,bround
,cbrt,ceil,coalesce,col,collect_list,collect_set,column,concat,concat_ws,conv,corr,cos,cosh,count,countDistinct,covar_pop,covar_samp
,crc32,create_map,cume_dist,current_date,current_timestamp
,date_add,date_format,date_sub,datediff,dayofmonth,dayofyear,decode,degrees,dense_rank,desc
,encode,exp,explode,expm1,expr
,factorial,first,floor,format_number,format_string,from_json,from_unixtime,from_utc_timestamp
,get_json_object,greatest,grouping,grouping_id
,hash,hex,hour,hypot
,initcap,input_file_name,instr,isnan,isnull
,json_tuple
,kurtosis
,lag,last,last_day,lead,least,length,levenshtein,lit,locate,log,log10,log1p,log2,lower,lpad,ltrim
,max,md5,mean,min,minute,monotonically_increasing_id,month,months_between
,nanvl,next_day,ntile
,percent_rank,posexplode,pow
,quarter
,radians,rand,randn,rank,regexp_extract,regexp_replace,repeat,reverse,rint,round,row_number,rpad,rtrim
,second,sha1,sha2,shiftLeft,shiftRight,shiftRightUnsigned,signum,sin,sinh,size,skewness,sort_array
,soundex,spark_partition_id,split,sqrt,stddev,stddev_pop,stddev_samp,struct,substring,substring_index
,sum,sumDistinct
,tan,tanh,toDegreesD,toRadiansD,to_date,to_json,to_utc_timestamp,translate,trim,trunc
,udf,unbase64,unhex,unix_timestamp,upper
,var_pop,var_samp,variance
,weekofyear,when,window
,year
```

# Row Conditional Statements

## Pandas
```Python
df['cond']=df.apply(lamda r:
  1 if r.mpg > 20 else 2 if r.cycl == 6 else 3,
  axis=1)
```

## PySpark
```Python
# Build a new DataFrame column from a condition over existing dataframe values
# Old and new DataFrame have the same numbers of rows
import pyspark.sql.functions as F

df.withColumn('cond',   \
  F.when(df.mpg > 20, 1) \
   .when(df.cyl == 6, 2) \
   .otherwise(3))
```

# Python when Required

## Pandas
`df['disp1'] = df.disp.apply(lambda x: x+1)`

## PySpark
```Python
import pyspark.sql.functions as F
from payspark.sql.types import DoubleType

fn = F.udf(lambda x: x+1, DoubleType())
df.withColumn('disp', fn(df.disp))
```

# Merge/Join DataFrames

## Pandas
```Python
left.merge(right, on='key')
left.merge(right, left_on='a', right_on='b')
```

## PySpark
```Python
left.join(right, on='key')
left.join(right, left.a == right.b)
```

# Pivot Table

## Pandas
```Python
pd.pivot_table(df, values='D', \
  index=['A','B'], columns=['C'], \
  aggfunc=np.sum)
```

## PySpark
`df.groupBy("A", "B").pivot("C").sum("D")`

# Summary Statistics

## Pandas
`df.describe()`

## PySpark
`df.describe().show` (only count, mean standard deviation, min, max)

```Python
df.selectExpr(
   "percentile_approx(mpg, array(.25, .5, .75)) as mpg"
   ).show()
```

# Histogram

## Pandas
`df.hist()`

## PySpark
`df.sample(False, 0.1).toPandas().hist()`

# SQL

## Pandas
`n/a`

## PySpark
```Python
df.createOrReplaceTempView('foo')
df2 = spark.sql('select * from foo')
```

# Make Sure To

- Use `pyspark.sql.functions` and other build in functions 
- Use the same version of python and packages on cluster as driver
- Check out the UI at http://localhost:4040/
- Learn about SSH port forwarding
- Check out Sparl MLlib
- RTFM: https://spark.apache.org/docs/latest/

# Things not to do

- Try to iterate through rows
- Hard code a master in your driver
  - Use spark-submit for that

- df.toPandas().head()
  - instead do: df.limit(5).toPandas()

# Q & A

## Visualization

Databricks has forms of visualization if you are running Spark DataFrames on databricks.

## SciKitLearn vs Spark

Use Spark when memory forces you too.
