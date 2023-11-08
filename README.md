
# Google Calendar Event Manager

Google Calendar Event Manager is a .NET Core API-based web application that allows users to manage their Google Calendar events. With this application, users can create, view, and delete events from their Google Calendar. It simplifies the interaction with the Google Calendar API and provides a seamless way to work with calendar events.



## Table of Contents
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [Configuration](#configuration)
- [API Endpoints](#api-endpoints)
- [Authentication](#authentication)
- [Pagination](#pagination)
- [Contributing](#contributing)
- [License](#license)
## Features

- Create new events in the user's Google Calendar.
- View existing events from the user's Google Calendar.
- Delete events from the user's Google Calendar.
- Secure authentication using OAuth 2.0.
- Pagination support for handling large numbers of events.
## Prerequisites

Before running the application, make sure you have the following installed:

- .NET Core SDK
- Visual Studio or Visual Studio Code (optional)
- Google Developer Console project with Google Calendar API enabled
## Get Started
1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/yourusername/GoogleCalendarEventManagergit
   
2. Open the solution in your preferred development environment.
3. Open the solution in your preferred development environment.
## Configuration
1. Create a project in the Google Developer Console and enable the Google Calendar API.

2. Create OAuth 2.0 credentials for your project and download the JSON client secret file.

3. Set the client secret file path and other configuration settings in the appsettings.json or as environment variables.

4. Ensure the necessary permissions and scopes are configured for your Google API project.
## API Endpoints
The application provides the following API endpoints:

- POST /api/events: Create a new event.
- GET /api/events: Retrieve a list of events.
- GET /api/events/{searchWord}: Retrieve list of events with a search word.
- GET /api/events/{date}: Retrieve list of events with a range date.
- DELETE /api/events/{eventId}: Delete an event.
## Authentication

To use the API, users must authenticate using OAuth 2.0. They will be prompted to authorize the application to access their Google Calendar.
## Pagination

For large numbers of events, the API supports pagination. You can use the pageToken parameter to retrieve additional pages of events.
## Contributing

Contributions are always welcome!


## License

This project is licensed under the [MIT License](https://choosealicense.com/licenses/mit/)

