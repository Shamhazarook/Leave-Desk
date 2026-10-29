# Leave Desk – Leave Management System

## 1. Project Overview
Leave Desk is a web-based Leave Management System developed to simplify employee leave requests and administrative approval. It replaces manual leave management with a centralized system.

## 2. Objectives
- Allow employees to submit leave requests.
- Allow administrators to approve or reject requests.
- Store employee and leave information in a MySQL database.
- Validate leave dates and request details.
- Track leave request statuses.

## 3. Technologies Used
- **Frontend:** HTML, CSS, JavaScript
- **Backend:** ASP.NET Core Web API, C#
- **Database:** MySQL
- **ORM:** Entity Framework Core
- **API Documentation and Testing:** Swagger
- **Development Environment:** Visual Studio Code

## 4. Main Features
- Employee management
- Leave request submission
- Annual, Casual, and Medical leave types
- Automatic Pending status for new requests
- Admin approval and rejection
- Rejection reason recording
- Date and input validation
- Database persistence using MySQL

## 5. Database
Database name: `leave_management_db`

Main tables:
- `Employees` – stores employee details.
- `LeaveRequests` – stores leave request details, dates, reasons, and statuses.

## 6. API Endpoints

| Method | Endpoint                   | Description               |

| GET    | `/api/employees`           | Retrieve employees        |
| POST   | `/api/employees`           | Create an employee        |
| GET    | `/api/leaves`              | Retrieve leave requests   |
| POST   | `/api/leaves`              | Submit a leave request    |
| PATCH  | `/api/leaves/{id}/approve` | Approve a pending request |
| PATCH  | `/api/leaves/{id}/reject`  | Reject a pending request  |

## 7. How to Run the Project
1. Install the .NET SDK and MySQL Server.
2. Open the project folder in Visual Studio Code.
3. Configure the MySQL connection string in the project's configuration.
4. Ensure the `leave_management_db` database and required tables are available.
5. Open a terminal in the project folder.
6. Run:

   ```bash
   dotnet restore
   dotnet run
   ```

7. Open the local URL displayed in the terminal.
8. Open `/swagger` on that URL to explore the API endpoints.

## 8. Validation and Error Handling
The API validates leave dates, employee existence, leave types, and rejection reasons. It also prevents approval or rejection of requests that are no longer pending and returns appropriate HTTP error responses.

## 9. Testing
The application should be tested for successful leave submission, approval, rejection, invalid inputs, missing employee records, and persistence of changes after refreshing the application.

## 10. Conclusion
Leave Desk provides a centralized approach to managing employee leave requests. It combines a web interface, REST API, and MySQL database to improve the efficiency and reliability of leave management.
