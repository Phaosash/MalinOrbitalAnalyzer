# Satellite Data Processing Project – Malin Space Science Systems (MSSS) 
 
## Overview 
This project is developed for Malin Space Science Systems to process satellite sensor data using .NET Multi-platform App UI (WPF) with C#. The application performs sorting and searching on data collected from two sensors aboard a sun-synchronous satellite. It integrates with a custom DLL (`Galileo.dll`) and supports performance tracking of algorithms. 
 
## Key Features 
- Load raw data from `Galileo.dll` 
- Store data in `LinkedList<double>` structures 
- Perform Selection and Insertion sort 
- Conduct Recursive and Iterative binary search 
- Display processing times for each operation 
- View sensor data in ListBox and ListView formats 
- Adjustable Mean (Mu) and Standard Deviation (Sigma) values 
 
## Technologies Used 
- .NET 9 (WPF) 
- C# 
- Galileo.dll (external dependency) 
- GitHub for version control and project tracking 
 
## Installation 
1. Clone the repository: 
    ```bash 
    git clone https://github.com/YourUsername/Satellite-Data-Processing-MSSS.git 
    ``` 
2. Open the solution in Visual Studio 2022 or later. 
3. Restore NuGet packages (if any). 
4. Add the provided `Galileo.dll` to the `/lib` folder and reference it in the project. 
5. Build and run the application. 
 
## How to Use 
1. Set values for Mu and Sigma. 
2. Click “Load Data” to generate and load sensor readings. 
3. Click “Selection Sort” or “Insertion Sort” to sort data. 
4. Enter a number in the search box and choose a binary search method. 
5. View search results and processing times. 
 
## Project Structure 
/src → C# source files and WPF XAML 
/docs → Design documents and reports 
/tests → Unit and integration tests 
/lib → Galileo.dll and other dependencies 
/assets → Screenshots, diagrams, mockups 
/build → Compiled output 

 
## Contributors 
- Lead Developer: Michael McKIE
- Organisation: CITE Managed Services 
- Client: Malin Space Science Systems 
 
## License 
This project is proprietary and developed for educational and contractual purposes. © 2025 MSSS & CITE.
