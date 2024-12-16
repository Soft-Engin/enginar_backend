# enginar_backend

# BackEngin Project

This solution consists of one main project, **BackEngin**, and two supporting subprojects: **DataAccess** and **Models**.

## Setup

To work with collaboratively without accidents this command should be run in the repository folder before start to work on this project. It enables custom hooks to prevent accidents and has improvements to maintain code quality.

```sh
git config core.hooksPath .githooks
```

## Using Docker

To run the whole project you should first edit the `.env.example` file and copy into `.env` file then edit according your preferred settings. Then you can run the project with given command in the project folder.

```sh
# If you run the project first time or made update and didn't built the docker images.
docker compose up -d --build
```

```sh
# If you didn't change anything on the source code and already built the docker images.
docker compose up -d
```

### Key Update: 
* `FindAsync` function: has been added to the base repository in the DataAccess project. This method allows filtering records based on specified conditions, improving flexibility in data querying.
* `GetPaginatedAsync` function: Added to the base repository in the DataAccess project. This method facilitates paginated data retrieval by accepting `page number` and `size` as parameters and returning the relevant data subset efficiently.
* `Pagination DTO`: Introduced the `PaginatedResponseDTO<T>` class to standardize paginated responses. This DTO includes properties such as `Items`, `TotalCount`, `PageNumber`, and `PageSize` to provide detailed pagination metadata alongside the data.


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

