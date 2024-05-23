# Log Analyzer Library

## Description
A .NET library for analyzing log files.

## Features
- Search logs in directories
- Count unique errors per log file
- Count duplicated errors per log file
- Delete logs from a period
- Archive logs from a period
- Upload logs to a remote server
- Search logs by size
- Search logs per directory

## Setup
1. Clone the repository:
   ```sh
   git clone https://github.com/ToLuWaNiMe/Log-Analyzer-Library.git
   ```
2. Navigate to the project directory:
   ```sh
   cd Log-Analyzer-Library
   ```
3. Build the project:
   ```sh
   dotnet build
   ```

## Usage
1. Run the application:
   ```sh
   dotnet run
   ```
2. Open your web browser and navigate to `https://localhost:7247/swagger` to access the Swagger UI.

## API Documentation

### Swagger
This API uses [Swagger](https://swagger.io/) for documentation. Swagger UI allows you to explore and test the API endpoints interactively.

After running the application, open your web browser and navigate to `https://localhost:7247/swagger` to view the Swagger UI.

### Endpoints

Here is a summary of the available endpoints in the Log Analyzer Library API:

1. **Search Logs in Directories**
   - **URL**: `/api/logs/search`
   - **Method**: `GET`
   - **Description**: Search logs in specified directories.

2. **Count Unique Errors**
   - **URL**: `/api/logs/unique-errors`
   - **Method**: `GET`
   - **Description**: Count the number of unique errors per log file.

3. **Count Duplicated Errors**
   - **URL**: `/api/logs/duplicated-errors`
   - **Method**: `GET`
   - **Description**: Count the number of duplicated errors per log file.

4. **Delete Logs from a Period**
   - **URL**: `/api/logs/delete`
   - **Method**: `DELETE`
   - **Description**: Delete logs from a specified period.

5. **Archive Logs from a Period**
   - **URL**: `/api/logs/archive`
   - **Method**: `POST`
   - **Description**: Archive logs from a specified period.

6. **Upload Logs to Remote Server**
   - **URL**: `/api/logs/upload`
   - **Method**: `POST`
   - **Description**: Upload logs to a remote server.

7. **Search Logs by Size**
   - **URL**: `/api/logs/search-by-size`
   - **Method**: `GET`
   - **Description**: Search logs by size range.

8. **Search Logs by Directory**
   - **URL**: `/api/logs/search-by-directory`
   - **Method**: `GET`
   - **Description**: Search logs in a specific directory.

### Example Requests

You can use tools like [Postman](https://www.postman.com/) or Swagger UI to make requests to these endpoints.

Here is an example of a GET request to search logs in directories:

```sh
curl -X GET "http://localhost:5000/api/logs/search?directory=C:\Logs" -H "accept: application/json"
```

With this setup, your API documentation will be available at `https://localhost:7247/swagger` when you run your application.
