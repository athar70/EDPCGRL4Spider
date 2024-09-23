
# Arachnophobia Exposure Therapy using EDPCGRL

The project processes physiological signals to estimate stress levels and adapts the attributes of virtual spiders in a VR environment based on these stress estimates. The spider adaptation is done through two methods: rule-based and Reinforcement Learning (Q-Learning).

## Key Features
- **Physiological Signal Processing**: Reads and processes physiological signals (previously PPG signals from a Bitalino device, now EDA signals from a Biopac device) to estimate stress.
- **Spider Attribute Adaptation**: The system adapts spider attributes in the VR environment to elicit specific stress responses in users, using:
  - **Rule-Based Method**: Adapts spider attributes based on predefined rules and thresholds.
  - **Reinforcement Learning (Q-Learning)**: Uses Q-Learning to dynamically adjust the spider attributes based on stress levels.
- **Multi-threading**: Utilizes separate threads for reading and processing signals.
- **Database Integration**: Processed physiological data is saved into a database.
- **Unity Communication**: Communicates with Unity via text files to ensure stable interaction (previously used sockets, which were unstable).
- **Logging**: All runtime logs are stored in the `Logs` folder.
- **Constant Management**: `ManageCONST` writes constants to a file for Unity to read, ensuring consistent values.

## How to Run

1. Install all dependencies (see below).
2. Run the `RunCodes.py` file:
   ```bash
   python RunCodes.py
   ```

## Script Functionality

This script performs the following tasks:

- **EDA Processing**: Initializes one thread to read and process Electrodermal Activity (EDA) data, saving the processed results every second into the `Database` folder.
- **Constant Management**: Another thread writes constants to a file for Unity to read, ensuring that Unity has access to the necessary parameters for the VR environment.
- **Logging**: Execution logs and details are saved in the `Logs` folder for future reference.

## Folder Structure

- **Database**: Contains the processed physiological data files.
- **Logs**: Stores log files generated during runtime for debugging and analysis.


## Dependencies

- [Python3.7 +](https://www.python.org/downloads/)
- [NumPy](https://github.com/numpy/numpy)
- [pySerial](https://github.com/pyserial/pyserial)
- [PyBluez](https://github.com/pybluez/pybluez) (Not needed for macOS)
- [neurokit2](https://github.com/neuropsychology/NeuroKit.git)
- [bitalino](https://pypi.org/project/bitalino/)
- [pyzmq](https://github.com/zeromq/pyzmq.git)
- [gym](https://github.com/openai/gym.git)




