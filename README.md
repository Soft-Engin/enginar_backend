# enginar_backend

# BackEngin Project

This solution consists of one main project, **BackEngin**, and two supporting subprojects: **DataAccess** and **Models**.

## Setup

To work with collaboratively without accidents this command should be run in the repository folder before start to work on this project. It enables custom hooks to prevent accidents and has improvements to maintain code quality.

```sh
git config core.hooksPath .githooks
```

## Project Structure

### 1. **BackEngin (Main Project)**

   - **Purpose**: The main Web API project that contains controllers and API endpoints.
   - **Controllers**: API code is defined here, with an initial example provided in the `WeatherForecast` controller.
   - **API Documentation**: Swagger is integrated to document and test the API endpoints.

### 2. **Models (Subproject)**

   - **Purpose**: Contains ORM classes that map to database tables.
   - **Responsibilities**: Manages the structure of the database entities, making this project essential for database-related operations and management.

### 3. **DataAccess (Subproject)**

   - **Purpose**: Manages data interaction, using the Repository and Unit of Work patterns for improved data handling.
   - **Repository Pattern**: Implements a base repository with essential methods like `GetAllAsync`, `GetByIdAsync`, `AddAsync`, `Update`, and `Remove`.
   - **Unit of Work**: Provides a coordinated way to work with multiple repositories, ensuring efficient and organized data transactions.
   - **Database Connection**: Currently configured to connect to a local database on `localhost` for development and testing purposes. This will later be updated to a production-ready host as needed.

