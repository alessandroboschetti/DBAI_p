# AskDB Desktop

AskDB Desktop is a Windows Forms application that enables users to interact with SQL Server, MySQL, and PostgreSQL databases using natural language. The application leverages OpenAI's GPT models to translate user requests into SQL queries, execute them, and display results in a user-friendly interface. It also provides features for mapping table and column aliases, managing user settings, and exporting query results.

## Features

- **Natural Language to SQL**: Enter questions or requests in plain English and receive SQL queries and results.
- **Multi-Database Support**: Connect to SQL Server, MySQL, or PostgreSQL databases.
- **User Authentication**: Register and log in to manage personal settings and mappings.
- **Table & Column Mapping**: Assign friendly aliases to tables and columns for more intuitive querying.
- **Query Modes**: Choose between read-only or read/write query generation.
- **Sample Data & Schema Extraction**: Optionally include sample data in schema descriptions for better AI context.
- **Export Results**: Export query results to CSV/Excel.
- **Settings Management**: Store API keys, default database type, and preferences per user.
- **Query Help & Optimization**: Get AI-powered help and suggestions for improving SQL queries.

## Installation

1. **Requirements**
   - Windows 10 or later
   - .NET Framework 4.8.1
   - Visual Studio 2022 (for development)
   - Access to a SQL Server, MySQL, or PostgreSQL database
   - OpenAI API key

2. **Setup**
   - Clone the repository.
   - Open the solution in Visual Studio 2022.
   - Restore NuGet packages (MetroFramework, MySql.Data, Npgsql, Newtonsoft.Json).
   - Build and run the project.

3. **Database Preparation**
   - Ensure your user database (for authentication and settings) is available and connection strings are configured.
   - The application expects tables for users, roles, user settings, table/column mappings, and aliases.

## Usage

1. **Login/Register**
   - Start the application and register a new user or log in with existing credentials.

2. **Connect to Database**
   - Enter your target database connection string and select the database type and mode.

3. **Map Tables & Columns**
   - Enable/disable tables for querying and assign friendly aliases as needed.

4. **Query**
   - Use the chat interface to ask questions in natural language.
   - View, export, or further analyze results.

5. **Settings**
   - Access user settings to manage your OpenAI API key, default database type, and preferences.

## Configuration

- **API Key**: Store your OpenAI API key in the settings dialog.
- **User Database Connection**: The application uses a separate connection string for user authentication and settings (default: `Server=localhost\SQLEXPRESS;Database=AskDBinfo;Trusted_Connection=True;`).

## Security Notes

- Passwords are stored in plain text in the sample implementation. For production, use secure password hashing.
- API keys are stored per user; protect your OpenAI credentials.

## Dependencies

- [MetroFramework](https://github.com/thielj/MetroFramework)
- [MySql.Data](https://www.nuget.org/packages/MySql.Data)
- [Npgsql](https://www.npgsql.org/)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)

## License

This project is provided for educational and demonstration purposes. See [LICENSE](LICENSE) for details.

## Acknowledgments

- OpenAI for GPT models
- MetroFramework for modern WinForms UI
