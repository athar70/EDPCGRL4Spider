# Data for Personalized Arachnophobia Exposure Therapy using EDPCGRL

## Overview

This repository contains human subject data from the experiment described in the paper, *"Spiders Based on Anxiety: How Reinforcement Learning Can Deliver Desired User Experience in Virtual Reality Personalized Arachnophobia Treatment."* The dataset includes Electrodermal Activity (EDA) sensor data for both anxious and relaxing environments, as well as Subjective Unit of Distress (SUDs) scale data.

## Contents


### Structure of the `SUDs.csv` File

This folder contains a CSV file named `SUDs.csv`, which provides SUDs data for the anxious environment. The anxiety levels were adapted using either the Reinforcement Learning (RL) method or the rule-based method, based on desired anxiety levels of 3 (low-anxiety) or 7 (high-anxiety). Data points were recorded once per second, and due to system performance issues, some data points were lost at the beginning and end of each anxious segment, leaving 136 data points per column. The file structure is as follows:

- **Columns**:
  - `PID_AdaptiveMethod_DesiredAnxietyLevel`: 
    - `PID`: Participant ID.
    - `AdaptiveMethod`: The method used (RL or Rule).
    - `DesiredAnxietyLevel`: The desired anxiety level (3 or 7).

### Structure of the `EDA_Anxious.csv` File

This file provides normalized EDA data specific to the anxious environment. The data was recorded at a rate of one data point per second, resulting in 140 data points per column. The structure mirrors that of the `SUDs.csv` file. Columns are formatted as:

- **Columns**:
  - `PID_AdaptiveMethod_DesiredAnxietyLevel`:
    - `PID`: Participant ID.
    - `AdaptiveMethod`: Adaptive method used (RL or Rule).
    - `DesiredAnxietyLevel`: Desired anxiety level (3 or 7).

### Structure of the `Tonic_Anxious.csv` and `Phasic_Anxious.csv` Files

These files contain the tonic and phasic components of the EDA signals for the anxious environment. The structure of these files is identical to that of `EDA_Anxious.csv`, with 140 data points per column.

- **Columns**:
  - `PID_AdaptiveMethod_DesiredAnxietyLevel`:
    - `PID`: Participant ID.
    - `AdaptiveMethod`: Adaptive method used (RL or Rule).
    - `DesiredAnxietyLevel`: Desired anxiety level (3 or 7).

### Structure of the `EDA_Relaxing.csv` File

This file contains normalized EDA data for the relaxing environment, with each column containing 120 data points. The columns are formatted as:

- **Columns**:
  - `PID_EDA_relax#`: 
    - `PID`: Participant ID.
    - `relax#`: Indicates if this was the first (`relax1`) or second (`relax2`) time the participant encountered the relaxing environment.
