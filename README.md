# Client-Server App - Second Project of System Programming 
A file named proba.txt has been added on the server side for testing the correct functionality of the program.

Task 13:
 Create a web server that counts words in a specified file. Only words that start with a capital letter and contain more than 5 letters should be counted.
 All requests to the server are sent via a web browser using the GET method. The request includes the filename as a parameter. The server should:
 1. Accept the request.
 2. Search the root folder and all its subfolders for the requested file.
 3. Count the words in the file according to the criteria above.
 4. If the requested file does not exist, the server should return an error message to the user.
    
 Example server request:
   http://localhost:5050/fajl.txt

The main difference between the first and second project is synchronous vs. asynchronous execution.
First project utilises blocking methods. The server thread waits while reading files and sending responses, which can limit scalability.
Second project uses async/await.
File reading and response sending are non-blocking, allowing the server to handle multiple requests concurrently and more efficiently.

Faculty: Faculty of Electronics, University of Niš  
Semester: 6th  
Course: System Programming  
Program: Computer Science and Informatics
