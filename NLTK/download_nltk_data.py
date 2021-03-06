# -*- coding: utf-8 -*-
"""Download_NLTK_Data.ipynb

Automatically generated by Colaboratory.

Original file is located at
    https://colab.research.google.com/drive/1Z9wFvGKD-KccDuBbmiR_6z2a3iB9Y4LL

#Overview
This script shows how seperate models and other NTLK items can be downloaded via zip file using Google Colab environment.
"""

import nltk

nltk.download('stopwords')
nltk.download('wordnet')
nltk.download('punkt')
nltk.download('averaged_perceptron_tagger')

!ls /root/nltk_data/

!ls /root/nltk_data/tokenizers/punkt

nltk.download('popular')

!ls /root/nltk_data/

#!rm nltk_data.zip
!cd /root/nltk_data;zip -r nltk_data .

!ls /root/nltk_data/

# Download file from virtual CoLab environment to local computer
from google.colab import files
files.download('/root/nltk_data/nltk_data.zip')

