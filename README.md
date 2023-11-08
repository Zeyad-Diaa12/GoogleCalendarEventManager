Google Calendar Event Manager
Google Calendar Event Manager is a .NET Core API-based web application that allows users to manage their Google Calendar events. With this application, users can create, view, and delete events from their Google Calendar. It simplifies the interaction with the Google Calendar API and provides a seamless way to work with calendar events.

Table of Contents
Features
Prerequisites
Getting Started
Configuration
Usage
API Endpoints
Authentication
Pagination
Attachments
Contributing
License
Features
Create new events in the user's Google Calendar.
View existing events from the user's Google Calendar.
Delete events from the user's Google Calendar.
Secure authentication using OAuth 2.0.
Pagination support for handling large numbers of events.
Attachments: Allow users to add and retrieve attachments for events.
Prerequisites
Before running the application, make sure you have the following installed:

.NET Core SDK
Visual Studio or Visual Studio Code (optional)
Google Developer Console project with Google Calendar API enabled
Getting Started
Clone this repository to your local machine:

bash
Copy code
git clone https://github.com/yourusername/GoogleCalendarEventManager.git
Open the solution in your preferred development environment.

Build and run the application.

Configuration
Before running the application, you need to configure the Google Calendar API credentials and other settings:

Create a project in the Google Developer Console and enable the Google Calendar API.

Create OAuth 2.0 credentials for your project and download the JSON client secret file.

Set the client secret file path and other configuration settings in the appsettings.json or as environment variables.

Ensure the necessary permissions and scopes are configured for your Google API project.

Usage
API Endpoints
The application provides the following API endpoints:

POST /api/events: Create a new event.
GET /api/events: Retrieve a list of events with optional query parameters for filtering.
GET /api/events/{eventId}: Retrieve details of a specific event.
PUT /api/events/{eventId}: Update an existing event.
DELETE /api/events/{eventId}: Delete an event.
Authentication
To use the API, users must authenticate using OAuth 2.0. They will be prompted to authorize the application to access their Google Calendar.

Pagination
For large numbers of events, the API supports pagination. You can use the pageToken parameter to retrieve additional pages of events.

Attachments
Users can add attachments to their events and retrieve attachments from existing events. Attachments are stored in Google Drive.

Contributing
Contributions to this project are welcome. Please follow the Contributing Guidelines for more information.

License
This project is licensed under the MIT License.

